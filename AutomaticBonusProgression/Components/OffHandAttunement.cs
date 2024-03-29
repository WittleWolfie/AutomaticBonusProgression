﻿using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic;

namespace AutomaticBonusProgression.Components
{
  [TypeId("ed3eaba1-600b-439d-a913-e5d323b58577")]
  internal class OffHandAttunement : WeaponAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(OffHandAttunement));

    internal OffHandAttunement(
      BlueprintBuffReference effectBuff,
      int cost,
      WeaponRangeType[] allowedRanges,
      PhysicalDamageForm[] allowedForms,
      bool onlyLightWeapons = false,
      bool onlyHeavyWeapons = false)
      : base(effectBuff, cost, allowedRanges, allowedForms, onlyLightWeapons, onlyHeavyWeapons) { }

    public override EnhancementType Type => EnhancementType.OffHand;

    public override bool IsAvailable(UnitDescriptor unit)
    {
      var weapon = Common.GetSecondaryWeapon(unit);
      return weapon is not null && IsSuitableWeapon(weapon);
    }
  }
}
