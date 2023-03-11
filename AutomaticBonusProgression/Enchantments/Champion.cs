using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
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

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 1);

      var buff = BuffConfigurator.New(BuffName, Guids.ChampionBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo))
        .AddComponent(BonusAgainstTarget.AC(smiteEvil, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(smiteEvilAura, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(challenge, 2, ModifierDescriptor.Sacred))
        .Configure();

      var abilityInfo =
        new BlueprintInfo(
          AbilityName, Guids.ChampionAbility, new AlignmentActivatableRestriction(AlignmentComponent.Good));
      var featureInfo =
        new BlueprintInfo(
          ChampionName, Guids.Champion, new PrerequisiteAlignment() { Alignment = AlignmentMaskType.Good });

      var ability = EnchantmentTool.CreateEnchantAbility(enchantInfo, buff, abilityInfo);
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability);
    }
  }
}
