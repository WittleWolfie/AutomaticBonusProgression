using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder.ContextEx;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Furious
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Furious));

    private const string FuriousName = "LW.Furious.Name";
    private const string FuriousBuff = "LW.Furious.Buff";
    private const string FuriousEffect = "LW.Furious.Effect";
    private const string FuriousOffHandBuff = "LW.Furious.OffHand.Buff";
    private const string FuriousOffHandEffect = "LW.Furious.OffHand.Effect";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Furious");

      var furious = WeaponEnchantmentRefs.Furious.Reference.Get();
      var furiousEnchantInfo = new WeaponEnchantInfo(FuriousName, furious.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        furiousEnchantInfo,
        effectBuff: GetBuffInfo(FuriousEffect, Guids.FuriousEffect),
        parentBuff: new(FuriousBuff, Guids.FuriousBuff));
      EnchantTool.CreateVariantEnchant(
        furiousEnchantInfo,
        effectBuff: GetBuffInfo(
          FuriousOffHandEffect,
          Guids.FuriousOffHandEffect,
          toPrimaryWeapon: false),
        variantBuff: new(FuriousOffHandBuff, Guids.FuriousOffHandBuff));

      EnchantTool.AddEnhancementEquivalenceWeapon(furious, furiousEnchantInfo);
    }

    private static BlueprintInfo GetBuffInfo(
      string name,
      string guid,
      bool toPrimaryWeapon = true)
    {
      return new(name, guid, new FuriousComponent(toPrimaryWeapon));
    }

    [TypeId("7e5adc83-e9fd-48c3-a070-362564d3b109")]
    private class FuriousComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
      private static readonly BlueprintBuffReference Rage =
        BuffRefs.StandartRageBuff.Cast<BlueprintBuffReference>().Reference;
      private static readonly BlueprintBuffReference RageSpell =
        BuffRefs.RageSpellBuff.Cast<BlueprintBuffReference>().Reference;

      private readonly bool ToPrimaryWeapon;

      internal FuriousComponent(bool toPrimaryWeapon)
      {
        ToPrimaryWeapon = toPrimaryWeapon;
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (evt.AttackWithWeapon is null)
            return;

          if (!Owner.HasFact(Rage) && !Owner.HasFact(RageSpell))
          {
            Logger.Verbose(() => $"{Owner.CharacterName} is not under the effects of rage");
            return;
          }

          var isPrimaryWeapon = Common.IsPrimaryWeapon(evt.Weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {evt.Weapon.Name}");
            return;
          }

          var target = evt.AttackWithWeapon.Target;
          if (target is null)
          {
            Logger.Warning("No target! (attack w/ weapon)");
            return;
          }

          evt.Enhancement.AddExtraModifier(new(2, Fact, ModifierDescriptor.UntypedStackable));
        }
        catch (Exception e)
        {
          Logger.LogException("FuriousComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
    }
  }
}