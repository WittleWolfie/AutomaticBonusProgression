using AutomaticBonusProgression.Components;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using System.Collections.Generic;

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
        EnhancementType type,
        int cost,
        int ranks,
        PrerequisiteInfo prerequisite = null,
        params ArmorProficiencyGroup[] allowedTypes) :
      base(displayName, description, icon, type, cost, ranks, prerequisite)
    {
      AllowedTypes = allowedTypes;
    }
  }

  internal class PrerequisiteInfo
  {
    internal readonly Blueprint<BlueprintFeatureReference> Feature;
    internal readonly int Ranks;

    internal PrerequisiteInfo(Blueprint<BlueprintFeatureReference> feature, int ranks)
    {
      Feature = feature;
      Ranks = ranks;
    }
  }
}
