﻿using AutomaticBonusProgression.Util;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Newtonsoft.Json;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Implements the actual unit effect to grant legendary enchantments.
  /// </summary>
  internal class SelectLegendaryEnchantment : ILevelUpAction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(SelectLegendaryEnchantment));

    internal readonly EnchantmentType Type;
    private readonly LegendaryGiftState State;

    [JsonConstructor]
    public SelectLegendaryEnchantment() { }

    internal SelectLegendaryEnchantment(EnchantmentType type, LegendaryGiftState state)
    {
      Type = type;
      State = state;
    }

    public LevelUpActionPriority Priority => LevelUpActionPriority.Features;

    public bool NeedUpdateUnitView => false;

    public void Apply(LevelUpState state, UnitDescriptor unit)
    {
      Feature feature = null;
      switch (Type)
      {
        case EnchantmentType.Armor:
          feature = unit.AddFact(Common.LegendaryArmor) as Feature;
          break;
        case EnchantmentType.Shield:
          feature = unit.AddFact(Common.LegendaryShield) as Feature;
          break;
        case EnchantmentType.Weapon:
          feature = unit.AddFact(Common.LegendaryWeapon) as Feature;
          break;
        case EnchantmentType.OffHand:
          feature = unit.AddFact(Common.LegendaryOffHand) as Feature;
          break;
      }
      feature!.SetSource(Common.MythicClass, state.NextMythicLevel);
    }

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      Feature feature = null;
      switch (Type)
      {
        case EnchantmentType.Armor:
          feature = unit.GetFeature(Common.LegendaryArmor);
          break;
        case EnchantmentType.Shield:
          feature = unit.GetFeature(Common.LegendaryShield);
          break;
        case EnchantmentType.Weapon:
          feature = unit.GetFeature(Common.LegendaryWeapon);
          break;
        case EnchantmentType.OffHand:
          feature = unit.GetFeature(Common.LegendaryOffHand);
          break;
      }

      if (feature is null)
        return true;

      return feature.Rank < feature.Blueprint.Ranks;
    }

    public void PostLoad() { }
  }
}
