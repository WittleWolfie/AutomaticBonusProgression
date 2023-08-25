using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;

namespace AutomaticBonusProgression.Actions
{
  [TypeId("1f3025cc-c87b-4837-859d-abf007daabb3")]
  internal class RemoveParentBuff : ContextAction
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(RemoveParentBuff));

    public override string GetCaption()
    {
      return "Remove parent buff";
    }

    public override void RunAction()
    {
      try
      {
        var context = ContextData<ItemEnchantment.Data>.Current?.ItemEnchantment?.ParentContext;
        if (context is null)
        {
          Logger.Warning($"Unable to remove parent buff, missing context data.");
          return;
        }

        var owner = context.MaybeOwner;
        if (owner is null)
        {
          Logger.Warning($"Unable to remove parent buff, missing owner.");
          return;
        }

        var parentBuff = context.AssociatedBlueprint;
        foreach (var buff in owner.Buffs)
        {
          if (buff.Blueprint == parentBuff)
          {
            Logger.Verbose(() => $"Removing parent buff: {parentBuff?.name}");
            buff.Remove();
            return;
          }
        }

        Logger.Warning($"Parent buff not found: {parentBuff?.name}");
      }
      catch (Exception e)
      {
        Logger.LogException("RemoveParentBuff.RunAction", e);
      }
    }
  }
}
