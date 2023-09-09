using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;

namespace AutomaticBonusProgression.Enchantments
{
  internal class PhaseLocking
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(PhaseLocking));

    private const string EnchantName = "LW.PhaseLocking.Enchant";
    private const string EffectName = "LW.PhaseLocking";
    private const string BuffName = "LW.PhaseLocking.Buff";
    private const string OffHandEffectName = "LW.PhaseLocking.OffHand";
    private const string OffHandBuffName = "LW.PhaseLocking.OffHand.Buff";

    private const string DisplayName = "LW.PhaseLocking.Name";
    private const string Description = "LW.PhaseLocking.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring PhaseLocking");

      var dimensionalAnchor = BuffRefs.DimensionalAnchorBuff.Reference.Get();
      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.PhaseLockingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true, action: ActionsBuilder.New().ApplyBuff(dimensionalAnchor, ContextDuration.Fixed(1)))
        .Configure();

      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, dimensionalAnchor.Icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.PhaseLockingEffect, enchant),
        parentBuff: new(BuffName, Guids.PhaseLockingBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.PhaseLockingOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.PhaseLockingOffHandBuff));
    }
  }
}
