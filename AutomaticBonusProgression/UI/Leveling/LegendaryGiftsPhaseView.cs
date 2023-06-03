using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.Skills;
using Kingmaker.UI.MVVM._VM.CharGen;
using Kingmaker.UI.MVVM._VM.CharGen.Phases;
using Kingmaker.UI.MVVM._VM.InfoWindow;
using Kingmaker.UI.MVVM._VM.Other;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.Utility;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling
{
  internal class LegendaryGiftsPhaseView : CharGenPhaseDetailedBaseView<LegendaryGiftsPhaseVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryGiftsPhaseView));

    private CharGenAbilityScoresDetailedPCView AbilityScoresView;

    internal void Init(CharGenAbilityScoresDetailedPCView source)
    {
      AbilityScoresView = source;

      // Update labels
      AbilityScoresView.m_RaceBonus.SetText(UITool.GetString("Legendary.Prowess"));
      AbilityScoresView.m_PhaseLabel.SetText(
        UIUtility.GetSaberBookFormat(UITool.GetString("Legendary.Gifts.Selection")));
    }

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);

      AddDisposable(ViewModel.AvailablePoints.Subscribe(SetAvailablePoints));

      // There always should be 6 of each. We need to manually bind because we're being lazy and not rewriting the view
      // and VM classes.
      for (int i = 0; i < AbilityScoresView.m_StatAllocators.Count; i++)
      {
        var allocator = AbilityScoresView.m_StatAllocators[i];
        var vm = ViewModel.AbilityScoreVMs[i];

        allocator.m_LongName.SetText(vm.Name);
        allocator.m_ShortName.SetText(vm.ShortName);

        allocator.AddDisposable(vm.Value.Subscribe(_ => UpdateAllocator(allocator, vm)));
        allocator.AddDisposable(vm.CanAdd.Subscribe(allocator.UpButton.SetInteractable));
        allocator.AddDisposable(vm.CanRemove.Subscribe(allocator.DownButton.SetInteractable));
        allocator.AddDisposable(vm.Recommendation.Subscribe(allocator.m_RecommendationMark.Bind));
        allocator.AddDisposable(allocator.UpButton.OnLeftClickAsObservable().Subscribe(_ => vm.TryIncreaseValue()));
        allocator.AddDisposable(allocator.DownButton.OnLeftClickAsObservable().Subscribe(_ => vm.TryDecreaseValue()));
      }
    }

    public override void DestroyViewImplementation()
    {
      gameObject.SetActive(false);
    }

    private void SetAvailablePoints(int points)
    {
      AbilityScoresView.m_AvailiblePoints.SetText(points.ToString());
    }

    private void UpdateAllocator(CharGenAbilityScoreAllocatorPCView view, LegendaryAbilityScoreAllocatorVM vm)
    {
      view.m_Value.SetText(vm.Value.Value.ToString());
      view.m_Modifier.SetText(UIUtility.AddSign(vm.Modifier.Value));
      vm.TryShowTooltip();
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
      obj.transform.AddTo(source.transform.parent);

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

  // Section shown on the top bar of leveling screen
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

    internal readonly IntReactiveProperty AvailablePoints = new();
    internal readonly List<LegendaryAbilityScoreAllocatorVM> AbilityScoreVMs = new();

    internal LegendaryGiftsPhaseVM(LevelUpController levelUpController, int points) : base(levelUpController)
    {
      AvailablePoints.Value = points;
      SetPhaseName(UITool.GetString("Legendary.Gifts"));

      foreach (var stat in Attributes)
        AbilityScoreVMs.Add(new(stat, AvailablePoints, new(), LevelUpController));
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
      return AvailablePoints.Value == 0;
    }

    public override void OnBeginDetailedView()
    {
      //throw new NotImplementedException();
    }

    public override void DisposeImplementation()
    {
      foreach (var vm in  AbilityScoreVMs)
        vm.DisposeImplementation();
    }
  }

  // Need to use our own VM for the allocators otherwise it conflicts w/ the patches in ProwessPhaseView
  internal class LegendaryAbilityScoreAllocatorVM : BaseDisposable, IViewModel, IHasTooltipTemplate
  {
    private readonly IntReactiveProperty AvailableGifts;
    private readonly InfoSectionVM InfoVM;
    private readonly LevelUpController LevelUpController;
    private readonly ReactiveProperty<ModifiableValue> Stat;
    private StatType Type => Stat.Value.Type;

    private int SpentGifts = 0;

    public LegendaryAbilityScoreAllocatorVM(
      StatType type,
      IntReactiveProperty availableGifts,
      InfoSectionVM infoVM,
      LevelUpController levelUpController)
    {
      AvailableGifts = availableGifts;
      InfoVM = infoVM;
      LevelUpController = levelUpController;
      Stat.ToSequentialReadOnlyReactiveProperty(LevelUpController.Unit.Stats.GetStat(type));

      AddDisposable(Stat.Subscribe(_ => UpdateStats()));
      AddDisposable(AvailableGifts.Subscribe(_ => UpdateStats()));

      Name = LocalizedTexts.Instance.Stats.GetText(Type);
      ShortName = UIUtilityTexts.GetStatShortName(Type);
    }

    public override void DisposeImplementation() { }

    internal void SetRecommendationsForClass(ClassData classData)
    {
      var recommend = classData is not null && classData.RecommendedAttributes.Contains(Stat.Value.Type);
      if (Recommendation.Value is null)
        AddDisposable(Recommendation.Value = new(recommend));
      else
        Recommendation.Value.ChangeRecommendation(recommend);
    }

    internal void TryShowTooltip()
    {
      InfoVM.SetTemplate(TooltipTemplate());
    }

    // At least for now, copied from CharGenAbilityScoreAllocatorVM
    public TooltipBaseTemplate TooltipTemplate()
    {
      BlueprintArchetype archetype = null;
      if (LevelUpController.State.SelectedClass != null)
      {
        var classData = LevelUpController.Preview.Progression.GetClassData(LevelUpController.State.SelectedClass);
        archetype = classData?.Archetypes.FirstOrDefault();
      }
      Kingmaker.UI.MVVM._VM.Tooltip.Templates.ClassInformation classInformation = new();
      classInformation.Class = LevelUpController.State.SelectedClass;
      classInformation.Unit = LevelUpController.Preview.Descriptor;
      classInformation.Archetype = archetype;
      StatTooltipData statData = new StatTooltipData(Stat.Value);
      return new TooltipTemplateAbilityScoreAllocator(classInformation, statData);
    }

    internal void TryIncreaseValue()
    {
      if (!CanAdd.Value)
        return;

      SpentGifts++;
      LevelUpController.AddAction(new SelectLegendaryAbility(Type));
    }

    internal void TryDecreaseValue()
    {
      if (!CanRemove.Value)
        return;

      SpentGifts--;
      LevelUpController.RemoveAction<SelectLegendaryAbility>(a => a.Attribute == Type);
    }

    private void UpdateStats()
    {
      CanAdd.Value = AvailableGifts.Value > 0;
      CanRemove.Value = SpentGifts > 0;

      Value.Value =
        Stat.Value.PermanentValue + ProwessPhaseVM.GetProwessBonus(Stat.Value) + GetLegendaryBonus(Stat.Value);
      Modifier.Value = (Value.Value - 10) / 2;
    }

    internal static int GetLegendaryBonus(ModifiableValue stat)
    {
      var inherent = stat.GetModifiers(ModifierDescriptor.Inherent);
      if (inherent is null)
        return 0;

      switch (stat.Type)
      {
        case StatType.Strength:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryStr).Sum(mod => mod.ModValue);
        case StatType.Dexterity:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryDex).Sum(mod => mod.ModValue);
        case StatType.Constitution:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryCon).Sum(mod => mod.ModValue);
        case StatType.Intelligence:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryInt).Sum(mod => mod.ModValue);
        case StatType.Wisdom:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryWis).Sum(mod => mod.ModValue);
        case StatType.Charisma:
          return inherent.Where(mod => mod.Source.Blueprint == Common.LegendaryCha).Sum(mod => mod.ModValue);
      }
      return 0;
    }

    internal readonly string Name;
    internal readonly string ShortName;
    internal readonly IntReactiveProperty Value = new();
    internal readonly IntReactiveProperty Modifier = new();
    internal readonly BoolReactiveProperty CanAdd = new();
    internal readonly BoolReactiveProperty CanRemove = new();
    internal readonly ReactiveProperty<RecommendationMarkerVM> Recommendation = new();
  }
}
