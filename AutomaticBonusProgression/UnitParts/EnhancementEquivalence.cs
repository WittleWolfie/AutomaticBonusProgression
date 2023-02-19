using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AutomaticBonusProgression.UnitParts
{
  internal class EnhancementEquivalence : UnitPart
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnhancementEquivalence));

    [JsonProperty]
    private List<Buff> ArmorEnchantments = new();

    [JsonProperty]
    private int ArmorEnhancement = 0;

    internal void AddEnchantment(EnhancementEquivalenceComponent component)
    {
      switch (component.Type)
      {
        case EnhancementType.Armor:
          Add(ref ArmorEnhancement, ArmorEnchantments, component.Enhancement, component.Fact as Buff);
          break;
        case EnhancementType.Shield:
          break;
        case EnhancementType.MainHand:
          break;
        case EnhancementType.OffHand:
          break;
        default:
          break;
      }
    }

    private static void Add(ref int enhancement, List<Buff> enchantments, int toAdd, Buff buff)
    {
      enhancement += toAdd;

      if (buff is not null)
        enchantments.Add(buff);

      if (enhancement > 5)
      {
        Logger.Verbose(() => $"Current enhancement bonus too high, removing enchantments");
        int reduction = enhancement - 5;
        List<Buff> toRemove = new();
        foreach (var enchant in enchantments)
        {
          toRemove.Add(enchant);
          reduction -= enchant.GetComponent<EnhancementEquivalenceComponent>().Enhancement;
          if (reduction <= 0)
            break;
        }

        foreach (var enchant in toRemove)
          enchant.Remove();
      }
    }

    internal void RemoveEnchantment(EnhancementEquivalenceComponent component)
    {
      if (component.Type == EnhancementType.Armor)
      {
        ArmorEnhancement -= component.Enhancement;
        if (component.Fact is Buff buff)
          ArmorEnchantments.Remove(buff);
      }
    }

    internal bool CanEnchant(int enhancement, EnhancementType type)
    {
      return type switch
      {
        EnhancementType.Armor => ArmorEnhancement + enhancement <= 5,
        EnhancementType.Shield => false,
        EnhancementType.MainHand => false,
        EnhancementType.OffHand => false,
        _ => throw new ArgumentException($"Unexpected type: {type}"),
      };
    }
  }
}
