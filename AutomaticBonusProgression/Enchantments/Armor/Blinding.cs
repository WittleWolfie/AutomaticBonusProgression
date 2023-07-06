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
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Blinding
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Blinding));

    private const string EffectName = "LA.Blinding.Effect";
    private const string BuffName = "LA.Blinding.Buff";
    private const string AbilityName = "LA.Blinding.Ability";
    private const string ResourceName = "LA.Blinding.Resource";

    private const string DisplayName = "LA.Blinding.Name";
    private const string Description = "LA.Blinding.Description";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring BlindingEffect");

      var castResource = AbilityResourceConfigurator.New(ResourceName, Guids.BlindingResource)
        .SetMaxAmount(ResourceAmountBuilder.New(2))
        .Configure();

      var icon = BuffRefs.ShieldOfDawnBuff.Reference.Get().Icon;
      var castAbility = AbilityConfigurator.New(AbilityName, Guids.BlindingAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Swift)
        .AddAbilityTargetsAround(targetType: TargetType.Any, radius: 20.Feet())
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.BlindingEffect })
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

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        icon,
        EnhancementCost,
        ArmorProficiencyGroup.LightShield,
        ArmorProficiencyGroup.HeavyShield,
        ArmorProficiencyGroup.TowerShield);

      var addFacts = new AddFacts() { m_Facts = new[] { castAbility.ToReference<BlueprintUnitFactReference>() } };
      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.BlindingEffect),
        variantBuff: new(BuffName, Guids.BlindingBuff, addFacts, addResources));
    }
  }
}
