using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Microsoft.Build.Utilities;
using System;

namespace AutomaticBonusProgression
{
  internal class BonusProgression
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BonusProgression));

    private const string EnhancementCalculator = "EnhancementCalculator";

    internal static void Configure()
    {
      Logger.Log("Configuring bonus progression");

      // TODO: Grant to existing characters!

      var armorSelection = ArmorAttunement.Configure();
      var basicFeats = ProgressionConfigurator.For(ProgressionRefs.BasicFeatsProgression)
        .ModifyLevelEntries(
          levelEntry =>
          {
            // There's a special case for BasicFeatProgression where they don't actually grab all level entries for
            // level 1, just the first.
            if (levelEntry.Level == 1)
              levelEntry.Features.Add(ConfigureEnhancementCalculator());
          })
        .AddToLevelEntries(level: 2, armorSelection)
        .AddToLevelEntries(level: 3, armorSelection)
        .Configure();
    }

    private static BlueprintFeature ConfigureEnhancementCalculator()
    {
      Logger.Log("Configuring enhancement calculator");

      return FeatureConfigurator.New(EnhancementCalculator, Guids.EnhancementCalculator)
        .SetIsClassFeature()
        .SetHideInUI()
        .AddComponent<EnhancementBonusCalculator>()
        .Configure();
    }

    [TypeId("55b09ee7-cb57-4a50-847d-85c32dea4b29")]
    internal class EnhancementBonusCalculator : UnitFactComponentDelegate
    {
      private static BlueprintFeature _armorAttunement;
      private static BlueprintFeature ArmorAttunement
      {
        get
        {
          _armorAttunement ??= BlueprintTool.Get<BlueprintFeature>(Guids.ArmorAttunement);
          return _armorAttunement;
        }
      }

      internal int GetEnhancementBonus(ItemEntityArmor armor)
      {
        int tempBonus = 0;
        foreach (var enchantment in armor.Enchantments)
        {
          if (enchantment.GetComponent<MagicItem>() is not null)
            continue; // Skip these which represent the basic enchantments replaced by the mod

          var bonus = enchantment.GetComponent<ArmorEnhancementBonus>();
          if (bonus is not null && bonus.EnhancementValue > tempBonus)
            tempBonus = bonus.EnhancementValue;
        }

        var attunement = Owner.GetFact(ArmorAttunement);
        return Math.Max(tempBonus, attunement is null ? 0 : attunement.GetRank());
      }
    }
  }
}
