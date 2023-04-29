using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Conditions;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Determination
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Determination));

    private const string DeterminationName = "LegendaryArmor.Determination";
    private const string BuffName = "LegendaryArmor.Determination.Buff";
    private const string AbilityName = "LegendaryArmor.Determination.Ability";

    private const string CastResourceName = "LegendaryArmor.Determination.Cast.Resource";

    private const string DisplayName = "LegendaryArmor.Determination.Name";
    private const string Description = "LegendaryArmor.Determination.Description";
    private const int EnhancementCost = 5;

    // TODO: Add Shield variant!
    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Determination");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.DeterminationResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 5);

      var buff = BuffConfigurator.New(BuffName, Guids.DeterminationBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddComponent(new EnhancementEquivalence(enchantInfo))
        .AddIncomingDamageTrigger(
          reduceBelowZero: true,
          actions: ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New()
                .Add<HasResource>(a => a.Resource = castResource.ToReference<BlueprintAbilityResourceReference>()),
              ifTrue: ActionsBuilder.New()
                .CastSpell(AbilityRefs.BreathOfLifeTouch.ToString())
                .ContextSpendResource(castResource)))
        .Configure();

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        buff,
        new(AbilityName, Guids.DeterminationAbility));

      var featureInfo =
        new BlueprintInfo(
          DeterminationName,
          Guids.Determination,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantTool.CreateEnchantFeature(enchantInfo, featureInfo, ability);
    }
  }
}
