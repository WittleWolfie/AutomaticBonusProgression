using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Bolstering
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bolstering));

    private const string BolsteringName = "LegendaryArmor.Bolstering";
    private const string BuffName = "LegendaryArmor.Bolstering.Buff";
    private const string AbilityName = "LegendaryArmor.Bolstering.Ability";
    private const string BuffShieldName = "LegendaryArmor.Bolstering.Shield.Buff";
    private const string AbilityShieldName = "LegendaryArmor.Bolstering.Shield.Ability";

    private const string TargetBuffName = "LegendaryArmor.Bolstering.Target.Buff";

    private const string DisplayName = "LegendaryArmor.Bolstering.Name";
    private const string Description = "LegendaryArmor.Bolstering.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
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
        ranks: 1,
        ArmorProficiencyGroup.Medium,
        ArmorProficiencyGroup.Heavy,
        ArmorProficiencyGroup.LightShield,
        ArmorProficiencyGroup.HeavyShield,
        ArmorProficiencyGroup.TowerShield);

      var buff = BuffConfigurator.New(BuffName, Guids.BolsteringBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalence(enchantInfo))
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true,
          action: ActionsBuilder.New().ApplyBuff(targetBuff, ContextDuration.Fixed(1)))
        .AddComponent(BonusAgainstTarget.Saves(targetBuff.ToReference<BlueprintBuffReference>(), 2, ModifierDescriptor.Competence))
        .Configure();

      var abilityInfo = new BlueprintInfo(AbilityName, Guids.BolsteringAbility);
      var shieldBuffInfo = new BlueprintInfo(BuffShieldName, Guids.BolsteringShieldBuff);
      var shieldAbilityInfo = new BlueprintInfo(AbilityShieldName, Guids.BolsteringShieldAbility);

      var ability = EnchantmentTool.CreateEnchantAbility(enchantInfo, buff, abilityInfo);
      var shieldAbility =
        EnchantmentTool.CreateEnchantShieldVariant(enchantInfo, ability, shieldBuffInfo, shieldAbilityInfo);

      var featureInfo = new BlueprintInfo(BolsteringName, Guids.Bolstering);
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, shieldAbility);
    }
  }
}
