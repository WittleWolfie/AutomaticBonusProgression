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
