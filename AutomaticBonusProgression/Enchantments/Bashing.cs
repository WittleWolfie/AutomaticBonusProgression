using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Components;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Bashing
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Bashing));

    private const string BashingName = "LegendaryArmor.Bashing";
    private const string BuffName = "LegendaryArmor.Bashing.Buff";
    private const string AbilityName = "LegendaryArmor.Bashing.Ability";

    private const string DisplayName = "LegendaryArmor.Bashing.Name";
    private const string Description = "LegendaryArmor.Bashing.Description";
    private const int EnhancementCost = 1;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Bashing");

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ranks: 1,
        ArmorProficiencyGroup.LightShield,
        ArmorProficiencyGroup.HeavyShield);

      var buff = BuffConfigurator.New(BuffName, Guids.BashingBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo, typeOverride: EnhancementType.Shield))
        .AddComponent(new RequireShield(enchantInfo.AllowedTypes))
        .AddComponent<BashingComponent>()
        .Configure();

      var ability = ActivatableAbilityConfigurator.New(AbilityName, Guids.BashingAbility)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .SetBuff(buff)
        .SetDeactivateImmediately()
        .SetActivationType(AbilityActivationType.Immediately)
        .SetActivateWithUnitCommand(CommandType.Free)
        .AddComponent(new EnhancementRestriction(enchantInfo, typeOverride: EnhancementType.Shield))
        .AddComponent(new ShieldRestriction(enchantInfo.AllowedTypes))
        .AddComponent<OutOfCombatRestriction>()
        .SetHiddenInUI()
        .Configure();

      return EnchantmentTool.CreateEnchantFeature(enchantInfo, new(BashingName, Guids.Bashing), ability);
    }

    [TypeId("db92a7db-5c80-461c-9744-0faa92d92ce6")]
    private class BashingComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
      public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
      {
        try
        {
          if (!evt.Weapon.IsShield)
            return;

          Logger.Verbose(() => $"Increasing weapon size of shield for {Owner.CharacterName}");
          evt.IncreaseWeaponSize(1);
        }
        catch (Exception e)
        {
          Logger.LogException("BashingComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleCalculateWeaponStats evt) { }
    }
  }
}
