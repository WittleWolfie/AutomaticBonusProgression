using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Dastard
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Dastard));

    private const string DastardName = "LegendaryArmor.Dastard";
    private const string BuffName = "LegendaryArmor.Dastard.Buff";
    private const string AbilityName = "LegendaryArmor.Dastard.Ability";
    private const string BuffShieldName = "LegendaryArmor.Dastard.Shield.Buff";
    private const string AbilityShieldName = "LegendaryArmor.Dastard.Shield.Ability";

    private const string TargetBuffName = "LegendaryArmor.Dastard.Target.Buff";

    private const string DisplayName = "LegendaryArmor.Dastard.Name";
    private const string Description = "LegendaryArmor.Dastard.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Dastard");

      var smiteGood = BuffRefs.FiendishSmiteGoodBuff.Cast<BlueprintBuffReference>().Reference;
      var smiteGoodAlt = BuffRefs.HalfFiendSmiteGoodBuff.Cast<BlueprintBuffReference>().Reference;
      var sinfulAbsolution = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.SinfulAbsolutionBuff);
      var smiteGoodMod = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.SmiteGoodBuff);
      var buff = BuffConfigurator.New(BuffName, Guids.DastardBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, EnhancementCost))
        .AddComponent(BonusAgainstTarget.AC(smiteGood, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodAlt, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(sinfulAbsolution, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodMod, 2, ModifierDescriptor.Profane))
        .Configure();

      var ability = EnchantmentTool.CreateEnchantAbility(
        buff: buff,
        displayName: DisplayName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.DastardAbility,
        new ActivatableAlignmentRestriction(AlignmentComponent.Evil));

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: Description,
        featureName: DastardName,
        featureGuid: Guids.Dastard,
        featureRanks: 1,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        ability);
    }
  }
}
