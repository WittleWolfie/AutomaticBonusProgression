using AutomaticBonusProgression.Components;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using System;
using System.Collections.Generic;

namespace AutomaticBonusProgression.Util
{
  internal class BlueprintInfo
  {
    internal readonly string Name;
    internal readonly string Guid;
    internal readonly BlueprintComponent[] Components;

    internal readonly string VariantName;
    internal readonly string VariantGuid;

    internal BlueprintInfo(string name, string guid, params BlueprintComponent[] components)
    {
      Name = name;
      Guid = guid;
      Components = components;
    }
  }

  internal abstract class EnchantInfo
  {
    internal readonly LocalString DisplayName;
    internal readonly LocalString Description;
    internal readonly string Icon;
    internal readonly EnhancementType Type;
    internal readonly int Cost;

    protected EnchantInfo(
      LocalString displayName, LocalString description, string icon, EnhancementType type, int cost)
    {
      DisplayName = displayName;
      Description = description;
      Icon = icon;
      Type = type;
      Cost = cost;
    }

    /// <summary>
    /// Creates the <see cref="AttunementEffect"/> component to apply the enchant effect when available.
    /// </summary>
    /// <param name="effectBuff">Actual enchant effect bufff</param>
    /// <param name="variant">If true, the appropriate variant component is created. e.g. Shield variant for armor enchantments</param>
    internal abstract AttunementEffect GetAttunementComponent(
      Blueprint<BlueprintBuffReference> effectBuff, bool variant = false);

    /// <summary>
    /// Creates the <see cref="EnhancementEquivalence"/> component for this enchantment.
    /// </summary>
    /// <returns></returns>
    internal BlueprintComponent GetEnhancementComponent()
    {
      return new EnhancementEquivalence(this);
    }
  }

  internal class ArmorEnchantInfo : EnchantInfo
  {
    internal readonly ArmorProficiencyGroup[] AllowedTypes;

    public ArmorEnchantInfo(
        LocalString displayName, LocalString description, string icon, int cost, params ArmorProficiencyGroup[] allowedTypes)
      : base(displayName, description, icon, EnhancementType.Armor, cost)
    {
      AllowedTypes = allowedTypes;
    }

    internal override AttunementEffect GetAttunementComponent(
      Blueprint<BlueprintBuffReference> effectBuff, bool variant = false)
    {
      if (variant)
        return new ShieldAttunement(effectBuff.Reference, Cost, AllowedTypes);
      return new ArmorAttunement(effectBuff.Reference, Cost, AllowedTypes);
    }
  }

  internal class WeaponEnchantInfo : EnchantInfo
  {
    internal readonly WeaponRangeType[] AllowedRanges = Array.Empty<WeaponRangeType>();
    internal readonly PhysicalDamageForm[] AllowedForms = Array.Empty<PhysicalDamageForm>();

    public WeaponEnchantInfo(LocalString displayName, LocalString description, string icon, int cost)
      : base(displayName, description, icon, EnhancementType.MainHand, cost) { }

    public WeaponEnchantInfo(
      LocalString displayName, LocalString description, string icon, int cost, params WeaponRangeType[] allowedRanges)
      : base(displayName, description, icon, EnhancementType.MainHand, cost)
    {
      AllowedRanges = allowedRanges;
    }

    public WeaponEnchantInfo(
      LocalString displayName, LocalString description, string icon, int cost, params PhysicalDamageForm[] allowedForms)
      : base(displayName, description, icon, EnhancementType.MainHand, cost)
    {
      AllowedForms = allowedForms;
    }

    public WeaponEnchantInfo(
      LocalString displayName,
      LocalString description,
      string icon,
      int cost,
      List<WeaponRangeType> allowedRanges,
      List<PhysicalDamageForm> allowedForms)
      : base(displayName, description, icon, EnhancementType.MainHand, cost)
    {
      AllowedRanges = allowedRanges.ToArray();
      AllowedForms = allowedForms.ToArray();
    }

    internal override AttunementEffect GetAttunementComponent(
      Blueprint<BlueprintBuffReference> effectBuff, bool variant = false)
    {
      if (variant)
        return new OffHandAttunement(effectBuff.Reference, Cost, AllowedRanges, AllowedForms);
      return new WeaponAttunement(effectBuff.Reference, Cost, AllowedRanges, AllowedForms);
    }
  }
}
