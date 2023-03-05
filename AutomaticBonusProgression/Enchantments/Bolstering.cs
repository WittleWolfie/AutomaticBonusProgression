using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;

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

      var buff = BuffConfigurator.New(BuffName, Guids.BolsteringBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, EnhancementCost))
        .AddInitiatorAttackWithWeaponTrigger(
          onlyHit: true,
          action: ActionsBuilder.New().ApplyBuff(targetBuff, ContextDuration.Fixed(1)))
        .AddComponent(new SavingThrowBonusAgainstTarget(targetBuff))
        .Configure();

      var ability = EnchantmentTool.CreateEnchantAbility(
        buff: buff,
        displayName: DisplayName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: AbilityName,
        abilityGuid: Guids.BolsteringAbility);
      var shieldAbility = EnchantmentTool.CreateEnchantShieldVariant(
        ability,
        buffName: BuffShieldName,
        buffGuid: Guids.BolsteringShieldBuff,
        abilityName: AbilityShieldName,
        abilityGuid: Guids.BolsteringShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: Description,
        featureName: BolsteringName,
        featureGuid: Guids.Bolstering,
        featureRanks: 1,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        ability,
        shieldAbility);
    }
  }
}
