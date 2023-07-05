using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.Tooltip.Bricks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Owlcat.Runtime.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Tooltip for selecting Legendary Enchantments.
  /// </summary>
  internal class TooltipTemplateLegendaryEnchantment : TooltipBaseTemplate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(TooltipTemplateLegendaryEnchantment));

    private readonly BlueprintFeature Feature;
    private readonly List<IUIDataProvider> Enchantments = new();

    internal TooltipTemplateLegendaryEnchantment(EnchantmentType type)
    {
      Feature = type switch
      {
        EnchantmentType.Armor => Common.LegendaryArmor,
        EnchantmentType.Shield => Common.LegendaryShield,
        EnchantmentType.Weapon => Common.LegendaryWeapon,
        EnchantmentType.OffHand => Common.LegendaryOffHand,
        _ => throw new NotImplementedException(),
      };
    }

    public override void Prepare(TooltipTemplateType type)
    {
      var sortedEnchantments =
        Feature.GetComponent<AttunementBuffsComponent>().Buffs
          .Select(bp => bp.Get())
          .OrderBy(buff => buff.GetComponent<AttunementEffect>().Cost);

      Enchantments.Clear();
      Enchantments.AddRange(sortedEnchantments);
    }

    public override IEnumerable<ITooltipBrick> GetHeader(TooltipTemplateType type)
    {
      return new List<ITooltipBrick>() { new TooltipBrickFeature(Feature, isHeader: true) };
    }

    public override IEnumerable<ITooltipBrick> GetBody(TooltipTemplateType type)
    {
      yield return GetDescription();

      yield return GetEnchantmentsTitle();
      foreach (var enchant in Enchantments)
        yield return GetEnchantment(enchant);

      yield break;
    }

    private TooltipBrickText GetDescription()
    {
      return new(UIUtilityTexts.GetLongOrShortText(Feature.Description, state: true), TooltipTextType.Paragraph);
    }

    private TooltipBrickTitle GetEnchantmentsTitle()
    {
      return new(UITool.GetString("Enchantments"), TooltipTitleType.H2);
    }

    private TooltipBrickFeature GetEnchantment(IUIDataProvider enchantment)
    {
      return new(enchantment);
    }
  }
}
