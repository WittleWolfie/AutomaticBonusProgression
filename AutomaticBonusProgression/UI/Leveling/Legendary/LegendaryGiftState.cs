﻿using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using System;
using System.ComponentModel;
using System.Linq;
using UniRx;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Carries the leveling state for legendary gifts.
  /// </summary>
  internal class LegendaryGiftState
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftState));

    internal readonly IntReactiveProperty AvailableGifts = new();
    internal readonly LevelUpController Controller;

    internal LegendaryGiftState(LevelUpController controller, int gifts)
    {
      AvailableGifts.Value = gifts;
      Controller = controller;
    }

    #region Legendary Ability
    internal void TryAddLegendaryAbility(StatType type)
    {
      if (!CanAddLegendaryAbility(type))
        return;

      Logger.Verbose(() => $"Applying Legendary Ability {type}");
      Controller.AddAction(new SelectLegendaryAbility(type, this));
      AvailableGifts.Value--;
    }

    internal void TryRemoveLegendaryAbility(StatType type)
    {
      if (!CanRemoveLegendaryAbility(type))
        return;

      if (Controller.RemoveAction<SelectLegendaryAbility>(a => a.Attribute == type))
      {
        Logger.Verbose(() => $"Removed Legendary Ability {type}");
        AvailableGifts.Value++;
        Controller.UpdatePreview();
      }
    }

    internal bool CanAddLegendaryAbility(StatType type, bool checkGifts = true)
    {
      // CheckGifts is used so that `SelectLegendaryAbility` returns correctly. Without this, selecting a later option
      // will clear any legendary ability selections since it will be checked after gifts are spent.
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      return type switch
      {
        StatType.Strength => !IsMaxRank(Common.LegendaryStr),
        StatType.Dexterity => !IsMaxRank(Common.LegendaryDex),
        StatType.Constitution => !IsMaxRank(Common.LegendaryCon),
        StatType.Intelligence => !IsMaxRank(Common.LegendaryInt),
        StatType.Wisdom => !IsMaxRank(Common.LegendaryWis),
        StatType.Charisma => !IsMaxRank(Common.LegendaryCha),
        _ => throw new NotImplementedException(),
      };
    }

    internal bool CanRemoveLegendaryAbility(StatType type)
    {
      return Controller.LevelUpActions.OfType<SelectLegendaryAbility>().Any(a => a.Attribute == type);
    }
    #endregion

    #region Legendary Prowess
    internal void TrySelectProwess(StatType type)
    {
      if (!CanSelectProwess(type))
        return;

      Logger.Verbose(() => $"Applying Prowess {type}");
      switch (type)
      {
        case StatType.Strength:
        case StatType.Dexterity:
        case StatType.Constitution:
          Controller.AddAction(new SelectPhysicalProwess(type, isGift: true));
          break;
        case StatType.Intelligence:
        case StatType.Wisdom:
        case StatType.Charisma:
          Controller.AddAction(new SelectMentalProwess(type, isGift: true));
          break;
        default:
          throw new InvalidEnumArgumentException($"Prowess isn't supported for {type}");
      }
      AvailableGifts.Value--;
    }

    internal void TryUnselectProwess(StatType type)
    {
      var removedAction = false;
      switch (type)
      {
        case StatType.Strength:
        case StatType.Dexterity:
        case StatType.Constitution:
          removedAction = Controller.RemoveAction<SelectPhysicalProwess>(a => a.Attribute == type && a.IsGift);
          break;
        case StatType.Intelligence:
        case StatType.Wisdom:
        case StatType.Charisma:
          removedAction = Controller.RemoveAction<SelectMentalProwess>(a => a.Attribute == type && a.IsGift);
          break;
        default:
          throw new InvalidEnumArgumentException($"Prowess isn't supported for {type}");
      }

      if (removedAction)
      {
        Logger.Verbose(() => $"Removed Legendary Prowess {type}");
        AvailableGifts.Value++;
        Controller.UpdatePreview();
      }
    }

    internal bool IsProwessSelected(StatType type)
    {
      return Controller.LevelUpActions.OfType<SelectProwess>().Any(a => a.Attribute == type && a.IsGift);
    }

    internal bool CanSelectProwess(StatType type, bool checkGifts = true)
    {
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      var isMaxLegendaryRank = type switch
      {
        StatType.Strength => IsMaxRank(Common.LegendaryPhysicalProwess),
        StatType.Dexterity => IsMaxRank(Common.LegendaryPhysicalProwess),
        StatType.Constitution => IsMaxRank(Common.LegendaryPhysicalProwess),
        StatType.Intelligence => IsMaxRank(Common.LegendaryMentalProwess),
        StatType.Wisdom => IsMaxRank(Common.LegendaryMentalProwess),
        StatType.Charisma => IsMaxRank(Common.LegendaryMentalProwess),
        _ => throw new NotImplementedException(),
      };
      if (isMaxLegendaryRank)
      {
        Logger.Verbose(() => $"Legendary Prowess is max rank: {type}");
        return false;
      }

      var prowessRanks = type switch
      {
        StatType.Strength => GetRank(Common.StrProwess),
        StatType.Dexterity => GetRank(Common.DexProwess),
        StatType.Constitution => GetRank(Common.ConProwess),
        StatType.Intelligence => GetRank(Common.IntProwess),
        StatType.Wisdom => GetRank(Common.WisProwess),
        StatType.Charisma => GetRank(Common.ChaProwess),
        _ => throw new NotImplementedException(),
      };
      // Not using IsMaxRank since the logic is custom for handling +4 / +6 limits.
      if (prowessRanks >= 3)
      {
        Logger.Verbose(() => $"Legendary Prowess selected three times already: {type}");
        return false;
      }

      var isPhysical = type switch
      {
        StatType.Strength => true,
        StatType.Dexterity => true,
        StatType.Constitution => true,
        StatType.Intelligence => false,
        StatType.Wisdom => false,
        StatType.Charisma => false,
        _ => throw new NotImplementedException(),
      };

      var characterLevel = Controller.State.NextCharacterLevel;
      if (Controller.State.Mode == LevelUpState.CharBuildMode.Mythic)
        characterLevel = Controller.Unit.Progression.CharacterLevel;
      Logger.Verbose(() => $"Legendary Prowess Eligibility: {characterLevel} - {type} - {prowessRanks}");
      if (isPhysical)
      {
        if (characterLevel < 7)
          return false;
        if (characterLevel < 12)
          return prowessRanks < 1;
        if (characterLevel < 16)
          return prowessRanks < 2;
      }
      else
      {
        if (characterLevel < 6)
          return false;
        if (characterLevel < 11)
          return prowessRanks < 1;
        if (characterLevel < 15)
          return prowessRanks < 2;
      }

      return true;
    }
    #endregion

    #region Legendary Enchantment
    internal int GetMaxEnchantment(EnchantmentType type)
    {
      return type switch
      {
        EnchantmentType.Armor => GetRank(Common.LegendaryArmor),
        EnchantmentType.Shield => GetRank(Common.LegendaryShield),
        EnchantmentType.Weapon => GetRank(Common.LegendaryWeapon),
        EnchantmentType.OffHand => GetRank(Common.LegendaryOffHand),
        _ => throw new NotImplementedException(),
      };
    }

    internal void TryAddLegendaryEnchantment(EnchantmentType type)
    {
      if (!CanAddLegendaryEnchantment(type))
        return;

      Logger.Verbose(() => $"Adding Legendary Enchantment {type}");
      Controller.AddAction(new SelectLegendaryEnchantment(type, this));
      AvailableGifts.Value--;
    }

    internal void TryRemoveLegendaryEnchantment(EnchantmentType type)
    {
      if (!CanRemoveLegendaryEnchantment(type))
        return;

      if (Controller.RemoveAction<SelectLegendaryEnchantment>(a => a.Type == type))
      {
        Logger.Verbose(() => $"Removed Legendary Enchantment {type}");
        AvailableGifts.Value++;
        Controller.UpdatePreview();
      }
    }

    internal bool CanAddLegendaryEnchantment(EnchantmentType type, bool checkGifts = true)
    {
      // CheckGifts is used so that `SelectLegendaryEnchantment` returns correctly. Without this, selecting a later
      // option will clear any legendary ability selections since it will be checked after gifts are spent.
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      return type switch
      {
        EnchantmentType.Armor => !IsMaxRank(Common.LegendaryArmor),
        EnchantmentType.Shield => !IsMaxRank(Common.LegendaryShield),
        EnchantmentType.Weapon => !IsMaxRank(Common.LegendaryWeapon),
        EnchantmentType.OffHand => !IsMaxRank(Common.LegendaryOffHand),
        _ => throw new NotImplementedException(),
      };
    }

    internal bool CanRemoveLegendaryEnchantment(EnchantmentType type)
    {
      return Controller.LevelUpActions.OfType<SelectLegendaryEnchantment>().Any(a => a.Type == type);
    }
    #endregion

    #region Legendary Features
    internal void TrySelectFeature(BlueprintFeature feature)
    {
      if (!CanSelectFeature(feature))
        return;

      Logger.Verbose(() => $"Adding Feature {feature.Name}");
      Controller.AddAction(new SelectLegendaryFeature(feature, this));
      AvailableGifts.Value--;
    }

    internal void TryUnselectFeature(BlueprintFeature feature)
    {
      if (Controller.RemoveAction<SelectLegendaryFeature>(a => a.Feature == feature))
      {
        Logger.Verbose(() => $"Removed Feature {feature.Name}");
        Controller.UpdatePreview();
        AvailableGifts.Value++;
      }
    }

    internal bool IsFeatureSelected(BlueprintFeature feature)
    {
      return Controller.LevelUpActions.OfType<SelectLegendaryFeature>().Any(a => a.Feature == feature);
    }

    internal bool CanSelectFeature(BlueprintFeature feature, bool checkGifts = true)
    {
      if (checkGifts && AvailableGifts.Value == 0)
        return false;

      return feature.MeetsPrerequisites(
          selectionState: null, unit: Controller.Preview, state: Controller.State, fromProgression: false);
    }
    #endregion

    private bool IsMaxRank(BlueprintFeature blueprint)
    {
      return GetRank(blueprint) >= blueprint.Ranks;
    }

    private int GetRank(BlueprintFeature blueprint)
    {
      var feature = Controller.Preview.GetFeature(blueprint);
      if (feature is null)
        return 0;
      return feature.Rank;
    }
  }

  internal enum EnchantmentType
  {
    Armor,
    Shield,
    Weapon,
    OffHand
  }
}
