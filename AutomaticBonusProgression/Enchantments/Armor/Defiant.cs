using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using TabletopTweaks.Core.NewComponents.OwlcatReplacements.DamageResistance;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Defiant
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Defiant));

    #region Too Many Constants
    private const string DefiantAberrationsName = "LA.Defiant.Aberrations.Name";
    private const string DefiantAberrationsEffect = "LA.Defiant.Aberrations.Effect";
    private const string DefiantAberrationsBuff = "LA.Defiant.Aberrations.Buff";
    private const string DefiantAberrationsShieldBuff = "LA.Defiant.Aberrations.Shield.Buff";

    private const string DefiantAnimalsName = "LA.Defiant.Animals.Name";
    private const string DefiantAnimalsEffect = "LA.Defiant.Animals.Effect";
    private const string DefiantAnimalsBuff = "LA.Defiant.Animals.Buff";
    private const string DefiantAnimalsShieldBuff = "LA.Defiant.Animals.Shield.Buff";

    private const string DefiantConstructsName = "LA.Defiant.Constructs.Name";
    private const string DefiantConstructsEffect = "LA.Defiant.Constructs.Effect";
    private const string DefiantConstructsBuff = "LA.Defiant.Constructs.Buff";
    private const string DefiantConstructsShieldBuff = "LA.Defiant.Constructs.Shield.Buff";

    private const string DefiantDragonsName = "LA.Defiant.Dragons.Name";
    private const string DefiantDragonsEffect = "LA.Defiant.Dragons.Effect";
    private const string DefiantDragonsBuff = "LA.Defiant.Dragons.Buff";
    private const string DefiantDragonsShieldBuff = "LA.Defiant.Dragons.Shield.Buff";

    private const string DefiantFeyName = "LA.Defiant.Fey.Name";
    private const string DefiantFeyEffect = "LA.Defiant.Fey.Effect";
    private const string DefiantFeyBuff = "LA.Defiant.Fey.Buff";
    private const string DefiantFeyShieldBuff = "LA.Defiant.Fey.Shield.Buff";

    private const string DefiantHumanoidGiantName = "LA.Defiant.Humanoid.Giant.Name";
    private const string DefiantHumanoidGiantEffect = "LA.Defiant.Humanoid.Giant.Effect";
    private const string DefiantHumanoidGiantBuff = "LA.Defiant.Humanoid.Giant.Buff";
    private const string DefiantHumanoidGiantShieldBuff = "LA.Defiant.Humanoid.Giant.Shield.Buff";

    private const string DefiantHumanoidReptilianName = "LA.Defiant.Humanoid.Reptilian.Name";
    private const string DefiantHumanoidReptilianEffect = "LA.Defiant.Humanoid.Reptilian.Effect";
    private const string DefiantHumanoidReptilianBuff = "LA.Defiant.Humanoid.Reptilian.Buff";
    private const string DefiantHumanoidReptilianShieldBuff = "LA.Defiant.Humanoid.Reptilian.Shield.Buff";

    private const string DefiantHumanoidMonstrousName = "LA.Defiant.Humanoid.Monstrous.Name";
    private const string DefiantHumanoidMonstrousEffect = "LA.Defiant.Humanoid.Monstrous.Effect";
    private const string DefiantHumanoidMonstrousBuff = "LA.Defiant.Humanoid.Monstrous.Buff";
    private const string DefiantHumanoidMonstrousShieldBuff = "LA.Defiant.Humanoid.Monstrous.Shield.Buff";

    private const string DefiantMagicalBeastsName = "LA.Defiant.MagicalBeasts.Name";
    private const string DefiantMagicalBeastsEffect = "LA.Defiant.MagicalBeasts.Effect";
    private const string DefiantMagicalBeastsBuff = "LA.Defiant.MagicalBeasts.Buff";
    private const string DefiantMagicalBeastsShieldBuff = "LA.Defiant.MagicalBeasts.Shield.Buff";

    private const string DefiantOutsiderGoodName = "LA.Defiant.Outsider.Good.Name";
    private const string DefiantOutsiderGoodEffect = "LA.Defiant.Outsider.Good.Effect";
    private const string DefiantOutsiderGoodBuff = "LA.Defiant.Outsider.Good.Buff";
    private const string DefiantOutsiderGoodShieldBuff = "LA.Defiant.Outsider.Good.Shield.Buff";

    private const string DefiantOutsiderEvilName = "LA.Defiant.Outsider.Evil.Name";
    private const string DefiantOutsiderEvilEffect = "LA.Defiant.Outsider.Evil.Effect";
    private const string DefiantOutsiderEvilBuff = "LA.Defiant.Outsider.Evil.Buff";
    private const string DefiantOutsiderEvilShieldBuff = "LA.Defiant.Outsider.Evil.Shield.Buff";

    private const string DefiantOutsiderLawfulName = "LA.Defiant.Outsider.Lawful.Name";
    private const string DefiantOutsiderLawfulEffect = "LA.Defiant.Outsider.Lawful.Effect";
    private const string DefiantOutsiderLawfulBuff = "LA.Defiant.Outsider.Lawful.Buff";
    private const string DefiantOutsiderLawfulShieldBuff = "LA.Defiant.Outsider.Lawful.Shield.Buff";

    private const string DefiantOutsiderChaoticName = "LA.Defiant.Outsider.Chaotic.Name";
    private const string DefiantOutsiderChaoticEffect = "LA.Defiant.Outsider.Chaotic.Effect";
    private const string DefiantOutsiderChaoticBuff = "LA.Defiant.Outsider.Chaotic.Buff";
    private const string DefiantOutsiderChaoticShieldBuff = "LA.Defiant.Outsider.Chaotic.Shield.Buff";

    private const string DefiantOutsiderNeutralName = "LA.Defiant.Outsider.Neutral.Name";
    private const string DefiantOutsiderNeutralEffect = "LA.Defiant.Outsider.Neutral.Effect";
    private const string DefiantOutsiderNeutralBuff = "LA.Defiant.Outsider.Neutral.Buff";
    private const string DefiantOutsiderNeutralShieldBuff = "LA.Defiant.Outsider.Neutral.Shield.Buff";

    private const string DefiantPlantsName = "LA.Defiant.Plants.Name";
    private const string DefiantPlantsEffect = "LA.Defiant.Plants.Effect";
    private const string DefiantPlantsBuff = "LA.Defiant.Plants.Buff";
    private const string DefiantPlantsShieldBuff = "LA.Defiant.Plants.Shield.Buff";

    private const string DefiantUndeadName = "LA.Defiant.Undead.Name";
    private const string DefiantUndeadEffect = "LA.Defiant.Undead.Effect";
    private const string DefiantUndeadBuff = "LA.Defiant.Undead.Buff";
    private const string DefiantUndeadShieldBuff = "LA.Defiant.Undead.Shield.Buff";

    private const string DefiantVerminName = "LA.Defiant.Vermin.Name";
    private const string DefiantVerminEffect = "LA.Defiant.Vermin.Effect";
    private const string DefiantVerminBuff = "LA.Defiant.Vermin.Buff";
    private const string DefiantVerminShieldBuff = "LA.Defiant.Vermin.Shield.Buff";
    #endregion

    private const string Description = "LA.Defiant.Description";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Defiant");

      var icon = BuffRefs.DivineGuardianInHarmsWayBuff.Reference.Get().Icon;

      // Aberrations
      var aberrationsEnchantInfo = new ArmorEnchantInfo(DefiantAberrationsName, Description, icon, EnhancementCost);
      var aberrationsEffectBuff = ConfigureEffect(
        aberrationsEnchantInfo,
        new(DefiantAberrationsEffect, Guids.DefiantAberrationsEffect),
        FeatureRefs.AberrationType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        aberrationsEnchantInfo,
        aberrationsEffectBuff,
        parentBuff: new(DefiantAberrationsBuff, Guids.DefiantAberrationsBuff),
        variantBuff: new(DefiantAberrationsShieldBuff, Guids.DefiantAberrationsShieldBuff));

      // Animals
      var animalsEnchantInfo = new ArmorEnchantInfo(DefiantAnimalsName, Description, icon, EnhancementCost);
      var animalsEffectBuff = ConfigureEffect(
        animalsEnchantInfo,
        new(DefiantAnimalsEffect, Guids.DefiantAnimalsEffect),
        FeatureRefs.AnimalType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        animalsEnchantInfo,
        animalsEffectBuff,
        parentBuff: new(DefiantAnimalsBuff, Guids.DefiantAnimalsBuff),
        variantBuff: new(DefiantAnimalsShieldBuff, Guids.DefiantAnimalsShieldBuff));

      // Constructs
      var constructsEnchantInfo = new ArmorEnchantInfo(DefiantConstructsName, Description, icon, EnhancementCost);
      var constructsEffectBuff = ConfigureEffect(
        constructsEnchantInfo,
        new(DefiantConstructsEffect, Guids.DefiantConstructsEffect),
        FeatureRefs.ConstructType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        constructsEnchantInfo,
        constructsEffectBuff,
        parentBuff: new(DefiantConstructsBuff, Guids.DefiantConstructsBuff),
        variantBuff: new(DefiantConstructsShieldBuff, Guids.DefiantConstructsShieldBuff));

      // Dragons
      var dragonsEnchantInfo = new ArmorEnchantInfo(DefiantDragonsName, Description, icon, EnhancementCost);
      var dragonsEffectBuff = ConfigureEffect(
        dragonsEnchantInfo,
        new(DefiantDragonsEffect, Guids.DefiantDragonsEffect),
        FeatureRefs.DragonType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        dragonsEnchantInfo,
        dragonsEffectBuff,
        parentBuff: new(DefiantDragonsBuff, Guids.DefiantDragonsBuff),
        variantBuff: new(DefiantDragonsShieldBuff, Guids.DefiantDragonsShieldBuff));

      // Fey
      var feyEnchantInfo = new ArmorEnchantInfo(DefiantFeyName, Description, icon, EnhancementCost);
      var feyEffectBuff = ConfigureEffect(
        feyEnchantInfo,
        new(DefiantFeyEffect, Guids.DefiantFeyEffect),
        FeatureRefs.FeyType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        feyEnchantInfo,
        feyEffectBuff,
        parentBuff: new(DefiantFeyBuff, Guids.DefiantFeyBuff),
        variantBuff: new(DefiantFeyShieldBuff, Guids.DefiantFeyShieldBuff));

      // HumanoidGiant
      var humanoidGiantEnchantInfo = new ArmorEnchantInfo(DefiantHumanoidGiantName, Description, icon, EnhancementCost);
      var humanoidGiantEffectBuff = ConfigureEffect(
        humanoidGiantEnchantInfo,
        new(DefiantHumanoidGiantEffect, Guids.DefiantHumanoidGiantEffect),
        FeatureRefs.GiantSubtype.ToString());

      EnchantTool.CreateEnchantWithEffect(
        humanoidGiantEnchantInfo,
        humanoidGiantEffectBuff,
        parentBuff: new(DefiantHumanoidGiantBuff, Guids.DefiantHumanoidGiantBuff),
        variantBuff: new(DefiantHumanoidGiantShieldBuff, Guids.DefiantHumanoidGiantShieldBuff));

      // HumanoidReptilian
      var humanoidReptilianEnchantInfo = new ArmorEnchantInfo(DefiantHumanoidReptilianName, Description, icon, EnhancementCost);
      var humanoidReptilianEffectBuff = ConfigureEffect(
        humanoidReptilianEnchantInfo,
        new(DefiantHumanoidReptilianEffect, Guids.DefiantHumanoidReptilianEffect),
        FeatureRefs.ReptilianSubtype.ToString());

      EnchantTool.CreateEnchantWithEffect(
        humanoidReptilianEnchantInfo,
        humanoidReptilianEffectBuff,
        parentBuff: new(DefiantHumanoidReptilianBuff, Guids.DefiantHumanoidReptilianBuff),
        variantBuff: new(DefiantHumanoidReptilianShieldBuff, Guids.DefiantHumanoidReptilianShieldBuff));

      // HumanoidMonstrous
      var humanoidMonstrousEnchantInfo = new ArmorEnchantInfo(DefiantHumanoidMonstrousName, Description, icon, EnhancementCost);
      var humanoidMonstrousEffectBuff = ConfigureEffect(
        humanoidMonstrousEnchantInfo,
        new(DefiantHumanoidMonstrousEffect, Guids.DefiantHumanoidMonstrousEffect),
        FeatureRefs.MonstrousHumanoidType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        humanoidMonstrousEnchantInfo,
        humanoidMonstrousEffectBuff,
        parentBuff: new(DefiantHumanoidMonstrousBuff, Guids.DefiantHumanoidMonstrousBuff),
        variantBuff: new(DefiantHumanoidMonstrousShieldBuff, Guids.DefiantHumanoidMonstrousShieldBuff));

      // MagicalBeasts
      var magicalBeastsEnchantInfo = new ArmorEnchantInfo(DefiantMagicalBeastsName, Description, icon, EnhancementCost);
      var magicalBeastsEffectBuff = ConfigureEffect(
        magicalBeastsEnchantInfo,
        new(DefiantMagicalBeastsEffect, Guids.DefiantMagicalBeastsEffect),
        FeatureRefs.MagicalBeastType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        magicalBeastsEnchantInfo,
        magicalBeastsEffectBuff,
        parentBuff: new(DefiantMagicalBeastsBuff, Guids.DefiantMagicalBeastsBuff),
        variantBuff: new(DefiantMagicalBeastsShieldBuff, Guids.DefiantMagicalBeastsShieldBuff));

      // OutsiderGood
      var outsiderGoodEnchantInfo = new ArmorEnchantInfo(DefiantOutsiderGoodName, Description, icon, EnhancementCost);
      var outsiderGoodEffectBuff = ConfigureEffect(
        outsiderGoodEnchantInfo,
        new(DefiantOutsiderGoodEffect, Guids.DefiantOutsiderGoodEffect),
        FeatureRefs.OutsiderType.ToString(),
        AlignmentComponent.Good);

      EnchantTool.CreateEnchantWithEffect(
        outsiderGoodEnchantInfo,
        outsiderGoodEffectBuff,
        parentBuff: new(DefiantOutsiderGoodBuff, Guids.DefiantOutsiderGoodBuff),
        variantBuff: new(DefiantOutsiderGoodShieldBuff, Guids.DefiantOutsiderGoodShieldBuff));

      // OutsiderEvil
      var outsiderEvilEnchantInfo = new ArmorEnchantInfo(DefiantOutsiderEvilName, Description, icon, EnhancementCost);
      var outsiderEvilEffectBuff = ConfigureEffect(
        outsiderEvilEnchantInfo,
        new(DefiantOutsiderEvilEffect, Guids.DefiantOutsiderEvilEffect),
        FeatureRefs.OutsiderType.ToString(),
        AlignmentComponent.Evil);

      EnchantTool.CreateEnchantWithEffect(
        outsiderEvilEnchantInfo,
        outsiderEvilEffectBuff,
        parentBuff: new(DefiantOutsiderEvilBuff, Guids.DefiantOutsiderEvilBuff),
        variantBuff: new(DefiantOutsiderEvilShieldBuff, Guids.DefiantOutsiderEvilShieldBuff));

      // OutsiderLawful
      var outsiderLawfulEnchantInfo = new ArmorEnchantInfo(DefiantOutsiderLawfulName, Description, icon, EnhancementCost);
      var outsiderLawfulEffectBuff = ConfigureEffect(
        outsiderLawfulEnchantInfo,
        new(DefiantOutsiderLawfulEffect, Guids.DefiantOutsiderLawfulEffect),
        FeatureRefs.OutsiderType.ToString(),
        AlignmentComponent.Lawful);

      EnchantTool.CreateEnchantWithEffect(
        outsiderLawfulEnchantInfo,
        outsiderLawfulEffectBuff,
        parentBuff: new(DefiantOutsiderLawfulBuff, Guids.DefiantOutsiderLawfulBuff),
        variantBuff: new(DefiantOutsiderLawfulShieldBuff, Guids.DefiantOutsiderLawfulShieldBuff));

      // OutsiderChaotic
      var outsiderChaoticEnchantInfo = new ArmorEnchantInfo(DefiantOutsiderChaoticName, Description, icon, EnhancementCost);
      var outsiderChaoticEffectBuff = ConfigureEffect(
        outsiderChaoticEnchantInfo,
        new(DefiantOutsiderChaoticEffect, Guids.DefiantOutsiderChaoticEffect),
        FeatureRefs.OutsiderType.ToString(),
        AlignmentComponent.Chaotic);

      EnchantTool.CreateEnchantWithEffect(
        outsiderChaoticEnchantInfo,
        outsiderChaoticEffectBuff,
        parentBuff: new(DefiantOutsiderChaoticBuff, Guids.DefiantOutsiderChaoticBuff),
        variantBuff: new(DefiantOutsiderChaoticShieldBuff, Guids.DefiantOutsiderChaoticShieldBuff));

      // OutsiderNeutral
      var outsiderNeutralEnchantInfo = new ArmorEnchantInfo(DefiantOutsiderNeutralName, Description, icon, EnhancementCost);
      var outsiderNeutralEffectBuff = ConfigureEffect(
        outsiderNeutralEnchantInfo,
        new(DefiantOutsiderNeutralEffect, Guids.DefiantOutsiderNeutralEffect),
        FeatureRefs.OutsiderType.ToString(),
        AlignmentComponent.Neutral);

      EnchantTool.CreateEnchantWithEffect(
        outsiderNeutralEnchantInfo,
        outsiderNeutralEffectBuff,
        parentBuff: new(DefiantOutsiderNeutralBuff, Guids.DefiantOutsiderNeutralBuff),
        variantBuff: new(DefiantOutsiderNeutralShieldBuff, Guids.DefiantOutsiderNeutralShieldBuff));

      // Plants
      var plantsEnchantInfo = new ArmorEnchantInfo(DefiantPlantsName, Description, icon, EnhancementCost);
      var plantsEffectBuff = ConfigureEffect(
        plantsEnchantInfo,
        new(DefiantPlantsEffect, Guids.DefiantPlantsEffect),
        FeatureRefs.PlantType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        plantsEnchantInfo,
        plantsEffectBuff,
        parentBuff: new(DefiantPlantsBuff, Guids.DefiantPlantsBuff),
        variantBuff: new(DefiantPlantsShieldBuff, Guids.DefiantPlantsShieldBuff));

      // Undead
      var undeadEnchantInfo = new ArmorEnchantInfo(DefiantUndeadName, Description, icon, EnhancementCost);
      var undeadEffectBuff = ConfigureEffect(
        undeadEnchantInfo,
        new(DefiantUndeadEffect, Guids.DefiantUndeadEffect),
        FeatureRefs.UndeadType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        undeadEnchantInfo,
        undeadEffectBuff,
        parentBuff: new(DefiantUndeadBuff, Guids.DefiantUndeadBuff),
        variantBuff: new(DefiantUndeadShieldBuff, Guids.DefiantUndeadShieldBuff));

      // Vermin
      var verminEnchantInfo = new ArmorEnchantInfo(DefiantVerminName, Description, icon, EnhancementCost);
      var verminEffectBuff = ConfigureEffect(
        verminEnchantInfo,
        new(DefiantVerminEffect, Guids.DefiantVerminEffect),
        FeatureRefs.VerminType.ToString());

      EnchantTool.CreateEnchantWithEffect(
        verminEnchantInfo,
        verminEffectBuff,
        parentBuff: new(DefiantVerminBuff, Guids.DefiantVerminBuff),
        variantBuff: new(DefiantVerminShieldBuff, Guids.DefiantVerminShieldBuff));
    }

    private static BlueprintBuff ConfigureEffect(
      ArmorEnchantInfo enchantInfo,
      BlueprintInfo buff,
      Blueprint<BlueprintFeatureReference> typeFeature,
      AlignmentComponent? alignment = null)
    {
      return BuffConfigurator.New(buff.Name, buff.Guid)
        .SetDisplayName(enchantInfo.DisplayName)
        .SetDescription(enchantInfo.Description)
        .SetIcon(enchantInfo.Icon)
        .SetFlags(BlueprintBuff.Flags.StayOnDeath)
        .AddComponent(new DefiantComponent(typeFeature.Reference, alignment))
        .AddComponent(new DefiantResistanceComponent(typeFeature.Reference, alignment))
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