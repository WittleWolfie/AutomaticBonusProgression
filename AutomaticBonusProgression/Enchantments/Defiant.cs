using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Features;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using TabletopTweaks.Core.NewComponents.OwlcatReplacements.DamageResistance;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Defiant
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Defiant));

    private const string DefiantName = "LegendaryArmor.Defiant";

    private const string DefiantAbility = "LegendaryArmor.Defiant.Ability";
    private const string DefiantAbilityName = "LegendaryArmor.Defiant.Ability.Name";
    private const string DefiantShieldAbility = "LegendaryArmor.Defiant.Shield.Ability";
    private const string DefiantShieldAbilityName = "LegendaryShield.Defiant.Ability.Name";

    private const string DefiantAberrationsName = "LegendaryArmor.Defiant.Aberrations.Name";
    private const string DefiantAberrationsAbility = "LegendaryArmor.Defiant.Aberrations.Ability";
    private const string DefiantAberrationsShieldAbility = "LegendaryArmor.Defiant.Aberrations.Shield.Ability";
    private const string DefiantAberrationsBuff = "LegendaryArmor.Defiant.Aberrations.Buff";
    private const string DefiantAberrationsShieldBuff = "LegendaryArmor.Defiant.Aberrations.Shield.Buff";

    private const string DefiantAnimalsName = "LegendaryArmor.Defiant.Animals.Name";
    private const string DefiantAnimalsAbility = "LegendaryArmor.Defiant.Animals.Ability";
    private const string DefiantAnimalsShieldAbility = "LegendaryArmor.Defiant.Animals.Shield.Ability";
    private const string DefiantAnimalsBuff = "LegendaryArmor.Defiant.Animals.Buff";
    private const string DefiantAnimalsShieldBuff = "LegendaryArmor.Defiant.Animals.Shield.Buff";

    private const string DefiantConstructsName = "LegendaryArmor.Defiant.Constructs.Name";
    private const string DefiantConstructsAbility = "LegendaryArmor.Defiant.Constructs.Ability";
    private const string DefiantConstructsShieldAbility = "LegendaryArmor.Defiant.Constructs.Shield.Ability";
    private const string DefiantConstructsBuff = "LegendaryArmor.Defiant.Constructs.Buff";
    private const string DefiantConstructsShieldBuff = "LegendaryArmor.Defiant.Constructs.Shield.Buff";

    private const string DefiantDragonsName = "LegendaryArmor.Defiant.Dragons.Name";
    private const string DefiantDragonsAbility = "LegendaryArmor.Defiant.Dragons.Ability";
    private const string DefiantDragonsShieldAbility = "LegendaryArmor.Defiant.Dragons.Shield.Ability";
    private const string DefiantDragonsBuff = "LegendaryArmor.Defiant.Dragons.Buff";
    private const string DefiantDragonsShieldBuff = "LegendaryArmor.Defiant.Dragons.Shield.Buff";

    private const string DefiantFeyName = "LegendaryArmor.Defiant.Fey.Name";
    private const string DefiantFeyAbility = "LegendaryArmor.Defiant.Fey.Ability";
    private const string DefiantFeyShieldAbility = "LegendaryArmor.Defiant.Fey.Shield.Ability";
    private const string DefiantFeyBuff = "LegendaryArmor.Defiant.Fey.Buff";
    private const string DefiantFeyShieldBuff = "LegendaryArmor.Defiant.Fey.Shield.Buff";

    private const string DefiantHumanoidGiantName = "LegendaryArmor.Defiant.Humanoid.Giant.Name";
    private const string DefiantHumanoidGiantAbility = "LegendaryArmor.Defiant.Humanoid.Giant.Ability";
    private const string DefiantHumanoidGiantShieldAbility = "LegendaryArmor.Defiant.Humanoid.Giant.Shield.Ability";
    private const string DefiantHumanoidGiantBuff = "LegendaryArmor.Defiant.Humanoid.Giant.Buff";
    private const string DefiantHumanoidGiantShieldBuff = "LegendaryArmor.Defiant.Humanoid.Giant.Shield.Buff";

    private const string DefiantHumanoidReptilianName = "LegendaryArmor.Defiant.Humanoid.Reptilian.Name";
    private const string DefiantHumanoidReptilianAbility = "LegendaryArmor.Defiant.Humanoid.Reptilian.Ability";
    private const string DefiantHumanoidReptilianShieldAbility = "LegendaryArmor.Defiant.Humanoid.Reptilian.Shield.Ability";
    private const string DefiantHumanoidReptilianBuff = "LegendaryArmor.Defiant.Humanoid.Reptilian.Buff";
    private const string DefiantHumanoidReptilianShieldBuff = "LegendaryArmor.Defiant.Humanoid.Reptilian.Shield.Buff";

    private const string DisplayName = "LegendaryArmor.Defiant.Name";
    private const string Description = "LegendaryArmor.Defiant.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Defiant");

      var parent = ActivatableAbilityConfigurator.New(DefiantAbility, Guids.DefiantParent)
        .SetDisplayName(DefiantAbilityName)
        .SetDescription(LegendaryArmor.LegendaryArmorAbilityDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.DefiantAberrationsAbility,
              Guids.DefiantAnimalsAbility,
              Guids.DefiantConstructsAbility,
              Guids.DefiantDragonsAbility,
              Guids.DefiantFeyAbility,
              Guids.DefiantHumanoidGiantAbility,
              Guids.DefiantHumanoidMonstrousAbility,
              Guids.DefiantHumanoidReptilianAbility,
              Guids.DefiantMagicalBeastsAbility,
              Guids.DefiantOutsiderChaoticAbility,
              Guids.DefiantOutsiderEvilAbility,
              Guids.DefiantOutsiderGoodAbility,
              Guids.DefiantOutsiderLawfulAbility,
              Guids.DefiantOutsiderNeutralAbility,
              Guids.DefiantPlantsAbility,
              Guids.DefiantUndeadAbility,
              Guids.DefiantVerminAbility,
            })
        .AddActivationDisable()
        .Configure();
      var shieldParent = ActivatableAbilityConfigurator.New(DefiantShieldAbility, Guids.DefiantShieldParent)
        .SetDisplayName(DefiantShieldAbilityName)
        .SetDescription(LegendaryArmor.LegendaryShieldDescription)
        //.SetIcon()
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddActivatableAbilityVariants(
          variants:
            new()
            {
              Guids.DefiantAberrationsShieldAbility,
              Guids.DefiantAnimalsShieldAbility,
              Guids.DefiantConstructsShieldAbility,
              Guids.DefiantDragonsShieldAbility,
              Guids.DefiantFeyShieldAbility,
              Guids.DefiantHumanoidGiantShieldAbility,
              Guids.DefiantHumanoidMonstrousShieldAbility,
              Guids.DefiantHumanoidReptilianShieldAbility,
              Guids.DefiantMagicalBeastsShieldAbility,
              Guids.DefiantOutsiderChaoticShieldAbility,
              Guids.DefiantOutsiderEvilShieldAbility,
              Guids.DefiantOutsiderGoodShieldAbility,
              Guids.DefiantOutsiderLawfulShieldAbility,
              Guids.DefiantOutsiderNeutralShieldAbility,
              Guids.DefiantPlantsShieldAbility,
              Guids.DefiantUndeadShieldAbility,
              Guids.DefiantVerminShieldAbility,
            })
        .AddActivationDisable()
        .Configure();

      var aberrations = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantAberrationsBuff,
          Guids.DefiantAberrationsBuff,
          DefiantAberrationsName,
          FeatureRefs.AberrationType.ToString()),
        displayName: DefiantAberrationsName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantAberrationsAbility,
        abilityGuid: Guids.DefiantAberrationsAbility);
      var aberrationsShield = EnchantmentTool.CreateEnchantShieldVariant(
        aberrations,
        buffName: DefiantAberrationsShieldBuff,
        buffGuid: Guids.DefiantAberrationsShieldBuff,
        abilityName: DefiantAberrationsShieldAbility,
        abilityGuid: Guids.DefiantAberrationsShieldAbility);

      var animals = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantAnimalsBuff,
          Guids.DefiantAnimalsBuff,
          DefiantAnimalsName,
          FeatureRefs.AnimalType.ToString()),
        displayName: DefiantAnimalsName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantAnimalsAbility,
        abilityGuid: Guids.DefiantAnimalsAbility);
      var animalsShield = EnchantmentTool.CreateEnchantShieldVariant(
        animals,
        buffName: DefiantAnimalsShieldBuff,
        buffGuid: Guids.DefiantAnimalsShieldBuff,
        abilityName: DefiantAnimalsShieldAbility,
        abilityGuid: Guids.DefiantAnimalsShieldAbility);

      var constructs = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantConstructsBuff,
          Guids.DefiantConstructsBuff,
          DefiantConstructsName,
          FeatureRefs.ConstructType.ToString()),
        displayName: DefiantConstructsName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantConstructsAbility,
        abilityGuid: Guids.DefiantConstructsAbility);
      var constructsShield = EnchantmentTool.CreateEnchantShieldVariant(
        constructs,
        buffName: DefiantConstructsShieldBuff,
        buffGuid: Guids.DefiantConstructsShieldBuff,
        abilityName: DefiantConstructsShieldAbility,
        abilityGuid: Guids.DefiantConstructsShieldAbility);

      var dragons = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantDragonsBuff,
          Guids.DefiantDragonsBuff,
          DefiantDragonsName,
          FeatureRefs.DragonType.ToString()),
        displayName: DefiantDragonsName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantDragonsAbility,
        abilityGuid: Guids.DefiantDragonsAbility);
      var dragonsShield = EnchantmentTool.CreateEnchantShieldVariant(
        dragons,
        buffName: DefiantDragonsShieldBuff,
        buffGuid: Guids.DefiantDragonsShieldBuff,
        abilityName: DefiantDragonsShieldAbility,
        abilityGuid: Guids.DefiantDragonsShieldAbility);

      var fey = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantFeyBuff,
          Guids.DefiantFeyBuff,
          DefiantFeyName,
          FeatureRefs.FeyType.ToString()),
        displayName: DefiantFeyName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantFeyAbility,
        abilityGuid: Guids.DefiantFeyAbility);
      var feyShield = EnchantmentTool.CreateEnchantShieldVariant(
        fey,
        buffName: DefiantFeyShieldBuff,
        buffGuid: Guids.DefiantFeyShieldBuff,
        abilityName: DefiantFeyShieldAbility,
        abilityGuid: Guids.DefiantFeyShieldAbility);

      var humanoidGiant = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantHumanoidGiantBuff,
          Guids.DefiantHumanoidGiantBuff,
          DefiantHumanoidGiantName,
          FeatureRefs.GiantSubtype.ToString()),
        displayName: DefiantHumanoidGiantName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantHumanoidGiantAbility,
        abilityGuid: Guids.DefiantHumanoidGiantAbility);
      var humanoidGiantShield = EnchantmentTool.CreateEnchantShieldVariant(
        humanoidGiant,
        buffName: DefiantHumanoidGiantShieldBuff,
        buffGuid: Guids.DefiantHumanoidGiantShieldBuff,
        abilityName: DefiantHumanoidGiantShieldAbility,
        abilityGuid: Guids.DefiantHumanoidGiantShieldAbility);

      var humanoidReptilian = EnchantmentTool.CreateEnchantAbility(
        buff: ConfigureBuff(
          DefiantHumanoidReptilianBuff,
          Guids.DefiantHumanoidReptilianBuff,
          DefiantHumanoidReptilianName,
          FeatureRefs.ReptilianSubtype.ToString()),
        displayName: DefiantHumanoidReptilianName,
        description: Description,
        //icon: ??,
        type: EnhancementType.Armor,
        enhancementCost: EnhancementCost,
        abilityName: DefiantHumanoidReptilianAbility,
        abilityGuid: Guids.DefiantHumanoidReptilianAbility);
      var humanoidReptilianShield = EnchantmentTool.CreateEnchantShieldVariant(
        humanoidReptilian,
        buffName: DefiantHumanoidReptilianShieldBuff,
        buffGuid: Guids.DefiantHumanoidReptilianShieldBuff,
        abilityName: DefiantHumanoidReptilianShieldAbility,
        abilityGuid: Guids.DefiantHumanoidReptilianShieldAbility);

      return EnchantmentTool.CreateEnchantFeature(
        displayName: DisplayName,
        description: Description,
        //icon: ??,
        featureName: DefiantName,
        Guids.Defiant,
        featureRanks: EnhancementCost,
        prerequisiteFeature: "",
        prerequisiteRanks: 1,
        parent,
        shieldParent,
        aberrations,
        aberrationsShield,
        animals,
        animalsShield,
        constructs,
        constructsShield,
        dragons,
        dragonsShield,
        fey,
        feyShield,
        humanoidGiant,
        humanoidGiantShield,
        humanoidReptilian,
        humanoidReptilianShield);
    }

    private static BlueprintBuff ConfigureBuff(
      string buffName,
      string buffGuid,
      string displayName,
      //string icon,
      Blueprint<BlueprintFeatureReference> requisiteFeature,
      AlignmentComponent? alignment = null)
    {
      return BuffConfigurator.New(buffName, buffGuid)
        .SetDisplayName(displayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(EnhancementType.Armor, EnhancementCost))
        .AddComponent(new DefiantComponent(requisiteFeature.Reference, alignment))
        .AddComponent(new DefiantResistanceComponent(requisiteFeature.Reference, alignment))
        .Configure();
    }

    [TypeId("dd8ce838-7897-4932-9963-8901b27218ae")]
    private class DefiantComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCalculateAC>
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;

      internal DefiantComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment)
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
      }

      public void OnEventAboutToTrigger(RuleCalculateAC evt)
      {
        try
        {
          if (Alignment is not null && !evt.Initiator.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Defiant does not apply: {evt.Initiator.Alignment}, {Alignment} required");
            return;
          }

          if (!evt.Initiator.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Defiant does not apply, attacker does not have {RequisiteFeature}");
            return;
          }

          evt.AddModifier(2, Fact, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("DefiantComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateAC evt) { }
    }

    [TypeId("ac8768d8-3995-48e6-b8b0-4048252d0a8e")]
    private class DefiantResistanceComponent : TTAddDamageResistanceBase
    {
      private readonly BlueprintFeatureReference RequisiteFeature;
      private readonly AlignmentComponent? Alignment;

      internal DefiantResistanceComponent(BlueprintFeatureReference requisiteFeature, AlignmentComponent? alignment) : base()
      {
        RequisiteFeature = requisiteFeature;
        Alignment = alignment;
        Value = 2;
      }

      protected override bool Bypassed(ComponentRuntime runtime, BaseDamage damage, ItemEntityWeapon weapon)
      {
        try
        {
          if (Alignment is not null && !weapon.Owner.Alignment.ValueRaw.HasComponent(Alignment.Value))
          {
            Logger.Verbose(() => $"Defiant DR does not apply: {weapon.Owner.Alignment}, {Alignment} required");
            return true;
          }

          if (!weapon.Owner.HasFact(RequisiteFeature))
          {
            Logger.Verbose(() => $"Defiant does not apply, attacker does not have {RequisiteFeature}");
            return true;
          }

          return false;
        }
        catch (Exception e)
        {
          Logger.LogException("DefiantComponent.OnEventAboutToTrigger", e);
        }
        return true;
      }

      public override bool IsSameDRTypeAs(TTAddDamageResistanceBase other)
      {
        return other is DefiantResistanceComponent;
      }

      protected override void AdditionalInitFromVanillaDamageResistance(AddDamageResistanceBase vanillaResistance) { }
    }
  }
}