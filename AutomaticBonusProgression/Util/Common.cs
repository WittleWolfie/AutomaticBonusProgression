using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using System.Linq;

namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// Common utils
  /// </summary>
  internal static class Common
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Common));

    #region Blueprints
    private static BlueprintFeature _armorAttunement;
    internal static BlueprintFeature ArmorAttunement
    {
      get
      {
        _armorAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ArmorAttunement);
        return _armorAttunement;
      }
    }
    private static BlueprintFeature _armorAttunementBase;
    internal static BlueprintFeature ArmorAttunementBase
    {
      get
      {
        _armorAttunementBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.ArmorAttunementBase);
        return _armorAttunementBase;
      }
    }

    private static BlueprintFeature _shieldAttunement;
    internal static BlueprintFeature ShieldAttunement
    {
      get
      {
        _shieldAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ShieldAttunement);
        return _shieldAttunement;
      }
    }
    private static BlueprintFeature _shieldAttunementBase;
    internal static BlueprintFeature ShieldAttunementBase
    {
      get
      {
        _shieldAttunementBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.ShieldAttunementBase);
        return _shieldAttunementBase;
      }
    }

    private static BlueprintFeature _weaponAttunement;
    internal static BlueprintFeature WeaponAttunement
    {
      get
      {
        _weaponAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.WeaponAttunement);
        return _weaponAttunement;
      }
    }
    private static BlueprintFeature _weaponAttunementBase;
    internal static BlueprintFeature WeaponAttunementBase
    {
      get
      {
        _weaponAttunementBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.WeaponAttunementBase);
        return _weaponAttunementBase;
      }
    }

    private static BlueprintFeature _offHandAttunement;
    internal static BlueprintFeature OffHandAttunement
    {
      get
      {
        _offHandAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.OffHandAttunement);
        return _offHandAttunement;
      }
    }
    private static BlueprintFeature _offHandAttunementBase;
    internal static BlueprintFeature OffHandAttunementBase
    {
      get
      {
        _offHandAttunementBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.OffHandAttunementBase);
        return _offHandAttunementBase;
      }
    }

    private static BlueprintFeature _resistance;
    internal static BlueprintFeature Resistance
    {
      get
      {
        _resistance ??= BlueprintTool.Get<BlueprintFeature>(Guids.Resistance);
        return _resistance;
      }
    }
    private static BlueprintFeature _resistanceBase;
    internal static BlueprintFeature ResistanceBase
    {
      get
      {
        _resistanceBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.ResistanceBase);
        return _resistanceBase;
      }
    }

    private static BlueprintFeature _deflection;
    internal static BlueprintFeature Deflection
    {
      get
      {
        _deflection ??= BlueprintTool.Get<BlueprintFeature>(Guids.Deflection);
        return _deflection;
      }
    }
    private static BlueprintFeature _deflectionBase;
    internal static BlueprintFeature DeflectionBase
    {
      get
      {
        _deflectionBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.DeflectionBase);
        return _deflectionBase;
      }
    }

    private static BlueprintFeature _toughening;
    internal static BlueprintFeature Toughening
    {
      get
      {
        _toughening ??= BlueprintTool.Get<BlueprintFeature>(Guids.Toughening);
        return _toughening;
      }
    }
    private static BlueprintFeature _tougheningBase;
    internal static BlueprintFeature TougheningBase
    {
      get
      {
        _tougheningBase ??= BlueprintTool.Get<BlueprintFeature>(Guids.TougheningBase);
        return _tougheningBase;
      }
    }

    #region Prowess
    private static BlueprintFeature _physicalProwess;
    internal static BlueprintFeature PhysicalProwess
    {
      get
      {
        _physicalProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.PhysicalProwess);
        return _physicalProwess;
      }
    }
    private static BlueprintFeature _mentalProwess;
    internal static BlueprintFeature MentalProwess
    {
      get
      {
        _mentalProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.MentalProwess);
        return _mentalProwess;
      }
    }

    private static BlueprintFeature _strProwess;
    internal static BlueprintFeature StrProwess
    {
      get
      {
        _strProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.StrProwess);
        return _strProwess;
      }
    }
    private static BlueprintFeature _dexProwess;
    internal static BlueprintFeature DexProwess
    {
      get
      {
        _dexProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.DexProwess);
        return _dexProwess;
      }
    }
    private static BlueprintFeature _conProwess;
    internal static BlueprintFeature ConProwess
    {
      get
      {
        _conProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.ConProwess);
        return _conProwess;
      }
    }

    private static BlueprintFeature _intProwess;
    internal static BlueprintFeature IntProwess
    {
      get
      {
        _intProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.IntProwess);
        return _intProwess;
      }
    }
    private static BlueprintFeature _wisProwess;
    internal static BlueprintFeature WisProwess
    {
      get
      {
        _wisProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.WisProwess);
        return _wisProwess;
      }
    }
    private static BlueprintFeature _chaProwess;
    internal static BlueprintFeature ChaProwess
    {
      get
      {
        _chaProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.ChaProwess);
        return _chaProwess;
      }
    }
    #endregion

    #region Legendary
    private static BlueprintFeature _legendaryGifts;
    internal static BlueprintFeature LegendaryGifts
    {
      get
      {
        _legendaryGifts ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryGifts);
        return _legendaryGifts;
      }
    }

    private static BlueprintFeature _legendaryWeapon;
    internal static BlueprintFeature LegendaryWeapon
    {
      get
      {
        _legendaryWeapon ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryWeapon);
        return _legendaryWeapon;
      }
    }
    private static BlueprintFeature _legendaryOffHand;
    internal static BlueprintFeature LegendaryOffHand
    {
      get
      {
        _legendaryOffHand ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryOffHand);
        return _legendaryOffHand;
      }
    }
    private static BlueprintFeature _legendaryArmor;
    internal static BlueprintFeature LegendaryArmor
    {
      get
      {
        _legendaryArmor ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryArmor);
        return _legendaryArmor;
      }
    }
    private static BlueprintFeature _legendaryShield;
    internal static BlueprintFeature LegendaryShield
    {
      get
      {
        _legendaryShield ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryShield);
        return _legendaryShield;
      }
    }

    private static BlueprintAbilityResource _legendaryWeaponResource;
    internal static BlueprintAbilityResource LegendaryWeaponResource
    {
      get
      {
        _legendaryWeaponResource ??= BlueprintTool.Get<BlueprintAbilityResource>(Guids.LegendaryWeaponResource);
        return _legendaryWeaponResource;
      }
    }
    private static BlueprintAbilityResource _legendaryOffHandResource;
    internal static BlueprintAbilityResource LegendaryOffHandResource
    {
      get
      {
        _legendaryOffHandResource ??= BlueprintTool.Get<BlueprintAbilityResource>(Guids.LegendaryOffHandResource);
        return _legendaryOffHandResource;
      }
    }
    private static BlueprintAbilityResource _legendaryArmorResource;
    internal static BlueprintAbilityResource LegendaryArmorResource
    {
      get
      {
        _legendaryArmorResource ??= BlueprintTool.Get<BlueprintAbilityResource>(Guids.LegendaryArmorResource);
        return _legendaryArmorResource;
      }
    }
    private static BlueprintAbilityResource _legendaryShieldResource;
    internal static BlueprintAbilityResource LegendaryShieldResource
    {
      get
      {
        _legendaryShieldResource ??= BlueprintTool.Get<BlueprintAbilityResource>(Guids.LegendaryShieldResource);
        return _legendaryShieldResource;
      }
    }

    private static BlueprintFeature _legendaryStr;
    internal static BlueprintFeature LegendaryStr
    {
      get
      {
        _legendaryStr ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryStrength);
        return _legendaryStr;
      }
    }
    private static BlueprintFeature _legendaryDex;
    internal static BlueprintFeature LegendaryDex
    {
      get
      {
        _legendaryDex ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryDexterity);
        return _legendaryDex;
      }
    }
    private static BlueprintFeature _legendaryCon;
    internal static BlueprintFeature LegendaryCon
    {
      get
      {
        _legendaryCon ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryConstitution);
        return _legendaryCon;
      }
    }
    private static BlueprintFeature _legendaryInt;
    internal static BlueprintFeature LegendaryInt
    {
      get
      {
        _legendaryInt ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryIntelligence);
        return _legendaryInt;
      }
    }
    private static BlueprintFeature _legendaryWis;
    internal static BlueprintFeature LegendaryWis
    {
      get
      {
        _legendaryWis ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryWisdom);
        return _legendaryWis;
      }
    }
    private static BlueprintFeature _legendaryCha;
    internal static BlueprintFeature LegendaryCha
    {
      get
      {
        _legendaryCha ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryCharisma);
        return _legendaryCha;
      }
    }
    #endregion
    #endregion

    internal static bool IsReplacedByABP(StatType stat, ModifierDescriptor descriptor)
    {
      switch (stat)
      {
        case StatType.AC:
          return descriptor == ModifierDescriptor.Armor
            || descriptor == ModifierDescriptor.ArmorEnhancement
            || descriptor == ModifierDescriptor.NaturalArmorEnhancement
            || descriptor == ModifierDescriptor.ShieldEnhancement
            || descriptor == ModifierDescriptor.Deflection;
        case StatType.Charisma:
        case StatType.Constitution:
        case StatType.Dexterity:
        case StatType.Intelligence:
        case StatType.Strength:
        case StatType.Wisdom:
          return descriptor == ModifierDescriptor.Enhancement;
        case StatType.SaveFortitude:
        case StatType.SaveReflex:
        case StatType.SaveWill:
          return descriptor == ModifierDescriptor.Resistance;
      }
      return false;
    }

    internal static bool IsAffectedByABP(UnitEntityData unit)
    {
      if (unit is null)
        return false;
      return unit.IsInCompanionRoster() || (unit.Master is not null && unit.Master.IsInCompanionRoster());
    }

    internal static ItemEntityWeapon GetSecondaryWeapon(UnitEntityData unit)
    {
      return unit.Body.FindWeaponSlot(slot => slot.HasWeapon && !IsPrimaryWeapon(slot.Weapon))?.Weapon;
    }

    internal static bool HasSecondaryWeapon(UnitEntityData unit)
    {
      var secondaryWeapon = unit.Body.FindWeaponSlot(slot => slot.HasWeapon && !IsPrimaryWeapon(slot.Weapon));
      return secondaryWeapon is not null;
    }

    internal static bool IsPrimaryWeapon(ItemEntityWeapon weapon)
    {
      if (weapon.Blueprint.AlwaysPrimary || weapon.Blueprint.IsUnarmed)
        return true;

      if (weapon.ForceSecondary)
        return false;

      if (!weapon.Blueprint.IsNatural)
        return weapon.Wielder.Body.PrimaryHand.Weapon == weapon;

      var wielder = weapon.Wielder;
      if (weapon == wielder.Body.PrimaryHand.MaybeWeapon)
        return true;

      if (weapon == wielder.Body.SecondaryHand.MaybeWeapon)
        return true;

      // Natural weapons are secondary when they are not the primary or secondary hand
      return false;
    }

    /// <summary>
    /// Returns the prowess bonus applied to the stat.
    /// </summary>
    internal static int GetProwessBonus(ModifiableValue stat)
    {
      var enhancement = stat.GetModifiers(ModifierDescriptor.Enhancement);
      string log = $"Checking {stat.Type}: ";
      foreach (var mod in stat.Modifiers)
        log += $"[{mod.ModValue}:{mod.ModDescriptor}]";
      Logger.Log(log);
      if (enhancement is null)
        return 0;

      switch (stat.Type)
      {
        case StatType.Strength:
          return enhancement.Where(mod => mod.Source.Blueprint == StrProwess).Sum(mod => mod.ModValue);
        case StatType.Dexterity:
          return enhancement.Where(mod => mod.Source.Blueprint == DexProwess).Sum(mod => mod.ModValue);
        case StatType.Constitution:
          return enhancement.Where(mod => mod.Source.Blueprint == ConProwess).Sum(mod => mod.ModValue);
        case StatType.Intelligence:
          return enhancement.Where(mod => mod.Source.Blueprint == IntProwess).Sum(mod => mod.ModValue);
        case StatType.Wisdom:
          return enhancement.Where(mod => mod.Source.Blueprint == WisProwess).Sum(mod => mod.ModValue);
        case StatType.Charisma:
          return enhancement.Where(mod => mod.Source.Blueprint == ChaProwess).Sum(mod => mod.ModValue);
      }
      return 0;
    }

    /// <summary>
    /// Returns the legendary ability bonus applied to the stat.
    /// </summary>
    internal static int GetLegendaryBonus(ModifiableValue stat)
    {
      var inherent = stat.GetModifiers(ModifierDescriptor.Inherent);
      if (inherent is null)
        return 0;

      switch (stat.Type)
      {
        case StatType.Strength:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryStr).Sum(mod => mod.ModValue);
        case StatType.Dexterity:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryDex).Sum(mod => mod.ModValue);
        case StatType.Constitution:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryCon).Sum(mod => mod.ModValue);
        case StatType.Intelligence:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryInt).Sum(mod => mod.ModValue);
        case StatType.Wisdom:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryWis).Sum(mod => mod.ModValue);
        case StatType.Charisma:
          return inherent.Where(mod => mod.Source.Blueprint == LegendaryCha).Sum(mod => mod.ModValue);
      }
      return 0;
    }
  }
}
