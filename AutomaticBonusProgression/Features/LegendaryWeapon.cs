using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UI.Attunement;
using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Blueprints.Classes;

namespace AutomaticBonusProgression.Features
{
  internal class LegendaryWeapon
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryWeapon));

    private const string LegendaryWeaponName = "LegendaryWeapon";
    private const string LegendaryWeaponDisplayName = "LegendaryWeapon.Name";
    private const string LegendaryWeaponDescription = "LegendaryWeapon.Description";

    private const string LegendaryWeaponAbility = "LegendaryWeapon.Ability";
    private const string LegendaryWeaponAbilityDescription = "LegendaryWeapon.Ability.Description";

    private const string LegendaryWeaponResource = "LegendaryWeapon.Resource";

    internal static BlueprintFeature Configure()
    {
      Logger.Log("Configuring Legendary Weapon");

      // In Game:
      // - Bane [Done]
      // - Corrosive / Flaming / Frost / Shock / Thundering (Burst) [Done]
      // - Cruel [Done]
      // - Furious [Done]
      // - Ghost Touch [Done]
      // - Heartseeker [Done]
      // - Keen [Done]
      // - Vicious [Done]
      // - Anarchic / Axiomatic [Done]
      // - Disruption [Done]
      // - Furyborn [Done]
      // - Holy / Unholy [Done]
      // - Nullifying [Done]
      // - Speed [Done]
      // - Brilliant Energy [Done]
      // - Vorpal [Done]
      //
      // New:
      // - Bewildering
      // - BrawlingEffect
      // - Countering
      // - Courageous
      // - Cunning
      // - Dazzling Radiance
      // - Debilitating
      // - Distracting
      // - Dueling
      // - Fortuitous
      // - Grounding
      // - Growing
      // - Ki Focus (This might be too complicated)
      // - Leveraging
      // - Limning
      // - Menacing
      // - Ominous
      // - Quenching
      // - Thawing
      // - Valiant
      // - Sneaky
      // - Culling
      // - Defiant
      // - Impact
      // - Invigorating
      // - Legbreaker
      // - Lifesurge
      // - Phase Locking
      // - Quaking
      // - Wounding
      // - Gory

      var resource = AbilityResourceConfigurator.New(LegendaryWeaponResource, Guids.LegendaryWeaponResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var ability = AbilityConfigurator.New(LegendaryWeaponAbility, Guids.LegendaryWeaponAbility)
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponAbilityDescription)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.MainHand))
        .AddHideFeatureInInspect()
        .Configure();

      return FeatureConfigurator.New(LegendaryWeaponName, Guids.LegendaryWeapon)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryWeaponDisplayName)
        .SetDescription(LegendaryWeaponDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddHideFeatureInInspect()
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
            Guids.BrilliantEnergyBuff,
            Guids.CruelBuff,
            Guids.DisruptionBuff,
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
            Guids.FuriousBuff,
            Guids.FurybornBuff,
            Guids.GhostTouchBuff,
            Guids.HeartseekerBuff,
            Guids.KeenBuff,
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
    private const string LegendaryOffHandAbilityDescription = "LegendaryOffHand.Ability.Description";

    private const string LegendaryOffHandResource = "LegendaryOffHand.Resource";

    internal static BlueprintFeature ConfigureOffHand()
    {
      Logger.Log("Configuring Legendary Off-Hand");

      var resource = AbilityResourceConfigurator.New(LegendaryOffHandResource, Guids.LegendaryOffHandResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var ability = AbilityConfigurator.New(LegendaryOffHandAbility, Guids.LegendaryOffHandAbility)
        .SetDisplayName(LegendaryOffHandDisplayName)
        .SetDescription(LegendaryOffHandAbilityDescription)
        .AddAbilityCasterInCombat(not: true)
        .AddAbilityEffectRunAction(ActionsBuilder.New().Add<ShowAttunement>(a => a.Type = EnhancementType.MainHand))
        .AddHideFeatureInInspect()
        .Configure();

      return FeatureConfigurator.New(LegendaryOffHandName, Guids.LegendaryOffHand)
        .SetIsClassFeature()
        .SetDisplayName(LegendaryOffHandDisplayName)
        .SetDescription(LegendaryOffHandDescription)
        //.SetIcon()
        .SetRanks(5)
        .AddHideFeatureInInspect()
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
            Guids.BrilliantEnergyOffHandBuff,
            Guids.CruelOffHandBuff,
            Guids.DisruptionOffHandBuff,
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
            Guids.FuriousOffHandBuff,
            Guids.FurybornOffHandBuff,
            Guids.GhostTouchOffHandBuff,
            Guids.HeartseekerOffHandBuff,
            Guids.KeenOffHandBuff,
            Guids.NullifyingOffHandBuff,
            Guids.SpeedOffHandBuff,
            Guids.ViciousOffHandBuff,
            Guids.VorpalOffHandBuff
          ))
        .Configure();
    }
  }
}
