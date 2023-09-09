using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Controllers;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Ominous
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Ominous));

    private const string EnchantName = "LW.Ominous.Enchant";
    private const string EffectName = "LW.Ominous";
    private const string BuffName = "LW.Ominous.Buff";
    private const string OffHandEffectName = "LW.Ominous.OffHand";
    private const string OffHandBuffName = "LW.Ominous.OffHand.Buff";

    private const string DisplayName = "LW.Ominous.Name";
    private const string Description = "LW.Ominous.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Ominous");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.OminousEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<OminousComponent>()
        .Configure();

      var shaken = BuffRefs.Shaken.Reference.Get();
      var enchantInfo =
        new WeaponEnchantInfo(DisplayName, Description, shaken.Icon, EnhancementCost, WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.OminousEffect, enchant),
        parentBuff: new(BuffName, Guids.OminousBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.OminousOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.OminousOffHandBuff));
    }

    [TypeId("8b225fed-ad26-4685-aeb7-5d6f6dc52ef5")]
    private class OminousComponent : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private static BlueprintBuff _shaken;
      private static BlueprintBuff Shaken
      {
        get
        {
          _shaken ??= BuffRefs.Shaken.Reference.Get();
          return _shaken;
        }
      }

      public override void OnTurnOn()
      {
        try
        {
          var wielder = Owner.Wielder;
          if (wielder is null)
          {
            Logger.Warning("No wielder!");
            return;
          }

          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          Logger.Verbose(() => $"Adding +{bonus} to intimidate for {wielder.CharacterName}");
          wielder.Stats.GetStat(StatType.CheckIntimidate)
            .AddItemModifierUnique(bonus, Runtime, Owner, ModifierDescriptor.UniqueItem);
        }
        catch (Exception e)
        {
          Logger.LogException("OminousComponent.OnTurnOn", e);
        }
      }

      public override void OnTurnOff()
      {
        try
        {
          var wielder = Owner.Wielder;
          if (wielder is null)
          {
            Logger.Warning("No wielder!");
            return;
          }

          Logger.Verbose(() => $"Removing intimidate bonus from {wielder.CharacterName}");
          wielder.Stats.GetStat(StatType.CheckIntimidate).RemoveModifiersFrom(Runtime);
        }
        catch (Exception e)
        {
          Logger.LogException("OminousComponent.OnTurnOff", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var target = evt.Target;
          if (target is null)
            return;

          var attackRoll = evt.AttackRoll;
          if (!attackRoll.IsHit || !attackRoll.IsCriticalConfirmed)
            return;

          var durationMinutes = evt.WeaponStats.CriticalMultiplier - 1;
          Logger.Verbose(() => $"Applying shaken to {target.CharacterName} for {durationMinutes} minutes");
          target.AddBuff(Shaken, Context, durationMinutes.Minutes());
        }
        catch (Exception e)
        {
          Logger.LogException("OminousComponent.OnEventDidTrigger", e);
        }
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }
    }
  }
}
