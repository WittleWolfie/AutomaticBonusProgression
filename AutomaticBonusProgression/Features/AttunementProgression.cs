using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace AutomaticBonusProgression.Features
{
  internal class AttunementProgression
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementProgression));

    private const string EnhancementCalculator = "EnhancementCalculator";

    internal static void Configure()
    {
      Logger.Log("Configuring bonus progression");

      var basicFeats = ProgressionRefs.BasicFeatsProgression.Reference.Get();

      var armorAttunement = ArmorAttunement.ConfigureArmor();
      var shieldAttunement = ArmorAttunement.ConfigureShield();

      var weaponAttunement = WeaponAttunement.ConfigureWeapon();
      var offHandAttunement = WeaponAttunement.ConfigureOffHand();

      var deflection = Deflection.Configure();
      var toughening = Toughening.Configure();
      var resistance = Resistance.Configure();

      var mentalProwess = MentalProwess.Configure();
      var physicalProwess = PhysicalProwess.Configure();

      ProgressionConfigurator.For(basicFeats)
        .AddToLevelEntry(level: 1, ConfigureEnhancementCalculator())
        .AddToLevelEntry(level: 3, resistance)
        .AddToLevelEntry(
          level: 4,
          armorAttunement,
          weaponAttunement)
        .AddToLevelEntry(level: 5, deflection)
        .AddToLevelEntry(level: 6, mentalProwess)
        .AddToLevelEntry(level: 7, physicalProwess)
        .AddToLevelEntry(
          level: 8,
          offHandAttunement,
          resistance,
          shieldAttunement,
          toughening)
        .AddToLevelEntry(
          level: 9,
          armorAttunement,
          weaponAttunement)
        .AddToLevelEntry(
          level: 10,
          deflection,
          resistance)
        .AddToLevelEntry(level: 11, mentalProwess)
        .AddToLevelEntry(level: 12, physicalProwess)
        .AddToLevelEntry(
          level: 13,
          mentalProwess,
          physicalProwess,
          resistance,
          toughening)
        .AddToLevelEntry(
          level: 14,
          armorAttunement,
          offHandAttunement,
          resistance,
          shieldAttunement,
          weaponAttunement)
        .AddToLevelEntry(
          level: 15,
          armorAttunement,
          mentalProwess,
          offHandAttunement,
          shieldAttunement,
          weaponAttunement)
        .AddToLevelEntry(
          level: 16,
          deflection,
          physicalProwess,
          toughening)
        .AddToLevelEntry(
          level: 17,
          armorAttunement,
          shieldAttunement,
          weaponAttunement,
          offHandAttunement,
          deflection,
          toughening,
          mentalProwess,
          physicalProwess)
        .AddToLevelEntry(
          level: 18,
          deflection,
          toughening,
          mentalProwess,
          physicalProwess)
        .Configure();
    }

    private static BlueprintFeature ConfigureEnhancementCalculator()
    {
      Logger.Log("Configuring enhancement calculator");

      return FeatureConfigurator.New(EnhancementCalculator, Guids.EnhancementCalculator)
        .SetIsClassFeature()
        .SetHideInUI()
        .AddComponent<EnhancementBonusCalculator>()
        .AddHideFeatureInInspect()
        .Configure();
    }

    // TODO: Test
    // - Make sure recruited companions have it
    // - Test whether respec applies it correctly (in-game respec, not Barley)
    private static void ApplyToCompanions()
    {
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
      StatType mental18)
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
              }))
        .Configure();
    }

    [TypeId("55b09ee7-cb57-4a50-847d-85c32dea4b29")]
    internal class EnhancementBonusCalculator : UnitFactComponentDelegate
    {
      #region Armor/Shield
      private static BlueprintFeature _legendaryShieldmaster;
      private static BlueprintFeature LegendaryShieldmaster
      {
        get
        {
          _legendaryShieldmaster ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryShieldmaster);
          return _legendaryShieldmaster;
        }
      }

      internal int GetEnhancementBonus(ItemEntityShield shield)
      {
        var tempBonus = GetTempArmorBonus(shield);
        var attunement = GetShieldAttunement(shield.Wielder);

        Logger.Verbose(() => $"Shield Enhancement bonus for {shield.Blueprint.Type.DefaultName}: {attunement} + {tempBonus}");
        return Math.Min(5, attunement + tempBonus);
      }

      internal int GetEnhancementBonus(ItemEntityArmor armor)
      {
        if (armor.Shield is not null)
          return GetEnhancementBonus(armor.Shield);

        var tempBonus = GetTempArmorBonus(armor);
        var attunement = GetArmorAttunement(armor.Wielder);

        Logger.Verbose(() => $"Armor Enhancement bonus for {armor.Blueprint.Type.DefaultName}: {attunement} + {tempBonus}");
        return Math.Min(5, attunement + tempBonus);
      }

      private int GetTempArmorBonus(ItemEntity armor)
      {
        int tempBonus = 0;
        foreach (var enchantment in armor.Enchantments)
        {
          if (enchantment.GetComponent<MagicItem>() is not null)
            continue; // Skip these which represent the basic enchantments replaced by the mod

          var bonus = enchantment.GetComponent<ArmorEnhancementBonus>();
          if (bonus is not null && bonus.EnhancementValue > tempBonus)
            tempBonus = bonus.EnhancementValue;
        }

        return tempBonus;
      }

      private int GetShieldAttunement(UnitDescriptor unit)
      {
        // If they have legendary shieldmaster then armor attunement == shield attunement
        if (unit.HasFact(LegendaryShieldmaster))
          return GetArmorAttunement(unit);

        var attunement = unit.GetFact(Common.ShieldAttunement);
        if (attunement is not null)
          return attunement.GetRank();
        return 0;
      }

      private int GetArmorAttunement(UnitDescriptor unit)
      {
        var attunement = unit.GetFact(Common.ArmorAttunement);
        if (attunement is null)
          return 0;

        var rank = attunement.GetRank();
        if (unit.Body.SecondaryHand.MaybeShield is null || unit.HasFact(LegendaryShieldmaster))
          return rank;

        return Math.Max(1, rank - 1); // If you have a shield the armor bonus is 1 lower (min 1)
      }
      #endregion

      #region Weapons
      private static BlueprintFeature _legendaryTwinWeapons;
      private static BlueprintFeature LegendaryTwinWeapons
      {
        get
        {
          _legendaryTwinWeapons ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryTwinWeapons);
          return _legendaryTwinWeapons;
        }
      }

      internal int GetEnhancementBonus(ItemEntityWeapon weapon)
      {
        int tempBonus = 0;
        int stackingBonus = 0;
        foreach (var enchantment in weapon.Enchantments)
        {
          if (enchantment.GetComponent<MagicItem>() is not null)
            continue; // Skip these which represent the basic enchantments replaced by the mod

          var bonus = enchantment.GetComponent<WeaponEnhancementBonus>();
          if (bonus is not null && bonus.Stack)
          {
            if (bonus.Stack)
              stackingBonus += bonus.EnhancementBonus;
            else if (bonus.EnhancementBonus > tempBonus)
              tempBonus = bonus.EnhancementBonus;
          }
        }
        tempBonus += stackingBonus;

        var wielder = weapon.Wielder;
        var attunement =
          Common.IsPrimaryWeapon(weapon)
            ? GetWeaponAttunement(wielder)
            : GetOffHandAttunement(wielder);

        Logger.Verbose(() => $"Weapon Enhancement bonus for {weapon.Blueprint.Type.DefaultName}: {attunement} + {tempBonus}");
        return Math.Min(5, attunement + tempBonus);
      }

      private int GetWeaponAttunement(UnitDescriptor unit)
      {
        var attunement = unit.GetFact(Common.WeaponAttunement);
        if (attunement is null)
          return 0;

        var rank = attunement.GetRank();
        var offHandWeapon = unit.Body.SecondaryHand.MaybeWeapon;
        if (offHandWeapon is null || Common.IsPrimaryWeapon(offHandWeapon) || unit.HasFact(LegendaryTwinWeapons))
          return rank;

        return Math.Max(1, rank - 1); // If you have an off-hand weapon the armor bonus is 1 lower (min 1)
      }

      private int GetOffHandAttunement(UnitDescriptor unit)
      {
        // If they have legendary twin weapons then weapon attunement == off-hand attunement
        if (unit.HasFact(LegendaryTwinWeapons))
          return GetWeaponAttunement(unit);

        var attunement = unit.GetFact(Common.OffHandAttunement);
        if (attunement is not null)
          return attunement.GetRank();
        return 0;
      }
      #endregion
    }
  }
}
