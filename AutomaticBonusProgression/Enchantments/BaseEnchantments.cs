using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class BaseEnchantments
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(BaseEnchantments));

    internal static void Configure()
    {
      try
      {
        Logger.Log($"Configuring {nameof(BaseEnchantments)}");

        UpdateArmorEnchantments();
        UpdateWeaponEnchantments();
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
