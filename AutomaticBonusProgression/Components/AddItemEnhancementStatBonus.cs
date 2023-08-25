using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Applies the item's enhancement bonus as a stat bonus.
  /// </summary>
  [TypeId("bd45a955-eae3-488e-b7dd-3ddcc51a1916")]
  [AllowedOn(typeof(BlueprintItemEnchantment))]
  [AllowMultipleComponents]
  internal class AddItemEnhancementStatBonus : ItemEnchantmentComponentDelegate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddItemEnhancementStatBonus));

    private readonly StatType Stat;
    private readonly ModifierDescriptor Descriptor;

    internal AddItemEnhancementStatBonus(StatType stat, ModifierDescriptor descriptor = ModifierDescriptor.Enhancement)
    {
      Stat = stat;
      Descriptor = descriptor;
    }

    public override void OnTurnOn()
    {
      try
      {
        var statValue = Owner.Wielder?.Stats.GetStat(Stat);
        if (statValue is null)
        {
          Logger.Warning($"Cannot find {Stat} for {Owner.Wielder?.CharacterName}");
          return;
        }

        var bonus = GameHelper.GetItemEnhancementBonus(Owner);
        statValue.AddItemModifierUnique(bonus, Runtime, Owner, Descriptor);
      }
      catch (Exception e)
      {
        Logger.LogException("AddItemStatBonus.OnTurnOn", e);
      }
    }

    public override void OnTurnOff()
    {
      try
      {
        Owner.Wielder?.Stats.GetStat(Stat).RemoveModifiersFrom(Runtime);
      }
      catch (Exception e)
      {
        Logger.LogException("AddItemStatBonus.OnTurnOff", e);
      }
    }
  }
}
