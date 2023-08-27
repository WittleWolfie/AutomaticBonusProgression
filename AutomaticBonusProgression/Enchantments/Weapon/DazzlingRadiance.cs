using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using static Kingmaker.RuleSystem.RulebookEvent;

namespace AutomaticBonusProgression.Enchantments
{
  internal class DazzlingRadiance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(DazzlingRadiance));

    private const string EnchantName = "LW.DazzlingRadiance.Enchant";
    private const string EffectName = "LW.DazzlingRadiance";
    private const string BuffName = "LW.DazzlingRadiance.Buff";
    private const string OffHandEffectName = "LW.DazzlingRadiance.OffHand";
    private const string OffHandBuffName = "LW.DazzlingRadiance.OffHand.Buff";

    private const string DisplayName = "LW.DazzlingRadiance.Name";
    private const string Description = "LW.DazzlingRadiance.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring DazzlingRadiance");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.DazzlingRadianceEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithBonusValueProgression(17))
        .AddComponent(
          new DazzlingRadianceComponent(
            ActionsBuilder.New()
              .SavingThrow(
                SavingThrowType.Fortitude,
                customDC: ContextValues.Rank(),
                onResult: ActionsBuilder.New()
                  .ConditionalSaved(
                    succeed: ActionsBuilder.New()
                      .ApplyBuff(
                        BuffRefs.DazzledBuff.ToString(), ContextDuration.VariableDice(DiceType.D4, diceCount: 1)),
                    failed: ActionsBuilder.New()
                      .ApplyBuff(
                        BuffRefs.DazzledBuff.ToString(),
                        ContextDuration.VariableDice(DiceType.D4, diceCount: 1, bonus: 2))
                      .ApplyBuff(BuffRefs.Blind.ToString(), ContextDuration.Fixed(2))))))
        .Configure();

      var divinePower = BuffRefs.DivinePowerBuff.Reference.Get();
      var enchantInfo = new WeaponEnchantInfo(
        DisplayName,
        Description,
        divinePower.Icon,
        EnhancementCost,
        WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.DazzlingRadianceEffect, enchant),
        parentBuff: new(BuffName, Guids.DazzlingRadianceBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.DazzlingRadianceOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.DazzlingRadianceOffHandBuff));
    }

    [TypeId("bf64bc96-99d4-4055-9f24-7f046279f3de")]
    private class DazzlingRadianceComponent
      : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleCastSpell>
    {
      private static readonly List<BlueprintAbility> DazzlingDisplays = new();
      private static readonly CustomDataKey DuplicateKey = new("DazzlingRadiance.Duplicate");

      private readonly ActionList Actions;

      internal DazzlingRadianceComponent(ActionsBuilder actions)
      {
        Actions = actions.Build();
      }

      public void OnEventAboutToTrigger(RuleCastSpell evt)
      {
        try
        {
          var wielder = Owner.Wielder?.Unit;
          if (wielder is null)
          {
            Logger.Warning("No wielder!");
            return;
          }

          if (DazzlingDisplays.Empty())
            PopulateBlueprints();

          if (!DazzlingDisplays.Contains(evt.Spell.Blueprint))
            return;

          // The enchantment may be applied to multiple weapons; only trigger this once!
          if (evt.TryGetCustomData(DuplicateKey, out bool _))
          {
            Logger.Verbose(() => "Ignoring duplicate trigger");
            return;
          }
          evt.SetCustomData(DuplicateKey, true);

          foreach (var target in GameHelper.GetTargetsAround(wielder.Position, 15.Feet()))
          {
            if (target.IsAlly(wielder))
              continue;

            Logger.Verbose(() => $"Triggering Dazzling Radiance on {target}");
            using (Context.GetDataScope(target))
              Actions.Run();
          }
        }
        catch (Exception e)
        {
          Logger.LogException("DazzlingRadianceComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCastSpell evt) { }

      private static void PopulateBlueprints()
      {
        Logger.Verbose(() => "Populating Dazzling Display blueprints.");
        DazzlingDisplays.Add(AbilityRefs.DazzlingDisplayAction.Reference.Get());
        DazzlingDisplays.Add(AbilityRefs.DazzlingDisplayMoveAction.Reference.Get());
        DazzlingDisplays.Add(AbilityRefs.DazzlingDisplayStandardAction.Reference.Get());
        DazzlingDisplays.Add(AbilityRefs.DazzlingDisplaySwiftAction.Reference.Get());
        DazzlingDisplays.Add(AbilityRefs.DragonheirDazzlingDisplayAction.Reference.Get());
      }
    }
  }
}
