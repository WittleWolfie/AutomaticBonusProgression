using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Distracting
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Distracting));

    private const string EnchantName = "LW.Distracting.Enchant";
    private const string EffectName = "LW.Distracting";
    private const string BuffName = "LW.Distracting.Buff";
    private const string OffHandEffectName = "LW.Distracting.OffHand";
    private const string OffHandBuffName = "LW.Distracting.OffHand.Buff";

    private const string DisplayName = "LW.Distracting.Name";
    private const string Description = "LW.Distracting.Description";
    private const int EnhancementCost = 1;

    private const string EffectBuff = "LW.Distracting.Effect.Buff";
    private const string EffectDescription = "LW.Distracting.Effect.Buff.Description";

    internal static void Configure()
    {
      Logger.Log($"Configuring Distracting");

      var icon = AbilityRefs.PowerWordStun.Reference.Get().Icon;
      var effectBuff = BuffConfigurator.New(EffectBuff, Guids.DistractingEffectBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddNotDispelable()
        .AddConcentrationBonus(value: -5)
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.DistractingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true,
          action: ActionsBuilder.New().ApplyBuff(effectBuff, ContextDuration.Fixed(10)))
        .Configure();

      var enchantInfo = new WeaponEnchantInfo(
        DisplayName,
        Description,
        icon,
        EnhancementCost);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.DistractingEffect, enchant),
        parentBuff: new(BuffName, Guids.DistractingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.DistractingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.DistractingOffHandBuff));

      ConfigureGreater();
    }

    private const string GreaterEnchantName = "LW.Distracting.Greater.Enchant";
    private const string GreaterEffectName = "LW.Distracting.Greater";
    private const string GreaterBuffName = "LW.Distracting.Greater.Buff";
    private const string GreaterOffHandEffectName = "LW.Distracting.Greater.OffHand";
    private const string GreaterOffHandBuffName = "LW.Distracting.Greater.OffHand.Buff";

    private const string GreaterDisplayName = "LW.Distracting.Greater.Name";
    private const string GreaterDescription = "LW.Distracting.Greater.Description";
    private const int GreaterEnhancementCost = 2;

    private const string GreaterEffectBuff = "LW.Distracting.Greater.Effect.Buff";
    private const string GreaterEffectDescription = "LW.Distracting.Greater.Effect.Buff.Description";

    private static void ConfigureGreater()
    {
      var greaterIcon = AbilityRefs.WordOfChaos.Reference.Get().Icon;
      var greaterEffectBuff = BuffConfigurator.New(GreaterEffectBuff, Guids.DistractingGreaterEffectBuff)
        .SetDisplayName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        .SetIcon(greaterIcon)
        .AddNotDispelable()
        .AddConcentrationBonus(value: -10)
        .Configure();

      var greaterEnchant = WeaponEnchantmentConfigurator.New(GreaterEnchantName, Guids.DistractingGreaterEnchant)
        .SetEnchantName(GreaterDisplayName)
        .SetDescription(GreaterDescription)
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true,
          action: ActionsBuilder.New()
            .RemoveBuff(Guids.DistractingEffectBuff)
            .ApplyBuff(greaterEffectBuff, ContextDuration.Fixed(10)))
        .Configure();

      var greaterEnchantInfo = new WeaponEnchantInfo(
        GreaterDisplayName,
        GreaterDescription,
        greaterIcon,
        GreaterEnhancementCost);
      EnchantTool.CreateEnchant(
        greaterEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(GreaterEffectName, Guids.DistractingGreaterEffect, greaterEnchant),
        parentBuff: new(GreaterBuffName, Guids.DistractingGreaterBuff));
      EnchantTool.CreateVariantEnchant(
        greaterEnchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          GreaterOffHandEffectName,
          Guids.DistractingGreaterOffHandEffect,
          greaterEnchant,
          toPrimaryWeapon: false),
        variantBuff: new(GreaterOffHandBuffName, Guids.DistractingGreaterOffHandBuff));
    }
  }
}
