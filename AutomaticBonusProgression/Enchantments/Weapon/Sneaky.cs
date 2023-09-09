using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using UnityEngine;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Sneaky
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Sneaky));

    private const string EnchantName = "LW.Sneaky.Enchant";
    private const string EffectName = "LW.Sneaky";
    private const string BuffName = "LW.Sneaky.Buff";
    private const string OffHandEffectName = "LW.Sneaky.OffHand";
    private const string OffHandBuffName = "LW.Sneaky.OffHand.Buff";

    private const string SneakySurprise = "LW.Sneaky.Surprise";

    private const string MainHandResource = "LW.Sneaky.Cast.Resource";
    private const string MainHandAbilityName = "LW.Sneaky.Cast";
    private const string MainHandDisplayName = "LW.Sneaky.MainHand.Name";

    private const string OffHandResource = "LW.Sneaky.Cast.Resource.OffHand";
    private const string OffHandAbilityName = "LW.Sneaky.Cast.OffHand";
    private const string OffHandDisplayName = "LW.Sneaky.OffHand.Name";

    private const string DisplayName = "LW.Sneaky.Name";
    private const string Description = "LW.Sneaky.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Sneaky");

      var icon = FeatureRefs.RogueSneakAttack.Reference.Get().Icon;

      var mainHandResource = AbilityResourceConfigurator.New(MainHandResource, Guids.SneakyMainHandResource)
        .SetIcon(icon)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var offHandResource = AbilityResourceConfigurator.New(OffHandResource, Guids.SneakyOffHandResource)
        .SetIcon(icon)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var targetBuff = BuffConfigurator.New(SneakySurprise, Guids.SneakySurprise)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddNotDispelable()
        .Configure();

      var mainHandCast = AbilityConfigurator.New(MainHandAbilityName, Guids.SneakyMainHandAbility)
        .SetDisplayName(MainHandDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AllowTargeting(enemies: true)
        .SetRange(AbilityRange.Long)
        .SetType(AbilityType.Supernatural)
        .SetActionType(CommandType.Free)
        .SetAvailableMetamagic()
        .AddAbilityResourceLogic(requiredResource: mainHandResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.SneakyEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(targetBuff))
        .Configure();

      var offHandCast = AbilityConfigurator.New(OffHandAbilityName, Guids.SneakyOffHandAbility)
        .CopyFrom(mainHandCast)
        .SetDisplayName(OffHandDisplayName)
        .AddAbilityResourceLogic(requiredResource: offHandResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.SneakyOffHandEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(targetBuff))
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.SneakyEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<SneakyComponent>()
        .Configure();

      var mainHandAddFacts = new AddFacts() { m_Facts = new[] { mainHandCast.ToReference<BlueprintUnitFactReference>() } };
      var offHandAddFacts = new AddFacts() { m_Facts = new[] { offHandCast.ToReference<BlueprintUnitFactReference>() } };
      var mainHandAddResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = mainHandResource.ToReference<BlueprintAbilityResourceReference>()
        };
      var offHandAddResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = offHandResource.ToReference<BlueprintAbilityResourceReference>()
        };

      var increaseHuntersSurpriseResource = new IncreaseResourceAmount()
      {
        m_Resource = AbilityResourceRefs.HuntersSurpriseResource.Cast<BlueprintAbilityResourceReference>().Reference
      };

      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, icon, EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.SneakyEffect, enchant),
        parentBuff: new(BuffName, Guids.SneakyBuff, mainHandAddFacts, mainHandAddResources, increaseHuntersSurpriseResource));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.SneakyOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.SneakyOffHandBuff, offHandAddFacts, offHandAddResources, increaseHuntersSurpriseResource));
    }

    [TypeId("c99f0330-a53d-40de-9d61-f65f5700a687")]
    private class SneakyComponent
      : ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RuleAttackWithWeapon>,
      IInitiatorRulebookHandler<RulePrepareDamage>
    {
      private static BlueprintBuff _sneakySurprise;
      private static BlueprintBuff SneakySurprise
      {
        get
        {
          _sneakySurprise ??= BlueprintTool.Get<BlueprintBuff>(Guids.SneakySurprise);
          return _sneakySurprise;
        }
      }

      public void OnEventAboutToTrigger(RulePrepareDamage evt)
      {
        try
        {
          var attackRoll = evt.ParentRule.AttackRoll;
          if (attackRoll is null)
            return;

          var target = evt.Target;
          if (target is null)
            return;

          if (!target.HasFact(SneakySurprise))
            return;

          Logger.Verbose(() => $"Flagging attack against {target.CharacterName} as sneak attack");
          attackRoll.IsSneakAttack = true;
        }
        catch (Exception e)
        {
          Logger.LogException("OminousComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          var target = evt.Target;
          if (target is null)
            return;

          if (target.HasFact(SneakySurprise))
          {
            Logger.Verbose(() => $"Removing Sneaky Surprise from {target.CharacterName}");
            target.RemoveFact(SneakySurprise);
          }
        }
        catch (Exception e)
        {
          Logger.LogException("OminousComponent.OnEventDidTrigger", e);
        }
      }

      public void OnEventDidTrigger(RulePrepareDamage evt) { }
      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }
    }
  }
}
