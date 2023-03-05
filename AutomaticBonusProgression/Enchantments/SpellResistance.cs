using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class SpellResistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SpellResistance));

    private const string SpellResistance13Name = "LegendaryArmor.SpellResistance.13";
    private const string Buff13Name = "LegendaryArmor.SpellResistance.13.Buff";
    private const string Ability13Name = "LegendaryArmor.SpellResistance.13.Ability";
    private const string Buff13ShieldName = "LegendaryArmor.SpellResistance.13.Shield.Buff";
    private const string Ability13ShieldName = "LegendaryArmor.SpellResistance.13.Shield.Ability";

    private const string DisplayName13 = "LegendaryArmor.SpellResistance.13.Name";
    private const string Description13 = "LegendaryArmor.SpellResistance.13.Name";
    private const int EnhancementCost13 = 2;

    internal static BlueprintFeature Configure13()
    {
      Logger.Log($"Configuring Spell Resistance 13");

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SpellResistance13Feature, EnhancementType.Armor, EnhancementCost13);

      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: Buff13Name,
        buffGuid: Guids.SpellResistance13Buff,
        displayName: DisplayName13,
        description: Description13,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost13,
        abilityName: Ability13Name,
        abilityGuid: Guids.SpellResistance13Ability,
        buffComponents: spellResistFeature.GetComponent<AddSpellResistance>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: Buff13ShieldName,
        buffGuid: Guids.SpellResistance13ShieldBuff,
        abilityName: Ability13ShieldName,
        abilityGuid: Guids.SpellResistance13ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName13,
        description: Description13,
        //icon: ??,
        featureName: SpellResistance13Name,
        Guids.SpellResistance13,
        featureRanks: EnhancementCost13,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private const string SpellResistance16Name = "LegendaryArmor.SpellResistance.16";
    private const string Buff16Name = "LegendaryArmor.SpellResistance.16.Buff";
    private const string Ability16Name = "LegendaryArmor.SpellResistance.16.Ability";
    private const string Buff16ShieldName = "LegendaryArmor.SpellResistance.16.Shield.Buff";
    private const string Ability16ShieldName = "LegendaryArmor.SpellResistance.16.Shield.Ability";

    private const string DisplayName16 = "LegendaryArmor.SpellResistance.16.Name";
    private const string Description16 = "LegendaryArmor.SpellResistance.16.Name";
    private const int EnhancementCost16 = 3;

    internal static BlueprintFeature Configure16()
    {
      Logger.Log($"Configuring Spell Resistance 16");

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SpellResistance15Feature, EnhancementType.Armor, EnhancementCost16);

      UpdateSpellResistance(FeatureRefs.SpellResistance15Feature, 16);

      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: Buff16Name,
        buffGuid: Guids.SpellResistance16Buff,
        displayName: DisplayName16,
        description: Description16,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost16,
        abilityName: Ability16Name,
        abilityGuid: Guids.SpellResistance16Ability,
        buffComponents: spellResistFeature.GetComponent<AddSpellResistance>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: Buff16ShieldName,
        buffGuid: Guids.SpellResistance16ShieldBuff,
        abilityName: Ability16ShieldName,
        abilityGuid: Guids.SpellResistance16ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName16,
        description: Description16,
        //icon: ??,
        featureName: SpellResistance16Name,
        Guids.SpellResistance16,
        featureRanks: EnhancementCost16,
        prerequisiteFeature: Guids.SpellResistance13,
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private const string SpellResistance19Name = "LegendaryArmor.SpellResistance.19";
    private const string Buff19Name = "LegendaryArmor.SpellResistance.19.Buff";
    private const string Ability19Name = "LegendaryArmor.SpellResistance.19.Ability";
    private const string Buff19ShieldName = "LegendaryArmor.SpellResistance.19.Shield.Buff";
    private const string Ability19ShieldName = "LegendaryArmor.SpellResistance.19.Shield.Ability";

    private const string DisplayName19 = "LegendaryArmor.SpellResistance.19.Name";
    private const string Description19 = "LegendaryArmor.SpellResistance.19.Name";
    private const int EnhancementCost19 = 3;

    internal static BlueprintFeature Configure19()
    {
      Logger.Log($"Configuring Spell Resistance 19");

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SpellResistance17Feature, EnhancementType.Armor, EnhancementCost19);

      UpdateSpellResistance(FeatureRefs.SpellResistance17Feature, 19);

      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: Buff19Name,
        buffGuid: Guids.SpellResistance19Buff,
        displayName: DisplayName19,
        description: Description19,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost19,
        abilityName: Ability19Name,
        abilityGuid: Guids.SpellResistance19Ability,
        buffComponents: spellResistFeature.GetComponent<AddSpellResistance>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: Buff19ShieldName,
        buffGuid: Guids.SpellResistance19ShieldBuff,
        abilityName: Ability19ShieldName,
        abilityGuid: Guids.SpellResistance19ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName19,
        description: Description19,
        //icon: ??,
        featureName: SpellResistance19Name,
        Guids.SpellResistance19,
        featureRanks: EnhancementCost19,
        prerequisiteFeature: Guids.SpellResistance19,
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private const string SpellResistance22Name = "LegendaryArmor.SpellResistance.22";
    private const string Buff22Name = "LegendaryArmor.SpellResistance.22.Buff";
    private const string Ability22Name = "LegendaryArmor.SpellResistance.22.Ability";
    private const string Buff22ShieldName = "LegendaryArmor.SpellResistance.22.Shield.Buff";
    private const string Ability22ShieldName = "LegendaryArmor.SpellResistance.22.Shield.Ability";

    private const string DisplayName22 = "LegendaryArmor.SpellResistance.22.Name";
    private const string Description22 = "LegendaryArmor.SpellResistance.22.Name";
    private const int EnhancementCost22 = 3;

    internal static BlueprintFeature Configure22()
    {
      Logger.Log($"Configuring Spell Resistance 22");

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SpellResistance19Feature, EnhancementType.Armor, EnhancementCost22);

      UpdateSpellResistance(FeatureRefs.SpellResistance19Feature, 22);

      var ability = EnchantmentTool.CreateEnchantAbility(
        buffName: Buff22Name,
        buffGuid: Guids.SpellResistance22Buff,
        displayName: DisplayName22,
        description: Description22,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost22,
        abilityName: Ability22Name,
        abilityGuid: Guids.SpellResistance22Ability,
        buffComponents: spellResistFeature.GetComponent<AddSpellResistance>());
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: Buff22ShieldName,
        buffGuid: Guids.SpellResistance22ShieldBuff,
        abilityName: Ability22ShieldName,
        abilityGuid: Guids.SpellResistance22ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName22,
        description: Description22,
        //icon: ??,
        featureName: SpellResistance22Name,
        Guids.SpellResistance22,
        featureRanks: EnhancementCost22,
        prerequisiteFeature: Guids.SpellResistance19,
        prerequisiteRanks: 1,
        ability,
        abilityShield);
    }

    private static void UpdateSpellResistance(Blueprint<BlueprintReference<BlueprintFeature>> feature, int sr)
    {
      FeatureConfigurator.For(feature).EditComponent<AddSpellResistance>(c => c.Value = sr).Configure();
    }
  }
}
