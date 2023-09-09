using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Limning
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Limning));

    private const string EnchantName = "LW.Limning.Enchant";
    private const string EffectName = "LW.Limning";
    private const string BuffName = "LW.Limning.Buff";
    private const string OffHandEffectName = "LW.Limning.OffHand";
    private const string OffHandBuffName = "LW.Limning.OffHand.Buff";

    private const string DisplayName = "LW.Limning.Name";
    private const string Description = "LW.Limning.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Limning");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.LimningEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<LimningComponent>()
        .Configure();

      var faerieFire = AbilityRefs.FaerieFire.Reference.Get();
      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, faerieFire.Icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.LimningEffect, enchant),
        parentBuff: new(BuffName, Guids.LimningBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.LimningOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.LimningOffHandBuff));
    }

    [TypeId("808741d6-c0ae-4e5d-b0d6-5096e377550f")]
    private class LimningComponent : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
      private static BlueprintBuff _faerieFire;
      private static BlueprintBuff FaerieFire
      {
        get
        {
          _faerieFire ??= BuffRefs.FaerieFireBuff.Reference.Get();
          return _faerieFire;
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
          if (!attackRoll.IsHit)
            return;

          var targetIsInvisible =
            target.State.HasCondition(UnitCondition.Invisible)
              || target.State.HasCondition(UnitCondition.GreaterInvisibility);
          var targetHasConcealment = target.Get<UnitPartConcealment>()?.m_Concealments?.Count > 0;

          if (!targetIsInvisible && !targetHasConcealment)
            return;

          Logger.Verbose(() => $"Revealing {target.CharacterName} for 1 round");
          target.AddBuff(FaerieFire, Context, 1.Rounds().Seconds);
        }
        catch (Exception e)
        {
          Logger.LogException("LimningComponent.OnEventDidTrigger(RuleAttackWithWeapon)", e);
        }
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt) { }
    }
  }
}
