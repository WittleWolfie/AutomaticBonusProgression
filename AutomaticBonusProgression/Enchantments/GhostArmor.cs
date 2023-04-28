using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class GhostArmor
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(GhostArmor));

    private const string GhostArmorName = "LegendaryArmor.GhostArmor";
    private const string BuffName = "LegendaryArmor.GhostArmor.Buff";
    private const string AbilityName = "LegendaryArmor.GhostArmor.Ability";
    private const string BuffShieldName = "LegendaryArmor.GhostArmor.Buff.Shield";
    private const string AbilityShieldName = "LegendaryArmor.GhostArmor.Ability.Shield";

    private const string AuraName = "LegendaryArmor.GhostArmor.Aura";
    private const string AuraBuffName = "LegendaryArmor.GhostArmor.Aura.Buff";

    private const string DisplayName = "LegendaryArmor.GhostArmor.Name";
    private const string Description = "LegendaryArmor.GhostArmor.Description";
    private const int EnhancementCost = 3;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring GhostArmor Armor");

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ranks: 3);

      var buff = BuffConfigurator.New(BuffName, Guids.GhostArmorBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddComponent(new EnhancementEquivalence(enchantInfo))
        .AddComponent(new GhostArmorComponent())
        .Configure();

      var ability = EnchantTool.CreateEnchantAbility(
        enchantInfo,
        buff,
        new(AbilityName, Guids.GhostArmorAbility));

      // Since the buff is actually different for shield create the shield stuff manually (based on CreateEnchantShieldVariant)
      var shieldBuff = BuffConfigurator.New(BuffShieldName, Guids.GhostArmorShieldBuff)
        .CopyFrom(buff)
        .AddComponent(new EnhancementEquivalence(enchantInfo, typeOverride: EnhancementType.Shield))
        .AddComponent(new RequireShield(enchantInfo.AllowedTypes))
        .AddComponent(new GhostArmorComponent(toShield: true))
        .Configure();
      var shieldAbility = ActivatableAbilityConfigurator.New(AbilityShieldName, Guids.GhostArmorShieldAbility)
        .CopyFrom(ability, c => c is not EnhancementRestriction && c is not ArmorRestriction)
        .AddComponent(new EnhancementRestriction(enchantInfo, typeOverride: EnhancementType.Shield))
        .AddComponent(new ShieldRestriction(enchantInfo.AllowedTypes))
        .SetBuff(shieldBuff)
        .Configure();

      return EnchantTool.CreateEnchantFeature(
        enchantInfo,
        new(GhostArmorName, Guids.GhostArmor),
        ability,
        shieldAbility);
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
