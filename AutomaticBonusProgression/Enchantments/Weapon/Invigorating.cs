using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Invigorating
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Invigorating));

    private const string EnchantName = "LW.Invigorating.Enchant";
    private const string EffectName = "LW.Invigorating";
    private const string BuffName = "LW.Invigorating.Buff";
    private const string OffHandEffectName = "LW.Invigorating.OffHand";
    private const string OffHandBuffName = "LW.Invigorating.OffHand.Buff";

    private const string EffectBuff = "LW.Invigorating.Effect.Buff";
    private const string EffectBuffDescription = "LW.Invigorating.Effect.Description";

    private const string DisplayName = "LW.Invigorating.Name";
    private const string Description = "LW.Invigorating.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Invigorating");

      var exhausted = BuffRefs.Exhausted.Reference.Get();
      var fatigued = BuffRefs.Fatigued.Reference.Get();

      var icon = FeatureRefs.DefensiveStanceFeature.Reference.Get().Icon;
      var buff = BuffConfigurator.New(EffectBuff, Guids.InvigoratingEffectBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(EffectBuffDescription)
        .SetIcon(icon)
        .AddStatBonus(stat: StatType.AdditionalAttackBonus, value: 2, descriptor: ModifierDescriptor.Morale)
        .AddBuffMovementSpeed(value: 10, descriptor: ModifierDescriptor.Enhancement)
        .AddNotDispelable()
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.InvigoratingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddInitiatorAttackWithWeaponTrigger(
          reduceHPToZero: true,
          actionsOnInitiator: true,
          action: ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New().HasBuff(exhausted),
              ifTrue: ActionsBuilder.New().RemoveBuff(exhausted).ApplyBuffPermanent(fatigued),
              ifFalse: ActionsBuilder.New()
                .Conditional(
                  ConditionsBuilder.New().HasBuff(fatigued),
                  ifTrue: ActionsBuilder.New().RemoveBuff(fatigued),
                  ifFalse: ActionsBuilder.New().ApplyBuff(buff, ContextDuration.Fixed(1)))))
        .Configure();

      var enchantInfo = 
        new WeaponEnchantInfo(
          DisplayName,
          Description,
          icon,
          EnhancementCost,
          WeaponRangeType.Melee);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.InvigoratingEffect, enchant),
        parentBuff: new(BuffName, Guids.InvigoratingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.InvigoratingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.InvigoratingOffHandBuff));
    }
  }
}
