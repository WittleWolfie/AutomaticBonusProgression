using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Expeditious
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Expeditious));

    private const string EffectName = "LA.Expeditious.Effect";
    private const string BuffName = "LA.Expeditious.Buff";

    private const string AbilityName = "LA.Expeditious.Cast";
    private const string CastBuffName = "LA.Expeditious.Cast.Buff";
    private const string ResourceName = "LA.Expeditious.Cast.Resource";

    private const string DisplayName = "LA.Expeditious.Name";
    private const string Description = "LA.Expeditious.Description";
    private const int EnhancementCost = 2;

    internal static void Configure()
    {
      Logger.Log($"Configuring Expeditious");

      var castResource = AbilityResourceConfigurator.New(ResourceName, Guids.ExpeditiousResource)
        .SetMaxAmount(ResourceAmountBuilder.New(3))
        .Configure();

      var icon = FeatureRefs.FastMovement.Reference.Get().Icon;
      var castBuff = BuffConfigurator.New(CastBuffName, Guids.ExpeditiousCastBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .AddBuffMovementSpeed(value: 10, descriptor: ModifierDescriptor.Enhancement)
        .Configure();

      var castAbility = AbilityConfigurator.New(AbilityName, Guids.ExpeditiousAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Swift)
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.ExpeditiousBuff })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(castBuff, ContextDuration.Fixed(1)))
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      var addFacts = new AddFacts() { m_Facts = new[] { castAbility.ToReference<BlueprintUnitFactReference>() } };
      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.ExpeditiousEffect),
        parentBuff: new(BuffName, Guids.ExpeditiousBuff, addFacts, addResources));
    }
  }
}
