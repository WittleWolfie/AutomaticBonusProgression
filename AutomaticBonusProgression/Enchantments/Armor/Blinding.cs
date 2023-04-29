using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Utility;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
    internal class Blinding
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Blinding));

        private const string BlindingName = "LegendaryArmor.Blinding";
        private const string BuffName = "LegendaryArmor.Blinding.Buff";
        private const string AbilityName = "LegendaryArmor.Blinding.Ability";

        private const string CastAbilityName = "LegendaryArmor.Blinding.Cast";
        private const string CastResourceName = "LegendaryArmor.Blinding.Cast.Resource";

        private const string DisplayName = "LegendaryArmor.Blinding.Name";
        private const string Description = "LegendaryArmor.Blinding.Description";
        private const int EnhancementCost = 1;

        internal static BlueprintFeature Configure()
        {
            Logger.Log($"Configuring Blinding");

            var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.BlindingCastResource)
              .SetMaxAmount(ResourceAmountBuilder.New(2))
              .Configure();

            var castAbility = AbilityConfigurator.New(CastAbilityName, Guids.BlindingCastAbility)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              //.SetIcon()
              .SetType(AbilityType.SpellLike)
              .SetRange(AbilityRange.Personal)
              .SetActionType(CommandType.Swift)
              .AddAbilityTargetsAround(targetType: TargetType.Any, radius: 20.Feet())
              .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
              .AddAbilityCasterHasFacts(new() { Guids.BlindingBuff })
              .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithBonusValueProgression(14))
              .AddAbilityEffectRunAction(
                ActionsBuilder.New()
                  .Conditional(
                    ConditionsBuilder.New().IsCaster(),
                    ifFalse: ActionsBuilder.New()
                      .SavingThrow(
                        SavingThrowType.Reflex,
                        customDC: ContextValues.Rank(),
                        onResult: ActionsBuilder.New()
                          .ConditionalSaved(
                            failed: ActionsBuilder.New()
                              .ApplyBuff(BuffRefs.Blind.ToString(), ContextDuration.FixedDice(DiceType.D4))))))
              .Configure();

            var enchantInfo =
              new ArmorEnchantInfo(
                DisplayName,
                Description,
                "",
                EnhancementCost,
                ranks: 1);

            var ability = EnchantTool.CreateEnchantShieldVariant(
              enchantInfo, new(BuffName, Guids.BlindingBuff), new(AbilityName, Guids.BlindingAbility));

            var featureInfo =
              new BlueprintInfo(
                BlindingName,
                Guids.Blinding,
                new AddAbilityResources()
                {
                    RestoreAmount = true,
                    m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
                });
            return EnchantTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, castAbility);
        }
    }
}
