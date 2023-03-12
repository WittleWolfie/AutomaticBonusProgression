using AutomaticBonusProgression.Util;
using Kingmaker;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Reflecting
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Reflecting));

    private const string ReflectingName = "LegendaryArmor.Reflecting";
    private const string BuffName = "LegendaryArmor.Reflecting.Buff";
    private const string AbilityName = "LegendaryArmor.Reflecting.Ability";

    private const string DisplayName = "LegendaryArmor.Reflecting.Name";
    private const string Description = "LegendaryArmor.Reflecting.Description";
    private const int EnhancementCost = 5;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Reflecting");

      var enchantInfo = new ArmorEnchantInfo(
        DisplayName,
        Description,
        "",
        EnhancementCost,
        ranks: 5);

      var ability = EnchantmentTool.CreateEnchantShieldVariant(
        enchantInfo,
        new(BuffName, Guids.ReflectingBuff, new ReflectingComponent()),
        new(AbilityName, Guids.ReflectingAbility));
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, new(ReflectingName, Guids.Reflecting), ability);
    }

    [TypeId("5e021fee-7b14-4bae-874b-d3e7ec0d594d")]
    private class ReflectingComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCastSpell>
    {
      public void OnEventAboutToTrigger(RuleCastSpell evt) { }

      public void OnEventDidTrigger(RuleCastSpell evt)
      {
        try
        {
          //if (!evt.Initiator.IsEnemy(Owner))
          //  return;

          //var random = UnityEngine.Random.Range(1, 21);
          //if (random < 20)
          //{
          //  Logger.Verbose(() => $"No spell Reflecting: {random}");
          //  return;
          //}

          Logger.Verbose(() => $"Reflecting {evt.Spell.Name} targeting {Owner.CharacterName} back at {evt.Initiator.CharacterName}");
          evt.SetSuccess(false);
          evt.CancelAbilityExecution();
          var context = evt.Result.Context.CloneFor(Owner, evt.Initiator);
          Game.Instance.AbilityExecutor.Execute(context);
          EventBus.RaiseEvent<ISpellTurningHandler>(h => h.HandleSpellTurned(evt.Initiator, Owner, evt.Spell));
        }
        catch (Exception e)
        {
          Logger.LogException("ReflectingComponent.OnEventAboutToTrigger", e);
        }
      }
    }
  }
}
