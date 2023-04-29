using AutomaticBonusProgression.Components;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;

namespace AutomaticBonusProgression.Util
{
  /// Classes here are just to simplify utilities and readability. Only data containers!

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
    internal readonly string DisplayName;
    internal readonly string Description;
    internal readonly string Icon;
    internal readonly EnhancementType Type;
    internal readonly int Cost;
    internal readonly int Ranks;
    internal readonly PrerequisiteInfo Prerequisite;

    protected EnchantInfo(
      string displayName,
      string description,
      string icon,
      EnhancementType type,
      int cost,
      int ranks,
      PrerequisiteInfo prerequisite = null)
    {
      DisplayName = displayName;
      Description = description;
      Icon = icon;
      Type = type;
      Cost = cost;
      Ranks = ranks;
      Prerequisite = prerequisite;
    }

    /// <summary>
    /// Creates the <see cref="AttunementEffect"/> component to apply the enchant effect when available.
    /// </summary>
    /// <param name="effectBuff">Actual enchant effect bufff</param>
    /// <param name="variant">If true, the appropriate variant component is created. e.g. Shield variant for armor enchantments</param>
    internal abstract AttunementEffect GetAttunementComponent(
      Blueprint<BlueprintBuffReference> effectBuff, bool variant = false);

    internal BlueprintComponent GetEnhancementComponent()
    {
      return new EnhancementEquivalence(this);
    }
  }

  internal class ArmorEnchantInfo : EnchantInfo
  {
    internal readonly ArmorProficiencyGroup[] AllowedTypes;

    public ArmorEnchantInfo(
        string displayName,
        string description,
        string icon,
        int cost,
        int ranks,
        PrerequisiteInfo prerequisite,
        params ArmorProficiencyGroup[] allowedTypes) :
      base(displayName, description, icon, EnhancementType.Armor, cost, ranks, prerequisite)
    {
      AllowedTypes = allowedTypes;
    }

    public ArmorEnchantInfo(
        string displayName,
        string description,
        string icon,
        int cost,
        int ranks,
        params ArmorProficiencyGroup[] allowedTypes) :
      this(displayName, description, icon, cost, ranks, prerequisite: null, allowedTypes)
    { }

    internal override AttunementEffect GetAttunementComponent(
      Blueprint<BlueprintBuffReference> effectBuff, bool variant = false)
    {
      if (variant)
        return new ShieldAttunement(effectBuff.Reference, AllowedTypes);
      return new ArmorAttunement(effectBuff.Reference, AllowedTypes);
    }
  }

  internal class WeaponEnchantInfo : EnchantInfo
  {
    public WeaponEnchantInfo(
        string displayName,
        string description,
        string icon,
        int cost,
        int ranks,
        PrerequisiteInfo prerequisite) :
      base(displayName, description, icon, EnhancementType.MainHand, cost, ranks, prerequisite)
    { }

    public WeaponEnchantInfo(
        string displayName,
        string description,
        string icon,
        int cost,
        int ranks) :
      this(displayName, description, icon, cost, ranks, prerequisite: null)
    { }

    internal override AttunementEffect GetAttunementComponent(Blueprint<BlueprintBuffReference> effectBuff, bool variant = false)
    {
      throw new System.NotImplementedException();
    }
  }

  internal class PrerequisiteInfo
  {
    internal readonly Blueprint<BlueprintFeatureReference> Feature;
    internal readonly int Ranks;

    internal PrerequisiteInfo(Blueprint<BlueprintFeatureReference> feature, int ranks = 1)
    {
      Feature = feature;
      Ranks = ranks;
    }
  }
}
