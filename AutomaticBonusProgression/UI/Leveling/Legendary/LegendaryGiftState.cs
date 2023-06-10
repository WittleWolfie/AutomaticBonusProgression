using AutomaticBonusProgression.Util;
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
        Logger.Verbose(() => $"Removed Prowess {type}");
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

      return type switch
      {
        StatType.Strength => !IsMaxRank(Common.StrProwess),
        StatType.Dexterity => !IsMaxRank(Common.DexProwess),
        StatType.Constitution => !IsMaxRank(Common.ConProwess),
        StatType.Intelligence => !IsMaxRank(Common.IntProwess),
        StatType.Wisdom => !IsMaxRank(Common.WisProwess),
        StatType.Charisma => !IsMaxRank(Common.ChaProwess),
        _ => throw new NotImplementedException(),
      };
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
        Logger.Verbose(() => $"Removed Legenary Enchantment {type}");
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
        AvailableGifts.Value++;
        Controller.UpdatePreview();
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

      return IsFeatureSelected(feature) || !Controller.Unit.HasFact(feature);
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
