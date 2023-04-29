using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Bolstering
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bolstering));

    private const string EffectName = "LA.Bolstering.Efffect";
    private const string BuffName = "LA.Bolstering.Buff";
    private const string ShieldBuffName = "LA.Bolstering.Buff.Shield";

    private const string TargetBuffName = "LA.Bolstering.Target.Buff";

    private const string DisplayName = "LA.Bolstering.Name";
    private const string Description = "LA.Bolstering.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Bolstering");

      var targetBuff = BuffConfigurator.New(TargetBuffName, Guids.BolsteringTargetBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ArmorProficiencyGroup.Medium,
        ArmorProficiencyGroup.Heavy,
        ArmorProficiencyGroup.LightShield,
        ArmorProficiencyGroup.HeavyShield,
        ArmorProficiencyGroup.TowerShield);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.BolsteringEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true,
          action: ActionsBuilder.New().ApplyBuff(targetBuff, ContextDuration.Fixed(1)))
        .AddComponent(
          BonusAgainstTarget.Saves(targetBuff.ToReference<BlueprintBuffReference>(), 2, ModifierDescriptor.Competence))
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.BolsteringBuff),
        variantBuff: new(ShieldBuffName, Guids.BolsteringShieldBuff));
    }
  }
}
