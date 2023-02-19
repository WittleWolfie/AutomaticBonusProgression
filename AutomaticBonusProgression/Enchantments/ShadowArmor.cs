using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class ShadowArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShadowArmor));

    private const string ShadowArmorName = "LegendaryArmor.Shadow";
    private const string BuffName = "LegendaryArmor.Shadow.Buff";
    private const string AbilityName = "LegendaryArmor.Shadow.Ability";

    private const string DisplayName = "LegendaryArmor.Shadow.Name";
    private const int EnhancementCost = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Shadow Armor");

      var shadowFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorShadowFeature, EnhancementType.Armor, EnhancementCost);
      EnchantmentTool.AddEnhancementEquivalence(ArmorEnchantmentRefs.ShadowArmor, EnhancementType.Armor, EnhancementCost);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowEnchant.Reference.Get();
      return EnchantmentTool.CreateEnchant(
        buffName: BuffName,
        buffGuid: Guids.ShadowArmorBuff,
        displayName: DisplayName,
        description: enchant.m_Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.ShadowArmorAbility,
        featureName: ShadowArmorName,
        Guids.ShadowArmor,
        buffComponents: shadowFeature.GetComponent<AddStatBonus>());
    }

    private const string ImprovedShadowArmorName = "LegendaryArmor.Shadow.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Shadow.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Shadow.Improved.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Shadow.Improved.Name";
    private const int ImprovedEnhancementCost = 4;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Shadow Armor (Improved)");

      var shadowFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ArcaneArmorShadowGreaterFeature, EnhancementType.Armor, ImprovedEnhancementCost);
      EnchantmentTool.AddEnhancementEquivalence(
        ArmorEnchantmentRefs.GreaterShadow, EnhancementType.Armor, ImprovedEnhancementCost);

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowGreaterEnchant.Reference.Get();
      return EnchantmentTool.CreateEnchant(
        buffName: ImprovedBuffName,
        buffGuid: Guids.ImprovedShadowArmorBuff,
        displayName: ImprovedDisplayName,
        description: enchant.m_Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: ImprovedAbilityName,
        abilityGuid: Guids.ImprovedShadowArmorAbility,
        featureName: ImprovedShadowArmorName,
        Guids.ImprovedShadowArmor,
        ranks: 2,
        prerequisiteFeature: Guids.ShadowArmor,
        buffComponents: shadowFeature.GetComponent<AddStatBonus>());
    }

    private const string GreaterShadowArmorName = "LegendaryArmor.Shadow.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Shadow.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Shadow.Greater.Ability";

    private const string GreaterDisplayName = "LegendaryArmor.Shadow.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.Shadow.Greater.Description";
    private const int GreaterEnhancementCost = 5;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Shadow Armor (Greater)");

      var buff = BuffConfigurator.New(GreaterBuffName, Guids.GreaterShadowArmorBuff)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .AddStatBonus(stat: StatType.SkillStealth, value: 15, descriptor: ModifierDescriptor.Competence)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, GreaterEnhancementCost))
        .Configure();

      return EnchantmentTool.CreateEnchant(
        buff: buff,
        displayName: GreaterDisplayName,
        description: GreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: GreaterAbilityName,
        abilityGuid: Guids.GreaterShadowArmorAbility,
        featureName: GreaterShadowArmorName,
        Guids.GreaterShadowArmor,
        ranks: 2,
        prerequisiteFeature: Guids.ImprovedShadowArmor,
        prerequisiteRanks: 2);
    }
  }
}
