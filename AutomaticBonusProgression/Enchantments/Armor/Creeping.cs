using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Creeping
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Creeping));

    private const string EffectName = "LA.Creeping.Effect";
    private const string BuffName = "LA.Creeping.Buff";

    private const string AbilityName = "LA.Creeping.Cast";
    private const string CastBuffName = "LA.Creeping.Cast.Buff";
    private const string ResourceName = "LA.Creeping.Cast.Resource";

    private const string DisplayName = "LA.Creeping.Name";
    private const string Description = "LA.Creeping.Description";

    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Creeping");

      var castResource = AbilityResourceConfigurator.New(ResourceName, Guids.CreepingResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = BuffRefs.VanishBuff.Reference.Get().Icon;
      var castBuff = BuffConfigurator.New(CastBuffName, Guids.CreepingCastBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddContextRankConfig(ContextRankConfigs.FeatureRank(Guids.ArmorAttunement))
        .AddStatBonusAbilityValue(
          stat: StatType.SkillStealth, value: ContextValues.Rank(), descriptor: ModifierDescriptor.UntypedStackable)
        .Configure();

      var castAbility = AbilityConfigurator.New(AbilityName, Guids.CreepingAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Swift)
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.CreepingEffect })
        .AddAbilityEffectRunAction(
          ActionsBuilder.New().ApplyBuff(castBuff, ContextDuration.Fixed(1, rate: DurationRate.Minutes)))
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      var addFacts = new AddFacts() { m_Facts = new[] { castAbility.ToReference<BlueprintUnitFactReference>() } };
      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.CreepingEffect),
        parentBuff: new(BuffName, Guids.CreepingBuff, addFacts, addResources));
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
