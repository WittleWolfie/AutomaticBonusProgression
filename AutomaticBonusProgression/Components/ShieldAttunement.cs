using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("d7779c1b-f19c-4453-a34b-7340d832878b")]
  internal class ShieldAttunement : AttunementEffect
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShieldAttunement));

    internal readonly ArmorProficiencyGroup[] AllowedTypes;

    internal ShieldAttunement(BlueprintBuffReference effectBuff, params ArmorProficiencyGroup[] allowedTypes) : base(effectBuff)
    {
      AllowedTypes = allowedTypes;
    }

    protected override bool AffectsSlot(ItemSlot slot)
    {
      return slot is HandSlot;
    }

    public override bool IsAvailable(UnitDescriptor unit)
    {
      if (!unit.Body.SecondaryHand.HasShield)
        return false;

      var shield = unit.Body.SecondaryHand.Shield.ArmorComponent;
      return AllowedTypes.Contains(shield.ArmorType());
    }
  }
}
