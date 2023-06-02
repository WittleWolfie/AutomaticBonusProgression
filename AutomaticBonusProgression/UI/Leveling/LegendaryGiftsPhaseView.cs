using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Skills;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases;
using Kingmaker.UnitLogic.Class.LevelUp;
using Owlcat.Runtime.UI.MVVM;
using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling
{
  internal class LegendaryGiftsPhaseView : ViewBase<LegendaryGiftsPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseView));

    public override void BindViewImplementation() { }

    public override void DestroyViewImplementation() { }

    #region Setup
    private static LegendaryGiftsPhaseView PhaseView;
    private static LegendaryGiftsRoadmapView RoadmapView;

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

    private static LegendaryGiftsRoadmapView CreateRoadmap(CharGenRoadmapMenuView __instance)
    {
      // Copy the skills phase view since it's basically the same UI.
      var skillsView = GameObject.Instantiate(__instance.SkillsPhaseRoadmapPcView);
      var obj = skillsView.gameObject;

      obj.DestroyChildren("Console_RoadMapItemBackground/Content/Stats");

      var view = obj.AddComponent<LegendaryGiftsRoadmapView>();
      view.Init(skillsView);
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
      m_PhaseName.Value = UITool.GetString("Leveling.Gifts");
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
