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
      PhysicalProwessSelector.gameObject.SetActive(false);
      MentalProwessSelector.gameObject.SetActive(false);
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
          {
            BaseView?.gameObject?.SetActive(false);
            return;
          }

          Logger.Log($"Binding ProwessPhaseVM");
          BaseView.Bind(new(__instance.ViewModel, isPhysicalProwessAvailable, isMentalProwessAvailable));
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
        label.SetText(UIUtility.GetSaberBookFormat(UITool.GetString("Prowess")));

        // Create a second selector
        var physicalSelector = obj.gameObject.ChildObject("SqeuntionalSelector");
        var mentalSelector = GameObject.Instantiate(physicalSelector);
        mentalSelector.transform.AddTo(obj.transform);

        // Get rid of the counters which cause issues with size when both are displayed
        physicalSelector.DestroyChildren("MainContainer/Counter");
        mentalSelector.DestroyChildren("MainContainer/Counter");

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

          // Add Prowess and Legendary Ability bonuses to the displayed value
          var prowessBonus = Common.GetProwessBonus(__instance.Stat.Value);
          var legendaryBonus = Common.GetLegendaryBonus(__instance.Stat.Value);
          __instance.StatValue.Value += prowessBonus + legendaryBonus;
          __instance.Bonus.Value = (__instance.StatValue.Value - 10) / 2;
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenAbilityScoreAllocatorVM_Patch.UpdateStatDistribution", e);
        }
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

    private StringSequentialSelectorVM GetSelectorVM(
      List<StatType> stats,
      Action<StatType> onSelect)
    {
      StringSequentialEntity selected = null;
      List<StringSequentialEntity> selections = new();
      foreach (var type in stats)
      {
        var stat = LevelUpController.Preview.Stats.GetStat(type);
        if (LevelUpController.LevelUpActions.OfType<SelectProwess>().Any(a => a.Attribute == type && !a.IsGift))
        {
          selected = GetSelection(stat, onSelect);
          selections.Add(selected);
        }
        else if (IsEligible(stat))
        {
          selections.Add(GetSelection(stat, onSelect));
        }
      }
      // This is a weird workaround because the selector doesn't work if there's only one value
      if (selections.Count == 1)
        selections.Add(selections.First());
      return new(selections.ToList(), current: selected);
    }

    private StringSequentialEntity GetSelection(ModifiableValue stat, Action<StatType> onSelect)
    {
      return
        new StringSequentialEntity()
        {
          Title = LocalizedTexts.Instance.Stats.GetText(stat.Type),
          Setter = new(() => onSelect(stat.Type)),
          Tooltip = stat
        };
    }

    private bool IsEligible(ModifiableValue stat)
    {
      int bonusLimit = 6;
      int characterLevel = LevelUpController.State.NextCharacterLevel;
      if (characterLevel < 11)
        bonusLimit = 2;
      else if (characterLevel < 15)
        bonusLimit = 4;
      Logger.Warning($"Is Prowess Eligible {stat.Type}: {Common.GetProwessBonus(stat)} < {bonusLimit}");
      return Common.GetProwessBonus(stat) < bonusLimit;
    }

    private void SelectPhysicalProwess(StatType stat)
    {
      LevelUpController.RemoveAction<SelectPhysicalProwess>(a => !a.IsGift);
      LevelUpController.AddAction(new SelectPhysicalProwess(stat));
    }

    private void SelectMentalProwess(StatType stat)
    {
      LevelUpController.RemoveAction<SelectMentalProwess>(a => !a.IsGift);
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
