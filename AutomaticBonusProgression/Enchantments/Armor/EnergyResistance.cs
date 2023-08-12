﻿using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class EnergyResistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnergyResistance));

    internal static void Configure()
    {
      ConfigureBasic();
      ConfigureImproved();
      ConfigureGreater();
    }

    #region Resist 10 Constants
    private const string Acid10EffectName = "LA.EnergyResistance.10.Acid.Effect";
    private const string Acid10BuffName = "LA.EnergyResistance.10.Acid.Buff";
    private const string Acid10ShieldBuffName = "LA.EnergyResistance.10.Acid.Buff.Shield";
    private const string Acid10DisplayName = "LA.EnergyResistance.10.Acid.Name";
    private const string Acid10Description = "LA.EnergyResistance.10.Acid.Description";

    private const string Cold10EffectName = "LA.EnergyResistance.10.Cold.Buff.Effect";
    private const string Cold10BuffName = "LA.EnergyResistance.10.Cold.Buff";
    private const string Cold10ShieldBuffName = "LA.EnergyResistance.10.Cold.Buff.Shield";
    private const string Cold10DisplayName = "LA.EnergyResistance.10.Cold.Name";
    private const string Cold10Description = "LA.EnergyResistance.10.Cold.Description";

    private const string Electricity10EffectName = "LA.EnergyResistance.10.Electricity.Buff.Effect";
    private const string Electricity10BuffName = "LA.EnergyResistance.10.Electricity.Buff";
    private const string Electricity10ShieldBuffName = "LA.EnergyResistance.10.Electricity.Buff.Shield";
    private const string Electricity10DisplayName = "LA.EnergyResistance.10.Electricity.Name";
    private const string Electricity10Description = "LA.EnergyResistance.10.Electricity.Description";

    private const string Fire10EffectName = "LA.EnergyResistance.10.Fire.Buff.Effect";
    private const string Fire10BuffName = "LA.EnergyResistance.10.Fire.Buff";
    private const string Fire10ShieldBuffName = "LA.EnergyResistance.10.Fire.Buff.Shield";
    private const string Fire10DisplayName = "LA.EnergyResistance.10.Fire.Name";
    private const string Fire10Description = "LA.EnergyResistance.10.Fire.Description";

    private const string Sonic10EffectName = "LA.EnergyResistance.10.Sonic.Buff.Effect";
    private const string Sonic10BuffName = "LA.EnergyResistance.10.Sonic.Buff";
    private const string Sonic10ShieldBuffName = "LA.EnergyResistance.10.Sonic.Buff.Shield";
    private const string Sonic10DisplayName = "LA.EnergyResistance.10.Sonic.Name";
    private const string Sonic10Description = "LA.EnergyResistance.10.Sonic.Description";
    private const string Sonic10Icon = "assets/icons/resistsonic10.png";

    private const int Resist10Cost = 2;
    #endregion

    internal static void ConfigureBasic()
    {
      Logger.Log($"Configuring Energy Resistance 10");

      var acidInfo =
        new ArmorEnchantInfo(
          Acid10DisplayName,
          Acid10Description,
          BuffRefs.SacredArmorEnchantAcidResist10Buff.Reference.Get().Icon,
          Resist10Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.AcidResistance10Enchant, acidInfo);

      EnchantTool.CreateEnchant(
        acidInfo,
        effectBuff: new(
          Acid10EffectName,
          Guids.AcidResist10Effect,
          FeatureRefs.AcidResistance10Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Acid10BuffName, Guids.AcidResist10Buff),
        variantBuff: new(Acid10ShieldBuffName, Guids.AcidResist10ShieldBuff));

      var coldInfo =
        new ArmorEnchantInfo(
          Cold10DisplayName,
          Cold10Description,
          BuffRefs.SacredArmorEnchantColdResist10Buff.Reference.Get().Icon,
          Resist10Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ColdResistance10Enchant, coldInfo);

      EnchantTool.CreateEnchant(
        coldInfo,
        effectBuff: new(
          Cold10EffectName,
          Guids.ColdResist10Effect,
          FeatureRefs.ColdResistance10Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Cold10BuffName, Guids.ColdResist10Buff),
        variantBuff: new(Cold10ShieldBuffName, Guids.ColdResist10ShieldBuff));

      var electricityInfo =
        new ArmorEnchantInfo(
          Electricity10DisplayName,
          Electricity10Description,
          BuffRefs.SacredArmorEnchantElectricityResist10Buff.Reference.Get().Icon,
          Resist10Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ElectricityResistance10Enchant, electricityInfo);

      EnchantTool.CreateEnchant(
        electricityInfo,
        effectBuff: new(
          Electricity10EffectName,
          Guids.ElectricityResist10Effect,
          FeatureRefs.ElectricityResistance10Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Electricity10BuffName, Guids.ElectricityResist10Buff),
        variantBuff: new(Electricity10ShieldBuffName, Guids.ElectricityResist10ShieldBuff));

      var fireInfo =
        new ArmorEnchantInfo(
          Fire10DisplayName,
          Fire10Description,
          BuffRefs.SacredArmorEnchantFireResist10Buff.Reference.Get().Icon,
          Resist10Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.FireResistance10Enchant, fireInfo);

      EnchantTool.CreateEnchant(
        fireInfo,
        effectBuff: new(
          Fire10EffectName,
          Guids.FireResist10Effect,
          FeatureRefs.FireResistance10Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Fire10BuffName, Guids.FireResist10Buff),
        variantBuff: new(Fire10ShieldBuffName, Guids.FireResist10ShieldBuff));

      var sonicInfo = new ArmorEnchantInfo(Sonic10DisplayName, Sonic10Description, Sonic10Icon, Resist10Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.SonicResistance10Enchant, sonicInfo);

      EnchantTool.CreateEnchant(
        sonicInfo,
        effectBuff: new(
          Sonic10EffectName,
          Guids.SonicResist10Effect,
          FeatureRefs.SonicResistance10Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Sonic10BuffName, Guids.SonicResist10Buff),
        variantBuff: new(Sonic10ShieldBuffName, Guids.SonicResist10ShieldBuff));
    }

    #region Resist 20 Constants
    private const string Acid20EffectName = "LA.EnergyResistance.20.Acid.Effect";
    private const string Acid20BuffName = "LA.EnergyResistance.20.Acid.Buff";
    private const string Acid20ShieldBuffName = "LA.EnergyResistance.20.Acid.Buff.Shield";
    private const string Acid20DisplayName = "LA.EnergyResistance.20.Acid.Name";
    private const string Acid20Description = "LA.EnergyResistance.20.Acid.Description";

    private const string Cold20EffectName = "LA.EnergyResistance.20.Cold.Buff.Effect";
    private const string Cold20BuffName = "LA.EnergyResistance.20.Cold.Buff";
    private const string Cold20ShieldBuffName = "LA.EnergyResistance.20.Cold.Buff.Shield";
    private const string Cold20DisplayName = "LA.EnergyResistance.20.Cold.Name";
    private const string Cold20Description = "LA.EnergyResistance.20.Cold.Description";

    private const string Electricity20EffectName = "LA.EnergyResistance.20.Electricity.Buff.Effect";
    private const string Electricity20BuffName = "LA.EnergyResistance.20.Electricity.Buff";
    private const string Electricity20ShieldBuffName = "LA.EnergyResistance.20.Electricity.Buff.Shield";
    private const string Electricity20DisplayName = "LA.EnergyResistance.20.Electricity.Name";
    private const string Electricity20Description = "LA.EnergyResistance.20.Electricity.Description";

    private const string Fire20EffectName = "LA.EnergyResistance.20.Fire.Buff.Effect";
    private const string Fire20BuffName = "LA.EnergyResistance.20.Fire.Buff";
    private const string Fire20ShieldBuffName = "LA.EnergyResistance.20.Fire.Buff.Shield";
    private const string Fire20DisplayName = "LA.EnergyResistance.20.Fire.Name";
    private const string Fire20Description = "LA.EnergyResistance.20.Fire.Description";

    private const string Sonic20EffectName = "LA.EnergyResistance.20.Sonic.Buff.Effect";
    private const string Sonic20BuffName = "LA.EnergyResistance.20.Sonic.Buff";
    private const string Sonic20ShieldBuffName = "LA.EnergyResistance.20.Sonic.Buff.Shield";
    private const string Sonic20DisplayName = "LA.EnergyResistance.20.Sonic.Name";
    private const string Sonic20Description = "LA.EnergyResistance.20.Sonic.Description";
    private const string Sonic20Icon = "assets/icons/resistsonic20.png";

    private const int Resist20Cost = 3;
    #endregion

    internal static void ConfigureImproved()
    {
      Logger.Log($"Configuring Energy Resistance 20");

      var acidInfo =
        new ArmorEnchantInfo(
          Acid20DisplayName,
          Acid20Description,
          BuffRefs.SacredArmorEnchantAcidResist20Buff.Reference.Get().Icon,
          Resist20Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.AcidResistance20Enchant, acidInfo);

      EnchantTool.CreateEnchant(
        acidInfo,
        effectBuff: new(
          Acid20EffectName,
          Guids.AcidResist20Effect,
          FeatureRefs.AcidResistance20Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Acid20BuffName, Guids.AcidResist20Buff),
        variantBuff: new(Acid20ShieldBuffName, Guids.AcidResist20ShieldBuff));

      var coldInfo =
        new ArmorEnchantInfo(
          Cold20DisplayName,
          Cold20Description,
          BuffRefs.SacredArmorEnchantColdResist20Buff.Reference.Get().Icon,
          Resist20Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ColdResistance20Enchant, coldInfo);

      EnchantTool.CreateEnchant(
        coldInfo,
        effectBuff: new(
          Cold20EffectName,
          Guids.ColdResist20Effect,
          FeatureRefs.ColdResistance20Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Cold20BuffName, Guids.ColdResist20Buff),
        variantBuff: new(Cold20ShieldBuffName, Guids.ColdResist20ShieldBuff));

      var electricityInfo =
        new ArmorEnchantInfo(
          Electricity20DisplayName,
          Electricity20Description,
          BuffRefs.SacredArmorEnchantElectricityResist20Buff.Reference.Get().Icon,
          Resist20Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ElectricityResistance20Enchant, electricityInfo);

      EnchantTool.CreateEnchant(
        electricityInfo,
        effectBuff: new(
          Electricity20EffectName,
          Guids.ElectricityResist20Effect,
          FeatureRefs.ElectricityResistance20Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Electricity20BuffName, Guids.ElectricityResist20Buff),
        variantBuff: new(Electricity20ShieldBuffName, Guids.ElectricityResist20ShieldBuff));

      var fireInfo =
        new ArmorEnchantInfo(
          Fire20DisplayName,
          Fire20Description,
          BuffRefs.SacredArmorEnchantFireResist20Buff.Reference.Get().Icon,
          Resist20Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.FireResistance20Enchant, fireInfo);

      EnchantTool.CreateEnchant(
        fireInfo,
        effectBuff: new(
          Fire20EffectName,
          Guids.FireResist20Effect,
          FeatureRefs.FireResistance20Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Fire20BuffName, Guids.FireResist20Buff),
        variantBuff: new(Fire20ShieldBuffName, Guids.FireResist20ShieldBuff));

      var sonicInfo = new ArmorEnchantInfo(Sonic20DisplayName, Sonic20Description, Sonic20Icon, Resist20Cost);
      // Sonic doesn't have a +20 enchant for some reason
      var resistSonicFeature = FeatureRefs.SonicResistance20.Reference.Get();

      EnchantTool.CreateEnchant(
        sonicInfo,
        effectBuff: new(
          Sonic20EffectName,
          Guids.SonicResist20Effect,
          resistSonicFeature.GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Sonic20BuffName, Guids.SonicResist20Buff),
        variantBuff: new(Sonic20ShieldBuffName, Guids.SonicResist20ShieldBuff));
    }

    #region Resist 30 Constants
    private const string Acid30EffectName = "LA.EnergyResistance.30.Acid.Effect";
    private const string Acid30BuffName = "LA.EnergyResistance.30.Acid.Buff";
    private const string Acid30ShieldBuffName = "LA.EnergyResistance.30.Acid.Buff.Shield";
    private const string Acid30DisplayName = "LA.EnergyResistance.30.Acid.Name";
    private const string Acid30Description = "LA.EnergyResistance.30.Acid.Description";

    private const string Cold30EffectName = "LA.EnergyResistance.30.Cold.Buff.Effect";
    private const string Cold30BuffName = "LA.EnergyResistance.30.Cold.Buff";
    private const string Cold30ShieldBuffName = "LA.EnergyResistance.30.Cold.Buff.Shield";
    private const string Cold30DisplayName = "LA.EnergyResistance.30.Cold.Name";
    private const string Cold30Description = "LA.EnergyResistance.30.Cold.Description";

    private const string Electricity30EffectName = "LA.EnergyResistance.30.Electricity.Buff.Effect";
    private const string Electricity30BuffName = "LA.EnergyResistance.30.Electricity.Buff";
    private const string Electricity30ShieldBuffName = "LA.EnergyResistance.30.Electricity.Buff.Shield";
    private const string Electricity30DisplayName = "LA.EnergyResistance.30.Electricity.Name";
    private const string Electricity30Description = "LA.EnergyResistance.30.Electricity.Description";

    private const string Fire30EffectName = "LA.EnergyResistance.30.Fire.Buff.Effect";
    private const string Fire30BuffName = "LA.EnergyResistance.30.Fire.Buff";
    private const string Fire30ShieldBuffName = "LA.EnergyResistance.30.Fire.Buff.Shield";
    private const string Fire30DisplayName = "LA.EnergyResistance.30.Fire.Name";
    private const string Fire30Description = "LA.EnergyResistance.30.Fire.Description";

    private const string Sonic30EffectName = "LA.EnergyResistance.30.Sonic.Buff.Effect";
    private const string Sonic30BuffName = "LA.EnergyResistance.30.Sonic.Buff";
    private const string Sonic30ShieldBuffName = "LA.EnergyResistance.30.Sonic.Buff.Shield";
    private const string Sonic30DisplayName = "LA.EnergyResistance.30.Sonic.Name";
    private const string Sonic30Description = "LA.EnergyResistance.30.Sonic.Description";
    private const string Sonic30Icon = "assets/icons/resistsonic30.png";

    private const int Resist30Cost = 4;
    #endregion

    internal static void ConfigureGreater()
    {
      Logger.Log($"Configuring Energy Resistance 30");

      var acidInfo =
        new ArmorEnchantInfo(
          Acid30DisplayName,
          Acid30Description,
          BuffRefs.SacredArmorEnchantAcidResist30Buff.Reference.Get().Icon,
          Resist30Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.AcidResistance30Enchant, acidInfo);

      EnchantTool.CreateEnchant(
        acidInfo,
        effectBuff: new(
          Acid30EffectName,
          Guids.AcidResist30Effect,
          FeatureRefs.AcidResistance30Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Acid30BuffName, Guids.AcidResist30Buff),
        variantBuff: new(Acid30ShieldBuffName, Guids.AcidResist30ShieldBuff));

      var coldInfo =
        new ArmorEnchantInfo(
          Cold30DisplayName,
          Cold30Description,
          BuffRefs.SacredArmorEnchantColdResist30Buff.Reference.Get().Icon,
          Resist30Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ColdResistance30Enchant, coldInfo);

      EnchantTool.CreateEnchant(
        coldInfo,
        effectBuff: new(
          Cold30EffectName,
          Guids.ColdResist30Effect,
          FeatureRefs.ColdResistance30Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Cold30BuffName, Guids.ColdResist30Buff),
        variantBuff: new(Cold30ShieldBuffName, Guids.ColdResist30ShieldBuff));

      var electricityInfo =
        new ArmorEnchantInfo(
          Electricity30DisplayName,
          Electricity30Description,
          BuffRefs.SacredArmorEnchantElectricityResist30Buff.Reference.Get().Icon,
          Resist30Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.ElectricityResistance30Enchant, electricityInfo);

      EnchantTool.CreateEnchant(
        electricityInfo,
        effectBuff: new(
          Electricity30EffectName,
          Guids.ElectricityResist30Effect,
          FeatureRefs.ElectricityResistance30Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Electricity30BuffName, Guids.ElectricityResist30Buff),
        variantBuff: new(Electricity30ShieldBuffName, Guids.ElectricityResist30ShieldBuff));

      var fireInfo =
        new ArmorEnchantInfo(
          Fire30DisplayName,
          Fire30Description,
          BuffRefs.SacredArmorEnchantFireResist30Buff.Reference.Get().Icon,
          Resist30Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.FireResistance30Enchant, fireInfo);

      EnchantTool.CreateEnchant(
        fireInfo,
        effectBuff: new(
          Fire30EffectName,
          Guids.FireResist30Effect,
          FeatureRefs.FireResistance30Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Fire30BuffName, Guids.FireResist30Buff),
        variantBuff: new(Fire30ShieldBuffName, Guids.FireResist30ShieldBuff));

      var sonicInfo = new ArmorEnchantInfo(Sonic30DisplayName, Sonic30Description, Sonic30Icon, Resist30Cost);
      EnchantTool.AddEnhancementEquivalenceArmor(ArmorEnchantmentRefs.SonicResistance30Enchant, sonicInfo);

      EnchantTool.CreateEnchant(
        sonicInfo,
        effectBuff: new(
          Sonic30EffectName,
          Guids.SonicResist30Effect,
          FeatureRefs.SonicResistance30Feature.Reference.Get().GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(Sonic30BuffName, Guids.SonicResist30Buff),
        variantBuff: new(Sonic30ShieldBuffName, Guids.SonicResist30ShieldBuff));
    }
  }
}
