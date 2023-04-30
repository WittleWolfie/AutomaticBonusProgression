using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  /// <summary>
  /// Includes rebalance from 13 / 15 / 17 / 19 --> 13 / 16 / 19 / 22
  /// </summary>
  internal class SpellResistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SpellResistance));

    internal static void Configure()
    {
      Logger.Log($"Configuring Spell Resistance");

      Configure13();
      Configure16();
      Configure19();
      Configure22();
    }

    private const string Effect13Name = "LA.SpellResistance.13";
    private const string Buff13Name = "LA.SpellResistance.13.Buff";
    private const string Buff13ShieldName = "LA.SpellResistance.13.Shield.Buff";

    private const string DisplayName13 = "LA.SpellResistance.13.Name";
    private const string Description13 = "LA.SpellResistance.13.Name";
    private const int EnhancementCost13 = 2;

    private static void Configure13()
    {
      var enchantInfo = new ArmorEnchantInfo(DisplayName13, Description13, "", EnhancementCost13);
      var spellResistFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance13Feature, enchantInfo);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(Effect13Name, Guids.SpellResistance13Effect, spellResistFeature.GetComponent<AddSpellResistance>()),
        parentBuff: new(Buff13Name, Guids.SpellResistance13Buff),
        variantBuff: new(Buff13ShieldName, Guids.SpellResistance13ShieldBuff));
    }

    private const string Effect16Name = "LA.SpellResistance.16";
    private const string Buff16Name = "LA.SpellResistance.16.Buff";
    private const string Buff16ShieldName = "LA.SpellResistance.16.Shield.Buff";

    private const string DisplayName16 = "LA.SpellResistance.16.Name";
    private const string Description16 = "LA.SpellResistance.16.Name";
    private const int EnhancementCost16 = 3;

    private static void Configure16()
    {
      var enchantInfo = new ArmorEnchantInfo(DisplayName16, Description16, "", EnhancementCost16);
      var spellResistFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance15Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance15Feature, 16);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(Effect16Name, Guids.SpellResistance16Effect, spellResistFeature.GetComponent<AddSpellResistance>()),
        parentBuff: new(Buff16Name, Guids.SpellResistance16Buff),
        variantBuff: new(Buff16ShieldName, Guids.SpellResistance16ShieldBuff));
    }

    private const string Effect19Name = "LA.SpellResistance.19";
    private const string Buff19Name = "LA.SpellResistance.19.Buff";
    private const string Buff19ShieldName = "LA.SpellResistance.19.Shield.Buff";

    private const string DisplayName19 = "LA.SpellResistance.19.Name";
    private const string Description19 = "LA.SpellResistance.19.Name";
    private const int EnhancementCost19 = 4;

    private static void Configure19()
    {
      var enchantInfo = new ArmorEnchantInfo(DisplayName19, Description19, "", EnhancementCost19);
      var spellResistFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance17Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance17Feature, 19);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(Effect19Name, Guids.SpellResistance19Effect, spellResistFeature.GetComponent<AddSpellResistance>()),
        parentBuff: new(Buff19Name, Guids.SpellResistance19Buff),
        variantBuff: new(Buff19ShieldName, Guids.SpellResistance19ShieldBuff));
    }

    private const string Effect22Name = "LA.SpellResistance.22";
    private const string Buff22Name = "LA.SpellResistance.22.Buff";
    private const string Buff22ShieldName = "LA.SpellResistance.22.Shield.Buff";

    private const string DisplayName22 = "LA.SpellResistance.22.Name";
    private const string Description22 = "LA.SpellResistance.22.Name";
    private const int EnhancementCost22 = 5;

    private static void Configure22()
    {
      var enchantInfo = new ArmorEnchantInfo(DisplayName22, Description22, "", EnhancementCost22);
      var spellResistFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SpellResistance19Feature, enchantInfo);
      UpdateSpellResistance(FeatureRefs.SpellResistance19Feature, 22);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(Effect22Name, Guids.SpellResistance22Effect, spellResistFeature.GetComponent<AddSpellResistance>()),
        parentBuff: new(Buff22Name, Guids.SpellResistance22Buff),
        variantBuff: new(Buff22ShieldName, Guids.SpellResistance22ShieldBuff));
    }

    private static void UpdateSpellResistance(Blueprint<BlueprintReference<BlueprintFeature>> feature, int sr)
    {
      FeatureConfigurator.For(feature).EditComponent<AddSpellResistance>(c => c.Value = sr).Configure();
    }
  }
}
