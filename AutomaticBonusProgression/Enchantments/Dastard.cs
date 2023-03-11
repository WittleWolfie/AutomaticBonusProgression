using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Dastard
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Dastard));

    private const string DastardName = "LegendaryArmor.Dastard";
    private const string BuffName = "LegendaryArmor.Dastard.Buff";
    private const string AbilityName = "LegendaryArmor.Dastard.Ability";

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
      var challenge = BuffRefs.CavalierChallengeBuffTarget.Cast<BlueprintBuffReference>().Reference;

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);

      var buff = BuffConfigurator.New(BuffName, Guids.DastardBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo))
        .AddComponent(BonusAgainstTarget.AC(smiteGood, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodAlt, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(sinfulAbsolution, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodMod, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(challenge, 2, ModifierDescriptor.Profane))
        .Configure();

      var abilityInfo =
        new BlueprintInfo(
          AbilityName, Guids.DastardAbility, new AlignmentActivatableRestriction(AlignmentComponent.Evil));
      var featureInfo =
        new BlueprintInfo(
          DastardName, Guids.Dastard, new PrerequisiteAlignment() { Alignment = AlignmentMaskType.Evil });

      var ability = EnchantmentTool.CreateEnchantAbility(enchantInfo, buff, abilityInfo);
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability);
    }
  }
}
