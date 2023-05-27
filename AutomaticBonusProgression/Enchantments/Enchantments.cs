using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Enchantments.Armor;
using AutomaticBonusProgression.Enchantments.Weapon;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
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
        Cruel.Configure();
        Elemental.Configure();
        Furious.Configure();
        GhostTouch.Configure();
        Heartseeker.Configure();
        Keen.Configure();
        Vicious.Configure();
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
  }
}
