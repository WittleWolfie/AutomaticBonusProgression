using AutomaticBonusProgression.Util;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Utility;
using System;
using static Kingmaker.UnitLogic.Abilities.Blueprints.BlueprintAbilityAreaEffect;

namespace AutomaticBonusProgression.Enchantments.Armor
{
  internal class Rallying
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Rallying));

    private const string EffectName = "LA.Rallying";
    private const string BuffName = "LA.Rallying.Buff";
    private const string BuffShieldName = "LA.Rallying.Buff.Shield";

    private const string AuraName = "LA.Rallying.Aura";
    private const string AuraBuffName = "LA.Rallying.Aura.Buff";

    private const string DisplayName = "LA.Rallying.Name";
    private const string Description = "LA.Rallying.Description";
    private const int EnhancementCost = 2;

    // TODO: Aura FX?
    internal static void Configure()
    {
      Logger.Log($"Configuring Rallying Armor");

      var auraBuff = BuffConfigurator.New(AuraBuffName, Guids.RallyingAuraBuff)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddComponent<RallyingComponent>()
        .Configure();

      var aura = AbilityAreaEffectConfigurator.New(AuraName, Guids.RallyingAura)
        .SetTargetType(TargetType.Ally)
        .SetShape(AreaEffectShape.Cylinder)
        .SetSize(30.Feet())
        .AddAbilityAreaEffectBuff(buff: auraBuff)
        .Configure();

      var enchantInfo = new ArmorEnchantInfo(DisplayName, Description, "", EnhancementCost);

      var effectBuff = BuffConfigurator.New(EffectName, Guids.RallyingEffect)
        .SetDisplayName(DisplayName)
        .SetDescription(Description)
        //.SetIcon()
        .AddAreaEffect(areaEffect: aura)
        .Configure();

      EnchantTool.CreateEnchantWithEffect(
        enchantInfo,
        effectBuff,
        parentBuff: new(BuffName, Guids.RallyingBuff),
        variantBuff: new(BuffShieldName, Guids.RallyingShieldBuff));
    }

    [TypeId("cc1557d2-4726-47fc-8e3d-2158a97353e4")]
    private class RallyingComponent : UnitBuffComponentDelegate, IInitiatorRulebookHandler<RuleSavingThrow>
    {
      private static BlueprintCharacterClass _paladin;
      private static BlueprintCharacterClass Paladin
      {
        get
        {
          _paladin ??= CharacterClassRefs.PaladinClass.Reference.Get();
          return _paladin;
        }
      }

      private static BlueprintCharacterClass _bard;
      private static BlueprintCharacterClass Bard
      {
        get
        {
          _bard ??= CharacterClassRefs.BardClass.Reference.Get();
          return _bard;
        }
      }

      private static BlueprintCharacterClass _cavalier;
      private static BlueprintCharacterClass Cavalier
      {
        get
        {
          _cavalier ??= CharacterClassRefs.CavalierClass.Reference.Get();
          return _cavalier;
        }
      }

      public void OnEventAboutToTrigger(RuleSavingThrow evt)
      {
        try
        {
          if (evt.Reason.Context is null)
            return;

          if (!evt.Reason.Context.SpellDescriptor.HasFlag(SpellDescriptor.Fear))
            return;

          var bonus = 4;
          if (Context.MaybeCaster.Progression.GetClassLevel(Paladin) > 0
              || Context.MaybeCaster.Progression.GetClassLevel(Bard) > 0
              || Context.MaybeCaster.Progression.GetClassLevel(Cavalier) > 0)
            bonus = 6;

          Logger.Verbose(() => $"Adding {bonus} to saving throw against {evt.Reason.Context.SourceAbility.Name} for {Owner.CharacterName}");
          evt.AddModifier(bonus, Buff, ModifierDescriptor.Morale);
        }
        catch (Exception e)
        {
          Logger.LogException("RallyingComponent.OnEventAboutToTrigger", e);
        }
      }

      public void OnEventDidTrigger(RuleSavingThrow evt) { }
    }
  }
}
