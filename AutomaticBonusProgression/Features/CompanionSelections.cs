using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
using System.Linq;

namespace AutomaticBonusProgression.Features
{
  // TODO: Honestly I don't think this works yet jesus fucking christ.
  // I think we need to do all this shit in a patch or I'm gonna lose my mind.
  // Actual note is that until AddClassLevels triggers we still have this problem and I fucking hate it. Need to
  // test and see if the logic for determining levels actually works... I still don't understand how the fucking AddClassLevels does it.
  internal class CompanionSelections
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(CompanionSelections));

    internal static void Configure()
    {
      Logger.Log("Configuring companions");

      // Arueshalae
      ApplyToCompanion(FeatureRefs.Arueshalae_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Constitution,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Constitution,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (4)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.IntProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (14)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryIntelligence, Guids.LegendaryIntelligence,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Arueshalae Evil
      ApplyToCompanion(FeatureRefs.ArueshalaeEvil_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Constitution,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Constitution,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (4)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.IntProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (14)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryIntelligence, Guids.LegendaryIntelligence,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Camellia
      ApplyToCompanion(FeatureRefs.Camelia_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Constitution,
        physical16: StatType.Dexterity,
        physical17: StatType.Constitution,
        physical18: StatType.Constitution,
        mental6: StatType.Wisdom,
        mental11: StatType.Wisdom,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (2)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.IntProwess,
        // Shieldmaster (1)
        Guids.LegendaryShieldmaster,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        // Ability Scores (10)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryCharisma,
        Guids.LegendaryStrength);

      // Ciar
      ApplyToCompanion(FeatureRefs.Ciar_FeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Dexterity,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Wisdom,
        // Shieldmaster (1)
        Guids.LegendaryShieldmaster,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        // Ability Scores (12)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryIntelligence);
      ApplyToCompanion(FeatureRefs.Ciar_FeatureList_DLC1,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Dexterity,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Wisdom,
        // Shieldmaster (1)
        Guids.LegendaryShieldmaster,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        // Ability Scores (12)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryIntelligence);

      // Daeran
      ApplyToCompanion(FeatureRefs.DaeranPregenTestFeature,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Constitution,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Constitution,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Intelligence,
        mental18: StatType.Wisdom,
        // Prowess (2)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (16)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Delamere
      ApplyToCompanion(FeatureRefs.Delamere_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Strength,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Strength,
        mental6: StatType.Charisma,
        mental11: StatType.Wisdom,
        mental13: StatType.Charisma,
        mental15: StatType.Wisdom,
        mental17: StatType.Intelligence,
        mental18: StatType.Charisma,
        // Prowess (1)
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (17)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryIntelligence);

      // Ember
      ApplyToCompanion(FeatureRefs.Ember_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Constitution,
        physical13: StatType.Dexterity,
        physical16: StatType.Constitution,
        physical17: StatType.Dexterity,
        physical18: StatType.Strength,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Intelligence,
        mental18: StatType.Wisdom,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.StrProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        Guids.LegendaryMentalProwess, Guids.IntProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Greybor
      ApplyToCompanion(FeatureRefs.GreyborPregenTestFeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Dexterity,
        physical13: StatType.Strength,
        physical16: StatType.Constitution,
        physical17: StatType.Strength,
        physical18: StatType.Dexterity,
        mental6: StatType.Intelligence,
        mental11: StatType.Wisdom,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        // Twin weapons (1)
        Guids.LegendaryTwinWeapons,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        // Ability Scores (9)
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity);

      // Kestoglyr
      ApplyToCompanion(FeatureRefs.Kestoglyr_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Strength,
        mental6: StatType.Charisma,
        mental11: StatType.Wisdom,
        mental13: StatType.Charisma,
        mental15: StatType.Wisdom,
        mental17: StatType.Intelligence,
        mental18: StatType.Charisma,
        // Prowess (2)
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Twin weapons (1)
        Guids.LegendaryTwinWeapons,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        // Ability Scores (10)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma);

      // Lann
      ApplyToCompanion(FeatureRefs.Lann_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Constitution,
        physical17: StatType.Strength,
        physical18: StatType.Dexterity,
        mental6: StatType.Wisdom,
        mental11: StatType.Wisdom,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Intelligence,
        mental18: StatType.Intelligence,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.StrProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryIntelligence);

      // Nenio
      ApplyToCompanion(FeatureRefs.Nenio_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Constitution,
        physical13: StatType.Dexterity,
        physical16: StatType.Constitution,
        physical17: StatType.Dexterity,
        physical18: StatType.Constitution,
        mental6: StatType.Intelligence,
        mental11: StatType.Intelligence,
        mental13: StatType.Wisdom,
        mental15: StatType.Intelligence,
        mental17: StatType.Charisma,
        mental18: StatType.Wisdom,
        // Prowess (1)
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (17)
        Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryStrength);

      // Queen Galfrey
      ApplyToCompanion(FeatureRefs.Galfrey_FeatureList_0,
        physical7: StatType.Dexterity,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Strength,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Intelligence,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Intelligence,
        // Prowess (2)
        Guids.LegendaryMentalProwess, Guids.IntProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (16)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence);

      // Regill
      ApplyToCompanion(FeatureRefs.Regill_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Constitution,
        physical13: StatType.Dexterity,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Constitution,
        mental6: StatType.Charisma,
        mental11: StatType.Wisdom,
        mental13: StatType.Charisma,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (2)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Twin Weapons (1)
        Guids.LegendaryTwinWeapons,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        // Ability Scores (10)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution,
        Guids.LegendaryWisdom,
        Guids.LegendaryStrength);

      // Seelah
      ApplyToCompanion(FeatureRefs.Seelah_FeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Constitution,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Constitution,
        mental6: StatType.Charisma,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Wisdom,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        // Shieldmaster (1)
        Guids.LegendaryShieldmaster,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        // Ability Scores (9)
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryWisdom);

      // Sosiel
      ApplyToCompanion(FeatureRefs.SosielVaenic_FeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Constitution,
        physical16: StatType.Strength,
        physical17: StatType.Dexterity,
        physical18: StatType.Constitution,
        mental6: StatType.Wisdom,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Charisma,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution);

      // Staunton Vhane
      ApplyToCompanion(FeatureRefs.Staunton_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Strength,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Strength,
        mental6: StatType.Wisdom,
        mental11: StatType.Charisma,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Charisma,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (18)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma, Guids.LegendaryCharisma,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Wenduag
      ApplyToCompanion(FeatureRefs.Wenduag_FeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Strength,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Strength,
        mental6: StatType.Wisdom,
        mental11: StatType.Intelligence,
        mental13: StatType.Wisdom,
        mental15: StatType.Intelligence,
        mental17: StatType.Wisdom,
        mental18: StatType.Intelligence,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution);

      // Woljif
      ApplyToCompanion(FeatureRefs.WoljifPregenTestFeatureList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Constitution,
        physical16: StatType.Dexterity,
        physical17: StatType.Strength,
        physical18: StatType.Constitution,
        mental6: StatType.Intelligence,
        mental11: StatType.Wisdom,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Charisma,
        mental18: StatType.Intelligence,
        // Prowess (2)
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.WisProwess,
        // Twin Weapons (1)
        Guids.LegendaryTwinWeapons,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryOffHand,
        // Ability Scores (10)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence, Guids.LegendaryIntelligence,
        Guids.LegendaryConstitution);

      // Ulbrig
      ApplyToCompanion(FeatureRefs.Shifter_FeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Constitution,
        physical18: StatType.Dexterity,
        mental6: StatType.Wisdom,
        mental11: StatType.Wisdom,
        mental13: StatType.Intelligence,
        mental15: StatType.Wisdom,
        mental17: StatType.Intelligence,
        mental18: StatType.Charisma,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom, Guids.LegendaryWisdom,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution);

      // Rekarth - DLC
      // Rekarth uses the same feature list shared by NPCs, so create a new one
      var rekarthList = FeatureConfigurator.New("Rekarth.ABP.FeatureList", Guids.Rekarth_FeatureList)
        .CopyFrom(FeatureRefs.Scout_FeatureList, c => true)
        .Configure();
      UnitConfigurator.For(UnitRefs.Rekarth_companion).SetAddFacts(rekarthList).Configure();
      ApplyToCompanion(rekarthList,
        physical7: StatType.Dexterity,
        physical12: StatType.Dexterity,
        physical13: StatType.Strength,
        physical16: StatType.Dexterity,
        physical17: StatType.Constitution,
        physical18: StatType.Strength,
        mental6: StatType.Wisdom,
        mental11: StatType.Intelligence,
        mental13: StatType.Wisdom,
        mental15: StatType.Intelligence,
        mental17: StatType.Charisma,
        mental18: StatType.Wisdom,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.StrProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryMentalProwess, Guids.ConProwess,
        // Armor / Weapons (10)
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        Guids.LegendaryArmor, Guids.LegendaryWeapon,
        // Ability Scores (15)
        Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution, Guids.LegendaryConstitution,
        Guids.LegendaryWisdom, Guids.LegendaryWisdom);

      // Trever - DLC
      ApplyToCompanion(FeatureRefs.Trever_FeatureList,
        physical7: StatType.Strength,
        physical12: StatType.Strength,
        physical13: StatType.Dexterity,
        physical16: StatType.Strength,
        physical17: StatType.Constitution,
        physical18: StatType.Dexterity,
        mental6: StatType.Wisdom,
        mental11: StatType.Intelligence,
        mental13: StatType.Wisdom,
        mental15: StatType.Charisma,
        mental17: StatType.Wisdom,
        mental18: StatType.Intelligence,
        // Prowess (3)
        Guids.LegendaryPhysicalProwess, Guids.DexProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        Guids.LegendaryPhysicalProwess, Guids.ConProwess,
        // Shieldmaster (1)
        Guids.LegendaryShieldmaster,
        // Armor / Weapons (15)
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        Guids.LegendaryArmor, Guids.LegendaryWeapon, Guids.LegendaryShield,
        // Ability Scores (9)
        Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength, Guids.LegendaryStrength,
        Guids.LegendaryDexterity, Guids.LegendaryDexterity,
        Guids.LegendaryConstitution, Guids.LegendaryConstitution);
    }

    private static void ApplyToCompanion(
      Blueprint<BlueprintReference<BlueprintFeature>> featureList,
      StatType physical7,
      StatType physical12,
      StatType physical13,
      StatType physical16,
      StatType physical17,
      StatType physical18,
      StatType mental6,
      StatType mental11,
      StatType mental13,
      StatType mental15,
      StatType mental17,
      StatType mental18,
      params Blueprint<BlueprintFeatureReference>[] legendaryGifts)
    {
      FeatureConfigurator.For(featureList)
        .AddComponent(
          new AddABPSelections(
            physicalProwess:
              new()
              {
                physical7,
                physical12,
                physical13,
                physical16,
                physical17,
                physical18,
              },
            mentalProwess:
              new()
              {
                mental6,
                mental11,
                mental13,
                mental15,
                mental17,
                mental18,
              },
            legendaryGifts: legendaryGifts.Select(bp => bp.Reference).ToList()))
        .Configure();
    }
  }
}
