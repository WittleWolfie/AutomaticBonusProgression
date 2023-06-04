using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Skills;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// The book view class for legendary gifts phase. Currently is just a modified copy of CharGenAbilityScoresDetailedPCView.
  /// 
  /// Eventually should have stuff for all Legendary Gifts & Tooltip support.
  /// </summary>
  internal class LegendaryGiftsPhaseView : CharGenPhaseDetailedBaseView<LegendaryGiftsPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseView));

    private CharGenAbilityScoresDetailedPCView AbilityScoresView;
    private readonly List<LegendaryAbilityAllocatorView> Allocators = new();

    internal void Init(CharGenAbilityScoresDetailedPCView source)
    {
      AbilityScoresView = source;

      // Update labels
      AbilityScoresView.m_RaceBonus.SetText(UITool.GetString("Legendary.Prowess"));
      AbilityScoresView.m_PhaseLabel.SetText(
        UIUtility.GetSaberBookFormat(UITool.GetString("Legendary.Gifts.Selection")));

      foreach (var allocator in AbilityScoresView.m_StatAllocators)
      {
        var view = allocator.gameObject.AddComponent<LegendaryAbilityAllocatorView>();
        view.Init(allocator);
        Allocators.Add(view);
      }
    }

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);

      AddDisposable(ViewModel.State.AvailableGifts.Subscribe(SetAvailableGifts));

      for (int i = 0; i < Allocators.Count; i++)
      {
        var allocator = Allocators[i];
        var vm = ViewModel.AbilityScoreVMs[i];
        allocator.Bind(vm);
        AddDisposable(vm);
      }
    }

    private void SetAvailableGifts(int gifts)
    {
      AbilityScoresView.m_AvailiblePoints.SetText(gifts.ToString());
    }

    #region Setup
    private static LegendaryGiftsPhaseView PhaseView;
    private static LegendaryGiftsRoadmapView RoadmapView;

    // Handle RoadmapView
    [HarmonyPatch(typeof(CharGenRoadmapMenuView))]
    static class CharGenRoadmapMenuView_Patch
    {
      // Instantiates the roadmap view
      [HarmonyPatch(nameof(CharGenRoadmapMenuView.Initialize)), HarmonyPrefix]
      static void Initialize(CharGenRoadmapMenuView __instance)
      {
        try
        {
          Logger.Log($"Initializing LegendaryGiftsRoadmapView");
          RoadmapView = CreateRoadmap(__instance);
          __instance.RetrieveRoadmapView(RoadmapView);
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenRoadmapMenuView_Patch.Initialize", e);
        }
      }

      // Patch that shows the roadmap button when the phase is present
      [HarmonyPatch(nameof(CharGenRoadmapMenuView.GetRoadmapPhaseView)), HarmonyPrefix]
      static bool GetRoadmapPhaseView(
        CharGenRoadmapMenuView __instance,
        CharGenPhaseBaseVM phaseVM,
        ref ICharGenPhaseRoadmapView __result)
      {
        try
        {
          if (phaseVM is LegendaryGiftsPhaseVM vm)
          {
            Logger.Log($"Returning roadmap");
            RoadmapView.Bind(vm);
            vm.OnDispose += new(() => __instance.RetrieveRoadmapView(RoadmapView));
            __result = RoadmapView;
            return false;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenRoadmapMenuView_Patch.GetRoadmapPhaseView", e);
        }
        return true;
      }
    }

    // Handle PhaseView
    [HarmonyPatch(typeof(CharGenPhaseDetailedPCViewsFactory))]
    static class CharGenPhaseDetailedPCViewsFactory_Patch
    {
      [HarmonyPatch(nameof(CharGenPhaseDetailedPCViewsFactory.Initialize)), HarmonyPostfix]
      static void Initialize(CharGenPhaseDetailedPCViewsFactory __instance)
      {
        try
        {
          Logger.Log($"Initializing LegendaryGiftsPhaseView");
          PhaseView = CreatePhase(__instance.AbilityScoresPhaseDetailedPcView);
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenPhaseDetailedPCViewsFactory_Patch.Initialize", e);
        }
      }

      [HarmonyPatch(nameof(CharGenPhaseDetailedPCViewsFactory.GetDetailedPhaseView)), HarmonyPrefix]
      static bool GetDetailedPhaseView(
        CharGenPhaseDetailedPCViewsFactory __instance,
        CharGenPhaseBaseVM phaseVM,
        ref ICharGenPhaseDetailedView __result)
      {
        try
        {
          if (phaseVM is LegendaryGiftsPhaseVM vm)
          {
            Logger.Log($"Returning PhaseView");
            PhaseView.Bind(vm);
            __result = PhaseView;
            return false;
          }
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenPhaseDetailedPCViewsFactory_Patch.Initialize", e);
        }
        return true;
      }
    }

    // Patch which adds the Legendary Gifts phase
    [HarmonyPatch(typeof(CharGenVM))]
    static class CharGenVM_Patch
    {
      [HarmonyPatch(nameof(CharGenVM.UpdateAllPhases)), HarmonyPrefix]
      static void UpdateAllPhases(CharGenVM __instance)
      {
        try
        {
          int availableGifts = 0;
          if (__instance.IsMythic)
            availableGifts = 2;
          else if (__instance.m_LevelUpController.State.NextCharacterLevel == 19)
            availableGifts = 3;
          else if (__instance.m_LevelUpController.State.NextCharacterLevel == 20)
            availableGifts = 5;
          else
            availableGifts = 2; // TODO: Delete once testing is done

          if (__instance.TryClearPhaseFromList<LegendaryGiftsPhaseVM>(availableGifts > 0, __instance.m_PhasesList))
          {
            Logger.Log($"Adding legendary gifts phase");
            __instance.AddPhase(
              __instance.m_PhasesList,
              new LegendaryGiftsPhaseVM(__instance.m_LevelUpController, availableGifts));
          }
        }
        catch (Exception e)
        {
          Logger.LogException("CharGenVM_Patch.UpdateAllPhases", e);
        }
      }
    }

    private static LegendaryGiftsRoadmapView CreateRoadmap(CharGenRoadmapMenuView source)
    {
      // Copy the skills phase view since it's basically the same UI.
      var skillsView = Instantiate(source.SkillsPhaseRoadmapPcView);
      var obj = skillsView.gameObject;

      obj.DestroyChildren("Console_RoadMapItemBackground/Content/Stats");

      var view = obj.AddComponent<LegendaryGiftsRoadmapView>();
      view.Init(skillsView);
      return view;
    }

    private static LegendaryGiftsPhaseView CreatePhase(CharGenAbilityScoresDetailedPCView source)
    {
      // Copy the ability scores view to start since it has the ability gifts section.
      var abilityScoresView = Instantiate(source);
      var obj = abilityScoresView.gameObject;
      obj.transform.AddTo(source.transform.parent);

      // Init the base view w/o Initialize() so it doesn't interfere w/ Prowess patch
      var charGen = UIStrings.Instance.CharGen;
      abilityScoresView.m_StatAllocators =
        abilityScoresView.m_StatAllocatorsContainer
          .GetComponentsInChildren<CharGenAbilityScoreAllocatorPCView>()
          .ToList();
      abilityScoresView.m_AvailiblePointsLabel.SetText(charGen.Points);
      abilityScoresView.m_StatLabel.SetText(charGen.StatName);
      abilityScoresView.m_ScoresLabel.SetText(charGen.Scores);
      abilityScoresView.m_ModifierLabel.SetText(charGen.Modifier);

      // Update the ability selectors and remove additional selections
      obj.DestroyChildren("AllocatorPlace/Selector/RaceBonusSelector");
      obj.DestroyChildren("AllocatorPlace/Selector/RaceBonusSelector(Clone)");
      foreach (var allocator in abilityScoresView.m_StatAllocators)
      {
        allocator.gameObject.DestroyChildren("Bonus/RaceBonus");
        allocator.gameObject.ChildObject("Score/Selected/CostArrowDown/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("-1");
        allocator.gameObject.ChildObject("Score/Selected/CostArrowUp/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("+1");
      }

      // TODO: Add check mark selectors for Legendary Prowess (note that you can select up to two times)

      // TODO: Add second section for Armor / Shield / Weapon / Off-Hand
      // - Need to figure out how to make it all compact enough. Notably, skill rows are smaller so we can probably
      //   just use those numbers.

      // TODO: Add Tooltips (see CharGenAbilityScoreAllocatorPCView.OnPointerEnter)

      // TODO: Maybe we hide the progression view entirely and try shifting everything to the right?

      // TODO: If we do the shift everything right plan, why not re-use the Racial Selector again for Prowess?
      // - What about when you can get more than 2 at a time tho?

      var view = obj.AddComponent<LegendaryGiftsPhaseView>();
      view.Init(abilityScoresView);
      return view;
    }
    #endregion
  }

  /// <summary>
  /// Roadmap view is shown at the top bar and is used to indicate the legendary gifts phase applies.
  /// </summary>
  internal class LegendaryGiftsRoadmapView : CharGenPhaseRoadmapBaseView<LegendaryGiftsPhaseVM>
  {
    private TextMeshProUGUI PointsLabel;

    internal void Init(CharGenSkillsPhaseRoadmapPCView skillsView)
    {
      Initialize(); // Make sure the parent classes are set up
      PointsLabel = skillsView.m_Points;
      // Copy values that should exist from the existing component
      m_ButtonBackground = skillsView.m_Button;
      m_Label = skillsView.m_Label;
      m_ButtonLabel = skillsView.m_ButtonLabel;
      m_Button = skillsView.m_Button;
    }

    public override void BindViewImplementation()
    {
      base.BindViewImplementation();

      AddDisposable(ViewModel.State.AvailableGifts.Subscribe(UpdateGifts));
    }

    private void UpdateGifts(int gifts)
    {
      PointsLabel.SetText(gifts.ToString());
    }
  }

  // TODO: Provide string for spending the gifts
  internal class LegendaryGiftsPhaseVM : CharGenPhaseBaseVM
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseVM));

    public override int OrderPriority => GetBaseOrderPriority(ChargenPhasePriority.AbilityScores);

    internal readonly LegendaryGiftState State;
    internal readonly List<LegendaryAbilityScoreAllocatorVM> AbilityScoreVMs = new();

    internal LegendaryGiftsPhaseVM(LevelUpController levelUpController, int gifts) : base(levelUpController)
    {
      State = new(levelUpController, gifts);
      SetPhaseName(UITool.GetString("Legendary.Gifts"));

      foreach (var stat in Attributes)
      {
        var vm = new LegendaryAbilityScoreAllocatorVM(stat, State, new());
        AbilityScoreVMs.Add(vm);
        AddDisposable(vm);
      }

      AddDisposable(State.Controller.UpdateCommand.Subscribe(_ => UpdateStats()));
    }

    private void UpdateStats()
    {
      foreach (var vm in AbilityScoreVMs)
        vm.UpdateStat();
    }

    private static readonly List<StatType> Attributes =
      new()
      {
        StatType.Strength,
        StatType.Dexterity,
        StatType.Constitution,
        StatType.Intelligence,
        StatType.Wisdom,
        StatType.Charisma
      };

    public override bool CheckIsCompleted()
    {
      return State.AvailableGifts.Value == 0;
    }

    public override void OnBeginDetailedView()
    {
      BlueprintCharacterClass clazz = null;
      foreach (var action in LevelUpController.LevelUpActions)
      {
        if (action is SelectClass select)
          clazz = select.CharacterClass;
      }
      if (clazz is null)
        return;

      var classData = LevelUpController.Preview.Progression.GetClassData(clazz);
      foreach (var vm in AbilityScoreVMs)
        vm.SetRecommendationsForClass(classData);
    }
  }
}
