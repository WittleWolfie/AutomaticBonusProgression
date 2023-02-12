﻿using AutomaticBonusProgression.Components;
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

namespace AutomaticBonusProgression
{
  internal class AttunementProgression
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementProgression));

    private const string EnhancementCalculator = "EnhancementCalculator";

    internal static void Configure()
    {
      Logger.Log("Configuring bonus progression");

      // TODO: Grant to existing characters!
      // TODO: Also make sure to grant to recruited homies

      var basicFeats = ProgressionRefs.BasicFeatsProgression.Reference.Get();

      var armorAttunement = ArmorAttunement.ConfigureArmor();
      var shieldAttunement = ArmorAttunement.ConfigureShield();
      ProgressionConfigurator.For(basicFeats)
        .AddToLevelEntry(level: 1, ConfigureEnhancementCalculator())
        .AddToLevelEntry(level: 4, armorAttunement)
        .AddToLevelEntry(level: 8, shieldAttunement)
        .AddToLevelEntry(level: 9, armorAttunement)
        .AddToLevelEntry(level: 14, armorAttunement, shieldAttunement)
        .AddToLevelEntry(level: 15, armorAttunement, shieldAttunement)
        .AddToLevelEntry(level: 17, armorAttunement, shieldAttunement)
        .Configure();
    }

    private static BlueprintFeature ConfigureEnhancementCalculator()
    {
      Logger.Log("Configuring enhancement calculator");

      return FeatureConfigurator.New(EnhancementCalculator, Guids.EnhancementCalculator)
        .SetIsClassFeature()
        .SetHideInUI()
        .AddComponent<EnhancementBonusCalculator>()
        .Configure();
    }

    [TypeId("55b09ee7-cb57-4a50-847d-85c32dea4b29")]
    internal class EnhancementBonusCalculator : UnitFactComponentDelegate
    {
      private static BlueprintFeature _armorAttunement;
      private static BlueprintFeature ArmorAttunement
      {
        get
        {
          _armorAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ArmorAttunement);
          return _armorAttunement;
        }
      }
      private static BlueprintFeature _shieldAttunement;
      private static BlueprintFeature ShieldAttunement
      {
        get
        {
          _shieldAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ShieldAttunement);
          return _shieldAttunement;
        }
      }

      internal int GetEnhancementBonus(ItemEntityShield shield)
      {
        var tempBonus = GetTempArmorBonus(shield);
        var attunement = GetShieldAttunement(shield.Wielder);

        Logger.Verbose(() => $"Shield Armor Enhancement bonus: {attunement} + {tempBonus}");
        return attunement + tempBonus;
      }

      internal int GetEnhancementBonus(ItemEntityArmor armor)
      {
        var tempBonus = GetTempArmorBonus(armor);
        var attunement = GetArmorAttunement(armor.Wielder);

        Logger.Verbose(() => $"Armor Enhancement bonus: {attunement} + {tempBonus}");
        return attunement + tempBonus;
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
        var attunement = unit.GetFact(ShieldAttunement);
        if (attunement is null)
          return 0;

        return attunement.GetRank();
      }

      private int GetArmorAttunement(UnitDescriptor unit)
      {
        var attunement = unit.GetFact(ArmorAttunement);
        if (attunement is null)
          return 0;

        var rank = attunement.GetRank();
        if (unit.Body.SecondaryHand.MaybeShield is null)
          return rank;

        return Math.Min(1, rank - 1); // If you have a shield the armor bonus is 1 lower (min 1)
      }

      internal int GetEnhancementBonus(ItemEntityWeapon weapon)
      {
        int tempBonus = 0;
        foreach (var enchantment in weapon.Enchantments)
        {
          if (enchantment.GetComponent<MagicItem>() is not null)
            continue; // Skip these which represent the basic enchantments replaced by the mod

          var bonus = enchantment.GetComponent<WeaponEnhancementBonus>();
          if (bonus is not null && bonus.Stack)
            tempBonus += bonus.EnhancementBonus;
        }

        var attunement = weapon.Wielder.GetFact(ArmorAttunement);
        var finalBonus = tempBonus + (attunement is null ? 0 : attunement.GetRank());
        Logger.Verbose(() => $"Weapon Enhancement bonus: {finalBonus - tempBonus} + {tempBonus}");
        return finalBonus;
      }
    }
  }
}