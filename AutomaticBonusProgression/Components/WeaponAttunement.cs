using AutomaticBonusProgression.UI;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("f0ddafd2-07e3-484c-ba80-2a7a94a3337c")]
  internal class WeaponAttunement : AttunementEffect
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponAttunement));

    protected readonly List<WeaponRangeType> AllowedRanges = new();

    internal WeaponAttunement(BlueprintBuffReference effectBuff, int cost, params WeaponRangeType[] allowedRanges)
      : base(effectBuff, cost)
    {
      AllowedRanges.AddRange(allowedRanges);
    }

    public override EnhancementType Type => EnhancementType.MainHand;

    public override bool IsAvailable(UnitDescriptor unit)
    {
      return unit.Body.PrimaryHand.HasWeapon && IsSuitableWeapon(unit.Body.PrimaryHand.Weapon);
    }

    public override string GetRequirements()
    {
      var requirements = string.Join(", ", AllowedRanges.Select(GetLocalizedText).Where(str => !string.IsNullOrEmpty(str)));
      return requirements.Truncate(35);
    }

    protected bool IsSuitableWeapon(ItemEntityWeapon weapon)
    {
      if (!AllowedRanges.Any())
        return true;

      foreach (var range in AllowedRanges)
      {
        if (range.IsSuitableWeapon(weapon))
          return true;
      }
      return false;
    }

    private string GetLocalizedText(WeaponRangeType rangeType)
    {
      return rangeType switch
      {
        WeaponRangeType.Melee => UITool.GetString("Attunement.Weapon.Melee"),
        WeaponRangeType.Ranged => UITool.GetString("Attunement.Weapon.Ranged"),
      };
    }
  }
}
