using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.Configurators.Items.Ecnchantments;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using System;

namespace AutomaticBonusProgression.Enchantments
{
  internal class Cunning
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Cunning));

    private const string EnchantName = "LW.Cunning.Enchant";
    private const string EffectName = "LW.Cunning";
    private const string BuffName = "LW.Cunning.Buff";
    private const string OffHandEffectName = "LW.Cunning.OffHand";
    private const string OffHandBuffName = "LW.Cunning.OffHand.Buff";

    private const string DisplayName = "LW.Cunning.Name";
    private const string Description = "LW.Cunning.Description";
    private const int EnhancementCost = 1;

    internal static void Configure()
    {
      Logger.Log($"Configuring Cunning");

      var enchant = WeaponEnchantmentConfigurator.New(EnchantName, Guids.CunningEnchant)
        .SetEnchantName(DisplayName)
        .SetDescription(Description)
        .AddComponent<CunningComponent>()
        .Configure();

      var criticalFocus = FeatureRefs.CriticalFocus.Reference.Get();
      var enchantInfo = new WeaponEnchantInfo(DisplayName, Description, criticalFocus.Icon, EnhancementCost);
      EnchantTool.CreateEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(EffectName, Guids.CunningEffect, enchant),
        parentBuff: new(BuffName, Guids.CunningBuff));
      EnchantTool.CreateVariantEnchant(
        enchantInfo,
        effectBuff: EnchantTool.GetWeaponEffectInfo(
          OffHandEffectName,
          Guids.CunningOffHandEffect,
          enchant,
          toPrimaryWeapon: false),
        variantBuff: new(OffHandBuffName, Guids.CunningOffHandBuff));
    }

    [TypeId("38954b9a-a0d6-460e-8925-90d3d275d989")]
    private class CunningComponent : ItemEnchantmentComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>
    {
      public void OnEventAboutToTrigger(RuleAttackRoll evt)
      {
        try
        {
          if (evt.Weapon != Owner)
            return;

          var wielder = Owner.Wielder;
          if (wielder is null)
          {
            Logger.Warning($"No wielder!");
            return;
          }

          var skill = GetIdentifySkill(evt.Target);
          var ranks = wielder.Stats.GetStat(skill);
          if (ranks < 5)
          {
            Logger.Verbose(() => $"{wielder.CharacterName} does not have enough ranks in {skill}: {ranks}");
            return;
          }

          var bonus = ranks < 15 ? 4 : 6;
          evt.CriticalConfirmationBonus += bonus;
        }
        catch (Exception e)
        {
          Logger.LogException("CunningComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleAttackRoll evt) { }

      private static StatType GetIdentifySkill(UnitEntityData unit)
      {
        return unit.Blueprint.Type ? unit.Blueprint.Type.KnowledgeStat : StatType.SkillLoreNature;
      }
    }
  }
}
