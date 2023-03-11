using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Righteous
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Righteous));

    private const string RighteousName = "LegendaryArmor.Righteous";
    private const string BuffName = "LegendaryArmor.Righteous.Buff";
    private const string AbilityName = "LegendaryArmor.Righteous.Ability";

    private const string CastAbilityName = "LegendaryArmor.Righteous.Cast";
    private const string CastResourceName = "LegendaryArmor.Righteous.Cast.Resource";

    private const string DisplayName = "LegendaryArmor.Righteous.Name";
    private const string Description = "LegendaryArmor.Righteous.Description";
    private const int EnhancementCost = 5;

    internal static BlueprintFeature Configure()
    {
      Logger.Log($"Configuring Righteous");

      var righteousMight = AbilityRefs.RighteousMight.Reference.Get();
      var castResource = AbilityResourceConfigurator.New(CastResourceName, Guids.RighteousCastResource)
        .SetIcon(righteousMight.Icon)
        .SetMaxAmount(ResourceAmountBuilder.New(1))
        .Configure();

      var castAbility = AbilityConfigurator.New(CastAbilityName, Guids.RighteousCastAbility)
        .CopyFrom(righteousMight, typeof(SpellComponent), typeof(AbilityEffectRunAction), typeof(AbilitySpawnFx))
        .SetDisplayName(DisplayName)
        .SetType(AbilityType.SpellLike)
        .SetActionType(CommandType.Swift)
        .SetAvailableMetamagic()
        .AddAbilityResourceLogic(requiredResource: castResource, isSpendResource: true)
        .AddAbilityCasterHasFacts(new() { Guids.RighteousBuff })
        .Configure();

      var enchantInfo =
        new ArmorEnchantInfo(
          DisplayName,
          Description,
          "",
          EnhancementCost,
          ranks: 5);

      var ability = EnchantmentTool.CreateEnchantAbility(
        enchantInfo,
        new BlueprintInfo(BuffName, Guids.RighteousBuff),
        new(AbilityName, Guids.RighteousAbility));

      var featureInfo =
        new BlueprintInfo(
          RighteousName,
          Guids.Righteous,
          new AddAbilityResources()
          {
            RestoreAmount = true,
            m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
          });
      return EnchantmentTool.CreateEnchantFeature(enchantInfo, featureInfo, ability, castAbility);
    }
  }
}
