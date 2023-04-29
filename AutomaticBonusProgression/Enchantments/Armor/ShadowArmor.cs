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

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowEnchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          enchant.m_Description.m_Key,
          "",
          EnhancementCost,
          ranks: 2);

      var shadowFeature = EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorShadowFeature, enchantInfo);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ShadowArmor, enchantInfo);

      return EnchantTool.CreateEnchant(
        enchantInfo,
        new BlueprintInfo(BuffName, Guids.ShadowArmorBuff, shadowFeature.GetComponent<AddStatBonus>()),
        new(AbilityName, Guids.ShadowArmorAbility),
        new(ShadowArmorName, Guids.ShadowArmor));
    }

    private const string ImprovedShadowArmorName = "LegendaryArmor.Shadow.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Shadow.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Shadow.Improved.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Shadow.Improved.Name";
    private const int ImprovedEnhancementCost = 4;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Shadow Armor (Improved)");

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowGreaterEnchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          ImprovedDisplayName,
          enchant.m_Description.m_Key,
          "",
          ImprovedEnhancementCost,
          ranks: 2,
          prerequisite: new(Guids.ShadowArmor, ranks: 2));

      var shadowFeature = EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorShadowGreaterFeature, enchantInfo);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.GreaterShadow, enchantInfo);

      return EnchantTool.CreateEnchant(
        enchantInfo,
        new BlueprintInfo(ImprovedBuffName, Guids.ImprovedShadowArmorBuff, shadowFeature.GetComponent<AddStatBonus>()),
        new(ImprovedAbilityName, Guids.ImprovedShadowArmorAbility),
        new(ImprovedShadowArmorName, Guids.ImprovedShadowArmor));
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

      var enchantInfo =
        new ArmorEnchantInfo(
          GreaterDisplayName,
          GreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1,
          prerequisite: new(Guids.ImprovedShadowArmor, ranks: 2));

      var buff = BuffConfigurator.New(GreaterBuffName, Guids.GreaterShadowArmorBuff)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .AddStatBonus(stat: StatType.SkillStealth, value: 15, descriptor: ModifierDescriptor.Competence)
        .AddComponent(new EnhancementEquivalence(enchantInfo))
        .Configure();

      return EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        buff,
        new(GreaterAbilityName, Guids.GreaterShadowArmorAbility),
        new(GreaterShadowArmorName, Guids.GreaterShadowArmor));
    }
  }
}
