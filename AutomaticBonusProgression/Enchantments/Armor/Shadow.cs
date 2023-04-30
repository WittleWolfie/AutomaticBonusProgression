using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Shadow
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Shadow));

    internal static void Configure()
    {
      Logger.Log($"Configuring Shadow Armor");

      ConfigureBasic();
      ConfigureImproved();
      ConfigureGreater();
    }

    private const string EffectName = "LA.Shadow";
    private const string BuffName = "LA.Shadow.Buff";

    private const string DisplayName = "LA.Shadow.Name";
    private const int BasicCost = 2;

    private static void ConfigureBasic()
    {
      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowEnchant.Reference.Get();
      var enchantInfo = new ArmorEnchantInfo(DisplayName, enchant.m_Description.m_Key, "", BasicCost);

      var shadowFeature = EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorShadowFeature, enchantInfo);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ShadowArmor, enchantInfo);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.ShadowEffect, shadowFeature.GetComponent<AddStatBonus>()),
        parentBuff: new(BuffName, Guids.ShadowBuff));
    }

    private const string ImprovedEffectName = "LA.Shadow.Improved";
    private const string ImprovedBuffName = "LA.Shadow.Improved.Buff";

    private const string ImprovedDisplayName = "LA.Shadow.Improved.Name";
    private const int ImprovedCost = 4;

    internal static void ConfigureImproved()
    {
      Logger.Log($"Configuring Shadow Armor (Improved)");

      var enchant = ArmorEnchantmentRefs.ArcaneArmorShadowGreaterEnchant.Reference.Get();
      var enchantInfo = new ArmorEnchantInfo(ImprovedDisplayName, enchant.m_Description.m_Key, "", ImprovedCost);

      var shadowFeature = EnchantTool.AddEnhancementEquivalence(FeatureRefs.ArcaneArmorShadowGreaterFeature, enchantInfo);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.GreaterShadow, enchantInfo);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(ImprovedEffectName, Guids.ImprovedShadowEffect, shadowFeature.GetComponent<AddStatBonus>()),
        parentBuff: new(ImprovedBuffName, Guids.ImprovedShadowBuff));
    }

    private const string GreaterEffectName = "LA.Shadow.Greater";
    private const string GreaterBuffName = "LA.Shadow.Greater.Buff";

    private const string GreaterDisplayName = "LA.Shadow.Greater.Name";
    private const string GreaterDescription = "LA.Shadow.Greater.Description";
    private const int GreaterCost = 5;

    internal static void ConfigureGreater()
    {
      Logger.Log($"Configuring Shadow Armor (Greater)");

      var enchantInfo = new ArmorEnchantInfo(GreaterDisplayName, GreaterDescription, "", GreaterCost);

      var effectBuff = BuffConfigurator.New(GreaterEffectName, Guids.GreaterShadowEffect)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        //.SetIcon()
        .AddStatBonus(stat: StatType.SkillStealth, value: 15, descriptor: ModifierDescriptor.Competence)
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(GreaterBuffName, Guids.GreaterShadowBuff));
    }
  }
}
