using Kingmaker.UI.MVVM._VM.Tooltip.Bricks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Owlcat.Runtime.UI.Tooltips;
using System.Collections.Generic;

namespace AutomaticBonusProgression.UI.Attunement
{
  internal class TooltipTemplateEnchantment : TooltipBaseTemplate
  {
    private readonly BlueprintBuff Enchant;

    internal TooltipTemplateEnchantment(BlueprintBuff enchant)
    {
      Enchant = enchant;
    }

    public override IEnumerable<ITooltipBrick> GetHeader(TooltipTemplateType type)
    {
      yield return new TooltipBrickEntityHeader(Enchant.Name, Enchant.Icon);
      yield break;
    }

    public override IEnumerable<ITooltipBrick> GetBody(TooltipTemplateType type)
    {
      yield return new TooltipBrickText(Enchant.Description, type: TooltipTextType.Paragraph);
    }
  }
}
