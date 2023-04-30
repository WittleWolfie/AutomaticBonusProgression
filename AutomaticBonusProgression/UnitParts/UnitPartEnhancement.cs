using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using UniRx;

namespace AutomaticBonusProgression.UnitParts
{
  internal class UnitPartEnhancement : UnitPart
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitPartEnhancement));

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

    [JsonIgnore]
    internal ReactiveProperty<int> TempEnhancement = new();
    [JsonIgnore]
    private int MaxTemp = 0;
    /// Tracks the base enhancement (i.e. not provided by Attunement)
    [JsonIgnore]
    private int BaseTempEnhancement = 0;

    internal void ResetTempEnhancement(EnhancementType type, int max)
    {
      MaxTemp = max;
      TempEnhancement.Value = 0;

      BaseTempEnhancement = type switch
      {
        EnhancementType.Armor => Armor,
        EnhancementType.Shield => Shield,
        EnhancementType.MainHand => MainHand,
        EnhancementType.OffHand => OffHand,
      };
    }

    internal bool CanAddTemp(int enhancement)
    {
      if (TempEnhancement.Value + enhancement > MaxTemp)
        return false;

      return TempEnhancement.Value + BaseTempEnhancement + enhancement <= 5;
    }

    internal void AddTemp(int enhancement, bool active)
    {
      if (active)
        BaseTempEnhancement -= enhancement;
      TempEnhancement.Value += enhancement;
    }

    internal void RemoveTemp(int enhancement)
    {
      TempEnhancement.Value -= enhancement;
    }
  }
}
