using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Wyrmsbreath
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Wyrmsbreath));

    private const string WyrmsbreathName = "LegendaryArmor.Wyrmsbreath";
    private const string BuffName = "LegendaryArmor.Wyrmsbreath.Buff";
    private const string AbilityName = "LegendaryArmor.Wyrmsbreath.Ability";
    private const string ResourceName = "LegendaryArmor.Wyrmsbreath.Cast.Resource";

    private const string AcidAbilityName = "LegendaryArmor.Wyrmsbreath.Ability.Acid";
    private const string AcidDisplayName = "LegendaryArmor.Wyrmsbreath.Acid.Name";

    private const string ColdAbilityName = "LegendaryArmor.Wyrmsbreath.Ability.Cold";
    private const string ColdDisplayName = "LegendaryArmor.Wyrmsbreath.Cold.Name";

    private const string ElectricityAbilityName = "LegendaryArmor.Wyrmsbreath.Ability.Electricity";
    private const string ElectricityDisplayName = "LegendaryArmor.Wyrmsbreath.Electricity.Name";

    private const string FireAbilityName = "LegendaryArmor.Wyrmsbreath.Ability.Fire";
    private const string FireDisplayName = "LegendaryArmor.Wyrmsbreath.Fire.Name";

    private const string DisplayName = "LegendaryArmor.Wyrmsbreath.Name";
    private const string Description = "LegendaryArmor.Wyrmsbreath.Description";
    private const int EnhancementCost = 2;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Wyrmsbreath");

      var castResource = AbilityResourceConfigurator.New(ResourceName, Guids.WyrmsbreathResource)
        .SetMaxAmount(ResourceAmountBuilder.New(2))
        .Configure();

      var acidAbility = CreateBreathAbility(
        new(AcidAbilityName, Guids.WyrmsbreathAcidAbility),
        AcidDisplayName,
        Description,
        DamageEnergyType.Acid);
      var coldAbility = CreateBreathAbility(
        new(ColdAbilityName, Guids.WyrmsbreathColdAbility),
        ColdDisplayName,
        Description,
        DamageEnergyType.Cold);
      var electricityAbility = CreateBreathAbility(
        new(ElectricityAbilityName, Guids.WyrmsbreathElectricityAbility),
        ElectricityDisplayName,
        Description,
        DamageEnergyType.Electricity);
      var fireAbility = CreateBreathAbility(
        new(FireAbilityName, Guids.WyrmsbreathFireAbility),
        FireDisplayName,
        Description,
        DamageEnergyType.Fire);

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 2);

      var ability = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo, new(BuffName, Guids.WyrmsbreathBuff), new(AbilityName, Guids.WyrmsbreathAbility));

      var featureInfo =
        new BlueprintInfo(
          WyrmsbreathName,
          Guids.Wyrmsbreath,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantmentTool.CreateEnchantFeature(
        enchantInfo,
        featureInfo,
        ability,
        acidAbility,
        coldAbility,
        electricityAbility,
        fireAbility);
    }

    private static BlueprintAbility CreateBreathAbility(
      BlueprintInfo abilityInfo,
      string displayName,
      string description,
      //string icon,
      DamageEnergyType energyType)
    {
      string projectile;
      SpellDescriptor descriptor;
      switch (energyType)
      {
        case DamageEnergyType.Acid:
          projectile = ProjectileRefs.AcidCone15Feet00.ToString();
          descriptor = SpellDescriptor.Acid;
          break;
        case DamageEnergyType.Cold:
          projectile = ProjectileRefs.ColdCone15Feet00.ToString();
          descriptor = SpellDescriptor.Cold;
          break;
        case DamageEnergyType.Electricity:
          projectile = ProjectileRefs.SonicCone15Feet00.ToString();
          descriptor = SpellDescriptor.Electricity;
          break;
        case DamageEnergyType.Fire:
          projectile = ProjectileRefs.FireCone15Feet00.ToString();
          descriptor = SpellDescriptor.Fire;
          break;
        default:
          throw new ArgumentException($"Unsupported energy type: {energyType}");
      }
      return AbilityConfigurator.New(abilityInfo.Name, abilityInfo.Guid)
        .SetDisplayName(displayName)
        .SetDescription(description)
        //.SetIcon(icon)
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Projectile)
        .SetActionType(CommandType.Swift)
        .AddSpellDescriptorComponent(descriptor | SpellDescriptor.BreathWeapon)
        .AddAbilityResourceLogic(requiredResource: Guids.WyrmsbreathResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.WyrmsbreathBuff })
        .AddContextRankConfig(ContextRankConfigs.MythicLevel().WithBonusValueProgression(11))
        .AddAbilityDeliverProjectile(projectiles: new() { projectile })
        .AddAbilityEffectRunAction(
          ActionsBuilder.New()
            .SavingThrow(
              SavingThrowType.Reflex,
              customDC: ContextValues.Rank(),
              onResult: ActionsBuilder.New()
                .DealDamage(
                  DamageTypes.Energy(energyType),
                  ContextDice.Value(DiceType.D4, diceCount: 5),
                  isAoE: true,
                  halfIfSaved: true)))
        .Configure();

    }
  }
}
