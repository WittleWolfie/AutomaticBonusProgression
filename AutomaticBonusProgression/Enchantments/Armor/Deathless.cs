using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Components;
using System;

namespace AutomaticBonusProgression.Enchantments.Armor
{
    internal class Deathless
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Deathless));

        private const string DeathlessName = "LegendaryArmor.Deathless";
        private const string BuffName = "LegendaryArmor.Deathless.Buff";
        private const string AbilityName = "LegendaryArmor.Deathless.Ability";

        private const string DisplayName = "LegendaryArmor.Deathless.Name";
        private const string Description = "LegendaryArmor.Deathless.Description";
        private const int EnhancementCost = 1;

        internal static BlueprintFeature Configure()
        {
            Logger.Log($"Configuring Deathless");

            var enchantInfo =
              new ArmorEnchantInfo(
                DisplayName,
                Description,
                "",
                EnhancementCost,
                ranks: 1);

            var buff = BuffConfigurator.New(BuffName, Guids.DeathlessBuff)
              .SetDisplayName(DisplayName)
              .SetDescription(Description)
              //.SetIcon(icon)
              .AddComponent(new EnhancementEquivalence(enchantInfo))
              .AddComponent<DeathlessComponent>()
              .AddDamageResistanceEnergy(type: DamageEnergyType.NegativeEnergy, value: 10)
              .AddDamageResistanceEnergy(type: DamageEnergyType.PositiveEnergy, value: 10)
              .Configure();

            var abilityInfo = new BlueprintInfo(AbilityName, Guids.DeathlessAbility);
            var featureInfo = new BlueprintInfo(DeathlessName, Guids.Deathless);

            var ability = EnchantTool.CreateEnchantAbility(enchantInfo, buff, abilityInfo);
            return EnchantTool.CreateEnchantFeature(enchantInfo, featureInfo, ability);
        }

        [TypeId("23ca83d5-128e-4937-9fd5-646a86a3ed0d")]
        private class DeathlessComponent : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleDrainEnergy>
        {
            public void OnEventAboutToTrigger(RuleDrainEnergy evt)
            {
                try
                {
                    if (UnityEngine.Random.Range(0, 4) == 0)
                    {
                        Logger.Verbose(() => "Negating energy drain");
                        evt.TargetIsImmune = true;
                    }
                }
                catch (Exception e)
                {
                    Logger.LogException("DeathlessComponent.OnEventAboutToTrigger", e);
                }
            }

            public void OnEventDidTrigger(RuleDrainEnergy evt) { }
        }
    }
}
