using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System.Collections.Generic;

namespace AutomaticBonusProgression.Enchantments
{
  internal static class EnchantTool
  {
    internal static BlueprintFeature AddEnhancementEquivalence(
      Blueprint<BlueprintReference<BlueprintFeature>> feature, EnchantInfo enchant)
    {
      return FeatureConfigurator.For(feature)
        .AddComponent(enchant.GetEnhancementComponent())
        .Configure();
    }

    internal static BlueprintArmorEnchantment AddEnhancementEquivalenceArmor(
      Blueprint<BlueprintReference<BlueprintArmorEnchantment>> enchantment, EnchantInfo enchant)
    {
      return ArmorEnchantmentConfigurator.For(enchantment)
        .AddComponent(enchant.GetEnhancementComponent())
        .Configure();
    }

    internal static BlueprintWeaponEnchantment AddEnhancementEquivalenceWeapon(
      Blueprint<BlueprintReference<BlueprintWeaponEnchantment>> enchantment, EnchantInfo enchant)
    {
      return WeaponEnchantmentConfigurator.For(enchantment)
        .AddComponent(enchant.GetEnhancementComponent())
        .Configure();
    }

    /// <summary>
    /// Returns blueprint info which applies the specified enchantment to primary weapons or all secondary weapons.
    /// </summary>
    internal static BlueprintInfo GetWeaponEffectInfo(
      string name,
      string guid,
      Blueprint<BlueprintItemEnchantmentReference> enchantment,
      bool toPrimaryWeapon = true)
    {
      return GetWeaponEffectInfo(name, guid, enchantments: new() { enchantment }, toPrimaryWeapon);
    }

    /// <summary>
    /// Returns blueprint info which applies the specified enchantment to primary weapons or all secondary weapons.
    /// </summary>
    internal static BlueprintInfo GetWeaponEffectInfo(
      string name,
      string guid,
      List<Blueprint<BlueprintItemEnchantmentReference>> enchantments,
      bool toPrimaryWeapon = true)
    {
      List<BuffEnchantAnyWeapon> components = new();
      foreach (var enchantment in enchantments)
      {
        if (toPrimaryWeapon)
        {
          components.Add(
            new BuffEnchantAnyWeapon()
            {
              m_EnchantmentBlueprint = enchantment.Reference,
              Slot = EquipSlotBase.SlotType.PrimaryHand
            });
        }
        else
        {
          components.Add(
            new BuffEnchantAnyWeapon()
            {
              m_EnchantmentBlueprint = enchantment.Reference,
              Slot = EquipSlotBase.SlotType.SecondaryHand
            });
          components.Add(
            new BuffEnchantAnyWeapon()
            {
              m_EnchantmentBlueprint = enchantment.Reference,
              Slot = EquipSlotBase.SlotType.AdditionalLimb
            });
        }
      }
      return new(name, guid, components.ToArray());
    }

    /// <summary>
    /// Creates a copy of the source enchantment and sets up the appropriate enhancement equivalence.
    /// </summary>
    internal static void SetUpWeaponEnchant(
      Blueprint<BlueprintReference<BlueprintWeaponEnchantment>> source,
      BlueprintInfo copy,
      WeaponEnchantInfo enchant)
    {
      WeaponEnchantmentConfigurator.New(copy.Name, copy.Guid)
        .CopyFrom(source, c => c is not EnhancementEquivalence)
        .Configure(delayed: true);
      AddEnhancementEquivalenceWeapon(source, enchant);
    }

    /// <summary>
    /// Creates an enchantment's parent buff, plus an optional variant. See also <see cref="CreateEnchant(EnchantInfo, BlueprintInfo, BlueprintInfo, BlueprintInfo)"/>
    /// </summary>
    internal static void CreateEnchantWithEffect(
      EnchantInfo enchant,
      BlueprintBuff effectBuff,
      BlueprintInfo parentBuff,
      BlueprintInfo variantBuff = null)
    {
      BuffConfigurator.For(effectBuff).AddComponent(enchant.GetEnhancementComponent()).Configure();

      // Although the parent buff shouldn't show up, it needs a name / description / icon for the attunement UI.
      var parent = BuffConfigurator.New(parentBuff.Name, parentBuff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        .SetIcon(enchant.Icon)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .AddComponent(enchant.GetAttunementComponent(effectBuff));
      foreach (var component in parentBuff.Components)
        parent.AddComponent(component);
      parent.Configure();

      if (variantBuff is null)
        return;

      var variant = BuffConfigurator.New(variantBuff.Name, variantBuff.Guid)
        .CopyFrom(parentBuff.Guid)
        .AddComponent(enchant.GetAttunementComponent(effectBuff, variant: true));
      foreach (var component in variantBuff.Components)
        variant.AddComponent(component);
      variant.Configure();
    }

    /// <summary>
    /// Creates an enchantment's parent and effect buff, plus an optional variant.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// The <paramref name="parentBuff"/> is responsible for applying <paramref name="effectBuff"/> as long as the
    /// requirements defined in <paramref name="enchant"/> are met. All functional components belong in <paramref name="effectBuff"/>.
    /// </para>
    /// 
    /// <para>
    /// The <paramref name="variantBuff"/> creates a second parent buff for shield / off-hand variants.
    /// </para>
    /// </remarks>
    internal static void CreateEnchant(
      EnchantInfo enchant, BlueprintInfo effectBuff, BlueprintInfo parentBuff, BlueprintInfo variantBuff = null)
    {
      var effect = BuffConfigurator.New(effectBuff.Name, effectBuff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        .SetIcon(enchant.Icon)
        .AddComponent(enchant.GetEnhancementComponent());
      foreach (var component in effectBuff.Components)
        effect.AddComponent(component);
      effect.Configure();

      // Although the parent buff shouldn't show up, it needs a name / description / icon for the attunement UI.
      var parent = BuffConfigurator.New(parentBuff.Name, parentBuff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        .SetIcon(enchant.Icon)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .AddComponent(enchant.GetAttunementComponent(effectBuff.Guid));
      foreach (var component in parentBuff.Components)
        parent.AddComponent(component);
      parent.Configure();

      if (variantBuff is null)
        return;

      var variant = BuffConfigurator.New(variantBuff.Name, variantBuff.Guid)
        .CopyFrom(parentBuff.Guid)
        .AddComponent(enchant.GetAttunementComponent(effectBuff.Guid, variant: true));
      foreach (var component in variantBuff.Components)
        variant.AddComponent(component);
      variant.Configure();
    }

    /// <summary>
    /// Creates a variant-only enchantment. See also <see cref="CreateEnchant(EnchantInfo, BlueprintInfo, BlueprintInfo, BlueprintInfo)"/>
    /// </summary>
    internal static void CreateVariantEnchant(
      EnchantInfo enchant, BlueprintInfo effectBuff, BlueprintInfo variantBuff)
    {
      var effect = BuffConfigurator.New(effectBuff.Name, effectBuff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        .SetIcon(enchant.Icon)
        .AddComponent(enchant.GetEnhancementComponent());
      foreach (var component in effectBuff.Components)
        effect.AddComponent(component);
      effect.Configure();

      var variant = BuffConfigurator.New(variantBuff.Name, variantBuff.Guid)
        .SetDisplayName(enchant.DisplayName)
        .SetDescription(enchant.Description)
        .SetIcon(enchant.Icon)
        .SetFlags(BlueprintBuff.Flags.HiddenInUi)
        .AddComponent(enchant.GetAttunementComponent(effectBuff.Guid, variant: true));
      foreach (var component in variantBuff.Components)
        variant.AddComponent(component);
      variant.Configure();
    }
  }
}
