using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using static Kingmaker.RuleSystem.RulebookEvent;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Culling
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Culling));

    private const string EnchantName = "LW.Culling.Enchant";
    private const string EffectName = "LW.Culling";
    private const string BuffName = "LW.Culling.Buff";
    private const string OffHandEffectName = "LW.Culling.OffHand";
    private const string OffHandBuffName = "LW.Culling.OffHand.Buff";

    private const string DisplayName = "LW.Culling.Name";
    private const string Description = "LW.Culling.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Culling");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.CullingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<CullingComponent>()
        .Configure();

      var icon = FeatureRefs.IntimidatingProwess.Reference.Get().Icon;
      var enchantInfo =
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          allowedRanges: new() { WeaponRangeType.Melee },
          allowedForms: new() { PhysicalDamageForm.Slashing });
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.CullingEffect, enchant),
        parentBuff: new(BuffName, Guids.CullingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.CullingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.CullingOffHandBuff));
    }

    [TypeId("dd47b691-9f57-44b2-9b12-673d6369e874")]
    private class CullingComponent :
      ItemEnchantmentComponentDelegate,
      IInitiatorRulebookHandler<RulePrepareDamage>
    {
      private static readonly CustomDataKey DuplicateKey = new("Culling.Duplicate");

      private static BlueprintAbility _cleave;
      private static BlueprintAbility Cleave
      {
        get
        {
          _cleave ??= AbilityRefs.CleaveAction.Reference.Get();
          return _cleave;
        }
      }
      private static BlueprintFeature _cleavingFinish;
      private static BlueprintFeature CleavingFinish
      {
        get
        {
          _cleavingFinish ??= FeatureRefs.CleavingFinish.Reference.Get();
          return _cleavingFinish;
        }
      }

      public void OnEventAboutToTrigger(RulePrepareDamage evt)
      {
        try
        {
          if (evt.DamageBundle.Weapon != Owner)
            return;


          var source = evt.Reason?.Context?.AssociatedBlueprint;
          if (source is null)
            return;

          if (source != Cleave && source != CleavingFinish)
            return;

          // Prevent multiple enchantments from applying the bonus
          if (evt.TryGetCustomData(DuplicateKey, out bool _))
          {
            Logger.Verbose(() => "Ignoring duplicate trigger");
            return;
          }
          evt.SetCustomData(DuplicateKey, true);

          Logger.Verbose(() => $"Adding dice to attack: {source.name}");
          var damageDescription = new DamageDescription();
          damageDescription.SourceFact = Enchantment;
          damageDescription.Dice = new DiceFormula(rollsCount: 2, diceType: DiceType.D6);

          var weaponDamage = evt.DamageBundle.First;
          damageDescription.TypeDescription = weaponDamage?.CreateTypeDescription();
          var damage = damageDescription.CreateDamage();
          damage.CriticalModifier = weaponDamage?.CriticalModifier;
          evt.Add(damage);
        }
        catch (Exception e)
        {
          Logger.LogException("CullingComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RulePrepareDamage evt) { }
    }
  }
}
