using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Brawling
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Brawling));

    private const string BrawlingName = "LegendaryArmor.Brawling";
    private const string BuffName = "LegendaryArmor.Brawling.Buff";
    private const string AbilityName = "LegendaryArmor.Brawling.Ability";

    private const string DisplayName = "LegendaryArmor.Brawling.Name";
    private const string Description = "LegendaryArmor.Brawling.Description";
    private const int EnhancementCost = 3;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Brawling");

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 3);

      var buff = BuffConfigurator.New(BuffName, Guids.BrawlingBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddComponent(new EnhancementEquivalenceComponent(enchantInfo))
        .AddComponent(new RequireArmor(enchantInfo.AllowedTypes))
        .AddWeaponTypeDamageBonus(weaponType: WeaponTypeRefs.Unarmed.ToString(), damageBonus: 2)
        .AddWeaponCategoryAttackBonus(category: WeaponCategory.UnarmedStrike, attackBonus: 2)
        .Configure();

      return EnchantmentTool.CreateEnchant(
        enchantInfo,
        buff,
        new(AbilityName, Guids.BrawlingAbility),
        new(BrawlingName, Guids.Brawling));
    }
  }
}
