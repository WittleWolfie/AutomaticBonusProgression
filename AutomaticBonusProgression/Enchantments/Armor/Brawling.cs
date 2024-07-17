﻿using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Brawling
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Brawling));

    private const string EffectBuff = "LA.Brawling.Effect";
    private const string BuffName = "LA.Brawling.Buff";

    private const string DisplayName = "LA.Brawling.Name";
    private const string Description = "LA.Brawling.Description";

    private const int EnhancementCost = 3;

    internal static void Configure()
    {
      Logger.Log($"Configuring Brawling");

      var icon = FeatureRefs.FlurryOfBlows.Reference.Get().Icon;
      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, icon, EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectBuff, Guids.BrawlingEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        .SetIcon(icon)
        .SetFlags(BlueprintBuff.Flags.StayOnDeath)
        .AddWeaponTypeDamageBonus(weaponType: WeaponTypeRefs.Unarmed.ToString(), damageBonus: 2)
        .AddWeaponCategoryAttackBonus(category: WeaponCategory.UnarmedStrike, attackBonus: 2)
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.BrawlingBuff));
    }
  }
}
