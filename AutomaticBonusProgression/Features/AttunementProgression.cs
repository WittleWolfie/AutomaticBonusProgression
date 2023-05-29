using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Microsoft.Build.Utilities;
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

      // TODO: Grant to existing characters! Do this by adjusting their *_FeatureList selections
      // TODO: Also make sure to grant to recruited homies
      // TODO: Grant to PETS

      var basicFeats = ProgressionRefs.BasicFeatsProgression.Reference.Get();

      var armorAttunement = ArmorAttunement.ConfigureArmor();
      var shieldAttunement = ArmorAttunement.ConfigureShield();

      var weaponAttunement = WeaponAttunement.ConfigureWeapon();
      var offHandAttunement = WeaponAttunement.ConfigureOffHand();

      var deflection = Deflection.Configure();
      var toughening = Toughening.Configure();
      var resistance = Resistance.Configure();

      var mentalProwess = MentalProwess.Configure();
      var mentalProwessSecondary = MentalProwess.ConfigureSecondary();
      var mentalProwessTertiary = MentalProwess.ConfigureTertiary();
      var mentalProwessAny = MentalProwess.ConfigureAny();

      var physicalProwessPrimary = PhysicalProwess.ConfigurePrimary();
      var physicalProwessSecondary = PhysicalProwess.ConfigureSecondary();
      var physicalProwessTertiary = PhysicalProwess.ConfigureTertiary();
      var physicalProwessAny = PhysicalProwess.ConfigureAny();

      // Here for testing only, eventually should be mythic (though maybe level 19/20?
      var legendaryGifts = LegendaryGifts.Configure();

      ProgressionConfigurator.For(basicFeats)
        .AddToLevelEntry(level: 1, ConfigureEnhancementCalculator())
        .AddToLevelEntry(level: 3, resistance)
        .AddToLevelEntry(level: 4, armorAttunement, weaponAttunement)
        .AddToLevelEntry(level: 5, deflection, legendaryGifts, legendaryGifts, legendaryGifts, legendaryGifts, legendaryGifts)
        .AddToLevelEntry(level: 6, mentalProwess, legendaryGifts, legendaryGifts)
        .AddToLevelEntry(level: 7, physicalProwessPrimary, legendaryGifts, legendaryGifts)
        .AddToLevelEntry(level: 8, shieldAttunement, offHandAttunement, toughening, resistance)
        .AddToLevelEntry(level: 9, armorAttunement, weaponAttunement)
        .AddToLevelEntry(level: 10, deflection, resistance)
        .AddToLevelEntry(level: 11, mentalProwess)
        .AddToLevelEntry(level: 13, toughening, resistance, mentalProwess, physicalProwessSecondary)
        .AddToLevelEntry(level: 14, armorAttunement, shieldAttunement, weaponAttunement, offHandAttunement, resistance)
        .AddToLevelEntry(level: 15, armorAttunement, shieldAttunement, weaponAttunement, offHandAttunement, mentalProwess)
        .AddToLevelEntry(level: 16, deflection, toughening, physicalProwessAny)
        .AddToLevelEntry(level: 17, armorAttunement, shieldAttunement, weaponAttunement, offHandAttunement, deflection, toughening, mentalProwess, physicalProwessTertiary)
        .AddToLevelEntry(level: 18, deflection, toughening, mentalProwess, physicalProwessAny)
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
