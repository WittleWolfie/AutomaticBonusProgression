using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Components;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;

namespace AutomaticBonusProgression.Components
{
  [AllowedOn(typeof(BlueprintItemEquipment))]
  [TypeId("50f5372b-4a4b-4ea6-9468-63ab01b0d51e")]
  internal class EquipmentRestrictionNoEquip : EquipmentRestriction
  {
    public override bool CanBeEquippedBy(UnitDescriptor unit)
    {
      return false;
    }
  }
}
