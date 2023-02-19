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
    private int Armor = 0;

    [JsonProperty]
    private int Shield = 0;

    [JsonProperty]
    private int MainHand = 0;

    [JsonProperty]
    private int OffHand = 0;

    internal void AddEnchantment(EnhancementType type, int enhancement)
    {
      switch (type)
      {
        case EnhancementType.Armor:
          Armor += enhancement;
          return;
        case EnhancementType.Shield:
          Shield += enhancement;
          return;
        case EnhancementType.MainHand:
          MainHand += enhancement;
          return;
        case EnhancementType.OffHand:
          OffHand += enhancement;
          return;
      }
    }

    internal void RemoveEnchantment(EnhancementType type, int enhancement)
    {
      switch (type)
      {
        case EnhancementType.Armor:
          Armor -= enhancement;
          return;
        case EnhancementType.Shield:
          Shield -= enhancement;
          return;
        case EnhancementType.MainHand:
          MainHand -= enhancement;
          return;
        case EnhancementType.OffHand:
          OffHand -= enhancement;
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
        EnhancementType.Armor => Armor + enhancement <= 5,
        EnhancementType.Shield => Shield + enhancement <= 5,
        EnhancementType.MainHand => MainHand + enhancement <= 5,
        EnhancementType.OffHand => OffHand + enhancement <= 5,
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
        EnhancementType.Armor => Armor <= 5,
        EnhancementType.Shield => Shield <= 5,
        EnhancementType.MainHand => MainHand <= 5,
        EnhancementType.OffHand => OffHand <= 5,
        _ => throw new System.NotImplementedException(),
      };
    }
  }
}
