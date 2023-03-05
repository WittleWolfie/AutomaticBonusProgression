using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.FactLogic;

namespace AutomaticBonusProgression.Enchantments
{
  internal class EnergyResistance
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnergyResistance));

    private const string EnergyResistanceName = "LegendaryArmor.EnergyResistance";

    private const string AcidBuffName = "LegendaryArmor.EnergyResistance.Buff.Acid";
    private const string AcidAbilityName = "LegendaryArmor.EnergyResistance.Ability.Acid";
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

    private const string DisplayName = "LegendaryArmor.EnergyResistance.Name";
    private const string Description = "LegendaryArmor.EnergyResistance.Description";
    private const int EnhancementCost = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Energy Resistance 10");

      var resistAcidFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.AcidResistance10Feature, EnhancementType.Armor, EnhancementCost);
      var resistAcid = EnchantmentTool.CreateEnchantAbility(
        buffName: AcidBuffName,
        buffGuid: Guids.AcidResist10Buff,
        displayName: AcidDisplayName,
        description: AcidDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AcidAbilityName,
        abilityGuid: Guids.AcidResist10Ability,
        buffComponents: resistAcidFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistAcidShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistAcid,
        buffName: AcidShieldBuffName,
        buffGuid: Guids.AcidResist10ShieldBuff,
        abilityName: AcidShieldAbilityName,
        abilityGuid: Guids.AcidResist10ShieldAbility);

      var resistColdFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ColdResistance10Feature, EnhancementType.Armor, EnhancementCost);
      var resistCold = EnchantmentTool.CreateEnchantAbility(
        buffName: ColdBuffName,
        buffGuid: Guids.ColdResist10Buff,
        displayName: ColdDisplayName,
        description: ColdDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: ColdAbilityName,
        abilityGuid: Guids.ColdResist10Ability,
        buffComponents: resistColdFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistColdShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistCold,
        buffName: ColdShieldBuffName,
        buffGuid: Guids.ColdResist10ShieldBuff,
        abilityName: ColdShieldAbilityName,
        abilityGuid: Guids.ColdResist10ShieldAbility);

      var resistElectricityFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ElectricityResistance10Feature, EnhancementType.Armor, EnhancementCost);
      var resistElectricity = EnchantmentTool.CreateEnchantAbility(
        buffName: ElectricityBuffName,
        buffGuid: Guids.ElectricityResist10Buff,
        displayName: ElectricityDisplayName,
        description: ElectricityDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: ElectricityAbilityName,
        abilityGuid: Guids.ElectricityResist10Ability,
        buffComponents: resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistElectricityShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistElectricity,
        buffName: ElectricityShieldBuffName,
        buffGuid: Guids.ElectricityResist10ShieldBuff,
        abilityName: ElectricityShieldAbilityName,
        abilityGuid: Guids.ElectricityResist10ShieldAbility);

      var resistFireFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.FireResistance10Feature, EnhancementType.Armor, EnhancementCost);
      var resistFire = EnchantmentTool.CreateEnchantAbility(
        buffName: FireBuffName,
        buffGuid: Guids.FireResist10Buff,
        displayName: FireDisplayName,
        description: FireDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: FireAbilityName,
        abilityGuid: Guids.FireResist10Ability,
        buffComponents: resistFireFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistFireShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistFire,
        buffName: FireShieldBuffName,
        buffGuid: Guids.FireResist10ShieldBuff,
        abilityName: FireShieldAbilityName,
        abilityGuid: Guids.FireResist10ShieldAbility);

      var resistSonicFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SonicResistance10Feature, EnhancementType.Armor, EnhancementCost);
      var resistSonic = EnchantmentTool.CreateEnchantAbility(
        buffName: SonicBuffName,
        buffGuid: Guids.SonicResist10Buff,
        displayName: SonicDisplayName,
        description: SonicDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: SonicAbilityName,
        abilityGuid: Guids.SonicResist10Ability,
        buffComponents: resistSonicFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistSonicShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistSonic,
        buffName: SonicShieldBuffName,
        buffGuid: Guids.SonicResist10ShieldBuff,
        abilityName: SonicShieldAbilityName,
        abilityGuid: Guids.SonicResist10ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: Description,
        //icon: ??,
        featureName: EnergyResistanceName,
        Guids.EnergyResist10,
        featureRanks: EnhancementCost,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
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

    private const string ImprovedDisplayName = "LegendaryArmor.EnergyResistance.Improved.Name";
    private const string ImprovedDescription = "LegendaryArmor.EnergyResistance.Improved.Description";
    private const int ImprovedEnhancementCost = 3;

    internal static BlueprintFeature ConfigureImproved()
    {
      Logger.Log($"Configuring Energy Resistance 20");

      var resistAcidFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.AcidResistance20Feature, EnhancementType.Armor, ImprovedEnhancementCost);
      var resistAcid = EnchantmentTool.CreateEnchantAbility(
        buffName: AcidImprovedBuffName,
        buffGuid: Guids.AcidResist20Buff,
        displayName: AcidImprovedDisplayName,
        description: AcidImprovedDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: AcidImprovedAbilityName,
        abilityGuid: Guids.AcidResist20Ability,
        buffComponents: resistAcidFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistAcidShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistAcid,
        buffName: AcidImprovedShieldBuffName,
        buffGuid: Guids.AcidResist20ShieldBuff,
        abilityName: AcidImprovedShieldAbilityName,
        abilityGuid: Guids.AcidResist20ShieldAbility);

      var resistColdFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ColdResistance20Feature, EnhancementType.Armor, ImprovedEnhancementCost);
      var resistCold = EnchantmentTool.CreateEnchantAbility(
        buffName: ColdImprovedBuffName,
        buffGuid: Guids.ColdResist20Buff,
        displayName: ColdImprovedDisplayName,
        description: ColdImprovedDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: ColdImprovedAbilityName,
        abilityGuid: Guids.ColdResist20Ability,
        buffComponents: resistColdFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistColdShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistCold,
        buffName: ColdImprovedShieldBuffName,
        buffGuid: Guids.ColdResist20ShieldBuff,
        abilityName: ColdImprovedShieldAbilityName,
        abilityGuid: Guids.ColdResist20ShieldAbility);

      var resistElectricityFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ElectricityResistance20Feature, EnhancementType.Armor, ImprovedEnhancementCost);
      var resistElectricity = EnchantmentTool.CreateEnchantAbility(
        buffName: ElectricityImprovedBuffName,
        buffGuid: Guids.ElectricityResist20Buff,
        displayName: ElectricityImprovedDisplayName,
        description: ElectricityImprovedDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: ElectricityImprovedAbilityName,
        abilityGuid: Guids.ElectricityResist20Ability,
        buffComponents: resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistElectricityShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistElectricity,
        buffName: ElectricityImprovedShieldBuffName,
        buffGuid: Guids.ElectricityResist20ShieldBuff,
        abilityName: ElectricityImprovedShieldAbilityName,
        abilityGuid: Guids.ElectricityResist20ShieldAbility);

      var resistFireFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.FireResistance20Feature, EnhancementType.Armor, ImprovedEnhancementCost);
      var resistFire = EnchantmentTool.CreateEnchantAbility(
        buffName: FireImprovedBuffName,
        buffGuid: Guids.FireResist20Buff,
        displayName: FireImprovedDisplayName,
        description: FireImprovedDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: FireImprovedAbilityName,
        abilityGuid: Guids.FireResist20Ability,
        buffComponents: resistFireFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistFireShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistFire,
        buffName: FireImprovedShieldBuffName,
        buffGuid: Guids.FireResist20ShieldBuff,
        abilityName: FireImprovedShieldAbilityName,
        abilityGuid: Guids.FireResist20ShieldAbility);

      // Sonic doesn't have a +20 enhcant for some reason
      var resistSonicFeature = FeatureRefs.SonicResistance20.Reference.Get();
      var resistSonic = EnchantmentTool.CreateEnchantAbility(
        buffName: SonicImprovedBuffName,
        buffGuid: Guids.SonicResist20Buff,
        displayName: SonicImprovedDisplayName,
        description: SonicImprovedDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: ImprovedEnhancementCost,
        abilityName: SonicImprovedAbilityName,
        abilityGuid: Guids.SonicResist20Ability,
        buffComponents: resistSonicFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistSonicShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistSonic,
        buffName: SonicImprovedShieldBuffName,
        buffGuid: Guids.SonicResist20ShieldBuff,
        abilityName: SonicImprovedShieldAbilityName,
        abilityGuid: Guids.SonicResist20ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: ImprovedDisplayName,
        description: ImprovedDescription,
        //icon: ??,
        featureName: ImprovedEnergyResistanceName,
        Guids.EnergyResist20,
        featureRanks: ImprovedEnhancementCost - EnhancementCost,
        prerequisiteFeature: Guids.EnergyResist10,
        prerequisiteRanks: EnhancementCost,
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

    private const string GreaterDisplayName = "LegendaryArmor.EnergyResistance.Greater.Name";
    private const string GreaterDescription = "LegendaryArmor.EnergyResistance.Greater.Description";
    private const int GreaterEnhancementCost = 4;

    internal static BlueprintFeature ConfigureGreater()
    {
      Logger.Log($"Configuring Energy Resistance 30");

      var resistAcidFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.AcidResistance30Feature, EnhancementType.Armor, GreaterEnhancementCost);
      var resistAcid = EnchantmentTool.CreateEnchantAbility(
        buffName: AcidGreaterBuffName,
        buffGuid: Guids.AcidResist30Buff,
        displayName: AcidGreaterDisplayName,
        description: AcidGreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: AcidGreaterAbilityName,
        abilityGuid: Guids.AcidResist30Ability,
        buffComponents: resistAcidFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistAcidShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistAcid,
        buffName: AcidGreaterShieldBuffName,
        buffGuid: Guids.AcidResist30ShieldBuff,
        abilityName: AcidGreaterShieldAbilityName,
        abilityGuid: Guids.AcidResist30ShieldAbility);

      var resistColdFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ColdResistance30Feature, EnhancementType.Armor, GreaterEnhancementCost);
      var resistCold = EnchantmentTool.CreateEnchantAbility(
        buffName: ColdGreaterBuffName,
        buffGuid: Guids.ColdResist30Buff,
        displayName: ColdGreaterDisplayName,
        description: ColdGreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: ColdGreaterAbilityName,
        abilityGuid: Guids.ColdResist30Ability,
        buffComponents: resistColdFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistColdShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistCold,
        buffName: ColdGreaterShieldBuffName,
        buffGuid: Guids.ColdResist30ShieldBuff,
        abilityName: ColdGreaterShieldAbilityName,
        abilityGuid: Guids.ColdResist30ShieldAbility);

      var resistElectricityFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.ElectricityResistance30Feature, EnhancementType.Armor, GreaterEnhancementCost);
      var resistElectricity = EnchantmentTool.CreateEnchantAbility(
        buffName: ElectricityGreaterBuffName,
        buffGuid: Guids.ElectricityResist30Buff,
        displayName: ElectricityGreaterDisplayName,
        description: ElectricityGreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: ElectricityGreaterAbilityName,
        abilityGuid: Guids.ElectricityResist30Ability,
        buffComponents: resistElectricityFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistElectricityShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistElectricity,
        buffName: ElectricityGreaterShieldBuffName,
        buffGuid: Guids.ElectricityResist30ShieldBuff,
        abilityName: ElectricityGreaterShieldAbilityName,
        abilityGuid: Guids.ElectricityResist30ShieldAbility);

      var resistFireFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.FireResistance30Feature, EnhancementType.Armor, GreaterEnhancementCost);
      var resistFire = EnchantmentTool.CreateEnchantAbility(
        buffName: FireGreaterBuffName,
        buffGuid: Guids.FireResist30Buff,
        displayName: FireGreaterDisplayName,
        description: FireGreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: FireGreaterAbilityName,
        abilityGuid: Guids.FireResist30Ability,
        buffComponents: resistFireFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistFireShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistFire,
        buffName: FireGreaterShieldBuffName,
        buffGuid: Guids.FireResist30ShieldBuff,
        abilityName: FireGreaterShieldAbilityName,
        abilityGuid: Guids.FireResist30ShieldAbility);

      var resistSonicFeature =
        EnchantmentTool.AddEnhancementEquivalence(
          FeatureRefs.SonicResistance30Feature, EnhancementType.Armor, GreaterEnhancementCost);
      var resistSonic = EnchantmentTool.CreateEnchantAbility(
        buffName: SonicGreaterBuffName,
        buffGuid: Guids.SonicResist30Buff,
        displayName: SonicGreaterDisplayName,
        description: SonicGreaterDescription,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: GreaterEnhancementCost,
        abilityName: SonicGreaterAbilityName,
        abilityGuid: Guids.SonicResist30Ability,
        buffComponents: resistSonicFeature.GetComponent<AddDamageResistanceEnergy>());
      var resistSonicShield = EnchantmentTool.CreateEnchantShieldVariant(
        resistSonic,
        buffName: SonicGreaterShieldBuffName,
        buffGuid: Guids.SonicResist30ShieldBuff,
        abilityName: SonicGreaterShieldAbilityName,
        abilityGuid: Guids.SonicResist30ShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: GreaterDisplayName,
        description: GreaterDescription,
        //icon: ??,
        featureName: GreaterEnergyResistanceName,
        Guids.EnergyResist30,
        featureRanks: GreaterEnhancementCost - ImprovedEnhancementCost,
        prerequisiteFeature: Guids.EnergyResist20,
        prerequisiteRanks: ImprovedEnhancementCost,
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
