using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;

namespace AutomaticBonusProgression.UnitParts
{
  internal class EnhancementEquivalence : UnitPart
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalence));

    [JsonProperty]
    private int ArmorEnhancement = 0;

    internal void AddEnchantment(EnhancementType type, int enhancement)
    {
      switch (type)
      {
        case EnhancementType.Armor:
          ArmorEnhancement += enhancement;
          return;
      }
    }

    internal void RemoveEnchantment(EnhancementType type, int enhancement)
    {
      switch (type)
      {
        case EnhancementType.Armor:
          ArmorEnhancement -= enhancement;
          return;
      }
    }

    /// <summary>
    /// Whether a new enchantment can be activated.
    /// </summary>
    internal bool CanAdd(EnhancementType type, int enhancement)
    {
      return type switch
      {
        EnhancementType.Armor => ArmorEnhancement + enhancement <= 5,
        EnhancementType.Shield => throw new System.NotImplementedException(),
        EnhancementType.MainHand => throw new System.NotImplementedException(),
        EnhancementType.OffHand => throw new System.NotImplementedException(),
        _ => throw new System.NotImplementedException(),
      };
    }

    /// <summary>
    /// Whether or not an enchantment can remain active after it has been activated.
    /// </summary>
    internal bool CanKeep(EnhancementType type)
    {
      return type switch
      {
        EnhancementType.Armor => ArmorEnhancement <= 5,
        EnhancementType.Shield => throw new System.NotImplementedException(),
        EnhancementType.MainHand => throw new System.NotImplementedException(),
        EnhancementType.OffHand => throw new System.NotImplementedException(),
        _ => throw new System.NotImplementedException(),
      };
    }
  }
}
