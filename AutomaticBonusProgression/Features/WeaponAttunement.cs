﻿using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
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
    private const string WeaponDisplayName = "WeaponAttunement.Name";
    private const string WeaponDescription = "WeaponAttunement.Description";

    internal static BlueprintFeature ConfigureWeapon()
    {
      Logger.Log($"Configuring Weapon Attunement");

      return FeatureConfigurator.New(WeaponName, Guids.WeaponAttunement)
        .SetIsClassFeature()
        .SetDisplayName(WeaponDisplayName)
        .SetDescription(WeaponDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddComponent<NaturalEnhancementBonus>()
        .Configure();
    }

    private const string OffHandName = "OffHandAttunement";
    private const string OffHandDisplayName = "OffHandAttunement.Name";
    private const string OffHandDescription = "OffHandAttunement.Description";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log($"Configuring OffHand Attunement");

      return FeatureConfigurator.New(OffHandName, Guids.OffHandAttunement)
        .SetIsClassFeature()
        .SetDisplayName(OffHandDisplayName)
        .SetDescription(OffHandDescription)
        //.SetIcon()
        .SetRanks(4)
        .Configure();
    }

    [TypeId("4af2e953-6831-4bae-92a6-28621272f010")]
    private class NaturalEnhancementBonus
      : UnitFactComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RuleCalculateDamage>,
      IInitiatorRulebookHandler<RuleCalculateAttackBonusWithoutTarget>
    {
      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (!CheckWeapon(evt.Weapon))
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(evt.Weapon);
          evt.Enhancement += bonus;
          evt.EnhancementTotal += bonus;
        }
        catch (Exception e)
        {
          Logger.LogException("NaturalEnhancementBonus.OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateDamage evt)
      {
        try
        {
          if (!CheckWeapon(evt.DamageBundle.Weapon))
            return;

          var damage = evt.DamageBundle.WeaponDamage as PhysicalDamage;
          if (damage == null)
            return;

          var bonus = GameHelper.GetItemEnhancementBonus(evt.DamageBundle.Weapon);
          damage.Enchantment += bonus;
          damage.EnchantmentTotal += bonus;
        }
        catch (Exception e)
        {
          Logger.LogException("NaturalEnhancementBonus.OnEventAboutToTrigger(RuleCalculateDamage)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget evt)
      {
        try
        {
          if (!CheckWeapon(evt.Weapon))
            return;

          evt.AddModifier(GameHelper.GetItemEnhancementBonus(evt.Weapon), Fact, ModifierDescriptor.Enhancement);
        }
        catch (Exception e)
        {
          Logger.LogException("NaturalEnhancementBonus.OnEventAboutToTrigger(RuleCalculateAttackBonusWithoutTarget)", e);
        }
      }

      private bool CheckWeapon(ItemEntityWeapon weapon)
      {
        return weapon is not null && (weapon.Blueprint.IsNatural || weapon.Blueprint.IsUnarmed);
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
      public void OnEventDidTrigger(RuleCalculateDamage evt) { }
      public void OnEventDidTrigger(RuleCalculateAttackBonusWithoutTarget evt) { }
    }
  }
}