using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.EntitySystem;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using Newtonsoft.Json;
using System;
using UniRx;
using static Kingmaker.Blueprints.Area.FactHolder;

// Note: Namespace doesn't match for test save compatibility
// Another Note: Ideally we'd just dynamically check the value every time it's requested, however, there's some problems with
// the ComponentRuntime the currently prevent checking items during some stages of equip / unequip. If there are more bugs in
// the future I should seriously consider rebuilding this calculation to something more robust. The current caching mechanism
// barely works.
namespace AutomaticBonusProgression.UnitParts
{
  internal class UnitPartTempEnhancement : UnitPart
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(UnitPartTempEnhancement));

    [JsonIgnore]
    internal ReactiveProperty<int> TempBonus = new();
    [JsonIgnore]
    private int MaxBonus = 0;

    internal void Reset(EnhancementType type)
    {
      Logger.Warning("Resetting!");
      TempBonus.Value = 0;
      MaxBonus = GetMax(type);
      Owner.Facts.List.ForEach(fact => ApplyTemp(fact, type));
      Owner.Body.Items.ForEach(item => item.Enchantments.ForEach(enchant => ApplyTemp(enchant, type)));
    }

    private void ApplyTemp(EntityFact fact, EnhancementType type)
    {
      fact.CallComponentsWithRuntime<EnhancementEquivalence>((c, r) => c.ApplyTemp(r, type));
      fact.CallComponentsWithRuntime<AttunementEffect>((c, r) => c.ApplyTemp(r, type));
    }

    private void ApplyTemp(ItemEnchantment enchantment, EnhancementType type)
    {
      enchantment.CallComponentsWithRuntime<EnhancementEquivalence>((c, r) => c.ApplyTemp(r, type));
    }

    internal void Add(int enhancement)
    {
      Logger.Warning($"Adding {enhancement} to {TempBonus.Value}");
      TempBonus.Value += enhancement;
    }

    internal void Remove(int enhancement)
    {
      Logger.Warning($"Removing {enhancement} from {TempBonus.Value}");
      TempBonus.Value -= enhancement;
    }

    internal bool CanAdd(int enhancement)
    {
      return TempBonus.Value + enhancement <= MaxBonus;
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
  }
}
