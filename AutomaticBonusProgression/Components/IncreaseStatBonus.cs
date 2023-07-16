using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Increases a specific bonus value. This forces stacking.
  /// </summary>
  [AllowedOn(typeof(BlueprintItemEnchantment))]
  [AllowMultipleComponents]
  [TypeId("be9fc483-7e05-450f-9d9c-8581fc8d2271")]
  internal class IncreaseStatBonus : ItemEnchantmentComponentDelegate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(IncreaseStatBonus));

    private readonly StatType Stat;
    private readonly int Bonus;
    private readonly ModifierDescriptor Descriptor;

    internal IncreaseStatBonus(StatType stat, int bonus, ModifierDescriptor descriptor)
    {
      Stat = stat;
      Bonus = bonus;
      Descriptor = descriptor;
    }

    public override void OnTurnOn()
    {
      try
      {
        var wielder = Owner.Wielder;
        if (wielder is null)
        {
          Logger.Warning("No owner!");
          return;
        }

        var stat = wielder.Stats.GetStat(Stat);
        if (stat is null)
        {
          Logger.Warning($"{Owner} has no stat: {Stat}!");
          return;
        }

        var modifier =
          new ModifiableValue.Modifier()
          {
            ModValue = Bonus,
            ModDescriptor = Descriptor,
            StackMode = ModifiableValue.StackMode.ForceStack,
            Source = Fact,
            SourceComponent = Runtime.SourceBlueprintComponentName,
            ItemSource = Owner
          };
        stat.AddModifier(modifier);
      }
      catch (Exception e)
      {
        Logger.LogException("IncreaseStatBonus.OnTurnOn", e);
      }
    }

    public override void OnTurnOff()
    {
      try
      {
        var wielder = Owner.Wielder;
        if (wielder is null)
        {
          Logger.Warning("No owner!");
          return;
        }

        var stat = wielder.Stats.GetStat(Stat);
        if (stat is null)
        {
          Logger.Warning($"{Owner} has no stat: {Stat}!");
          return;
        }

        stat.RemoveModifiersFrom(Runtime);
      }
      catch (Exception e)
      {
        Logger.LogException("IncreaseStatBonus.OnTurnOff", e);
      }
    }
  }
}
