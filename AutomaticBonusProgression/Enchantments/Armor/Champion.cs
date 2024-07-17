﻿using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Champion
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Champion));

    private const string EffectName = "LA.Champion.Effect";
    private const string BuffName = "LA.Champion.Buff";

    private const string DisplayName = "LA.Champion.Name";
    private const string Description = "LA.Champion.Description";

    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Champion");

      var smiteEvil = BuffRefs.SmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var smiteEvilAura = BuffRefs.AuraOfJusticeSmiteEvilBuff.Cast<BlueprintBuffReference>().Reference;
      var challenge = BuffRefs.CavalierChallengeBuffTarget.Cast<BlueprintBuffReference>().Reference;

      var icon = BuffRefs.ShieldOfFaithBuff.Reference.Get().Icon;
      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.ChampionEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetFlags(BlueprintBuff.Flags.StayOnDeath)
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
