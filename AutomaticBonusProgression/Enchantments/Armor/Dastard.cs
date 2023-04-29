using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Dastard
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Dastard));

    private const string EffectName = "LA.Dastard.Effect";
    private const string BuffName = "LA.Dastard.Buff";

    private const string DisplayName = "LA.Dastard.Name";
    private const string Description = "LA.Dastard.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Dastard");

      var smiteGood = BuffRefs.FiendishSmiteGoodBuff.Cast<BlueprintBuffReference>().Reference;
      var smiteGoodAlt = BuffRefs.HalfFiendSmiteGoodBuff.Cast<BlueprintBuffReference>().Reference;
      var sinfulAbsolution = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.SinfulAbsolutionBuff);
      var smiteGoodMod = BlueprintTool.GetRef<BlueprintBuffReference>(Guids.SmiteGoodBuff);
      var challenge = BuffRefs.CavalierChallengeBuffTarget.Cast<BlueprintBuffReference>().Reference;

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, "", EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.DastardEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon(icon)
        .AddComponent(BonusAgainstTarget.AC(smiteGood, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodAlt, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(sinfulAbsolution, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(smiteGoodMod, 2, ModifierDescriptor.Profane))
        .AddComponent(BonusAgainstTarget.AC(challenge, 2, ModifierDescriptor.Profane))
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.DastardBuff));
    }
  }
}
