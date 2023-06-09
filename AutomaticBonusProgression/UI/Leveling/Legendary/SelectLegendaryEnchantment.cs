using AutomaticBonusProgression.Util;
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

    public LevelUpActionPriority Priority => LevelUpActionPriority.AddAttribute;

    public bool NeedUpdateUnitView => false;

    public void Apply(LevelUpState state, UnitDescriptor unit)
    {
      switch (Type)
      {
        case EnchantmentType.Armor:
          unit.AddFact(Common.LegendaryArmor);
          break;
        case EnchantmentType.Shield:
          unit.AddFact(Common.LegendaryShield);
          break;
        case EnchantmentType.Weapon:
          unit.AddFact(Common.LegendaryWeapon);
          break;
        case EnchantmentType.OffHand:
          unit.AddFact(Common.LegendaryOffHand);
          break;
      }
    }

    public bool Check(LevelUpState state, UnitDescriptor unit)
    {
      return State.CanAddLegendaryEnchantment(Type, checkGifts: false);
    }

    public void PostLoad() { }
  }
}
