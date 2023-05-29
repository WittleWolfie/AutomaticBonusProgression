using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Common;
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
    }

    public override void BindViewImplementation()
    {
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

    public override void DestroyViewImplementation() { }

    #region Setup
    private static ProwessPhaseView BaseView;

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
          var nextLevel = __instance.ViewModel.LevelUpController.State.NextCharacterLevel;
          var isPhysicalProwessAvailable = SelectProwess.PhysicalProwessLevels.Contains(nextLevel);
          var isMentalProwessAvailable = SelectProwess.MentalProwessLevels.Contains(nextLevel);
          if (!isPhysicalProwessAvailable && !isMentalProwessAvailable)
          {
            Logger.Verbose(() => $"Prowess phase does not apply at level {nextLevel}");
            return;
          }

          Logger.Log($"Binding ProwessPhaseVM");
          BaseView.Bind(
            new(__instance.ViewModel.LevelUpController, isPhysicalProwessAvailable, isMentalProwessAvailable));

          // TODO:
          // - Hook up data / VM to the selector
          // - Handle binding, make sure it's all being disposed
          // - Tweak view to get the right size & position and all that
          // - Patch CharGenVM.NeedAbilityScoresPhase() when Prowess is needed
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
        label.SetText(UITool.GetString("Leveling.Prowess"));

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
    #endregion
  }

  internal class ProwessPhaseVM : BaseDisposable, IViewModel
  {
    private readonly LevelUpController LevelUpController;

    internal readonly StringSequentialSelectorVM PhysicalProwessVM;
    internal readonly StringSequentialSelectorVM MentalProwessVM;
    internal readonly bool IsPhysicalProwessAvailable;
    internal readonly bool IsMentalProwessAvailable;

    internal ProwessPhaseVM(
      LevelUpController levelUpController,
      bool isPhysicalProwessAvailable,
      bool isMentalProwessAvailable)
    {
      LevelUpController = levelUpController;

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
