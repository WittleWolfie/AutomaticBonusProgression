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
    private const string AcidDisplayName = "LegendaryArmor.EnergyResistance.Acid.Name";
    private const string AcidDescription = "LegendaryArmor.EnergyResistance.Acid.Description";

    private const string ColdBuffName = "LegendaryArmor.EnergyResistance.Buff.Cold";
    private const string ColdAbilityName = "LegendaryArmor.EnergyResistance.Ability.Cold";
    private const string ColdDisplayName = "LegendaryArmor.EnergyResistance.Cold.Name";
    private const string ColdDescription = "LegendaryArmor.EnergyResistance.Cold.Description";

    private const string ElectricityBuffName = "LegendaryArmor.EnergyResistance.Buff.Electricity";
    private const string ElectricityAbilityName = "LegendaryArmor.EnergyResistance.Ability.Electricity";
    private const string ElectricityDisplayName = "LegendaryArmor.EnergyResistance.Electricity.Name";
    private const string ElectricityDescription = "LegendaryArmor.EnergyResistance.Electricity.Description";

    private const string FireBuffName = "LegendaryArmor.EnergyResistance.Buff.Fire";
    private const string FireAbilityName = "LegendaryArmor.EnergyResistance.Ability.Fire";
    private const string FireDisplayName = "LegendaryArmor.EnergyResistance.Fire.Name";
    private const string FireDescription = "LegendaryArmor.EnergyResistance.Fire.Description";

    private const string SonicBuffName = "LegendaryArmor.EnergyResistance.Buff.Sonic";
    private const string SonicAbilityName = "LegendaryArmor.EnergyResistance.Ability.Sonic";
    private const string SonicDisplayName = "LegendaryArmor.EnergyResistance.Sonic.Name";
    private const string SonicDescription = "LegendaryArmor.EnergyResistance.Sonic.Description";

    private const string DisplayName = "LegendaryArmor.EnergyResistance.Name";
    private const string Description = "LegendaryArmor.EnergyResistance.Name";
    private const int EnhancementCost = 3;

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
        resistCold,
        resistElectricity,
        resistFire,
        resistSonic);
    }
  }
}
