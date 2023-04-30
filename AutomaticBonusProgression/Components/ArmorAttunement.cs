﻿using AutomaticBonusProgression.UI;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;
using System;
using System.Linq;

namespace AutomaticBonusProgression.Components
{
  [TypeId("4b835824-d53d-470b-8e7b-a8acb450005a")]
  internal class ArmorAttunement : AttunementEffect
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ArmorAttunement));

    protected readonly ArmorProficiencyGroup[] AllowedTypes;

    internal ArmorAttunement(BlueprintBuffReference effectBuff, params ArmorProficiencyGroup[] allowedTypes) : base(effectBuff)
    {
      AllowedTypes = allowedTypes;
    }

    protected override bool AffectsSlot(ItemSlot slot)
    {
      return slot is ArmorSlot;
    }

    public override bool IsAvailable(UnitDescriptor unit)
    {
      var armor = unit.Body.Armor;
      var armorType = armor.HasArmor ? armor.Armor.ArmorType() : ArmorProficiencyGroup.Light;
      return AllowedTypes.Contains(armorType);
    }

    public override string GetRequirements()
    {
      var requirements = string.Join(", ", AllowedTypes.Select(GetLocalizedText).Where(str => !string.IsNullOrEmpty(str)));
      return requirements.Truncate(35);
    }

    protected virtual string GetLocalizedText(ArmorProficiencyGroup type)
    {
      return type switch
      {
        ArmorProficiencyGroup.Light => UITool.GetString("Attunement.Armor.Light"),
        ArmorProficiencyGroup.Medium => UITool.GetString("Attunement.Armor.Medium"),
        ArmorProficiencyGroup.Heavy => UITool.GetString("Attunement.Armor.Heavy"),
        ArmorProficiencyGroup.Buckler => "",
        ArmorProficiencyGroup.LightShield => "",
        ArmorProficiencyGroup.HeavyShield => "",
        ArmorProficiencyGroup.TowerShield => "",
        _ => throw new InvalidOperationException($"Unsupported armor proficiency type: {type}")
      };
    }
  }
}
