using AutomaticBonusProgression.Components;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace AutomaticBonusProgression.Util
{
  /// <summary>
  /// Common utils
  /// </summary>
  internal static class Common
  {
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

    private static BlueprintBuff _armorEquivalence;
    internal static BlueprintBuff ArmorEquivalence
    {
      get
      {
        _armorEquivalence ??= BlueprintTool.Get<BlueprintBuff>(Guids.ArmorEnhancementEquivalence);
        return _armorEquivalence;
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

    internal static BlueprintFeature AddEnhancementEquivalence(
      Blueprint<BlueprintReference<BlueprintFeature>> feature, EnhancementType type, int enhancement)
    {
      return FeatureConfigurator.For(feature)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancement))
        .Configure();
    }

    internal static BlueprintArmorEnchantment AddEnhancementEquivalence(
      Blueprint<BlueprintReference<BlueprintArmorEnchantment>> enchantment, EnhancementType type, int enhancement)
    {
      return ArmorEnchantmentConfigurator.For(enchantment)
        .AddComponent(new EnhancementEquivalenceComponent(type, enhancement))
        .Configure();
    }
  }
}
