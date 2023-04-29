using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.FactLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class EnergyResistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnergyResistance));

    #region Too Many Constants
    private const string EnergyResistanceName = "LegendaryArmor.EnergyResistance";

    private const string EnergyResistanceAbility = "LegendaryArmor.EnergyResistance.Ability";
    private const string EnergyResistanceAbilityName = "LegendaryArmor.EnergyResistance.Ability.Name";
    private const string EnergyResistanceShieldAbility = "LegendaryArmor.EnergyResistance.Shield.Ability";
    private const string EnergyResistanceShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Name";

    private const string AcidBuffName = "LegendaryArmor.EnergyResistance.Buff.Acid";
    private const string AcidResistEffectBuffName = "LegendaryArmor.EnergyResistance.Ability.Acid";
    private const string AcidShieldBuffName = "LegendaryArmor.EnergyResistance.Shield.Buff.Acid";
    private const string AcidShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Acid";
    private const string AcidDisplayName = "LegendaryArmor.EnergyResistance.Acid.Name";
    private const string AcidDescription = "LegendaryArmor.EnergyResistance.Acid.Description";

    private const string ColdBuffName = "LegendaryArmor.EnergyResistance.Buff.Cold";
    private const string ColdAbilityName = "LegendaryArmor.EnergyResistance.Ability.Cold";
    private const string ColdShieldBuffName = "LegendaryArmor.EnergyResistance.Shield.Buff.Cold";
    private const string ColdShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Cold";
    private const string ColdDisplayName = "LegendaryArmor.EnergyResistance.Cold.Name";
    private const string ColdDescription = "LegendaryArmor.EnergyResistance.Cold.Description";

    private const string ElectricityBuffName = "LegendaryArmor.EnergyResistance.Buff.Electricity";
    private const string ElectricityAbilityName = "LegendaryArmor.EnergyResistance.Ability.Electricity";
    private const string ElectricityShieldBuffName = "LegendaryArmor.EnergyResistance.Shield.Buff.Electricity";
    private const string ElectricityShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Electricity";
    private const string ElectricityDisplayName = "LegendaryArmor.EnergyResistance.Electricity.Name";
    private const string ElectricityDescription = "LegendaryArmor.EnergyResistance.Electricity.Description";

    private const string FireBuffName = "LegendaryArmor.EnergyResistance.Buff.Fire";
    private const string FireAbilityName = "LegendaryArmor.EnergyResistance.Ability.Fire";
    private const string FireShieldBuffName = "LegendaryArmor.EnergyResistance.Shield.Buff.Fire";
    private const string FireShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Fire";
    private const string FireDisplayName = "LegendaryArmor.EnergyResistance.Fire.Name";
    private const string FireDescription = "LegendaryArmor.EnergyResistance.Fire.Description";

    private const string SonicBuffName = "LegendaryArmor.EnergyResistance.Buff.Sonic";
    private const string SonicAbilityName = "LegendaryArmor.EnergyResistance.Ability.Sonic";
    private const string SonicShieldBuffName = "LegendaryArmor.EnergyResistance.Shield.Buff.Sonic";
    private const string SonicShieldAbilityName = "LegendaryArmor.EnergyResistance.Shield.Ability.Sonic";
    private const string SonicDisplayName = "LegendaryArmor.EnergyResistance.Sonic.Name";
    private const string SonicDescription = "LegendaryArmor.EnergyResistance.Sonic.Description";
    #endregion

    private const string DisplayName = "LegendaryArmor.EnergyResistance.Name";
    private const string Description = "LegendaryArmor.EnergyResistance.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Energy Resistance 10");
      
      var acidInfo =
        new ArmorEnchantInfo(
          AcidDisplayName,
          AcidDescription,
          "",
          EnhancementCost,
          ranks: 2);
      var resistAcidFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.AcidResistance10Feature, acidInfo);

      EnchantTool.CreateEnchant(
        acidInfo,
        effectBuff: new(
          AcidResistEffectBuffName,
          Guids.AcidResist10EffectBuff,
          resistAcidFeature.GetComponent<AddDamageResistanceEnergy>()),
        parentBuff: new(AcidBuffName, Guids.AcidResist10Buff),
        variantBuff: new(AcidShieldBuffName, Guids.AcidResist10ShieldBuff));

      //var resistAcid = EnchantTool.CreateEnchantAbility(
      //  acidInfo,
      //  new BlueprintInfo(AcidBuffName, Guids.AcidResist10Buff, resistAcidFeature.GetComponent<AddDamageResistanceEnergy>()),
      //  new(AcidResistEffectBuffName, Guids.AcidResist10EffectBuff));
      //var resistAcidShield =
      //  EnchantTool.CreateEnchantShieldVariant(
      //    acidInfo, 
      //    resistAcid,
      //    new(AcidShieldBuffName, Guids.AcidResist10ShieldBuff),
      //    new(AcidShieldAbilityName, Guids.AcidResist10ShieldAbility));

      var coldInfo =
        new ArmorEnchantInfo(
          ColdDisplayName,
          ColdDescription,
          "",
          EnhancementCost,
          ranks: 2);
      var resistColdFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ColdResistance10Feature, coldInfo);

      var resistCold = EnchantTool.CreateEnchantAbility(
        coldInfo,
        new BlueprintInfo(ColdBuffName, Guids.ColdResist10Buff, resistColdFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ColdAbilityName, Guids.ColdResist10Ability));
      var resistColdShield =
        EnchantTool.CreateEnchantShieldVariant(
          coldInfo,
          resistCold,
          new(ColdShieldBuffName, Guids.ColdResist10ShieldBuff),
          new(ColdShieldAbilityName, Guids.ColdResist10ShieldAbility));

      var electricityInfo =
        new ArmorEnchantInfo(
          ElectricityDisplayName,
          ElectricityDescription,
          "",
          EnhancementCost,
          ranks: 2);
      var resistElectricityFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ElectricityResistance10Feature, electricityInfo);

      var resistElectricity = EnchantTool.CreateEnchantAbility(
        electricityInfo,
        new BlueprintInfo(ElectricityBuffName, Guids.ElectricityResist10Buff, resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ElectricityAbilityName, Guids.ElectricityResist10Ability));
      var resistElectricityShield =
        EnchantTool.CreateEnchantShieldVariant(
          electricityInfo,
          resistElectricity,
          new(ElectricityShieldBuffName, Guids.ElectricityResist10ShieldBuff),
          new(ElectricityShieldAbilityName, Guids.ElectricityResist10ShieldAbility));

      var fireInfo =
        new ArmorEnchantInfo(
          FireDisplayName,
          FireDescription,
          "",
          EnhancementCost,
          ranks: 2);
      var resistFireFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.FireResistance10Feature, fireInfo);

      var resistFire = EnchantTool.CreateEnchantAbility(
        fireInfo,
        new BlueprintInfo(FireBuffName, Guids.FireResist10Buff, resistFireFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(FireAbilityName, Guids.FireResist10Ability));
      var resistFireShield =
        EnchantTool.CreateEnchantShieldVariant(
          fireInfo,
          resistFire,
          new(FireShieldBuffName, Guids.FireResist10ShieldBuff),
          new(FireShieldAbilityName, Guids.FireResist10ShieldAbility));

      var sonicInfo =
        new ArmorEnchantInfo(
          SonicDisplayName,
          SonicDescription,
          "",
          EnhancementCost,
          ranks: 2);
      var resistSonicFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SonicResistance10Feature, sonicInfo);

      var resistSonic = EnchantTool.CreateEnchantAbility(
        sonicInfo,
        new BlueprintInfo(SonicBuffName, Guids.SonicResist10Buff, resistSonicFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(SonicAbilityName, Guids.SonicResist10Ability));
      var resistSonicShield =
        EnchantTool.CreateEnchantShieldVariant(
          sonicInfo,
          resistSonic,
          new(SonicShieldBuffName, Guids.SonicResist10ShieldBuff),
          new(SonicShieldAbilityName, Guids.SonicResist10ShieldAbility));
    }

    #region Too Many Constants
    private const string ImprovedEnergyResistanceName = "LegendaryArmor.EnergyResistance.Improved";

    private const string AcidImprovedBuffName = "LegendaryArmor.EnergyResistance.Improved.Buff.Acid";
    private const string AcidImprovedAbilityName = "LegendaryArmor.EnergyResistance.Improved.Ability.Acid";
    private const string AcidImprovedShieldBuffName = "LegendaryArmor.EnergyResistance.Improved.Shield.Buff.Acid";
    private const string AcidImprovedShieldAbilityName = "LegendaryArmor.EnergyResistance.Improved.Shield.Ability.Acid";
    private const string AcidImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Acid.Name";
    private const string AcidImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Acid.Description";

    private const string ColdImprovedBuffName = "LegendaryArmor.EnergyResistance.Improved.Buff.Cold";
    private const string ColdImprovedAbilityName = "LegendaryArmor.EnergyResistance.Improved.Ability.Cold";
    private const string ColdImprovedShieldBuffName = "LegendaryArmor.EnergyResistance.Improved.Shield.Buff.Cold";
    private const string ColdImprovedShieldAbilityName = "LegendaryArmor.EnergyResistance.Improved.Shield.Ability.Cold";
    private const string ColdImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Cold.Name";
    private const string ColdImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Cold.Description";

    private const string ElectricityImprovedBuffName = "LegendaryArmor.EnergyResistance.Improved.Buff.Electricity";
    private const string ElectricityImprovedAbilityName = "LegendaryArmor.EnergyResistance.Improved.Ability.Electricity";
    private const string ElectricityImprovedShieldBuffName = "LegendaryArmor.EnergyResistance.Improved.Shield.Buff.Electricity";
    private const string ElectricityImprovedShieldAbilityName = "LegendaryArmor.EnergyResistance.Improved.Shield.Ability.Electricity";
    private const string ElectricityImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Electricity.Name";
    private const string ElectricityImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Electricity.Description";

    private const string FireImprovedBuffName = "LegendaryArmor.EnergyResistance.Improved.Buff.Fire";
    private const string FireImprovedAbilityName = "LegendaryArmor.EnergyResistance.Improved.Ability.Fire";
    private const string FireImprovedShieldBuffName = "LegendaryArmor.EnergyResistance.Improved.Shield.Buff.Fire";
    private const string FireImprovedShieldAbilityName = "LegendaryArmor.EnergyResistance.Improved.Shield.Ability.Fire";
    private const string FireImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Fire.Name";
    private const string FireImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Fire.Description";

    private const string SonicImprovedBuffName = "LegendaryArmor.EnergyResistance.Improved.Buff.Sonic";
    private const string SonicImprovedAbilityName = "LegendaryArmor.EnergyResistance.Improved.Ability.Sonic";
    private const string SonicImprovedShieldBuffName = "LegendaryArmor.EnergyResistance.Improved.Shield.Buff.Sonic";
    private const string SonicImprovedShieldAbilityName = "LegendaryArmor.EnergyResistance.Improved.Shield.Ability.Sonic";
    private const string SonicImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Sonic.Name";
    private const string SonicImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Sonic.Description";
    #endregion

    private const string ImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Name";
    private const string ImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Description";
    private const int ImprovedEnhancementCost = 3;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Energy Resistance 20");

      var acidInfo =
        new ArmorEnchantInfo(
          AcidImprovedDisplayName,
          AcidImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1);
      var resistAcidFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.AcidResistance20Feature, acidInfo);

      var resistAcid = EnchantTool.CreateEnchantAbility(
        acidInfo,
        new BlueprintInfo(AcidImprovedBuffName, Guids.AcidResist20Buff, resistAcidFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(AcidImprovedAbilityName, Guids.AcidResist20Ability));
      var resistAcidShield =
        EnchantTool.CreateEnchantShieldVariant(
          acidInfo,
          resistAcid,
          new(AcidImprovedShieldBuffName, Guids.AcidResist20ShieldBuff),
          new(AcidImprovedShieldAbilityName, Guids.AcidResist20ShieldAbility));

      var coldInfo =
        new ArmorEnchantInfo(
          ColdImprovedDisplayName,
          ColdImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1);
      var resistColdFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ColdResistance20Feature, coldInfo);

      var resistCold = EnchantTool.CreateEnchantAbility(
        coldInfo,
        new BlueprintInfo(ColdImprovedBuffName, Guids.ColdResist20Buff, resistColdFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ColdImprovedAbilityName, Guids.ColdResist20Ability));
      var resistColdShield =
        EnchantTool.CreateEnchantShieldVariant(
          coldInfo,
          resistCold,
          new(ColdImprovedShieldBuffName, Guids.ColdResist20ShieldBuff),
          new(ColdImprovedShieldAbilityName, Guids.ColdResist20ShieldAbility));

      var electricityInfo =
        new ArmorEnchantInfo(
          ElectricityImprovedDisplayName,
          ElectricityImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1);
      var resistElectricityFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ElectricityResistance20Feature, electricityInfo);

      var resistElectricity = EnchantTool.CreateEnchantAbility(
        electricityInfo,
        new BlueprintInfo(ElectricityImprovedBuffName, Guids.ElectricityResist20Buff, resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ElectricityImprovedAbilityName, Guids.ElectricityResist20Ability));
      var resistElectricityShield =
        EnchantTool.CreateEnchantShieldVariant(
          electricityInfo,
          resistElectricity,
          new(ElectricityImprovedShieldBuffName, Guids.ElectricityResist20ShieldBuff),
          new(ElectricityImprovedShieldAbilityName, Guids.ElectricityResist20ShieldAbility));

      var fireInfo =
        new ArmorEnchantInfo(
          FireImprovedDisplayName,
          FireImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1);
      var resistFireFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.FireResistance20Feature, fireInfo);

      var resistFire = EnchantTool.CreateEnchantAbility(
        fireInfo,
        new BlueprintInfo(FireImprovedBuffName, Guids.FireResist20Buff, resistFireFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(FireImprovedAbilityName, Guids.FireResist20Ability));
      var resistFireShield =
        EnchantTool.CreateEnchantShieldVariant(
          fireInfo,
          resistFire,
          new(FireImprovedShieldBuffName, Guids.FireResist20ShieldBuff),
          new(FireImprovedShieldAbilityName, Guids.FireResist20ShieldAbility));

      var sonicInfo =
        new ArmorEnchantInfo(
          SonicImprovedDisplayName,
          SonicImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1);
      // Sonic doesn't have a +20 enhcant for some reason
      var resistSonicFeature = FeatureRefs.SonicResistance20.Reference.Get();

      var resistSonic = EnchantTool.CreateEnchantAbility(
        sonicInfo,
        new BlueprintInfo(SonicImprovedBuffName, Guids.SonicResist20Buff, resistSonicFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(SonicImprovedAbilityName, Guids.SonicResist20Ability));
      var resistSonicShield =
        EnchantTool.CreateEnchantShieldVariant(
          sonicInfo,
          resistSonic,
          new(SonicImprovedShieldBuffName, Guids.SonicResist20ShieldBuff),
          new(SonicImprovedShieldAbilityName, Guids.SonicResist20ShieldAbility));

      return EnchantTool.CreateEnchantFeature(
        new ArmorEnchantInfo(
          ImprovedDisplayName,
          ImprovedDescription,
          "",
          ImprovedEnhancementCost,
          ranks: 1,
          prerequisite: new(Guids.EnergyResist, ranks: 2)),
        new(ImprovedEnergyResistanceName, Guids.EnergyResist20),
        resistAcid,
        resistAcidShield,
        resistCold,
        resistColdShield,
        resistElectricity,
        resistElectricityShield,
        resistFire,
        resistFireShield,
        resistSonic,
        resistSonicShield);
    }

    private const string GreaterEnergyResistanceName = "LegendaryArmor.EnergyResistance.Greater";

    #region Too Many Constants
    private const string AcidGreaterBuffName = "LegendaryArmor.EnergyResistance.Greater.Buff.Acid";
    private const string AcidGreaterAbilityName = "LegendaryArmor.EnergyResistance.Greater.Ability.Acid";
    private const string AcidGreaterShieldBuffName = "LegendaryArmor.EnergyResistance.Greater.Shield.Buff.Acid";
    private const string AcidGreaterShieldAbilityName = "LegendaryArmor.EnergyResistance.Greater.Shield.Ability.Acid";
    private const string AcidGreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Acid.Name";
    private const string AcidGreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Acid.Description";

    private const string ColdGreaterBuffName = "LegendaryArmor.EnergyResistance.Greater.Buff.Cold";
    private const string ColdGreaterAbilityName = "LegendaryArmor.EnergyResistance.Greater.Ability.Cold";
    private const string ColdGreaterShieldBuffName = "LegendaryArmor.EnergyResistance.Greater.Shield.Buff.Cold";
    private const string ColdGreaterShieldAbilityName = "LegendaryArmor.EnergyResistance.Greater.Shield.Ability.Cold";
    private const string ColdGreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Cold.Name";
    private const string ColdGreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Cold.Description";

    private const string ElectricityGreaterBuffName = "LegendaryArmor.EnergyResistance.Greater.Buff.Electricity";
    private const string ElectricityGreaterAbilityName = "LegendaryArmor.EnergyResistance.Greater.Ability.Electricity";
    private const string ElectricityGreaterShieldBuffName = "LegendaryArmor.EnergyResistance.Greater.Shield.Buff.Electricity";
    private const string ElectricityGreaterShieldAbilityName = "LegendaryArmor.EnergyResistance.Greater.Shield.Ability.Electricity";
    private const string ElectricityGreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Electricity.Name";
    private const string ElectricityGreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Electricity.Description";

    private const string FireGreaterBuffName = "LegendaryArmor.EnergyResistance.Greater.Buff.Fire";
    private const string FireGreaterAbilityName = "LegendaryArmor.EnergyResistance.Greater.Ability.Fire";
    private const string FireGreaterShieldBuffName = "LegendaryArmor.EnergyResistance.Greater.Shield.Buff.Fire";
    private const string FireGreaterShieldAbilityName = "LegendaryArmor.EnergyResistance.Greater.Shield.Ability.Fire";
    private const string FireGreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Fire.Name";
    private const string FireGreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Fire.Description";

    private const string SonicGreaterBuffName = "LegendaryArmor.EnergyResistance.Greater.Buff.Sonic";
    private const string SonicGreaterAbilityName = "LegendaryArmor.EnergyResistance.Greater.Ability.Sonic";
    private const string SonicGreaterShieldBuffName = "LegendaryArmor.EnergyResistance.Greater.Shield.Buff.Sonic";
    private const string SonicGreaterShieldAbilityName = "LegendaryArmor.EnergyResistance.Greater.Shield.Ability.Sonic";
    private const string SonicGreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Sonic.Name";
    private const string SonicGreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Sonic.Description";
    #endregion

    private const string GreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Description";
    private const int GreaterEnhancementCost = 4;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Energy Resistance 30");

      var acidInfo =
        new ArmorEnchantInfo(
          AcidGreaterDisplayName,
          AcidGreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1);
      var resistAcidFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.AcidResistance30Feature, acidInfo);

      var resistAcid = EnchantTool.CreateEnchantAbility(
        acidInfo,
        new BlueprintInfo(AcidGreaterBuffName, Guids.AcidResist30Buff, resistAcidFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(AcidGreaterAbilityName, Guids.AcidResist30Ability));
      var resistAcidShield =
        EnchantTool.CreateEnchantShieldVariant(
          acidInfo,
          resistAcid,
          new(AcidGreaterShieldBuffName, Guids.AcidResist30ShieldBuff),
          new(AcidGreaterShieldAbilityName, Guids.AcidResist30ShieldAbility));

      var coldInfo =
        new ArmorEnchantInfo(
          ColdGreaterDisplayName,
          ColdGreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1);
      var resistColdFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ColdResistance30Feature, coldInfo);

      var resistCold = EnchantTool.CreateEnchantAbility(
        coldInfo,
        new BlueprintInfo(ColdGreaterBuffName, Guids.ColdResist30Buff, resistColdFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ColdGreaterAbilityName, Guids.ColdResist30Ability));
      var resistColdShield =
        EnchantTool.CreateEnchantShieldVariant(
          coldInfo,
          resistCold,
          new(ColdGreaterShieldBuffName, Guids.ColdResist30ShieldBuff),
          new(ColdGreaterShieldAbilityName, Guids.ColdResist30ShieldAbility));

      var electricityInfo =
        new ArmorEnchantInfo(
          ElectricityGreaterDisplayName,
          ElectricityGreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1);
      var resistElectricityFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.ElectricityResistance30Feature, electricityInfo);

      var resistElectricity = EnchantTool.CreateEnchantAbility(
        electricityInfo,
        new BlueprintInfo(ElectricityGreaterBuffName, Guids.ElectricityResist30Buff, resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(ElectricityGreaterAbilityName, Guids.ElectricityResist30Ability));
      var resistElectricityShield =
        EnchantTool.CreateEnchantShieldVariant(
          electricityInfo,
          resistElectricity,
          new(ElectricityGreaterShieldBuffName, Guids.ElectricityResist30ShieldBuff),
          new(ElectricityGreaterShieldAbilityName, Guids.ElectricityResist30ShieldAbility));

      var fireInfo =
        new ArmorEnchantInfo(
          FireGreaterDisplayName,
          FireGreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1);
      var resistFireFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.FireResistance30Feature, fireInfo);

      var resistFire = EnchantTool.CreateEnchantAbility(
        fireInfo,
        new BlueprintInfo(FireGreaterBuffName, Guids.FireResist30Buff, resistFireFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(FireGreaterAbilityName, Guids.FireResist30Ability));
      var resistFireShield =
        EnchantTool.CreateEnchantShieldVariant(
          fireInfo,
          resistFire,
          new(FireGreaterShieldBuffName, Guids.FireResist30ShieldBuff),
          new(FireGreaterShieldAbilityName, Guids.FireResist30ShieldAbility));

      var sonicInfo =
        new ArmorEnchantInfo(
          SonicGreaterDisplayName,
          SonicGreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1);
      var resistSonicFeature =
        EnchantTool.AddEnhancementEquivalence(FeatureRefs.SonicResistance30Feature, sonicInfo);

      var resistSonic = EnchantTool.CreateEnchantAbility(
        sonicInfo,
        new BlueprintInfo(SonicGreaterBuffName, Guids.SonicResist30Buff, resistSonicFeature.GetComponent<AddDamageResistanceEnergy>()),
        new(SonicGreaterAbilityName, Guids.SonicResist30Ability));
      var resistSonicShield =
        EnchantTool.CreateEnchantShieldVariant(
          sonicInfo,
          resistSonic,
          new(SonicGreaterShieldBuffName, Guids.SonicResist30ShieldBuff),
          new(SonicGreaterShieldAbilityName, Guids.SonicResist30ShieldAbility));

      return EnchantTool.CreateEnchantFeature(
        new ArmorEnchantInfo(
          GreaterDisplayName,
          GreaterDescription,
          "",
          GreaterEnhancementCost,
          ranks: 1,
          prerequisite: new(Guids.EnergyResist20)),
        new(GreaterEnergyResistanceName, Guids.EnergyResist30),
        resistAcid,
        resistAcidShield,
        resistCold,
        resistColdShield,
        resistElectricity,
        resistElectricityShield,
        resistFire,
        resistFireShield,
        resistSonic,
        resistSonicShield);
    }
  }
}
