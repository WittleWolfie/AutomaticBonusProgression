using AutomaticBonusProgression.Conditions;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Blueprints;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Martyring
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Martyring));

    private const string EffectName = "LA.Martyring.Effect";
    private const string BuffName = "LA.Martyring.Buff";

    private const string CastResourceName = "LA.Martyring.Cast.Resource";

    private const string DisplayName = "LA.Martyring.Name";
    private const string Description = "LA.Martyring.Description";
    private const int EnhancementCost = 4;

    internal static void Configure()
    {
      Logger.Log($"Configuring Martyring");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.MartyringResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, "", EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.MartyringEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
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

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.MartyringBuff));
    }
  }
}
