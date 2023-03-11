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

namespace AutomaticBonusProgression.Enchantments
{
  internal class Martyring
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Martyring));

    private const string MartyringName = "LegendaryArmor.Martyring";
    private const string BuffName = "LegendaryArmor.Martyring.Buff";
    private const string AbilityName = "LegendaryArmor.Martyring.Ability";

    private const string CastResourceName = "LegendaryArmor.Martyring.Cast.Resource";

    private const string DisplayName = "LegendaryArmor.Martyring.Name";
    private const string Description = "LegendaryArmor.Martyring.Description";
    private const int EnhancementCost = 4;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Martyring");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.MartyringResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 4);

      var buff = BuffConfigurator.New(BuffName, Guids.MartyringBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo))
        .AddTargetAttackRollTrigger(
          criticalHit: true,
          actionOnSelf: ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New()
                .Add<HasResource>(a => a.Resource = castResource.ToReference<BlueprintAbilityResourceReference>()),
              ifTrue: ActionsBuilder.New()
                .CastSpell(AbilityRefs.CureLightWoundsMass.ToString())
                .ContextSpendResource(castResource)))
        .Configure();

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        buff,
        new(AbilityName, Guids.MartyringAbility));

      var featureInfo =
        new BlueprintInfo(
          MartyringName,
          Guids.Martyring,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability);
    }
  }
}
