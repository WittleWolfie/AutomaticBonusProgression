using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace AutomaticBonusProgression.Enchantments
{
  /// <summary>
  /// Invisible buffs track the enhancement equivalence for non-enhancement buffs. Used to determine if more
  /// enhancements can be applied.
  /// </summary>
  internal class EnhancementEquivalence
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalence));

    private const string ArmorName = "EnhancementEquivalence.Armor";

    internal static void Configure()
    {
      Logger.Log($"Configuring Enhancement Equivalence");

      BuffConfigurator.New(ArmorName, Guids.ArmorEnhancementEquivalence)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .SetStacking(StackingType.Rank)
        .SetRanks(5)
        .Configure();
    }
  }
}
