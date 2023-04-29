using AutomaticBonusProgression.Util;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using System;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Reflecting
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Reflecting));

    private const string ReflectingName = "LegendaryArmor.Reflecting";
    private const string BuffName = "LegendaryArmor.Reflecting.Buff";
    private const string AbilityName = "LegendaryArmor.Reflecting.Ability";

    private const string CastAbilityName = "LegendaryArmor.Reflecting.Cast";
    private const string CastBuffName = "LegendaryArmor.Reflecting.Cast.Buff";
    private const string CastResourceName = "LegendaryArmor.Reflecting.Cast.Resource";

    private const string CastDisplayName = "LegendaryArmor.Reflecting.Cast.Name";
    private const string CastDescription = "LegendaryArmor.Reflecting.Cast.Description";

    private const string DisplayName = "LegendaryArmor.Reflecting.Name";
    private const string Description = "LegendaryArmor.Reflecting.Description";
    private const int EnhancementCost = 5;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Reflecting");

      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.ReflectingCastResource)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var castBuff = BuffConfigurator.New(CastBuffName, Guids.ReflectingCastBuff)
        .SetDisplayName(CastDisplayName)
        .SetDescription(CastDescription)
        //.SetIcon()
        .AddComponent<ReflectingComponent>()
        .AddNotDispelable()
        .Configure();

      var castAbility = AbilityConfigurator.New(CastAbilityName, Guids.ReflectingCastAbility)
        .SetDisplayName(CastDisplayName)
        .SetDescription(CastDescription)
        //.SetIcon(icon)
        .SetType(AbilityType.SpellLike)
        .SetRange(AbilityRange.Personal)
        .SetActionType(CommandType.Free)
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.ReflectingBuff })
        .AddAbilityEffectRunAction(ActionsBuilder.New().ApplyBuffPermanent(castBuff))
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ranks: 5);

      var ability = EnchantTool.CreateEnchantShieldVariant(
        enchantInfo,
        new(BuffName, Guids.ReflectingBuff),
        new(AbilityName, Guids.ReflectingAbility));
      var featureInfo =
        new BlueprintInfo(
          ReflectingName,
          Guids.Reflecting,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, castAbility);
    }

    [TypeId("5e021fee-7b14-4bae-874b-d3e7ec0d594d")]
    private class ReflectingComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCastSpell>
    {
      public void OnEventAboutToTrigger(RuleCastSpell evt) { }

      public void OnEventDidTrigger(RuleCastSpell evt)
      {
        try
        {
          var result = evt.Result;
          if (!evt.Success || evt.Result is null)
            return;

          if (!evt.Initiator.IsEnemy(Owner))
            return;

          Logger.Verbose(() => $"Reflecting {evt.Spell.Name} targeting {Owner.CharacterName} back at {evt.Initiator.CharacterName}");

          evt.SetSuccess(false);
          evt.CancelAbilityExecution();

          Game.Instance.AbilityExecutor.Execute(result.Context.CloneFor(Owner, evt.Initiator));
          EventBus.RaiseEvent<ISpellTurningHandler>(h => h.HandleSpellTurned(evt.Initiator, Owner, evt.Spell));

          Buff.Remove();
        }
        catch (Exception e)
        {
          Logger.LogException("ReflectingComponent.OnEventAboutToTrigger", e);
        }
      }
    }
  }
}
