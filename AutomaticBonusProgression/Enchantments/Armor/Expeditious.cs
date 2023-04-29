using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Expeditious
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Expeditious));

    private const string ExpeditiousName = "LegendaryArmor.Expeditious";
    private const string BuffName = "LegendaryArmor.Expeditious.Buff";
    private const string AbilityName = "LegendaryArmor.Expeditious.Ability";

    private const string CastAbilityName = "LegendaryArmor.Expeditious.Cast";
    private const string CastBuffName = "LegendaryArmor.Expeditious.Cast.Buff";
    private const string CastResourceName = "LegendaryArmor.Expeditious.Cast.Resource";

    private const string DisplayName = "LegendaryArmor.Expeditious.Name";
    private const string Description = "LegendaryArmor.Expeditious.Description";
    private const int EnhancementCost = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Expeditious");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.ExpeditiousCastResource)
        .SetMaxAmount(ResourceAmountBuilder.New(3))
        .Configure();

      var castBuff = BuffConfigurator.New(CastBuffName, Guids.ExpeditiousCastBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddBuffMovementSpeed(value: 10, descriptor: ModifierDescriptor.Enhancement)
        .Configure();

      var castAbility = AbilityConfigurator.New(CastAbilityName, Guids.ExpeditiousCastAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Swift)
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.ExpeditiousBuff })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuff(castBuff, ContextDuration.Fixed(1)))
        .Configure();

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 2);

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(BuffName, Guids.ExpeditiousBuff),
        new(AbilityName, Guids.ExpeditiousAbility));

      var featureInfo =
        new BlueprintInfo(
          ExpeditiousName,
          Guids.Expeditious,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, castAbility);
    }
  }
}
