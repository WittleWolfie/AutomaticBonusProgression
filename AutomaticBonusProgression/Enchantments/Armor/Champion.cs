using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Champion
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Champion));

    private const string EffectName = "LA.Champion.Effect";
    private const string BuffName = "LA.Champion.Buff";

    private const string DisplayName = "LA.Champion.Name";
    private const string Description = "LA.Champion.Description";
    // Shield of Faith
    private const string Icon = "11e18dc33de41764e879365344869f8f";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Champion");

      var smiteEvil = BuffRefs.SmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var smiteEvilAura = BuffRefs.AuraOfJusticeSmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var challenge = BuffRefs.CavalierChallengeBuffTarget.Cast<BlueprintBuffReference>().Reference;

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, Icon, EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.ChampionEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(Icon)
        .AddComponent(BonusAgainstTarget.AC(smiteEvil, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(smiteEvilAura, 2, ModifierDescriptor.Sacred))
        .AddComponent(BonusAgainstTarget.AC(challenge, 2, ModifierDescriptor.Sacred))
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.ChampionBuff));
    }
  }
}
