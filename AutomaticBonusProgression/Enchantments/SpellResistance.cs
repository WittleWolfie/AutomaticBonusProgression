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

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName13,
          Description13,
          "",
          EnhancementCost13,
          ranks: 2);
      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance13Feature, enchantInfo);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(Buff13Name, Guids.SpellResistance13Buff, spellResistFeature.GetComponent<AddSpellResistance>()),
        new(Ability13Name, Guids.SpellResistance13Ability));
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(Buff13ShieldName, Guids.SpellResistance13ShieldBuff),
        new(Ability13ShieldName, Guids.SpellResistance13ShieldAbility));

      return EnchantmentTool.CreateEnchantFeature(
        enchantInfo,
        new(SpellResistance13Name, Guids.SpellResistance13),
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

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName16,
          Description16,
          "",
          EnhancementCost16,
          ranks: 1,
          prerequisite: new(Guids.SpellResistance13, ranks: 2));

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance15Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance15Feature, 16);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(Buff16Name, Guids.SpellResistance16Buff, spellResistFeature.GetComponent<AddSpellResistance>()),
        new(Ability16Name, Guids.SpellResistance16Ability));
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(Buff16ShieldName, Guids.SpellResistance16ShieldBuff),
        new(Ability16ShieldName, Guids.SpellResistance16ShieldAbility));

      return EnchantmentTool.CreateEnchantFeature(
        enchantInfo,
        new(SpellResistance16Name, Guids.SpellResistance16),
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
    private const int EnhancementCost19 = 4;

    internal static BlueprintFeature Configure19()
    {
      Logger.Log($"Configuring Spell Resistance 19");

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName19,
          Description19,
          "",
          EnhancementCost19,
          ranks: 1,
          prerequisite: new(Guids.SpellResistance16));

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance17Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance17Feature, 19);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(Buff19Name, Guids.SpellResistance19Buff, spellResistFeature.GetComponent<AddSpellResistance>()),
        new(Ability19Name, Guids.SpellResistance19Ability));
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(Buff19ShieldName, Guids.SpellResistance19ShieldBuff),
        new(Ability19ShieldName, Guids.SpellResistance19ShieldAbility));

      return EnchantmentTool.CreateEnchantFeature(
        enchantInfo,
        new(SpellResistance19Name, Guids.SpellResistance19),
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
    private const int EnhancementCost22 = 5;

    internal static BlueprintFeature Configure22()
    {
      Logger.Log($"Configuring Spell Resistance 22");

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName22,
          Description22,
          "",
          EnhancementCost22,
          ranks: 1,
          prerequisite: new(Guids.SpellResistance19));

      var spellResistFeature =
        EnchantmentTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance19Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance19Feature, 22);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(Buff22Name, Guids.SpellResistance22Buff, spellResistFeature.GetComponent<AddSpellResistance>()),
        new(Ability22Name, Guids.SpellResistance22Ability));
      var abilityShield = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo,
        ability,
        new(Buff22ShieldName, Guids.SpellResistance22ShieldBuff),
        new(Ability22ShieldName, Guids.SpellResistance22ShieldAbility));

      return EnchantmentTool.CreateEnchantFeature(
        enchantInfo,
        new(SpellResistance22Name, Guids.SpellResistance22),
        ability,
        abilityShield);
    }

    private static void UpdateSpellResistance(Blueprint<BlueprintReference<BlueprintFeature>> feature, int sr)
    {
      FeatureConfigurator.For(feature).EditComponent<AddSpellResistance>(c => c.Value = sr).Configure();
    }
  }
}
