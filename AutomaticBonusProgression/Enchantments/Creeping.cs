using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Creeping
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Creeping));

    private const string CreepingName = "LegendaryArmor.Creeping";
    private const string BuffName = "LegendaryArmor.Creeping.Buff";
    private const string AbilityName = "LegendaryArmor.Creeping.Ability";

    private const string CastAbilityName = "LegendaryArmor.Creeping.Cast";
    private const string CastBuffName = "LegendaryArmor.Creeping.Cast.Buff";
    private const string CastResourceName = "LegendaryArmor.Creeping.Cast.Resource";

    private const string DisplayName = "LegendaryArmor.Creeping.Name";
    private const string Description = "LegendaryArmor.Creeping.Description";
    private const int EnhancementCost = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Creeping");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.CreepingCastResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var castBuff = BuffConfigurator.New(CastBuffName, Guids.CreepingCastBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddContextRankConfig(ContextRankConfigs.FeatureRank(Guids.ArmorAttunement))
        .AddStatBonusAbilityValue(
          stat: StatType.SkillStealth, value: ContextValues.Rank(), descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();

      var castAbility = AbilityConfigurator.New(CastAbilityName, Guids.CreepingCastAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Free)
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.CreepingBuff })
        .AddAbilityEffectRunAction(
          ActionsBuilder.New().ApplyBuff(castBuff, ContextDuration.Fixed(1, rate: DurationRate.Minutes)))
        .Configure();

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 2);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(BuffName, Guids.CreepingBuff, new CreepingComponent()),
        new(AbilityName, Guids.CreepingAbility));

      var featureInfo =
        new BlueprintInfo(
          CreepingName,
          Guids.Creeping,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, castAbility);
    }

    [TypeId("4b2da1d4-6a78-4ab2-8896-c540dd968a89")]
    private class CreepingComponent :
      UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateArmorCheckPenalty>
    {
      public override void OnTurnOn()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        }
        catch (Exception e)
        {
          Logger.LogException("CreepingComponent.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          Owner.Body.Armor.MaybeArmor?.RecalculateStats();
        }
        catch (Exception e)
        {
          Logger.LogException("CreepingComponent.OnTurnOff", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateArmorCheckPenalty evt) { }

      public void OnEventDidTrigger(RuleCalculateArmorCheckPenalty evt)
      {
        try
        {
          var armor = Owner.Body.Armor.MaybeArmor;
          if (armor is null)
            return;

          if (evt.Result >= 0)
            return;

          var bonus = -evt.Result;
          Logger.Verbose(() => $"Adding {bonus} to stealth check for {Owner.CharacterName}");
          armor.AddModifier(Owner.Stats.GetStat(StatType.SkillStealth), bonus);
        }
        catch (Exception e)
        {
          Logger.LogException("CreepingComponent.OnEventDidTrigger", e);
        }
      }
    }
  }
}
