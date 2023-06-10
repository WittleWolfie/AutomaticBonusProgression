using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using System;

namespace AutomaticBonusProgression.Features
{
  internal class WeaponAttunement
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(WeaponAttunement));

    private const string WeaponName = "WeaponAttunement";
    private const string WeaponBaseName = "WeaponAttunement.Base";
    private const string WeaponDisplayName = "WeaponAttunement.Name";
    private const string WeaponDescription = "WeaponAttunement.Description";

    internal static BlueprintFeature ConfigureWeapon()
    {
      Logger.Log($"Configuring Weapon Attunement");

      var effect = FeatureConfigurator.New(WeaponName, Guids.WeaponAttunement)
        .SetIsClassFeature()
        .SetRanks(5)
        .SetHideInUI()
        .AddComponent<WeaponEnhancementBonus>()
        .Configure();
      return FeatureConfigurator.New(WeaponBaseName, Guids.WeaponAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(WeaponDisplayName)
        .SetDescription(WeaponDescription)
        .SetIcon(AbilityRefs.HolySword.Reference.Get().Icon)
        .SetRanks(5)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    private const string OffHandName = "OffHandAttunement";
    private const string OffHandBaseName = "OffHandAttunement.Base";
    private const string OffHandDisplayName = "OffHandAttunement.Name";
    private const string OffHandDescription = "OffHandAttunement.Description";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log($"Configuring OffHand Attunement");

      var effect = FeatureConfigurator.New(OffHandName, Guids.OffHandAttunement)
        .SetIsClassFeature()
        .SetRanks(4)
        .SetHideInUI()
        .AddHideFeatureInInspect()
        .Configure();
      return FeatureConfigurator.New(OffHandBaseName, Guids.OffHandAttunementBase)
        .SetIsClassFeature()
        .SetDisplayName(OffHandDisplayName)
        .SetDescription(OffHandDescription)
        .SetIcon(BuffRefs.ArcaneAccuracyBuff.Reference.Get().Icon)
        .SetRanks(4)
        .AddComponent(new AddFeatureABP(effect))
        .AddHideFeatureInInspect()
        .Configure();
    }

    [TypeId("4af2e953-6831-4bae-92a6-28621272f010")]
    private class WeaponEnhancementBonus
      : UnitFactComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RuleCalculateDamage>,
      IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>
    {
      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (ShouldIgnore(evt.Weapon))
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(evt.Weapon);
          Logger.Verbose(() => $"Adding {bonus} to enhancement (stats)");
          evt.Enhancement.AddModifier(new(bonus, Fact, ModifierDescriptor.Enhancement));
          evt.EnhancementTotal += bonus;
        }
        catch (Exception e)
        {
          Logger.LogException("WeaponEnhancementBonus.OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateDamage evt)
      {
        try
        {
          if (ShouldIgnore(evt.DamageBundle.Weapon))
            return;

          if (evt.DamageBundle.WeaponDamage is not PhysicalDamage damage)
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(evt.DamageBundle.Weapon);
          Logger.Verbose(() => $"Adding {bonus} to enhancement (damage)");
          damage.Enchantment += bonus;
          damage.EnchantmentTotal += bonus;
        }
        catch (Exception e)
        {
          Logger.LogException("WeaponEnhancementBonus.OnEventAboutToTrigger(RuleCalculateDamage)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
      {
        try
        {
          if (ShouldIgnore(evt.Weapon))
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(evt.Weapon);
          Logger.Verbose(() => $"Adding {bonus} to enhancement (attack w/o target)");
          evt.AddModifier(bonus, Fact, ModifierDescriptor.Enhancement);
        }
        catch (Exception e)
        {
          Logger.LogException("WeaponEnhancementBonus.OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget)", e);
        }
      }

      private bool ShouldIgnore(ItemEntityWeapon weapon)
      {
        if (weapon is null)
        {
          Logger.Verbose(() => "Missing weapon!");
          return true;
        }

        return false;
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
      public void OnEventDidTrigger(RuleCalculateDamage evt) { }
      public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt) { }
    }
  }
}
