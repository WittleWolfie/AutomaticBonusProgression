using AutomaticBonusProgression.UI;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
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

    private readonly List<WeaponRangeType> AllowedRanges = new();
    private readonly List<PhysicalDamageForm> AllowedForms = new();

    internal WeaponAttunement(
      BlueprintBuffReference effectBuff, int cost, WeaponRangeType[] allowedRanges, PhysicalDamageForm[] allowedForms)
      : base(effectBuff, cost)
    {
      AllowedRanges.AddRange(allowedRanges);
      AllowedForms.AddRange(allowedForms);
    }

    public override EnhancementType Type => EnhancementType.MainHand;

    public override bool IsAvailable(UnitDescriptor unit)
    {
      if (unit.Body.PrimaryHand.HasWeapon)
        return IsSuitableWeapon(unit.Body.PrimaryHand.Weapon);
      return IsSuitableWeapon(unit.Body.EmptyHandWeapon);
    }

    public override string GetRequirements()
    {
      var ranges =
        string.Join(", ", AllowedRanges.Select(GetLocalizedText).Where(str => !string.IsNullOrEmpty(str)));
      var forms = string.Join(", ", AllowedForms.Select(GetLocalizedText).Where(str => !string.IsNullOrEmpty(str)));

      var requirements = string.Join("; ", new List<string>() { ranges, forms }.Where(str => !string.IsNullOrEmpty(str)));
      return requirements.Truncate(35);
    }

    protected bool IsSuitableWeapon(ItemEntityWeapon weapon)
    {
      if (weapon is null)
        return false;

      var isAllowedForm = IsAllowedForm(weapon);
      var isAllowedRange = IsAllowedRange(weapon);

      Logger.Verbose(() => $"Is {weapon.Name} suitable for {EffectBuff.NameSafe()}? Form::{isAllowedForm}, Range::{isAllowedRange}");
      return isAllowedForm && isAllowedRange;
    }

    private bool IsAllowedForm(ItemEntityWeapon weapon)
    {
      if (!AllowedForms.Any())
        return true;

      var damageType = weapon.Blueprint.DamageType;
      if (!damageType.IsPhysical)
        return false;

      foreach (var form in AllowedForms)
      {
        if (damageType.Physical.Form.HasFlag(form))
          return true;
      }
      return false;
    }

    private bool IsAllowedRange(ItemEntityWeapon weapon)
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
        WeaponRangeType.Melee => UITool.GetString("Weapon.Melee"),
        WeaponRangeType.Ranged => UITool.GetString("Weapon.Ranged"),
      };
    }

    private string GetLocalizedText(PhysicalDamageForm form)
    {
      return form switch
      {
        PhysicalDamageForm.Piercing => UITool.GetString("Weapon.Piercing"),
        PhysicalDamageForm.Bludgeoning => UITool.GetString("Weapon.Bludgeoning"),
        PhysicalDamageForm.Slashing => UITool.GetString("Weapon.Slashing"),
      };
    }
  }
}
