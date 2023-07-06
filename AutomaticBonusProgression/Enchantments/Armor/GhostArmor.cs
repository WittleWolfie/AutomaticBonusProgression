using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class GhostArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(GhostArmor));

    private const string EffectName = "LA.GhostArmor.Effect";
    private const string BuffName = "LA.GhostArmor.Buff";
    private const string ShieldEffectName = "LA.GhostArmor.Shield.Effect";
    private const string BuffShieldName = "LA.GhostArmor.Buff.Shield";

    private const string DisplayName = "LA.GhostArmor.Name";
    private const string Description = "LA.GhostArmor.Description";
    private const int EnhancementCost = 3;

    internal static void Configure()
    {
      Logger.Log($"Configuring GhostArmor Armor");

      var icon = AbilityRefs.ArcanistExploitArmoredMaskAbility.Reference.Get().Icon;
      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.GhostArmorEffect, new GhostArmorComponent()),
        parentBuff: new(BuffName, Guids.GhostArmorBuff));

      // Since the buff is actually different for shield create the variant separately
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: new(ShieldEffectName, Guids.GhostArmorShieldEffect, new GhostArmorComponent(toShield: true)),
        variantBuff: new(BuffShieldName, Guids.GhostArmorShieldBuff));
    }

    [TypeId("cc1557d2-4726-47fc-8e3d-2158a97353e4")]
    private class GhostArmorComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleAttackWithWeapon>
    {
      private static BlueprintWeaponType _incorporealTouch;
      private static BlueprintWeaponType IncorporealTouch
      {
        get
        {
          _incorporealTouch ??= WeaponTypeRefs.IncoporealTouchType.Reference.Get();
          return _incorporealTouch;
        }
      }

      private static BlueprintWeaponType _incorporealTouchReach;
      private static BlueprintWeaponType IncorporealTouchReach
      {
        get
        {
          _incorporealTouchReach ??= WeaponTypeRefs.ReachIncoporealTouchType.Reference.Get();
          return _incorporealTouchReach;
        }
      }

      private static BlueprintWeaponType _incorporealBite;
      private static BlueprintWeaponType IncorporealBite
      {
        get
        {
          _incorporealBite ??= WeaponTypeRefs.IncoporealBiteType.Reference.Get();
          return _incorporealBite;
        }
      }

      internal readonly bool ToShield;

      internal GhostArmorComponent(bool toShield = false)
      {
        ToShield = toShield;
      }

      public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          if (evt.Weapon.Blueprint.Type != IncorporealBite
              && evt.Weapon.Blueprint.Type != IncorporealTouch
              && evt.Weapon.Blueprint.Type != IncorporealTouchReach)
            return;

          var bonus = 0;
          if (ToShield && Owner.Body.SecondaryHand.HasShield)
          {
            foreach (var mod in Owner.Stats.AC.Modifiers)
            {
              if (mod.ModDescriptor == ModifierDescriptor.Shield || mod.ModDescriptor == ModifierDescriptor.ShieldFocus)
                bonus += mod.ModValue;
            }

            bonus += GameHelper.GetItemEnhancementBonus(Owner.Body.SecondaryHand.Shield);
          }
          else
          {
            foreach (var mod in Owner.Stats.AC.Modifiers)
            {
              if (mod.ModDescriptor == ModifierDescriptor.Armor
                  || mod.ModDescriptor == ModifierDescriptor.ArmorFocus
                  || mod.ModDescriptor == ModifierDescriptor.ArmorEnhancement)
                bonus += mod.ModValue;
            }

            // Unarmored enhancement bonus is applied directly to AC as ArmorEnhancement, but not if armor is equipped.
            if (Owner.Body.Armor.HasArmor)
            {
              bonus += GameHelper.GetItemEnhancementBonus(Owner.Body.Armor.Armor);
            }
          }

          Logger.Verbose(() => $"Adding {bonus} to AC against incorporeal attack");
          evt.Target.Stats.AC.AddModifierUnique(bonus, Runtime, ModifierDescriptor.UntypedStackable);
        }
        catch (Exception e)
        {
          Logger.LogException("GhostArmorComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackWithWeapon evt)
      {
        try
        {
          evt.Target.Stats.AC.RemoveModifiersFrom(Runtime);
        }
        catch (Exception e)
        {
          Logger.LogException("GhostArmorComponent.OnEventDidTrigger", e);
        }
      }
    }
  }
}
