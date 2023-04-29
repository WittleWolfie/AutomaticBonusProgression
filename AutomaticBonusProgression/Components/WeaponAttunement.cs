using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;

namespace AutomaticBonusProgression.Components
{
  [TypeId("f0ddafd2-07e3-484c-ba80-2a7a94a3337c")]
  internal class WeaponAttunement : AttunementEffect
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponAttunement));

    internal WeaponAttunement(BlueprintBuffReference effectBuff) : base(effectBuff) { }

    protected override bool AffectsSlot(ItemSlot slot)
    {
      return slot is HandSlot;
    }

    public override bool IsAvailable(UnitDescriptor unit)
    {
      return unit.Body.PrimaryHand.HasWeapon;
    }
  }
}
