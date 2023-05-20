using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Weapon
{
  internal class Elemental
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Elemental));

    #region Too Many Constants
    private const string CorrosiveName = "LW.Elemental.Corrosive.Name";
    private const string CorrosiveBuff = "LW.Elemental.Corrosive.Buff";
    private const string CorrosiveEffect = "LW.Elemental.Corrosive.Effect";
    private const string CorrosiveOffHandBuff = "LW.Elemental.Corrosive.OffHand.Buff";
    private const string CorrosiveOffHandEffect = "LW.Elemental.Corrosive.OffHand.Effect";

    private const string CorrosiveBurstName = "LW.Elemental.Corrosive.Burst.Name";
    private const string CorrosiveBurstBuff = "LW.Elemental.Corrosive.Burst.Buff";
    private const string CorrosiveBurstEffect = "LW.Elemental.Corrosive.Burst.Effect";
    private const string CorrosiveBurstOffHandBuff = "LW.Elemental.Corrosive.Burst.OffHand.Buff";
    private const string CorrosiveBurstOffHandEffect = "LW.Elemental.Corrosive.Burst.OffHand.Effect";

    private const string FlamingName = "LW.Elemental.Flaming.Name";
    private const string FlamingBuff = "LW.Elemental.Flaming.Buff";
    private const string FlamingEffect = "LW.Elemental.Flaming.Effect";
    private const string FlamingOffHandBuff = "LW.Elemental.Flaming.OffHand.Buff";
    private const string FlamingOffHandEffect = "LW.Elemental.Flaming.OffHand.Effect";

    private const string FlamingBurstName = "LW.Elemental.Flaming.Burst.Name";
    private const string FlamingBurstBuff = "LW.Elemental.Flaming.Burst.Buff";
    private const string FlamingBurstEffect = "LW.Elemental.Flaming.Burst.Effect";
    private const string FlamingBurstOffHandBuff = "LW.Elemental.Flaming.Burst.OffHand.Buff";
    private const string FlamingBurstOffHandEffect = "LW.Elemental.Flaming.Burst.OffHand.Effect";

    private const string FrostName = "LW.Elemental.Frost.Name";
    private const string FrostBuff = "LW.Elemental.Frost.Buff";
    private const string FrostEffect = "LW.Elemental.Frost.Effect";
    private const string FrostOffHandBuff = "LW.Elemental.Frost.OffHand.Buff";
    private const string FrostOffHandEffect = "LW.Elemental.Frost.OffHand.Effect";

    private const string FrostBurstName = "LW.Elemental.Frost.Burst.Name";
    private const string FrostBurstBuff = "LW.Elemental.Frost.Burst.Buff";
    private const string FrostBurstEffect = "LW.Elemental.Frost.Burst.Effect";
    private const string FrostBurstOffHandBuff = "LW.Elemental.Frost.Burst.OffHand.Buff";
    private const string FrostBurstOffHandEffect = "LW.Elemental.Frost.Burst.OffHand.Effect";

    private const string ShockingName = "LW.Elemental.Shocking.Name";
    private const string ShockingBuff = "LW.Elemental.Shocking.Buff";
    private const string ShockingEffect = "LW.Elemental.Shocking.Effect";
    private const string ShockingOffHandBuff = "LW.Elemental.Shocking.OffHand.Buff";
    private const string ShockingOffHandEffect = "LW.Elemental.Shocking.OffHand.Effect";

    private const string ShockingBurstName = "LW.Elemental.Shocking.Burst.Name";
    private const string ShockingBurstBuff = "LW.Elemental.Shocking.Burst.Buff";
    private const string ShockingBurstEffect = "LW.Elemental.Shocking.Burst.Effect";
    private const string ShockingBurstOffHandBuff = "LW.Elemental.Shocking.Burst.OffHand.Buff";
    private const string ShockingBurstOffHandEffect = "LW.Elemental.Shocking.Burst.OffHand.Effect";

    private const string ThunderingName = "LW.Elemental.Thundering.Name";
    private const string ThunderingBuff = "LW.Elemental.Thundering.Buff";
    private const string ThunderingEffect = "LW.Elemental.Thundering.Effect";
    private const string ThunderingOffHandBuff = "LW.Elemental.Thundering.OffHand.Buff";
    private const string ThunderingOffHandEffect = "LW.Elemental.Thundering.OffHand.Effect";

    private const string ThunderingBurstName = "LW.Elemental.Thundering.Burst.Name";
    private const string ThunderingBurstBuff = "LW.Elemental.Thundering.Burst.Buff";
    private const string ThunderingBurstEffect = "LW.Elemental.Thundering.Burst.Effect";
    private const string ThunderingBurstOffHandBuff = "LW.Elemental.Thundering.Burst.OffHand.Buff";
    private const string ThunderingBurstOffHandEffect = "LW.Elemental.Thundering.Burst.OffHand.Effect";
    #endregion

    private const int EnhancementCost = 1;
    private const int BurstEnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Elemental");

      // Corrosive
      var corrosive = WeaponEnchantmentRefs.Corrosive.Reference.Get();
      var corrosiveEnchantInfo = new WeaponEnchantInfo(CorrosiveName, corrosive.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        corrosiveEnchantInfo,
        effectBuff: GetBuffInfo(CorrosiveEffect, Guids.CorrosiveEffect, DamageEnergyType.Acid),
        parentBuff: new(CorrosiveBuff, Guids.CorrosiveBuff));
      EnchantTool.CreateVariantEnchant(
        corrosiveEnchantInfo,
        effectBuff: GetBuffInfo(
          CorrosiveOffHandEffect,
          Guids.CorrosiveOffHandEffect,
          DamageEnergyType.Acid,
          toPrimaryWeapon: false),
        variantBuff: new(CorrosiveOffHandBuff, Guids.CorrosiveOffHandBuff));

      // Corrosive Burst
      var corrosiveBurst = WeaponEnchantmentRefs.CorrosiveBurst.Reference.Get();
      var corrosiveBurstEnchantInfo =
        new WeaponEnchantInfo(CorrosiveBurstName, corrosiveBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        corrosiveEnchantInfo,
        effectBuff: GetBuffInfo(CorrosiveBurstEffect, Guids.CorrosiveBurstEffect, DamageEnergyType.Acid),
        parentBuff: new(CorrosiveBurstBuff, Guids.CorrosiveBurstBuff));
      EnchantTool.CreateVariantEnchant(
        corrosiveBurstEnchantInfo,
        effectBuff: GetBuffInfo(
          CorrosiveBurstOffHandEffect,
          Guids.CorrosiveBurstOffHandEffect,
          DamageEnergyType.Acid,
          isBurst: true,
          toPrimaryWeapon: false),
        variantBuff: new(CorrosiveBurstOffHandBuff, Guids.CorrosiveBurstOffHandBuff));

      // Flaming
      var flaming = WeaponEnchantmentRefs.Flaming.Reference.Get();
      var flamingEnchantInfo = new WeaponEnchantInfo(FlamingName, flaming.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        flamingEnchantInfo,
        effectBuff: GetBuffInfo(FlamingEffect, Guids.FlamingEffect, DamageEnergyType.Fire),
        parentBuff: new(FlamingBuff, Guids.FlamingBuff));
      EnchantTool.CreateVariantEnchant(
        flamingEnchantInfo,
        effectBuff: GetBuffInfo(
          FlamingOffHandEffect,
          Guids.FlamingOffHandEffect,
          DamageEnergyType.Fire,
          toPrimaryWeapon: false),
        variantBuff: new(FlamingOffHandBuff, Guids.FlamingOffHandBuff));

      // Flaming Burst
      var flamingBurst = WeaponEnchantmentRefs.FlamingBurst.Reference.Get();
      var flamingBurstEnchantInfo =
        new WeaponEnchantInfo(FlamingBurstName, flamingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        flamingEnchantInfo,
        effectBuff: GetBuffInfo(FlamingBurstEffect, Guids.FlamingBurstEffect, DamageEnergyType.Fire),
        parentBuff: new(FlamingBurstBuff, Guids.FlamingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        flamingBurstEnchantInfo,
        effectBuff: GetBuffInfo(
          FlamingBurstOffHandEffect,
          Guids.FlamingBurstOffHandEffect,
          DamageEnergyType.Fire,
          isBurst: true,
          toPrimaryWeapon: false),
        variantBuff: new(FlamingBurstOffHandBuff, Guids.FlamingBurstOffHandBuff));

      // Frost
      var frost = WeaponEnchantmentRefs.Frost.Reference.Get();
      var frostEnchantInfo = new WeaponEnchantInfo(FrostName, frost.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        frostEnchantInfo,
        effectBuff: GetBuffInfo(FrostEffect, Guids.FrostEffect, DamageEnergyType.Cold),
        parentBuff: new(FrostBuff, Guids.FrostBuff));
      EnchantTool.CreateVariantEnchant(
        frostEnchantInfo,
        effectBuff: GetBuffInfo(
          FrostOffHandEffect,
          Guids.FrostOffHandEffect,
          DamageEnergyType.Cold,
          toPrimaryWeapon: false),
        variantBuff: new(FrostOffHandBuff, Guids.FrostOffHandBuff));

      // Frost Burst
      var frostBurst = WeaponEnchantmentRefs.IcyBurst.Reference.Get();
      var frostBurstEnchantInfo =
        new WeaponEnchantInfo(FrostBurstName, frostBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        frostEnchantInfo,
        effectBuff: GetBuffInfo(FrostBurstEffect, Guids.FrostBurstEffect, DamageEnergyType.Cold),
        parentBuff: new(FrostBurstBuff, Guids.FrostBurstBuff));
      EnchantTool.CreateVariantEnchant(
        frostBurstEnchantInfo,
        effectBuff: GetBuffInfo(
          FrostBurstOffHandEffect,
          Guids.FrostBurstOffHandEffect,
          DamageEnergyType.Cold,
          isBurst: true,
          toPrimaryWeapon: false),
        variantBuff: new(FrostBurstOffHandBuff, Guids.FrostBurstOffHandBuff));

      // Shocking
      var shocking = WeaponEnchantmentRefs.Shock.Reference.Get();
      var shockingEnchantInfo = new WeaponEnchantInfo(ShockingName, shocking.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        shockingEnchantInfo,
        effectBuff: GetBuffInfo(ShockingEffect, Guids.ShockingEffect, DamageEnergyType.Electricity),
        parentBuff: new(ShockingBuff, Guids.ShockingBuff));
      EnchantTool.CreateVariantEnchant(
        shockingEnchantInfo,
        effectBuff: GetBuffInfo(
          ShockingOffHandEffect,
          Guids.ShockingOffHandEffect,
          DamageEnergyType.Electricity,
          toPrimaryWeapon: false),
        variantBuff: new(ShockingOffHandBuff, Guids.ShockingOffHandBuff));

      // Shocking Burst
      var shockingBurst = WeaponEnchantmentRefs.ShockingBurst.Reference.Get();
      var shockingBurstEnchantInfo =
        new WeaponEnchantInfo(ShockingBurstName, shockingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        shockingEnchantInfo,
        effectBuff: GetBuffInfo(ShockingBurstEffect, Guids.ShockingBurstEffect, DamageEnergyType.Electricity),
        parentBuff: new(ShockingBurstBuff, Guids.ShockingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        shockingBurstEnchantInfo,
        effectBuff: GetBuffInfo(
          ShockingBurstOffHandEffect,
          Guids.ShockingBurstOffHandEffect,
          DamageEnergyType.Electricity,
          isBurst: true,
          toPrimaryWeapon: false),
        variantBuff: new(ShockingBurstOffHandBuff, Guids.ShockingBurstOffHandBuff));

      // Thundering
      var thundering = WeaponEnchantmentRefs.Thundering.Reference.Get();
      var thunderingEnchantInfo = new WeaponEnchantInfo(ThunderingName, thundering.m_Description, "", EnhancementCost);
      EnchantTool.CreateEnchant(
        thunderingEnchantInfo,
        effectBuff: GetBuffInfo(ThunderingEffect, Guids.ThunderingEffect, DamageEnergyType.Sonic),
        parentBuff: new(ThunderingBuff, Guids.ThunderingBuff));
      EnchantTool.CreateVariantEnchant(
        thunderingEnchantInfo,
        effectBuff: GetBuffInfo(
          ThunderingOffHandEffect,
          Guids.ThunderingOffHandEffect,
          DamageEnergyType.Sonic,
          toPrimaryWeapon: false),
        variantBuff: new(ThunderingOffHandBuff, Guids.ThunderingOffHandBuff));

      // Thundering Burst
      var thunderingBurst = WeaponEnchantmentRefs.ThunderingBurst.Reference.Get();
      var thunderingBurstEnchantInfo =
        new WeaponEnchantInfo(ThunderingBurstName, thunderingBurst.m_Description, "", BurstEnhancementCost);
      EnchantTool.CreateEnchant(
        thunderingEnchantInfo,
        effectBuff: GetBuffInfo(ThunderingBurstEffect, Guids.ThunderingBurstEffect, DamageEnergyType.Sonic),
        parentBuff: new(ThunderingBurstBuff, Guids.ThunderingBurstBuff));
      EnchantTool.CreateVariantEnchant(
        thunderingBurstEnchantInfo,
        effectBuff: GetBuffInfo(
          ThunderingBurstOffHandEffect,
          Guids.ThunderingBurstOffHandEffect,
          DamageEnergyType.Sonic,
          isBurst: true,
          toPrimaryWeapon: false),
        variantBuff: new(ThunderingBurstOffHandBuff, Guids.ThunderingBurstOffHandBuff));

      // In-game "X Burst" enchantments don't actually implement X + Burst. All weapons just get X & X Burst. To
      // calculate the bonus correctly then just treat each as a +1.
      EnchantTool.AddEnhancementEquivalenceWeapon(corrosive, corrosiveEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(corrosiveBurst, corrosiveEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(flaming, flamingEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(flamingBurst, flamingEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(frost, frostEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(frostBurst, frostEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(shocking, shockingEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(shockingBurst, shockingEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(thundering, thunderingEnchantInfo);
      EnchantTool.AddEnhancementEquivalenceWeapon(thunderingBurst, thunderingEnchantInfo);
    }

    private static BlueprintInfo GetBuffInfo(
      string name,
      string guid,
      DamageEnergyType element,
      bool isBurst = false,
      bool toPrimaryWeapon = true)
    {
      return new(name, guid, new ElementalComponent(element, isBurst, toPrimaryWeapon));
    }

    [TypeId("0c13c656-b835-4573-82d7-9a12f27c60df")]
    private class ElementalComponent :
      UnitBuffComponentDelegate,
      IInitiatorRulebookHandler<RuleCalculateWeaponStats>,
      IInitiatorRulebookHandler<RuleDealDamage>
    {
      private static readonly DiceFormula DamageDice = new(rollsCount: 1, DiceType.D6);

      private readonly DamageEnergyType Element;
      private readonly bool IsBurst;
      private readonly bool ToPrimaryWeapon;

      internal ElementalComponent(DamageEnergyType element,  bool isBurst, bool toPrimaryWeapon)
      {
        Element = element;
        IsBurst = isBurst;
        ToPrimaryWeapon = toPrimaryWeapon;
      }

      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          var isPrimaryWeapon = Common.IsPrimaryWeapon(evt.Weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {evt.Weapon.Name}");
            return;
          }

          var damageType = DamageTypes.Energy(Element);
          var damage =
            new DamageDescription()
            {
              TypeDescription = DamageTypes.Energy(Element),
              Dice = DamageDice,
              SourceFact = Buff
            };
          evt.DamageDescription.Add(damage);
        }
        catch (Exception e)
        {
          Logger.LogException("ElementalComponent.OnEventAboutToTrigger(RuleCalculateWeaponStats)", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }

      public void OnEventAboutToTrigger(RuleDealDamage evt)
      {
        try
        {
          if (!IsBurst)
            return;

          var attack = evt.AttackRoll;
          if (attack is null || !attack.IsCriticalConfirmed || attack.FortificationNegatesCriticalHit)
            return;

          var weapon = attack.Weapon;
          if (weapon is null)
            return;

          var isPrimaryWeapon = Common.IsPrimaryWeapon(weapon);
          if (ToPrimaryWeapon && !isPrimaryWeapon || !ToPrimaryWeapon && isPrimaryWeapon)
          {
            Logger.Verbose(() => $"Wrong weapon: {ToPrimaryWeapon} - {isPrimaryWeapon} - {weapon.Name}");
            return;
          }

          var damageDice = new DiceFormula(Math.Max(attack.WeaponStats.CriticalMultiplier - 1, 1), DiceType.D10);
          evt.Add(new EnergyDamage(damageDice, Element));
        }
        catch (Exception e)
        {
          Logger.LogException("ElementalComponent.OnEventAboutToTrigger(RuleDealDamage)", e);
        }
      }

      public void OnEventDidTrigger(RuleDealDamage evt) { }
    }
  }
}