using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
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
    #region Basic
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
    #endregion

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
    private static BlueprintFeature _legendaryPhysicalProwess;
    internal static BlueprintFeature LegendaryPhysicalProwess
    {
      get
      {
        _legendaryPhysicalProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryPhysicalProwess);
        return _legendaryPhysicalProwess;
      }
    }
    private static BlueprintFeature _legendaryMentalProwess;
    internal static BlueprintFeature LegendaryMentalProwess
    {
      get
      {
        _legendaryMentalProwess ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryMentalProwess);
        return _legendaryMentalProwess;
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

    private static BlueprintFeature _twinWeapons;
    internal static BlueprintFeature TwinWeapons
    {
      get
      {
        _twinWeapons ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryTwinWeapons);
        return _twinWeapons;
      }
    }
    private static BlueprintFeature _shieldmaster;
    internal static BlueprintFeature Shieldmaster
    {
      get
      {
        _shieldmaster ??= BlueprintTool.Get<BlueprintFeature>(Guids.LegendaryShieldmaster);
        return _shieldmaster;
      }
    }
    #endregion

    #region Enchantment Replacements
    private static BlueprintEquipmentEnchantment _str2;
    internal static BlueprintEquipmentEnchantment Str2
    {
      get
      {
        _str2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Str2);
        return _str2;
      }
    }

    private static BlueprintEquipmentEnchantment _dex2;
    internal static BlueprintEquipmentEnchantment Dex2
    {
      get
      {
        _dex2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Dex2);
        return _dex2;
      }
    }

    private static BlueprintEquipmentEnchantment _con2;
    internal static BlueprintEquipmentEnchantment Con2
    {
      get
      {
        _con2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Con2);
        return _con2;
      }
    }

    private static BlueprintEquipmentEnchantment _int2;
    internal static BlueprintEquipmentEnchantment Int2
    {
      get
      {
        _int2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Int2);
        return _int2;
      }
    }

    private static BlueprintEquipmentEnchantment _wis2;
    internal static BlueprintEquipmentEnchantment Wis2
    {
      get
      {
        _wis2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Wis2);
        return _wis2;
      }
    }

    private static BlueprintEquipmentEnchantment _cha2;
    internal static BlueprintEquipmentEnchantment Cha2
    {
      get
      {
        _cha2 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.Cha2);
        return _cha2;
      }
    }

    private static BlueprintEquipmentEnchantment _increaseDeflection1;
    internal static BlueprintEquipmentEnchantment IncreaseDeflection1
    {
      get
      {
        _increaseDeflection1 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.IncreaseDeflection1);
        return _increaseDeflection1;
      }
    }

    private static BlueprintEquipmentEnchantment _increaseNaturalArmor1;
    internal static BlueprintEquipmentEnchantment IncreaseNaturalArmor1
    {
      get
      {
        _increaseNaturalArmor1 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.IncreaseNaturalArmor1);
        return _increaseNaturalArmor1;
      }
    }

    private static BlueprintEquipmentEnchantment _increaseResist1;
    internal static BlueprintEquipmentEnchantment IncreaseResist1
    {
      get
      {
        _increaseResist1 ??= BlueprintTool.Get<BlueprintEquipmentEnchantment>(Guids.IncreaseResist1);
        return _increaseResist1;
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
