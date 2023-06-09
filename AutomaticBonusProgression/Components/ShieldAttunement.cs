using AutomaticBonusProgression.UI;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("d7779c1b-f19c-4453-a34b-7340d832878b")]
  internal class ShieldAttunement : ArmorAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ShieldAttunement));

    internal ShieldAttunement(BlueprintBuffReference effectBuff, int cost, params ArmorProficiencyGroup[] allowedTypes)
      : base(effectBuff, cost, allowedTypes) { }

    public override EnhancementType Type => EnhancementType.Shield;

    public override bool IsAvailable(UnitDescriptor unit)
    {
      if (!unit.Body.SecondaryHand.HasShield)
        return false;

      if (!AllowedTypes.Any())
        return true;

      var shield = unit.Body.SecondaryHand.Shield.ArmorComponent;
      return AllowedTypes.Contains(shield.ArmorType());
    }

    protected override string GetLocalizedText(ArmorProficiencyGroup type)
    {
      return type switch
      {
        ArmorProficiencyGroup.Light => "",
        ArmorProficiencyGroup.Medium => "",
        ArmorProficiencyGroup.Heavy => "",
        ArmorProficiencyGroup.Buckler => UITool.GetString("Shield.Buckler"),
        ArmorProficiencyGroup.LightShield => UITool.GetString("Shield.Light"),
        ArmorProficiencyGroup.HeavyShield => UITool.GetString("Shield.Heavy"),
        ArmorProficiencyGroup.TowerShield => UITool.GetString("Shield.Tower"),
        _ => throw new InvalidOperationException($"Unsupported armor proficiency type: {type}")
      };
    }
  }
}
