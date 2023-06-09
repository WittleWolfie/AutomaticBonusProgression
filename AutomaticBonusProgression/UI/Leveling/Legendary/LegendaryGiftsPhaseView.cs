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
using Kingmaker.UI.MVVM._PCView.InfoWindow;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Owlcat.Runtime.UI.Tooltips;
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

    private InfoSectionView InfoView;
    private Transform InfoViewTransform;
    private TextMeshProUGUI AvailablePoints;
    private readonly List<LegendaryAbilityAllocatorView> AbilityAllocators = new();
    private readonly List<LegendaryEnchantmentAllocatorView> EnchantmentAllocators = new();

    internal void InitAbilityScores(CharGenAbilityScoresDetailedPCView source)
    {
      // Init the base view w/o Initialize() so it doesn't interfere w/ Prowess patch
      var charGen = UIStrings.Instance.CharGen;
      source.m_StatAllocators =
        source.m_StatAllocatorsContainer
          .GetComponentsInChildren<CharGenAbilityScoreAllocatorPCView>()
          .ToList();
      source.m_AvailiblePointsLabel.SetText(UITool.GetString("Legendary.Gifts.Points"));
      source.m_StatLabel.SetText(charGen.StatName);
      source.m_ScoresLabel.SetText(charGen.Scores);
      source.m_ModifierLabel.SetText(charGen.Modifier);

      // Update the ability selectors and remove additional selections
      var obj = source.gameObject;
      obj.DestroyChildren("ArtDollRoom");
      obj.DestroyChildren("AllocatorPlace/Selector/RaceBonusSelector");
      obj.DestroyChildren("AllocatorPlace/Selector/RaceBonusSelector(Clone)");
      foreach (var allocator in source.m_StatAllocators)
      {
        allocator.gameObject.DestroyChildren("Bonus/RaceBonus");
        allocator.gameObject.ChildObject("Score/Selected/CostArrowDown/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("-1");
        allocator.gameObject.ChildObject("Score/Selected/CostArrowUp/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("+1");

        var view = allocator.gameObject.AddComponent<LegendaryAbilityAllocatorView>();
        view.Init(allocator);
        AbilityAllocators.Add(view);

        // Remove the base component so no logic runs
        Destroy(allocator);
      }

      // Update labels
      source.m_RaceBonus.SetText(UITool.GetString("Legendary.Prowess"));
      source.m_PhaseLabel.SetText(
        UIUtility.GetSaberBookFormat(UITool.GetString("Legendary.Gifts.Selection")));

      AvailablePoints = source.m_AvailiblePoints;
      InfoView = source.InfoView;
      m_PageAnimator = source.m_PageAnimator;

      // This determines the position of tooltips
      InfoViewTransform = source.TargetSizeTransform;
      // For some reason the y size doesn't apply directly... trying to go from ~893 -> 840
      InfoViewTransform.Rect().sizeDelta = new(x: 600, y: -95);
      InfoViewTransform.localPosition = new(x: 512, y: 15);

      // Remove the component so no logic runs
      Destroy(source);
    }

    internal void InitEnchantments(CharGenAbilityScoresDetailedPCView source)
    {
      // Init the base view w/o Initialize() so it doesn't interfere w/ Prowess patch
      source.m_StatAllocators =
        source.m_StatAllocatorsContainer
          .GetComponentsInChildren<CharGenAbilityScoreAllocatorPCView>()
          .ToList();
      source.m_StatLabel.SetText(UITool.GetString("Legendary.EnchantmentType"));
      source.m_ScoresLabel.SetText(UITool.GetString("Legendary.MaxEnhancement"));

      // Update the ability selectors and remove additional selections
      for (int i = 0; i < 4; i++) // Only use the first 4
      {
        var allocator = source.m_StatAllocators[i];

        allocator.gameObject.DestroyChildren("Bonus");
        allocator.gameObject.DestroyChildren("Modifier");
        allocator.gameObject.DestroyChildren("Labels/RecommendationPlace");

        allocator.gameObject.ChildObject("Score/Selected/CostArrowDown/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("-1");
        allocator.gameObject.ChildObject("Score/Selected/CostArrowUp/Cost")
          .GetComponent<TextMeshProUGUI>()
          .SetText("+1");

        var hover = allocator.gameObject.ChildObject("Hover").Rect();
        hover.anchorMin = new(x: 0.15f, y: 0);
        hover.anchorMax = new(x: 0.87f, y: 1);

        var view = allocator.gameObject.AddComponent<LegendaryEnchantmentAllocatorView>();
        view.Init(allocator);
        EnchantmentAllocators.Add(view);

        // Remove the base component so no logic runs
        Destroy(allocator);
      }

      // Remove the base component so no logic runs
      Destroy(source);
    }

    // TODO: Add tooltips for enchantments
    // TODO: Add Twin Weapons & Shieldmaster
    public override void BindViewImplementation()
    {
      base.BindViewImplementation();

      gameObject.SetActive(true);

      InfoView.SetTransform(InfoViewTransform);
      InfoView.Bind(ViewModel.InfoVM);
      AddDisposable(ViewModel.State.AvailableGifts.Subscribe(SetAvailableGifts));

      for (int i = 0; i < AbilityAllocators.Count; i++)
      {
        var allocator = AbilityAllocators[i];
        var vm = ViewModel.AbilityScoreVMs[i];
        allocator.Bind(vm);
      }

      for (int i = 0; i < EnchantmentAllocators.Count; i++)
      {
        var allocator = EnchantmentAllocators[i];
        var vm = ViewModel.EnchantmentVMs[i];
        allocator.Bind(vm);
      }
    }

    public override void DestroyViewImplementation()
    {
      base.DestroyViewImplementation();

      InfoView.Unbind();
    }

    private void SetAvailableGifts(int gifts)
    {
      AvailablePoints.SetText(gifts.ToString());
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

      var view = obj.AddComponent<LegendaryGiftsPhaseView>();
      view.InitAbilityScores(abilityScoresView);

      // Copy again for enchantments
      var enchantmentsView = Instantiate(source);
      obj = enchantmentsView.gameObject.ChildObject("AllocatorPlace/Selector/AbilityScoresAllocator");
      obj.DestroyChildren("Content/Labels/RaceBonusBox");
      obj.DestroyChildren("Content/Labels/TotalBox");
      obj.DestroyChildren("Content/Console_StatAllocator (4)");
      obj.DestroyChildren("Content/Console_StatAllocator (5)");

      obj.transform.AddTo(view.transform);
      obj.transform.localPosition = new(x: -30, y: 135);
      view.InitEnchantments(enchantmentsView);
      return view;
    }
    #endregion
  }

  /// <summary>
  /// Roadmap view is shown at the top bar and is used to indicate the legendary gifts phase applies.
  /// </summary>
  internal class LegendaryGiftsRoadmapView : CharGenPhaseRoadmapBaseView<LegendaryGiftsPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsRoadmapView));

    private TextMeshProUGUI PointsLabel;

    internal void Init(CharGenSkillsPhaseRoadmapPCView skillsView)
    {
      Initialize(); // Make sure the parent classes are set up
      PointsLabel = skillsView.m_Points;

      // Copy values that should exist from the existing component
      m_ButtonBackground = skillsView.m_ButtonBackground;
      m_Label = skillsView.m_Label;
      m_LabelLayoutElement = skillsView.m_LabelLayoutElement;
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
      UpdateSelectableState();
    }
  }

  internal class LegendaryGiftsPhaseVM : CharGenPhaseBaseVM
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseVM));

    public override int OrderPriority => GetBaseOrderPriority(ChargenPhasePriority.AbilityScores);

    internal readonly LegendaryGiftState State;
    internal readonly List<LegendaryAbilityScoreAllocatorVM> AbilityScoreVMs = new();
    internal readonly List<LegendaryEnchantmentScoreAllocatorVM> EnchantmentVMs = new();

    internal LegendaryGiftsPhaseVM(LevelUpController levelUpController, int gifts) : base(levelUpController)
    {
      State = new(levelUpController, gifts);
      SetPhaseName(UITool.GetString("Legendary.Gifts"));

      AddDisposable(InfoVM = new());
      AddDisposable(State.Controller.UpdateCommand.Subscribe(_ => UpdateStats()));

      foreach (var stat in Attributes)
        AbilityScoreVMs.Add(new(stat, State, InfoVM));

      foreach (EnchantmentType type in Enum.GetValues(typeof(EnchantmentType)))
        EnchantmentVMs.Add(new(type, State, InfoVM));
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

    // Shows a tooltip telling the user to spend their gifts to move to the next phase
    public override TooltipBaseTemplate NotCompletedReasonTooltip
    {
      get
      {
        if (!CheckIsCompleted())
          return new TooltipTemplateSimple(UITool.GetString("Legendary.Gifts.Incomplete"));
        return null;
      }
    }
  }
}
