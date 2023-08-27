using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Growing
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Growing));

    private const string EffectName = "LW.Growing";
    private const string BuffName = "LW.Growing.Buff";
    private const string OffHandEffectName = "LW.Growing.OffHand";
    private const string OffHandBuffName = "LW.Growing.OffHand.Buff";

    private const string EnchantName = "LW.Growing.Enchant";
    private const string CastResourceName = "LW.Growing.Cast.Resource";
    private const string OffHandCastResourceName = "LW.Growing.Cast.OffHand.Resource";

    private const string MainHandAbilityName = "LW.Growing.Cast";
    private const string MainHandSelfBuffName = "LW.Growing.Buff.MainHand.Self";

    private const string OffHandAbilityName = "LW.Growing.Cast.OffHand";
    private const string OffHandSelfBuffName = "LW.Growing.Buff.OffHand.Self";

    private const string DisplayName = "LW.Growing.Name";
    private const string Description = "LW.Growing.Description";
    private const int EnhancementCost = 1;

    private const string MainHandDisplayName = "LW.Growing.MainHand.Name";
    private const string OffHandDisplayName = "LW.Growing.OffHand.Name";

    internal static void Configure()
    {
      Logger.Log($"Configuring Growing");

      var icon = FeatureRefs.TrueJudgmentFeature.Reference.Get().Icon;

      var mainHandResource = AbilityResourceConfigurator.New(CastResourceName, Guids.GrowingCastResource)
        .SetIcon(icon)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var offHandResource = AbilityResourceConfigurator.New(OffHandCastResourceName, Guids.GrowingOffHandCastResource)
        .SetIcon(icon)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var mainHandBuff = BuffConfigurator.New(MainHandSelfBuffName, Guids.GrowingMainHandSelfBuff)
        .SetDisplayName(MainHandDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddNotDispelable()
        .AddComponent(new GrowingComponent())
        .Configure();

      var mainHandCastAbility = AbilityConfigurator.New(MainHandAbilityName, Guids.GrowingMainHandAbility)
        .SetDisplayName(MainHandDisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AllowTargeting(self: true)
        .SetRange(AbilityRange.Personal)
        .SetType(AbilityType.Supernatural)
        .SetActionType(CommandType.Free)
        .SetAvailableMetamagic()
        .AddAbilityResourceLogic(requiredResource: mainHandResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.GrowingEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(mainHandBuff, ContextDuration.Fixed(10, rate: DurationRate.Minutes)))
        .Configure();

      var offHandBuff = BuffConfigurator.New(OffHandSelfBuffName, Guids.GrowingOffHandSelfBufff)
        .CopyFrom(mainHandBuff, c => c is not GrowingComponent)
        .AddComponent(new GrowingComponent(toPrimary: false))
        .Configure();

      var offHandCastAbility = AbilityConfigurator.New(OffHandAbilityName, Guids.GrowingOffHandAbility)
        .CopyFrom(mainHandCastAbility)
        .SetDisplayName(OffHandDisplayName)
        .AddAbilityResourceLogic(requiredResource: offHandResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.GrowingOffHandEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(offHandBuff, ContextDuration.Fixed(10, rate: DurationRate.Minutes)))
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.GrowingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .Configure();

      var mainHandAddFacts = new AddFacts() { m_Facts = new[] { mainHandCastAbility.ToReference<BlueprintUnitFactReference>() } };
      var offHandAddFacts = new AddFacts() { m_Facts = new[] { offHandCastAbility.ToReference<BlueprintUnitFactReference>() } };
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

      var enchantInfo =
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.GrowingEffect, enchant),
        parentBuff: new(BuffName, Guids.GrowingBuff, mainHandAddFacts, mainHandAddResources));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(OffHandEffectName, Guids.GrowingOffHandEffect, enchant, toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.GrowingOffHandBuff, offHandAddFacts, offHandAddResources));
    }

    [TypeId("84ecaa32-3ae3-4d25-9642-1a6e6bb80116")]
    private class GrowingComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
      private static BlueprintItemEnchantment _growing;
      private static BlueprintItemEnchantment Growing
      {
        get
        {
          _growing ??= BlueprintTool.Get<BlueprintItemEnchantment>(Guids.GrowingEnchant);
          return _growing;
        }
      }

      private bool ToPrimary;

      internal GrowingComponent(bool toPrimary = true)
      {
        ToPrimary = toPrimary;
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (!evt.Weapon.HasEnchantment(Growing))
            return;

          if (ToPrimary && !Common.IsPrimaryWeapon(evt.Weapon))
            return;

          if (!ToPrimary && Common.IsPrimaryWeapon(evt.Weapon))
            return;

          var currentSize = evt.WeaponSize;
          var newSize = currentSize.Shift(1);
          evt.WeaponDamageDice.Modify(
            WeaponDamageScaleTable.Scale(
              evt.WeaponDamageDice.ModifiedValue, newSize, currentSize, evt.Weapon.Blueprint),
            Fact);
        }
        catch (Exception e)
        {
          Logger.LogException("GrowingComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
    }
  }
}
