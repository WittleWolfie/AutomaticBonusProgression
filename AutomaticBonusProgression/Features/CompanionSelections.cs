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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Wisdom);
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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Charisma);

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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Charisma);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Charisma);

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
        mental18: StatType.Charisma);

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
        mental18: StatType.Intelligence);

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
        mental18: StatType.Intelligence);

      // Rekarth - DLC
      // Rekarth uses the same feature list shared by NPCs, so create a new one
      var rekarthList = FeatureConfigurator.New("Rekarth.ABP.FeatureList", Guids.Rekarth_FeatureList)
        .CopyFrom(FeatureRefs.Scout_FeatureList)
        .Configure();
      UnitConfigurator.For(UnitRefs.Rekarth_companion)
        .EditComponent<AddFacts>(
          c => c.m_Facts = CommonTool.Append(c.m_Facts, rekarthList.ToReference<BlueprintUnitFactReference>()))
        .Configure();
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
        mental18: StatType.Wisdom);

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
        mental18: StatType.Intelligence);
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
