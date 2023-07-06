using AutomaticBonusProgression.Conditions;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Determination
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Determination));

    private const string EffectName = "LA.Determination.Effect";
    private const string BuffName = "LA.Determination.Buff";
    private const string ShieldBuffName = "LA.Determination.Buff.Shield";

    private const string ResourceName = "LA.Determination.Resource";
    private const string ShieldResourceName = "LA.Determination.Resource.Shield";

    private const string DisplayName = "LA.Determination.Name";
    private const string Description = "LA.Determination.Description";
    private const int EnhancementCost = 5;

    internal static void Configure()
    {
      Logger.Log($"Configuring Determination");

      var resource = AbilityResourceConfigurator.New(ResourceName, Guids.DeterminationResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var shieldResource = AbilityResourceConfigurator.New(ShieldResourceName, Guids.DeterminationShieldResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = BuffRefs.DefensiveStanceBuff.Reference.Get().Icon;
      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.DeterminationEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddIncomingDamageTrigger(
          reduceBelowZero: true,
          actions: ActionsBuilder.New()
            .Conditional(
              ConditionsBuilder.New()
                .Add<HasResource>(a => a.Resource = resource.ToReference<BlueprintAbilityResourceReference>()),
              ifTrue: ActionsBuilder.New()
                .CastSpell(AbilityRefs.BreathOfLifeTouch.ToString())
                .ContextSpendResource(resource)))
        .Configure();

      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = resource.ToReference<BlueprintAbilityResourceReference>()
        };
      var addShieldResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = shieldResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.DeterminationBuff, addResources),
        variantBuff: new(ShieldBuffName, Guids.DeterminationShieldBuff, addShieldResources));
    }
  }
}
