using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UI.Attunement;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryArmor));

    private const string LegendaryArmorName = "LegendaryArmor";
    private const string LegendaryArmorDisplayName = "LegendaryArmor.Name";
    private const string LegendaryArmorDescription = "LegendaryArmor.Description";

    private const string LegendaryArmorAbility = "LegendaryArmor.Ability";

    private const string LegendaryArmorResource = "LegendaryArmor.Resource";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Armor");

      var resource = AbilityResourceConfigurator.New(LegendaryArmorResource, Guids.LegendaryArmorResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = BuffRefs.MageArmorBuff.Reference.Get().Icon;
      var ability = AbilityConfigurator.New(LegendaryArmorAbility, Guids.LegendaryArmorAbility)
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        .SetIcon(icon)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.Armor))
        .Configure();

      return FeatureConfigurator.New(LegendaryArmorName, Guids.LegendaryArmor)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryArmorDisplayName)
        .SetDescription(LegendaryArmorDescription)
        .SetIcon(icon)
        .SetRanks(5)
        .AddAbilityResources(resource: resource, restoreAmount: true)
        .AddFacts(new() { ability })
        .AddComponent(
          new AttunementBuffsComponent(
            Guids.BalancedBuff,
            Guids.BolsteringBuff,
            Guids.BrawlingBuff,
            Guids.ChampionBuff,
            Guids.CreepingBuff,
            Guids.DastardBuff,
            Guids.DeathlessBuff,
      #region Defiant
            Guids.DefiantAberrationsBuff,
            Guids.DefiantAnimalsBuff,
            Guids.DefiantConstructsBuff,
            Guids.DefiantDragonsBuff,
            Guids.DefiantFeyBuff,
            Guids.DefiantHumanoidGiantBuff,
            Guids.DefiantHumanoidReptilianBuff,
            Guids.DefiantHumanoidMonstrousBuff,
            Guids.DefiantMagicalBeastsBuff,
            Guids.DefiantOutsiderGoodBuff,
            Guids.DefiantOutsiderEvilBuff,
            Guids.DefiantOutsiderLawfulBuff,
            Guids.DefiantOutsiderChaoticBuff,
            Guids.DefiantOutsiderNeutralBuff,
            Guids.DefiantPlantsBuff,
            Guids.DefiantUndeadBuff,
            Guids.DefiantVerminBuff
      #endregion
      //      Guids.DeterminationBuff,
      //#region Energy Resistance
      //      // 10 Armor
      //      Guids.AcidResist10Buff,
      //      Guids.ColdResist10Buff,
      //      Guids.ElectricityResist10Buff,
      //      Guids.FireResist10Buff,
      //      Guids.SonicResist10Buff,

      //      // 20 Armor
      //      Guids.AcidResist20Buff,
      //      Guids.ColdResist20Buff,
      //      Guids.ElectricityResist20Buff,
      //      Guids.FireResist20Buff,
      //      Guids.SonicResist20Buff,

      //      // 30 Armor
      //      Guids.AcidResist30Buff,
      //      Guids.ColdResist30Buff,
      //      Guids.ElectricityResist30Buff,
      //      Guids.FireResist30Buff,
      //      Guids.SonicResist30Buff,
      //#endregion
      //      Guids.ExpeditiousBuff,
      //      Guids.FortificationBuff,
      //      Guids.ImprovedFortificationBuff,
      //      Guids.GreaterFortificationBuff,
      //      Guids.GhostArmorBuff,
      //      Guids.InvulnerabilityBuff,
      //      Guids.MartyringBuff,
      //      Guids.RallyingBuff,
      //      Guids.ReflectingBuff,
      //      Guids.RighteousBuff,
      //      Guids.ShadowBuff,
      //      Guids.ImprovedShadowBuff,
      //      Guids.GreaterShadowBuff,
      //      Guids.SpellResistance13Buff,
      //      Guids.SpellResistance16Buff,
      //      Guids.SpellResistance19Buff,
      //      Guids.SpellResistance22Buff
          ))
        .Configure();
    }

    private const string LegendaryShieldName = "LegendaryShield";
    private const string LegendaryShieldDisplayName = "LegendaryShield.Name";
    private const string LegendaryShieldDescription = "LegendaryShield.Description";

    private const string LegendaryShieldAbility = "LegendaryShield.Ability";

    private const string LegendaryShieldResource = "LegendaryShield.Resource";

    internal static BlueprintFeature ConfigureShield()
    {
      Logger.Log("Configuring Legendary Shield");

      var resource = AbilityResourceConfigurator.New(LegendaryShieldResource, Guids.LegendaryShieldResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = BuffRefs.MageShieldBuff.Reference.Get().Icon;
      var ability = AbilityConfigurator.New(LegendaryShieldAbility, Guids.LegendaryShieldAbility)
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        .SetIcon(icon)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.Shield))
        .Configure();

      return FeatureConfigurator.New(LegendaryShieldName, Guids.LegendaryShield)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryShieldDisplayName)
        .SetDescription(LegendaryShieldDescription)
        .SetIcon(icon)
        .SetRanks(5)
        .AddAbilityResources(resource: resource, restoreAmount: true)
        .AddFacts(new() { ability })
        .AddComponent(
          new AttunementBuffsComponent(
            Guids.BashingBuff,
            Guids.BolsteringShieldBuff,
      #region Defiant
            Guids.DefiantAberrationsShieldBuff,
            Guids.DefiantAnimalsShieldBuff,
            Guids.DefiantConstructsShieldBuff,
            Guids.DefiantDragonsShieldBuff,
            Guids.DefiantFeyShieldBuff,
            Guids.DefiantHumanoidGiantShieldBuff,
            Guids.DefiantHumanoidReptilianShieldBuff,
            Guids.DefiantHumanoidMonstrousShieldBuff,
            Guids.DefiantMagicalBeastsShieldBuff,
            Guids.DefiantOutsiderGoodShieldBuff,
            Guids.DefiantOutsiderEvilShieldBuff,
            Guids.DefiantOutsiderLawfulShieldBuff,
            Guids.DefiantOutsiderChaoticShieldBuff,
            Guids.DefiantOutsiderNeutralShieldBuff,
            Guids.DefiantPlantsShieldBuff,
            Guids.DefiantUndeadShieldBuff,
            Guids.DefiantVerminShieldBuff
      #endregion
      //      Guids.DeterminationShieldBuff,
      //#region Energy Resistance
      //      // 10 Shield
      //      Guids.AcidResist10ShieldBuff,
      //      Guids.ColdResist10ShieldBuff,
      //      Guids.ElectricityResist10ShieldBuff,
      //      Guids.FireResist10ShieldBuff,
      //      Guids.SonicResist10ShieldBuff,

      //      // 20 Shield
      //      Guids.AcidResist20ShieldBuff,
      //      Guids.ColdResist20ShieldBuff,
      //      Guids.ElectricityResist20ShieldBuff,
      //      Guids.FireResist20ShieldBuff,
      //      Guids.SonicResist20ShieldBuff,

      //      // 30 Shield
      //      Guids.AcidResist30ShieldBuff,
      //      Guids.ColdResist30ShieldBuff,
      //      Guids.ElectricityResist30ShieldBuff,
      //      Guids.FireResist30ShieldBuff,
      //      Guids.SonicResist30ShieldBuff,
      //#endregion
      //      Guids.FortificationShieldBuff,
      //      Guids.ImprovedFortificationShieldBuff,
      //      Guids.GreaterFortificationShieldBuff,
      //      Guids.GhostArmorShieldBuff,
      //      Guids.RallyingShieldBuff,
      //      Guids.SpellResistance13ShieldBuff,
      //      Guids.SpellResistance16ShieldBuff,
      //      Guids.SpellResistance19ShieldBuff,
      //      Guids.SpellResistance22ShieldBuff
          ))
        .Configure();
    }
  }
}
