using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.UnitLogic;
using Newtonsoft.Json;
using System;
using UniRx;

// Note: Namespace doesn't match for test save compatibility
// Another Note: Ideally we'd just dynamically check the value every time it's requested, however, there's some problems with
// the ComponentRuntime the currently prevent checking items during some stages of equip / unequip. If there are more bugs in
// the future I should seriously consider rebuilding this calculation to something more robust. The current caching mechanism
// barely works.
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
        EnhancementType.Armor => Armor + enhancement <= GetMax(type),
        EnhancementType.Shield => Shield + enhancement <= GetMax(type),
        EnhancementType.MainHand => MainHand + enhancement <= GetMax(type),
        EnhancementType.OffHand => OffHand + enhancement <= GetMax(type),
        _ => throw new NotImplementedException(),
      };
    }

    /// <summary>
    /// Whether or not an enchantment can remain active after it has been activated.
    /// </summary>
    internal bool CanKeep(EnhancementType type)
    {
      return type switch
      {
        EnhancementType.Armor => Armor <= GetMax(type),
        EnhancementType.Shield => Shield <= GetMax(type),
        EnhancementType.MainHand => MainHand <= GetMax(type),
        EnhancementType.OffHand => OffHand <= GetMax(type),
        _ => throw new NotImplementedException(),
      };
    }

    /// <summary>
    /// Max enhancement bonus (aka rank of LegendaryX feature)
    /// </summary>
    internal int GetMax(EnhancementType type)
    {
      var feature = type switch
      {
        EnhancementType.Armor => Owner.GetFeature(Common.LegendaryArmor),
        EnhancementType.Shield => Owner.GetFeature(Common.LegendaryShield),
        EnhancementType.MainHand => Owner.GetFeature(Common.LegendaryWeapon),
        EnhancementType.OffHand => Owner.GetFeature(Common.LegendaryOffHand),
        _ => throw new NotImplementedException(),
      };

      int maxIncrease = Owner.GetFeature(Common.TricksterArcanaBuff)?.GetRank() ?? 0;
      return maxIncrease + Math.Min(5, feature?.GetRank() ?? 0);
    }

    [JsonIgnore]
    internal ReactiveProperty<int> TempEnhancement = new();
    [JsonIgnore]
    private int MaxTemp = 0;
    /// Tracks the base enhancement (i.e. not provided by Attunement)
    [JsonIgnore]
    private int BaseTempEnhancement = 0;

    internal void ResetTempEnhancement(EnhancementType type)
    {
      MaxTemp = GetMax(type);
      TempEnhancement.Value = 0;

      BaseTempEnhancement = type switch
      {
        EnhancementType.Armor => Armor,
        EnhancementType.Shield => Shield,
        EnhancementType.MainHand => MainHand,
        EnhancementType.OffHand => OffHand,
        _ => throw new NotImplementedException(),
      };
    }

    internal int GetTempTotal()
    {
      return BaseTempEnhancement + TempEnhancement.Value;
    }

    internal bool CanAddTemp(int enhancement)
    {
      return TempEnhancement.Value + BaseTempEnhancement + enhancement <= MaxTemp;
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
