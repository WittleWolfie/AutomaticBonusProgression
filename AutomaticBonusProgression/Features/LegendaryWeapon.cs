using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UI.Attunement;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryWeapon
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryWeapon));

    private const string LegendaryWeaponName = "LegendaryWeapon";
    private const string LegendaryWeaponDisplayName = "LegendaryWeapon.Name";
    private const string LegendaryWeaponDescription = "LegendaryWeapon.Description";

    private const string LegendaryWeaponAbility = "LegendaryWeapon.Ability";

    private const string LegendaryWeaponResource = "LegendaryWeapon.Resource";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Weapon");

      // New:
      // - Limning
      // - Ominous
      // - Phase Locking
      // - Quaking
      // - Sneaky
      // - Valiant

      var resource = AbilityResourceConfigurator.New(LegendaryWeaponResource, Guids.LegendaryWeaponResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = BuffRefs.ArcaneAccuracyBuff.Reference.Get().Icon;
      var ability = AbilityConfigurator.New(LegendaryWeaponAbility, Guids.LegendaryWeaponAbility)
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponDescription)
        .SetIcon(icon)
        .SetType(AbilityType.Supernatural)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.MainHand))
        .Configure();

      return FeatureConfigurator.New(LegendaryWeaponName, Guids.LegendaryWeapon)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponDescription)
        .SetIcon(icon)
        .SetRanks(5)
        .AddAbilityResources(resource: resource, restoreAmount: true)
        .AddFacts(new() { ability })
        .AddComponent(
          new AttunementBuffsComponent(
      #region Aligned
            Guids.AnarchicBuff,
            Guids.AxiomaticBuff,
            Guids.HolyBuff,
            Guids.UnholyBuff,
      #endregion
      #region Bane
            Guids.BaneAberrationsBuff,
            Guids.BaneAnimalsBuff,
            Guids.BaneConstructsBuff,
            Guids.BaneDragonsBuff,
            Guids.BaneFeyBuff,
            Guids.BaneHumanoidGiantBuff,
            Guids.BaneHumanoidMonstrousBuff,
            Guids.BaneHumanoidReptilianBuff,
            Guids.BaneMagicalBeastsBuff,
            Guids.BaneOutsiderChaoticBuff,
            Guids.BaneOutsiderEvilBuff,
            Guids.BaneOutsiderGoodBuff,
            Guids.BaneOutsiderLawfulBuff,
            Guids.BaneOutsiderNeutralBuff,
            Guids.BanePlantsBuff,
            Guids.BaneUndeadBuff,
            Guids.BaneVerminBuff,
      #endregion
            Guids.BewilderingBuff,
            Guids.BrawlingWeaponBuff,
            Guids.BrilliantEnergyBuff,
            Guids.CourageousBuff,
            Guids.CruelBuff,
            Guids.CullingBuff,
            Guids.CunningBuff,
            Guids.DazzlingRadianceBuff,
            Guids.DebilitatingBuff,
            Guids.DisruptionBuff,
            Guids.DistractingBuff,
            Guids.DistractingGreaterBuff,
            Guids.DuelingBuff,
      #region Elemental
            Guids.CorrosiveBuff,
            Guids.CorrosiveBurstBuff,
            Guids.FlamingBuff,
            Guids.FlamingBurstBuff,
            Guids.FrostBuff,
            Guids.FrostBurstBuff,
            Guids.ShockingBuff,
            Guids.ShockingBurstBuff,
            Guids.ThunderingBuff,
            Guids.ThunderingBurstBuff,
      #endregion,
            Guids.FortuitousBuff,
            Guids.FuriousBuff,
            Guids.FurybornBuff,
            Guids.GhostTouchBuff,
            Guids.GrowingBuff,
            Guids.HeartseekerBuff,
            Guids.ImpactBuff,
            Guids.InvigoratingBuff,
            Guids.KeenBuff,
            Guids.LeveragingBuff,
            Guids.LifesurgeBuff,
            Guids.NullifyingBuff,
            Guids.SpeedBuff,
            Guids.ViciousBuff,
            Guids.VorpalBuff
          ))
        .Configure();
    }

    private const string LegendaryOffHandName = "LegendaryOffHand";
    private const string LegendaryOffHandDisplayName = "LegendaryOffHand.Name";
    private const string LegendaryOffHandDescription = "LegendaryOffHand.Description";

    private const string LegendaryOffHandAbility = "LegendaryOffHand.Ability";

    private const string LegendaryOffHandResource = "LegendaryOffHand.Resource";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log("Configuring Legendary Off-Hand");

      var resource = AbilityResourceConfigurator.New(LegendaryOffHandResource, Guids.LegendaryOffHandResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var icon = AbilityRefs.SacredWeaponEnchantSwitchAbility.Reference.Get().Icon;
      var ability = AbilityConfigurator.New(LegendaryOffHandAbility, Guids.LegendaryOffHandAbility)
        .SetDisplayName(LegendaryOffHandDisplayName)
        .SetDescription(LegendaryOffHandDescription)
        .SetIcon(icon)
        .SetType(AbilityType.Supernatural)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.OffHand))
        .Configure();

      return FeatureConfigurator.New(LegendaryOffHandName, Guids.LegendaryOffHand)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryOffHandDisplayName)
        .SetDescription(LegendaryOffHandDescription)
        .SetIcon(icon)
        .SetRanks(5)
        .AddAbilityResources(resource: resource, restoreAmount: true)
        .AddFacts(new() { ability })
        .AddComponent(
          new AttunementBuffsComponent(
      #region Aligned
            Guids.AnarchicOffHandBuff,
            Guids.AxiomaticOffHandBuff,
            Guids.HolyOffHandBuff,
            Guids.UnholyOffHandBuff,
      #endregion
      #region Bane
            Guids.BaneAberrationsOffHandBuff,
            Guids.BaneAnimalsOffHandBuff,
            Guids.BaneConstructsOffHandBuff,
            Guids.BaneDragonsOffHandBuff,
            Guids.BaneFeyOffHandBuff,
            Guids.BaneHumanoidGiantOffHandBuff,
            Guids.BaneHumanoidMonstrousOffHandBuff,
            Guids.BaneHumanoidReptilianOffHandBuff,
            Guids.BaneMagicalBeastsOffHandBuff,
            Guids.BaneOutsiderChaoticOffHandBuff,
            Guids.BaneOutsiderEvilOffHandBuff,
            Guids.BaneOutsiderGoodOffHandBuff,
            Guids.BaneOutsiderLawfulOffHandBuff,
            Guids.BaneOutsiderNeutralOffHandBuff,
            Guids.BanePlantsOffHandBuff,
            Guids.BaneUndeadOffHandBuff,
            Guids.BaneVerminOffHandBuff,
      #endregion
            Guids.BewilderingOffHandBuff,
            Guids.BrawlingWeaponOffHandBuff,
            Guids.BrilliantEnergyOffHandBuff,
            Guids.CourageousOffHandBuff,
            Guids.CruelOffHandBuff,
            Guids.CullingOffHandBuff,
            Guids.CunningOffHandBuff,
            Guids.DazzlingRadianceOffHandBuff,
            Guids.DebilitatingOffHandBuff,
            Guids.DisruptionOffHandBuff,
            Guids.DistractingOffHandBuff,
            Guids.DistractingGreaterOffHandBuff,
            Guids.DuelingOffHandBuff,
      #region Elemental
            Guids.CorrosiveOffHandBuff,
            Guids.CorrosiveBurstOffHandBuff,
            Guids.FlamingOffHandBuff,
            Guids.FlamingBurstOffHandBuff,
            Guids.FrostOffHandBuff,
            Guids.FrostBurstOffHandBuff,
            Guids.ShockingOffHandBuff,
            Guids.ShockingBurstOffHandBuff,
            Guids.ThunderingOffHandBuff,
            Guids.ThunderingBurstOffHandBuff,
      #endregion
            Guids.FortuitousOffHandBuff,
            Guids.FuriousOffHandBuff,
            Guids.FurybornOffHandBuff,
            Guids.GhostTouchOffHandBuff,
            Guids.GrowingOffHandBuff,
            Guids.HeartseekerOffHandBuff,
            Guids.ImpactOffHandBuff,
            Guids.InvigoratingOffHandBuff,
            Guids.KeenOffHandBuff,
            Guids.LeveragingOffHandBuff,
            Guids.LifesurgeOffHandBuff,
            Guids.NullifyingOffHandBuff,
            Guids.SpeedOffHandBuff,
            Guids.ViciousOffHandBuff,
            Guids.VorpalOffHandBuff
          ))
        .Configure();
    }
  }
}
