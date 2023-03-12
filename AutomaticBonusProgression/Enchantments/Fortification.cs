using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Fortification
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Fortification));

    private const string FortificationName = "LegendaryArmor.Fortification";
    private const string BuffName = "LegendaryArmor.Fortification.Buff";
    private const string AbilityName = "LegendaryArmor.Fortification.Ability";
    private const string BuffShieldName = "LegendaryArmor.Fortification.Shield.Buff";
    private const string AbilityShieldName = "LegendaryArmor.Fortification.Shield.Ability";

    private const string DisplayName = "LegendaryArmor.Fortification.Name";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Fortification");

      var enchant = ArmorEnchantmentRefs.Fortification25Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          enchant.m_Description.m_Key,
          "",
          EnhancementCost,
          ranks: 1);

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification25Feature, enchantInfo);

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(BuffName, Guids.FortificationBuff, fortification.GetComponent<AddFortification>()),
        new(AbilityName, Guids.FortificationAbility));
      var abilityShield = EnchantTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(BuffShieldName, Guids.FortificationShieldBuff),
        new(AbilityShieldName, Guids.FortificationShieldAbility));

      return EnchantTool.CreateEnchantFeature(
        enchantInfo,
        new(FortificationName, Guids.Fortification),
        ability,
        abilityShield);
    }

    private const string ImprovedFortificationName = "LegendaryArmor.Fortification.Improved";
    private const string ImprovedBuffName = "LegendaryArmor.Fortification.Improved.Buff";
    private const string ImprovedAbilityName = "LegendaryArmor.Fortification.Improved.Ability";
    private const string ImprovedBuffShieldName = "LegendaryArmor.Fortification.Improved.Shield.Buff";
    private const string ImprovedAbilityShieldName = "LegendaryArmor.Fortification.Improved.Shield.Ability";

    private const string ImprovedDisplayName = "LegendaryArmor.Fortification.Improved.Name";
    private const int ImprovedEnhancementCost = 3;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Fortification Armor (Improved)");

      var enchant = ArmorEnchantmentRefs.Fortification50Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          ImprovedDisplayName,
          enchant.m_Description.m_Key,
          "",
          ImprovedEnhancementCost,
          ranks: 2,
          prerequisite: new(Guids.Fortification));

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification50Feature, enchantInfo);

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(ImprovedBuffName, Guids.ImprovedFortificationBuff, fortification.GetComponent<AddFortification>()),
        new(ImprovedAbilityName, Guids.ImprovedFortificationAbility));
      var abilityShield = EnchantTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(ImprovedBuffShieldName, Guids.ImprovedFortificationShieldBuff),
        new(ImprovedAbilityShieldName, Guids.ImprovedFortificationShieldAbility));

      return EnchantTool.CreateEnchantFeature(
        enchantInfo,
        new(ImprovedFortificationName, Guids.ImprovedFortification),
        ability,
        abilityShield);
    }

    private const string GreaterFortificationName = "LegendaryArmor.Fortification.Greater";
    private const string GreaterBuffName = "LegendaryArmor.Fortification.Greater.Buff";
    private const string GreaterAbilityName = "LegendaryArmor.Fortification.Greater.Ability";
    private const string GreaterBuffShieldName = "LegendaryArmor.Fortification.Greater.Shield.Buff";
    private const string GreaterAbilityShieldName = "LegendaryArmor.Fortification.Greater.Shield.Ability";

    private const string GreaterDisplayName = "LegendaryArmor.Fortification.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.Fortification.Greater.Description";
    private const int GreaterEnhancementCost = 5;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Fortification Armor (Greater)");

      var enchant = ArmorEnchantmentRefs.Fortification75Enchant.Reference.Get();
      var enchantInfo =
        new ArmorEnchantInfo(
          GreaterDisplayName,
          enchant.m_Description.m_Key,
          "",
          GreaterEnhancementCost,
          ranks: 2,
          prerequisite: new(Guids.ImprovedFortification, ranks: 2));

      var fortification = EnchantTool.AddEnhancementEquivalence(FeatureRefs.Fortification75Feature, enchantInfo);

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(GreaterBuffName, Guids.GreaterFortificationBuff, fortification.GetComponent<AddFortification>()),
        new(GreaterAbilityName, Guids.GreaterFortificationAbility));
      var abilityShield = EnchantTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(GreaterBuffShieldName, Guids.GreaterFortificationShieldBuff),
        new(GreaterAbilityShieldName, Guids.GreaterFortificationShieldAbility));

      return EnchantTool.CreateEnchantFeature(
        enchantInfo,
        new(GreaterFortificationName, Guids.GreaterFortification),
        ability,
        abilityShield);
    }
  }
}
