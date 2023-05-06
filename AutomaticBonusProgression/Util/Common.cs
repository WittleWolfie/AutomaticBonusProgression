using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic;

namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// Common utils
  /// </summary>
  internal static class Common
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Common));

    private static BlueprintFeature _armorAttunement;
    internal static BlueprintFeature ArmorAttunement
    {
      get
      {
        _armorAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ArmorAttunement);
        return _armorAttunement;
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

    private static BlueprintFeature _weaponAttunement;
    internal static BlueprintFeature WeaponAttunement
    {
      get
      {
        _weaponAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.WeaponAttunement);
        return _weaponAttunement;
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
  }
}
