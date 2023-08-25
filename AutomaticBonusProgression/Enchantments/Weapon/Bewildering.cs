using AutomaticBonusProgression.Actions;
using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Bewildering
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bewildering));

    private const string EffectName = "LW.Bewildering";
    private const string BuffName = "LW.Bewildering.Buff";
    private const string OffHandEffectName = "LW.Bewildering.OffHand";
    private const string OffHandBuffName = "LW.Bewildering.OffHand.Buff";

    private const string TargetBuffName = "LW.Bewildering.Buff.Target";
    private const string EnchantName = "LW.Bewildering.Enchant";
    private const string CastResourceName = "LW.Bewildering.Cast.Resource";

    private const string MainHandAbilityName = "LW.Bewildering.Cast";
    private const string MainHandSelfBuffName = "LW.Bewildering.Buff.MainHand.Self";

    private const string OffHandAbilityName = "LW.Bewildering.Cast.OffHand";
    private const string OffHandSelfBuffName = "LW.Bewildering.Buff.OffHand.Self";

    private const string DisplayName = "LW.Bewildering.Name";
    private const string Description = "LW.Bewildering.Description";
    private const int EnhancementCost = 1;

    private const string MainHandDisplayName = "LW.Bewildering.MainHand.Name";
    private const string OffHandDisplayName = "LW.Bewildering.OffHand.Name";

    internal static void Configure()
    {
      Logger.Log($"Configuring Bewildering");

      var confusion = BuffRefs.Confusion.Reference.Get();
      var targetBuff = BuffConfigurator.New(TargetBuffName, Guids.BewilderingTargetBuff)
        .CopyFrom(confusion, c => c is not CombatStateTrigger)
        .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithBonusValueProgression(17))
        .AddFactContextActions(
          newRound: ActionsBuilder.New()
            .SavingThrow(
              SavingThrowType.Will,
              customDC: ContextValues.Rank(),
              onResult: ActionsBuilder.New().ConditionalSaved(succeed: ActionsBuilder.New().RemoveSelf())))
        .Configure();

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.BewilderingEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddInitiatorAttackWithWeaponTrigger(
          action: ActionsBuilder.New()
            .ApplyBuff(targetBuff, ContextDuration.VariableDice(DiceType.D6, diceCount: 1))
            .Add<RemoveParentBuff>(),
          onlyHit: true)
        .Configure();

      var confusionSpell = AbilityRefs.ConfusionSpell.Reference.Get();
      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.BewilderingCastResource)
        .SetIcon(confusionSpell.Icon)
        .SetMaxAmount(ResourceAmountBuilder.New(3))
        .Configure();

      var mainHandBuff = BuffConfigurator.New(MainHandSelfBuffName, Guids.BewilderingMainHandSelfBuff)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .AddNotDispelable()
        .AddComponent(new BuffEnchantAnyWeaponReplacement(enchant.ToReference<BlueprintItemEnchantmentReference>()))
        .Configure();

      var mainHandCastAbility = AbilityConfigurator.New(MainHandAbilityName, Guids.BewilderingMainHandAbility)
        .SetDisplayName(MainHandDisplayName)
        .SetDescription(Description)
        .SetIcon(confusionSpell.Icon)
        .AllowTargeting(self: true)
        .SetRange(AbilityRange.Personal)
        .SetType(AbilityType.Supernatural)
        .SetActionType(CommandType.Free)
        .SetAvailableMetamagic()
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.BewilderingEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(mainHandBuff))
        .Configure();

      var offHandBuff = BuffConfigurator.New(OffHandSelfBuffName, Guids.BewilderingOffHandSelfBufff)
        .CopyFrom(mainHandBuff, c => c is not BuffEnchantAnyWeaponReplacement)
        .AddComponent(
          new BuffEnchantAnyWeaponReplacement(
            enchant.ToReference<BlueprintItemEnchantmentReference>(), toPrimary: true))
        .Configure();

      var offHandCastAbility = AbilityConfigurator.New(OffHandAbilityName, Guids.BewilderingOffHandAbility)
        .CopyFrom(mainHandCastAbility, c => c is not AbilityEffectRunAction && c is not AbilityCasterHasFacts)
        .SetDisplayName(OffHandDisplayName)
        .AddAbilityCasterHasFacts(new() { Guids.BewilderingOffHandEffect })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(offHandBuff))
        .Configure();

      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, confusionSpell.Icon, EnhancementCost);
      var mainHandAddFacts = new AddFacts() { m_Facts = new[] { mainHandCastAbility.ToReference<BlueprintUnitFactReference>() } };
      var offHandAddFacts = new AddFacts() { m_Facts = new[] { offHandCastAbility.ToReference<BlueprintUnitFactReference>() } };
      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.BewilderingEffect),
        parentBuff: new(BuffName, Guids.BewilderingBuff, mainHandAddFacts, addResources));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: new(OffHandEffectName, Guids.BewilderingOffHandEffect),
        variantBuff: new(OffHandBuffName, Guids.BewilderingOffHandBuff, offHandAddFacts, addResources));
    }
  }
}
