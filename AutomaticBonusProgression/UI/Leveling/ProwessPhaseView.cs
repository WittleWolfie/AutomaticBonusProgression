using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Common;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.Common;
using Kingmaker.UnitLogic.Class.LevelUp;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling
{
  /// <summary>
  /// View for selecting Prowess bonuses. Extends the ability selection page of leveling
  /// </summary>
  internal class ProwessPhaseView : ViewBase<ProwessPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ProwessPhaseView));

    private SequentialSelectorPCView PhysicalProwessSelector;
    private SequentialSelectorPCView MentalProwessSelector;

    internal void Initialize(
      SequentialSelectorPCView physicalProwessSelector, SequentialSelectorPCView mentalProwessSelector)
    {
      PhysicalProwessSelector = physicalProwessSelector;
      MentalProwessSelector = mentalProwessSelector;
      gameObject.SetActive(false);
    }

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);
      if (ViewModel.IsPhysicalProwessAvailable)
      {
        PhysicalProwessSelector.Bind(ViewModel.PhysicalProwessVM);
        PhysicalProwessSelector.gameObject.SetActive(true);
      }
      else
      {
        PhysicalProwessSelector.gameObject.SetActive(false);
      }

      if (ViewModel.IsMentalProwessAvailable)
      {
        MentalProwessSelector.Bind(ViewModel.MentalProwessVM);
        MentalProwessSelector.gameObject.SetActive(true);
      }
      else
      {
        MentalProwessSelector.gameObject.SetActive(false);
      }
    }

    public override void DestroyViewImplementation()
    {
      gameObject.SetActive(false);
    }

    #region Setup
    private static ProwessPhaseView BaseView;

    // Instantiation & binding
    [HarmonyPatch(typeof(CharGenAbilityScoresDetailedPCView))]
    internal class CharGenAbilityScoresDetailedPCView_Patch
    {
      [HarmonyPatch(nameof(CharGenAbilityScoresDetailedPCView.Initialize)), HarmonyPostfix]
      static void Initialize(CharGenAbilityScoresDetailedPCView __instance)
      {
        try
        {
          Logger.Log($"Initializing ProwessPhaseView");
          BaseView = Create(__instance);
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenAbilityScoresDetailedPCView_Patch.Initialize", e);
        }
      }

      [HarmonyPatch(nameof(CharGenAbilityScoresDetailedPCView.BindViewImplementation)), HarmonyPostfix]
      static void BindViewImplementation(CharGenAbilityScoresDetailedPCView __instance)
      {
        try
        {
          var isPhysicalProwessAvailable = IsPhysicalProwessAvailable(__instance.ViewModel.LevelUpController);
          var isMentalProwessAvailable = IsMentalProwessAvailable(__instance.ViewModel.LevelUpController);
          if (!isPhysicalProwessAvailable && !isMentalProwessAvailable)
            return;

          Logger.Log($"Binding ProwessPhaseVM");
          BaseView.Bind(new(__instance.ViewModel, isPhysicalProwessAvailable, isMentalProwessAvailable));

          // TODO:
          // - Handle binding, make sure it's all being disposed
          // - Tweak view to get the right size & position and all that
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenAbilityScoresDetailedPCView_Patch.Initialize", e);
        }
      }

      internal static ProwessPhaseView Create(CharGenAbilityScoresDetailedPCView abilityScoreView)
      {
        var racialBonusSelector = abilityScoreView.gameObject.ChildObject("AllocatorPlace/Selector/RaceBonusSelector");
        // To create the prowess selection copy the Racial Bonus Selector, then copy the individual selector to create
        // one for Mental and one for Physical (showing one or both as needed)
        var obj = GameObject.Instantiate(racialBonusSelector);
        obj.transform.AddTo(racialBonusSelector.transform.parent);

        // Set up the label
        var label = obj.gameObject.ChildObject("RedLabel (1)/LabelText").GetComponent<TextMeshProUGUI>();
        label.SetText(UIUtility.GetSaberBookFormat(UITool.GetString("Leveling.Prowess")));

        // Create a second selector
        var physicalSelector = obj.gameObject.ChildObject("SqeuntionalSelector");
        var mentalSelector = GameObject.Instantiate(physicalSelector);
        mentalSelector.transform.AddTo(obj.transform);

        var view = obj.AddComponent<ProwessPhaseView>();
        view.Initialize(
          physicalSelector.GetComponent<SequentialSelectorPCView>(),
          mentalSelector.GetComponent<SequentialSelectorPCView>());
        return view;
      }
    }

    // Patch to ensure the ability scores phase is shown for levels where Prowess applies
    [HarmonyPatch(typeof(CharGenVM))]
    static class CharGenVM_Patch
    {
      [HarmonyPatch(nameof(CharGenVM.NeedAbilityScoresPhase)), HarmonyPostfix]
      static void NeedAbilityScoresPhase(CharGenVM __instance, ref bool __result)
      {
        try
        {
          var needPhysicalProwess = IsPhysicalProwessAvailable(__instance.m_LevelUpController);
          var needMentalProwess = IsMentalProwessAvailable(__instance.m_LevelUpController);
          var needAbilityScores = __result;

          // Prowess is an extension of Ability Scores Phase, so make sure it shows when needed
          Logger.Verbose(
            () => $"NeedAbilityScoresPhase: base::{needAbilityScores} || physical::{needPhysicalProwess} || mental::{needMentalProwess}");
          __result = needAbilityScores || needPhysicalProwess || needMentalProwess;
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenVM_Patch.NeedAbilityScoresPhase", e);
        }
      }
    }

    // Ensures the ability score UI reflects Prowess
    [HarmonyPatch(typeof(CharGenAbilityScoreAllocatorVM))]
    static class CharGenAbilityScoreAllocatorVM_Patch
    {
      [HarmonyPatch(nameof(CharGenAbilityScoreAllocatorVM.UpdateStatDistribution)), HarmonyPostfix]
      static void UpdateStatDistribution(CharGenAbilityScoreAllocatorVM __instance)
      {
        try
        {
          if (__instance.StatsDistribution.Available)
            return;

          // Override these so the cost text doesn't display since there are no points to spend
          __instance.UpCost.Value = string.Empty;
          __instance.DownCost.Value = string.Empty;

          // Add Prowess bonuses to the displayed value
          var prowessBonus = GetProwessBonus(__instance.Stat.Value);
          if (prowessBonus > 0)
          {
            int modBonus = prowessBonus / 2;
            Logger.Verbose(() => $"Applying Prowess to {__instance.Stat.Value.Type}: {prowessBonus} / +{modBonus}");
            __instance.StatValue.Value += prowessBonus;
            __instance.Bonus.Value += modBonus;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenAbilityScoreAllocatorVM_Patch.UpdateStatDistribution", e);
        }
      }

      private static int GetProwessBonus(ModifiableValue stat)
      {
        var enhancement = stat.GetModifiers(ModifierDescriptor.Enhancement);
        if (enhancement is null)
          return 0;

        switch (stat.Type)
        {
          case StatType.Strength:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.StrProwess).Sum(mod => mod.ModValue);
          case StatType.Dexterity:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.DexProwess).Sum(mod => mod.ModValue);
          case StatType.Constitution:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.ConProwess).Sum(mod => mod.ModValue);
          case StatType.Intelligence:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.IntProwess).Sum(mod => mod.ModValue);
          case StatType.Wisdom:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.WisProwess).Sum(mod => mod.ModValue);
          case StatType.Charisma:
            return enhancement.Where(mod => mod.Source.Blueprint == Common.ChaProwess).Sum(mod => mod.ModValue);
        }
        return 0;
      }
    }

    private static bool IsPhysicalProwessAvailable(LevelUpController levelUpController)
    {
      var level = levelUpController.State.NextCharacterLevel;
      return SelectProwess.PhysicalProwessLevels.Contains(level);
    }

    private static bool IsMentalProwessAvailable(LevelUpController levelUpController)
    {
      var level = levelUpController.State.NextCharacterLevel;
      return SelectProwess.MentalProwessLevels.Contains(level);
    }
    #endregion
  }

  internal class ProwessPhaseVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ProwessPhaseVM));

    private readonly CharGenAbilityScoresVM AbilityScoresVM;
    private readonly LevelUpController LevelUpController;

    internal readonly StringSequentialSelectorVM PhysicalProwessVM;
    internal readonly StringSequentialSelectorVM MentalProwessVM;
    internal readonly bool IsPhysicalProwessAvailable;
    internal readonly bool IsMentalProwessAvailable;

    internal ProwessPhaseVM(
      CharGenAbilityScoresVM abilityScoresVM,
      bool isPhysicalProwessAvailable,
      bool isMentalProwessAvailable)
    {
      AbilityScoresVM = abilityScoresVM;
      LevelUpController = AbilityScoresVM.m_LevelUpController;

      if (isPhysicalProwessAvailable)
      {
        IsPhysicalProwessAvailable = true;
        PhysicalProwessVM = GetSelectorVM(PhysicalProwess, SelectPhysicalProwess);
      }

      if (isMentalProwessAvailable)
      {
        IsMentalProwessAvailable = true;
        MentalProwessVM = GetSelectorVM(MentalProwess, SelectMentalProwess);
      }
    }

    public override void DisposeImplementation()
    {
      PhysicalProwessVM?.Dispose();
      MentalProwessVM?.Dispose();
    }

    private StringSequentialSelectorVM GetSelectorVM(List<StatType> stats, Action<StatType> onSelect)
    {
      var selections =
        stats.Select(
          type =>
          new StringSequentialEntity()
          {
            Title = LocalizedTexts.Instance.Stats.GetText(type),
            Setter = new(() => onSelect(type)),
            Tooltip = LevelUpController.Preview.Stats.GetStat(type)
          });
      return new(selections.ToList());
    }

    private void SelectPhysicalProwess(StatType stat)
    {
      LevelUpController.RemoveAction<SelectPhysicalProwess>();
      LevelUpController.AddAction(new SelectPhysicalProwess(stat));
    }

    private void SelectMentalProwess(StatType stat)
    {
      LevelUpController.RemoveAction<SelectMentalProwess>();
      LevelUpController.AddAction(new SelectMentalProwess(stat));
    }

    private static readonly List<StatType> PhysicalProwess =
      new()
      {
        StatType.Strength,
        StatType.Dexterity,
        StatType.Constitution
      };
    private static readonly List<StatType> MentalProwess =
      new()
      {
        StatType.Intelligence,
        StatType.Wisdom,
        StatType.Charisma
      };
  }
}
