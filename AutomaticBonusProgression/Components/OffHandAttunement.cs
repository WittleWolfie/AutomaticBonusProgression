using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;

namespace AutomaticBonusProgression.Components
{
  [TypeId("ed3eaba1-600b-439d-a913-e5d323b58577")]
  internal class OffHandAttunement : AttunementEffect
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(OffHandAttunement));

    internal OffHandAttunement(BlueprintBuffReference effectBuff) : base(effectBuff) { }

    protected override bool AffectsSlot(ItemSlot slot)
    {
      return slot is HandSlot;
    }

    public override bool IsAvailable(UnitDescriptor unit)
    {
      return unit.Body.SecondaryHand.HasWeapon;
    }
  }
}
