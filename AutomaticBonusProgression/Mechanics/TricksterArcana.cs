using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Mechanics
{
  /// <summary>
  /// Updates the Trickster Knowledge: Arcana feature so it isn't useless or harmful!
  /// </summary>
  internal static class TricksterArcana
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Mechanics.TricksterArcana));

    internal static void Configure()
    {
      var buff = ConfigureIncreaseMaxEnhancementCost();
      FeatureConfigurator.For(FeatureRefs.TricksterKnowledgeArcanaTier1Feature)
        .SetDescription("ABP.Abilities.TricksterArcanaTier1")
        .RemoveComponents(c => c is TricksterArcanaBetterEnhancements)
        .AddFacts(new() { buff })
        .Configure();

      FeatureConfigurator.For(FeatureRefs.TricksterKnowledgeArcanaTier3Feature)
        .SetDescription("ABP.Abilities.TricksterArcanaTier3")
        .AddFacts(new() { buff })
        .Configure();
    }

    private const string TricksterArcanaBuff = "TricksterArcana.IncreaseMaxEnhancementCost";
    private static BlueprintFeature ConfigureIncreaseMaxEnhancementCost()
    {
      return FeatureConfigurator.New(TricksterArcanaBuff, Guids.TricksterArcanaBuff)
        .SetHideInUI(true)
        .SetIsClassFeature()
        .SetRanks(2)
        .Configure();
    }
  }
}
