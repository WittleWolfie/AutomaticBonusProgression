using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.FactLogic;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Righteous
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Righteous));

    private const string EffectName = "LA.Righteous";
    private const string BuffName = "LA.Righteous.Buff";

    private const string CastAbilityName = "LA.Righteous.Cast";
    private const string CastResourceName = "LA.Righteous.Cast.Resource";

    private const string DisplayName = "LA.Righteous.Name";
    private const string Description = "LA.Righteous.Description";
    private const int EnhancementCost = 5;

    internal static void Configure()
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
        .AddAbilityCasterHasFacts(new() { Guids.RighteousEffect })
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, "", EnhancementCost);

      var addFacts = new AddFacts() { m_Facts = new[] { castAbility.ToReference<BlueprintUnitFactReference>() } };
      var addResources =
        new AddAbilityResources()
        {
          RestoreAmount = true,
          m_Resource = castResource.ToReference<BlueprintAbilityResourceReference>()
        };
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: new(EffectName, Guids.RighteousEffect),
        parentBuff: new(BuffName, Guids.RighteousBuff, addFacts, addResources));
    }
  }
}
