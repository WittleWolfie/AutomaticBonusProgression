﻿using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Enchantments.Armor;
using AutomaticBonusProgression.Enchantments.Replacements;
using AutomaticBonusProgression.Enchantments.Weapon;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Enchantments
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Enchantments));

    internal static void Configure()
    {
      try
      {
        Logger.Log($"Configuring {nameof(Enchantments)}");

        UpdateArmorEnchantments();
        UpdateWeaponEnchantments();
        UpdateTomes();

        // Armor
        Balanced.Configure();
        Bashing.Configure();
        Blinding.Configure();
        Bolstering.Configure();
        Brawling.Configure();
        Champion.Configure();
        Creeping.Configure();
        Dastard.Configure();
        Deathless.Configure();
        Defiant.Configure();
        Determination.Configure();
        EnergyResistance.Configure();
        Expeditious.Configure();
        Fortification.Configure();
        GhostArmor.Configure();
        Invulnerability.Configure();
        Martyring.Configure();
        Rallying.Configure();
        Reflecting.Configure();
        Righteous.Configure();
        Shadow.Configure();
        SpellResistance.Configure();
        Wyrmsbreath.Configure();

        // Weapon
        Anarchic.Configure();
        Axiomatic.Configure();
        Holy.Configure();
        Unholy.Configure();
        Bane.Configure();
        Bewildering.Configure();
        BrawlingWeapon.Configure();
        BrilliantEnergy.Configure();
        Courageous.Configure();
        Cruel.Configure();
        Culling.Configure();
        Cunning.Configure();
        DazzlingRadiance.Configure();
        Debilitating.Configure();
        Disruption.Configure();
        Distracting.Configure();
        Dueling.Configure();
        Elemental.Configure();
        Fortuitous.Configure();
        Furious.Configure();
        Furyborn.Configure();
        GhostTouch.Configure();
        Growing.Configure();
        Heartseeker.Configure();
        Impact.Configure();
        Invigorating.Configure();
        Keen.Configure();
        Leveraging.Configure();
        Lifesurge.Configure();
        Limning.Configure();
        Nullifying.Configure();
        Ominous.Configure();
        PhaseLocking.Configure();
        Quaking.Configure();
        Sneaky.Configure();
        Speed.Configure();
        Valiant.Configure();
        Vicious.Configure();
        Vorpal.Configure();

        // Replacements
        IncreasedStatBonus.Configure();
        SkillBonus.Configure();
        StatBonus.Configure();
      }
      catch (Exception e)
      {
        Logger.LogException("Enchantments.Configure", e);
      }
    }

    private static void UpdateArmorEnchantments()
    {
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus1);
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus2);
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus3);
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus4);
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus5);
      MarkAsMagic(ArmorEnchantmentRefs.ArmorEnhancementBonus6);

      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus1);
      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus2);
      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus3);
      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus4);
      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus5);
      MarkAsMagic(ArmorEnchantmentRefs.ShieldEnhancementBonus6);
    }

    private static void MarkAsMagic(Blueprint<BlueprintReference<BlueprintArmorEnchantment>> enchantment)
    {
      ArmorEnchantmentConfigurator.For(enchantment).AddComponent<MagicItem>().Configure();
    }

    private static void UpdateWeaponEnchantments()
    {
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement1);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement2);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement3);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement4);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement5);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement6);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement7);
      MarkAsMagic(WeaponEnchantmentRefs.Enhancement8);
    }

    private static void MarkAsMagic(Blueprint<BlueprintReference<BlueprintWeaponEnchantment>> enchantment)
    {
      WeaponEnchantmentConfigurator.For(enchantment).AddComponent<MagicItem>().Configure();
    }

    private static string TomeOfPerfection = "968e61c643344565b67d2a7f5e101132";
    private static void UpdateTomes()
    {
      MakeBonusStack(TomeOfPerfection);
      MakeBonusStack(FeatureRefs.TomeOfClearThoughtPlus2_Feature);
      MakeBonusStack(FeatureRefs.TomeOfLeadershipAndInfluencePlus2_Feature);
      MakeBonusStack(FeatureRefs.TomeOfUnderstandingPlus2_Feature);
      MakeBonusStack(FeatureRefs.ManualOfBodilyHealthPlus2_Feature);
      MakeBonusStack(FeatureRefs.ManualOfGainfulExercisePlus2_Feature);
      MakeBonusStack(FeatureRefs.ManualOfQuicknessOfActionPlus2_Feature);
    }

    private static void MakeBonusStack(Blueprint<BlueprintReference<BlueprintFeature>> feature)
    {
      FeatureConfigurator.For(feature)
        .EditComponents<AddStatBonus>(
          c => c.Descriptor = ModifierDescriptor.UntypedStackable,
          c => true)
        .Configure();
    }
  }
}
