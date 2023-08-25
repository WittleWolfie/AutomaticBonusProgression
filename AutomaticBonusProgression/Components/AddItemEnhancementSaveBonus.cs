using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;

namespace AutomaticBonusProgression.Components
{
  /// <summary>
  /// Applies the item's enhancement bonus as a saving throw bonus.
  /// </summary>
  [TypeId("c61be09b-d2d3-469b-8234-deaf289f8ac0")]
  [AllowedOn(typeof(BlueprintItemEnchantment))]
  [AllowMultipleComponents]
  internal class AddItemEnhancementSaveBonus
    : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AddItemEnhancementSaveBonus));

    private readonly SavingThrowType Save;
    private readonly SpellDescriptor SpellDescriptors;
    private readonly ModifierDescriptor Descriptor;

    internal AddItemEnhancementSaveBonus(
      SavingThrowType save,
      SpellDescriptor spellDescriptors = SpellDescriptor.None,
      ModifierDescriptor descriptor = ModifierDescriptor.Enhancement)
    {
      Save = save;
      SpellDescriptors = spellDescriptors;
      Descriptor = descriptor;
    }

    public void OnEventAboutToTrigger(RuleSavingThrow evt)
    {
      try
      {
        var context = evt.Reason.Context;
        if (context is null)
        {
          Logger.Warning($"Missing event context!");
          return;
        }

        if (SpellDescriptors == SpellDescriptor.None || context.SpellDescriptor.HasAnyFlag(SpellDescriptors))
        {
          var bonus = GameHelper.GetItemEnhancementBonus(Owner);
          evt.AddTemporaryModifier(evt.Initiator.Stats.SaveWill.AddModifier(bonus, Runtime, Descriptor));
          Logger.Verbose(() => $"Added {bonus} to saving throw for {evt.Initiator.CharacterName}");
        }
      }
      catch (Exception e)
      {
        Logger.LogException("AddItemEnhancementSaveBonus.OnEventAboutToTrigger", e);
      }
    }

    public void OnEventDidTrigger(RuleSavingThrow evt) { }
  }
}
