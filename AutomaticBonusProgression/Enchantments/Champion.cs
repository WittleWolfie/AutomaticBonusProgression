﻿using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Champion
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Champion));

    private const string ChampionName = "LegendaryArmor.Champion";
    private const string BuffName = "LegendaryArmor.Champion.Buff";
    private const string AbilityName = "LegendaryArmor.Champion.Ability";

    private const string TargetBuffName = "LegendaryArmor.Champion.Target.Buff";

    private const string DisplayName = "LegendaryArmor.Champion.Name";
    private const string Description = "LegendaryArmor.Champion.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Champion");

      var smiteEvil = BuffRefs.SmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var smiteEvilAura = BuffRefs.AuraOfJusticeSmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var challenge = BuffRefs.CavalierChallengeBuffTarget.Cast<BlueprintBuffReference>().Reference;
      var buff = BuffConfigurator.New(BuffName, Guids.ChampionBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, EnhancementCost))
        .AddComponent(BonusAgainstTarget.AC(smiteEvil, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(smiteEvilAura, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(challenge, 2, ModifierDescriptor.Sacred))
        .Configure();

      var ability = EnchantmentTool.CreateEnchantAbility(
        buff: buff,
        displayName: DisplayName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.ChampionAbility,
        new AlignmentActivatableRestriction(AlignmentComponent.Good));

      var feature = EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: Description,
        featureName: ChampionName,
        featureGuid: Guids.Champion,
        featureRanks: 1,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        ability);

      return FeatureConfigurator.For(feature).AddPrerequisiteAlignment(AlignmentMaskType.Good).Configure();
    }
  }
}
