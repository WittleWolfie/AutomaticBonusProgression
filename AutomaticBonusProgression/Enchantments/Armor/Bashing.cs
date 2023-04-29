using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Bashing
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bashing));

    private const string EffectName = "LA.Bashing.Effect";
    private const string BuffName = "LA.Bashing.Buff";

    private const string DisplayName = "LA.Bashing.Name";
    private const string Description = "LA.Bashing.Description";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring BashingEffect");

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ArmorProficiencyGroup.LightShield,
        ArmorProficiencyGroup.HeavyShield);

      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.BashingEffect, new BashingComponent()),
        variantBuff: new(BuffName, Guids.BashingBuff));
    }

    [TypeId("db92a7db-5c80-461c-9744-0faa92d92ce6")]
    private class BashingComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (!evt.Weapon.IsShield)
            return;

          Logger.Verbose(() => $"Increasing weapon size of shield for {Owner.CharacterName}");
          evt.IncreaseWeaponSize(1);
        }
        catch (Exception e)
        {
          Logger.LogException("BashingComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
    }
  }
}
