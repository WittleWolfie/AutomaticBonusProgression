using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._VM.InfoWindow;
using Kingmaker.UI.MVVM._VM.Other;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using System.Linq;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Wrapper view around CharGenAbilityScoreAllocatorPCView
  /// </summary>
  internal class LegendaryAbilityAllocatorView : ViewBase<LegendaryAbilityScoreAllocatorVM>, IHasTooltipTemplate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryAbilityAllocatorView));

    private CharGenAbilityScoreAllocatorPCView Allocator;
    private ToggleWorkaround FirstProwessToggle;
    private ToggleWorkaround SecondProwessToggle;

    internal void Init(CharGenAbilityScoreAllocatorPCView source)
    {
      Allocator = source;

      FirstProwessToggle = CreateToggle();
      SecondProwessToggle = CreateToggle(first: false);
    }

    public override void BindViewImplementation()
    {
      Allocator.m_LongName.SetText(ViewModel.Name);
      Allocator.m_ShortName.SetText(ViewModel.ShortName);
      

      AddDisposable(
        ViewModel.StatValue.Subscribe(value => Allocator.m_Value.SetText(value.ToString())));
      AddDisposable(
        ViewModel.Modifier.Subscribe(value => Allocator.m_Modifier.SetText(UIUtility.AddSign(value))));

      AddDisposable(ViewModel.CanAddAbility.Subscribe(UpdateCanAdd));
      AddDisposable(ViewModel.CanRemoveAbility.Subscribe(UpdateCanRemove));
      AddDisposable(ViewModel.CanSelectProwess.Subscribe(UpdateAvailableProwess));

      AddDisposable(ViewModel.Recommendation.Subscribe(Allocator.m_RecommendationMark.Bind));

      AddDisposable(
        Allocator.UpButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryIncreaseAbility()));
      AddDisposable(
        Allocator.DownButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.TryDecreaseAbility()));

      FirstProwessToggle.onValueChanged.AddListener(new(ViewModel.ToggleProwess));
      SecondProwessToggle.onValueChanged.AddListener(new(ViewModel.ToggleProwess));
    }

    public override void DestroyViewImplementation() { }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return ViewModel.TooltipTemplate();
    }

    private void UpdateCanAdd(bool canAdd)
    {
      Allocator.UpButton.SetInteractable(canAdd);
      Allocator.m_AddCost.SetText(canAdd ? "-1" : string.Empty, syncTextInputBox: true);
    }

    private void UpdateCanRemove(bool canRemove)
    {
      Allocator.DownButton.SetInteractable(canRemove);
      Allocator.m_RemoveCost.SetText(canRemove ? "+1" : string.Empty, syncTextInputBox: true);
    }

    private void UpdateAvailableProwess(bool canSelect)
    {
      FirstProwessToggle.interactable = FirstProwessToggle.isOn || canSelect;
      SecondProwessToggle.interactable = SecondProwessToggle.isOn || (FirstProwessToggle.isOn && canSelect);
    }

    private void DisableToggles(params ToggleWorkaround[] toggles)
    {
      // Don't disable toggles that are on so it can be unselected
      foreach (var toggle in toggles)
        if (!toggle.isOn)
          toggle.interactable = false;
    }

    private ToggleWorkaround CreateToggle(bool first = true)
    {
      var toggle = GameObject.Instantiate(Prefabs.Checkbox);
      toggle.transform.AddTo(gameObject.ChildObject("Bonus").transform);

      // Destroy unneeded children and components
      toggle.gameObject.DestroyChildren("Label");
      toggle.gameObject.DestroyComponents<HorizontalLayoutGroupWorkaround>();

      toggle.gameObject.Rect().localPosition = new(x: first ? -40 : 0, y: -25);

      return toggle;
    }
  }

  /// <summary>
  /// Replacement for the in-game CharGenAbilityScoreAllocatorVM, since that one is patched to work w/ Prowess which
  /// conflicts with the logic for Legendary Ability.
  /// </summary>
  internal class LegendaryAbilityScoreAllocatorVM : BaseDisposable, IViewModel, IHasTooltipTemplate
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryAbilityScoreAllocatorVM));

    private readonly ReactiveProperty<ModifiableValue> Stat = new();
    private readonly StatType Type;

    private readonly LegendaryGiftState State;
    private readonly InfoSectionVM InfoVM;

    public LegendaryAbilityScoreAllocatorVM(StatType type, LegendaryGiftState state, InfoSectionVM infoVM)
    {
      Type = type;
      State = state;
      InfoVM = infoVM;

      Stat.ToSequentialReadOnlyReactiveProperty();
      Stat.Value = State.Controller.Preview.Stats.GetStat(Type);

      AddDisposable(Stat.Subscribe(_ => UpdateValues()));
      AddDisposable(State.AvailableGifts.Subscribe(_ => UpdateEligibility()));

      Name = LocalizedTexts.Instance.Stats.GetText(Type);
      ShortName = UIUtilityTexts.GetStatShortName(Type);
    }

    public override void DisposeImplementation() { }

    internal void UpdateStat()
    {
      Stat.SetValueAndForceNotify(State.Controller.Preview.Stats.GetStat(Type));
    }

    internal void SetRecommendationsForClass(ClassData classData)
    {
      var recommend = classData is not null && classData.RecommendedAttributes.Contains(Type);
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
      if (State.Controller.State.SelectedClass != null)
      {
        var classData = State.Controller.Preview.Progression.GetClassData(State.Controller.State.SelectedClass);
        archetype = classData?.Archetypes.FirstOrDefault();
      }
      Kingmaker.UI.MVVM._VM.Tooltip.Templates.ClassInformation classInformation = new();
      classInformation.Class = State.Controller.State.SelectedClass;
      classInformation.Unit = State.Controller.Preview.Descriptor;
      classInformation.Archetype = archetype;
      StatTooltipData statData = new StatTooltipData(State.Controller.Preview.Stats.GetStat(Type));
      return new TooltipTemplateAbilityScoreAllocator(classInformation, statData);
    }

    internal void TryIncreaseAbility()
    {
      State.TryAddLegendaryAbility(Type);
    }

    internal void TryDecreaseAbility()
    {
      State.TryRemoveLegendaryAbility(Type);
    }

    internal void ToggleProwess(bool selected)
    {
      if (selected)
        State.TrySelectProwess(Type);
      else
        State.TryUnselectProwess(Type);
    }

    private void UpdateValues()
    {
      StatValue.Value =
        Stat.Value.PermanentValue + Common.GetProwessBonus(Stat.Value) + Common.GetLegendaryBonus(Stat.Value);
      Modifier.Value = (StatValue.Value - 10) / 2;
    }

    private void UpdateEligibility()
    {
      CanAddAbility.Value = State.CanAddLegendaryAbility(Type);
      CanRemoveAbility.Value = State.CanRemoveLegendaryAbility(Type);
      // Force notify or it only notifies on change
      CanSelectProwess.SetValueAndForceNotify(State.CanSelectProwess(Type));
    }

    internal readonly string Name;
    internal readonly string ShortName;

    // Stat UI
    internal readonly IntReactiveProperty StatValue = new();
    internal readonly IntReactiveProperty Modifier = new();
    internal readonly ReactiveProperty<RecommendationMarkerVM> Recommendation = new();

    // Legendary Ability
    internal readonly BoolReactiveProperty CanAddAbility = new();
    internal readonly BoolReactiveProperty CanRemoveAbility = new();

    // Legendary Prowess
    internal readonly BoolReactiveProperty CanSelectProwess = new();
  }
}
