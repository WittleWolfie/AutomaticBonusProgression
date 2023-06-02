using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Skills;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases;
using Kingmaker.UnitLogic.Class.LevelUp;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling
{
  internal class LegendaryGiftsPhaseView : CharGenPhaseDetailedBaseView<LegendaryGiftsPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseView));

    internal void Init(CharGenAbilityScoresDetailedPCView source)
    {
      // Re-use this column to select Legendary Prowess
      source.m_RaceBonusLabel.SetText(UITool.GetString("Legendary.Prowess"));
    }

    public override void BindViewImplementation() { }

    public override void DestroyViewImplementation() { }

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
          LegendaryGiftsPhaseVM vm = null;
          if (__instance.IsMythic)
            vm = new(__instance.m_LevelUpController, 2);
          else if (__instance.m_LevelUpController.State.NextCharacterLevel == 19)
            vm = new(__instance.m_LevelUpController, 3);
          else if (__instance.m_LevelUpController.State.NextCharacterLevel == 20)
            vm = new(__instance.m_LevelUpController, 5);
          else
            vm = new(__instance.m_LevelUpController, 1); // TODO: Delete once testing is done

          if (vm is null)
          {
            Logger.Log($"Skipping Legendary Gifts Phase: {__instance.IsMythic} - {__instance.m_LevelUpController.State.NextCharacterLevel}");
            return;
          }

          Logger.Log($"Adding legendary gifts phase");
          __instance.AddPhase(__instance.m_PhasesList, vm);
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
      var skillsView = GameObject.Instantiate(source.SkillsPhaseRoadmapPcView);
      var obj = skillsView.gameObject;

      obj.DestroyChildren("Console_RoadMapItemBackground/Content/Stats");

      var view = obj.AddComponent<LegendaryGiftsRoadmapView>();
      view.Init(skillsView);
      return view;
    }

    private static LegendaryGiftsPhaseView CreatePhase(CharGenAbilityScoresDetailedPCView source)
    {
      // Copy the ability scores view to start since it has the ability points section.
      var abilityScoresView = GameObject.Instantiate(source);
      abilityScoresView.Initialize();
      var obj = abilityScoresView.gameObject;

      // Don't need the racial bonus UI
      obj.DestroyChildren("AllocatorPlace/Selector/RaceBonusSelector");
      foreach (var allocator in abilityScoresView.m_StatAllocators)
        allocator.gameObject.DestroyChildren("Bonus/RaceBonus");

      // TODO: Add check mark selectors for Legendary Prowess (note that you can select up to two times)

      // TODO: Add second section for Armor / Shield / Weapon / Off-Hand
      // - Need to figure out how to make it all compact enough. Notably, skill rows are smaller so we can probably
      //   just use those numbers.

      // TODO: Add Tooltips (see CharGenAbilityScoreAllocatorPCView.OnPointerEnter)

      // TODO: Maybe we hide the progression view entirely and try shifting everything to the right?

      var view = obj.AddComponent<LegendaryGiftsPhaseView>();
      view.Init(abilityScoresView);
      return view;
    }
    #endregion
  }

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

      AddDisposable(ViewModel.AvailablePoints.Subscribe(UpdatePoints));
    }

    private void UpdatePoints(int points)
    {
      PointsLabel.SetText(points.ToString());
    }
  }

  internal class LegendaryGiftsPhaseVM : CharGenPhaseBaseVM
  {
    public override int OrderPriority => GetBaseOrderPriority(ChargenPhasePriority.AbilityScores);

    internal ReactiveProperty<int> AvailablePoints = new();

    internal LegendaryGiftsPhaseVM(LevelUpController levelUpController, int points) : base(levelUpController)
    {
      AvailablePoints.Value = points;
      m_PhaseName.Value = UITool.GetString("Legendary.Gifts");
    }

    public override bool CheckIsCompleted()
    {
      return AvailablePoints.Value == 0;
    }

    public override void OnBeginDetailedView()
    {
      //throw new NotImplementedException();
    }
  }
}
