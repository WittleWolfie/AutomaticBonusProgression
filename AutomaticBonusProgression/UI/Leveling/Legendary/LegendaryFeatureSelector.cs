﻿using AutomaticBonusProgression.Util;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UI;
using Kingmaker.UI.MVVM._PCView.CharGen.Phases.AbilityScores;
using Kingmaker.UI.MVVM._VM.InfoWindow;
using Kingmaker.UI.MVVM._VM.Tooltip.Templates;
using Kingmaker.UnitLogic;
using Owlcat.Runtime.UI.MVVM;
using Owlcat.Runtime.UI.Tooltips;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  /// <summary>
  /// Wrapper view around CharGenAbilityScoreAllocatorPCView for legendary armor / weapon gifts.
  /// </summary>
  internal class LegendaryFeatureSelectorView : ViewBase<LegendaryFeatureSelectorVM>, IPointerEnterHandler
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryFeatureSelectorView));

    private CharGenAbilityScoreAllocatorPCView Allocator;
    private ToggleWorkaround Toggle;
    private TextMeshProUGUI Label;

    internal void Init(CharGenAbilityScoreAllocatorPCView source)
    {
      Allocator = source;
      CreateToggle();
      CreateLabel();
    }

    public override void BindViewImplementation()
    {
      Label.SetText(ViewModel.Name);

      AddDisposable(ViewModel.HasFeature.Subscribe(value => UpdateToggleIsOn()));
      AddDisposable(ViewModel.CanSelectFeature.Subscribe(value => UpdateToggle()));
    }

    public override void DestroyViewImplementation() { }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return null;
    }

    private void CreateToggle()
    {
      Toggle = GameObject.Instantiate(Prefabs.Checkbox);
      var parent = gameObject.ChildObject("Bonus").transform;
      var toggle = Toggle.gameObject;
      toggle.transform.AddTo(parent);

      // Destroy unneeded children and components
      toggle.DestroyChildren("Label");
      toggle.DestroyComponents<HorizontalLayoutGroupWorkaround>();

      toggle.Rect().localPosition = new(x: -20, y: -25);
    }

    private void CreateLabel()
    {
      Label = GameObject.Instantiate(Prefabs.EarlyText);
      var parent = gameObject.ChildObject("Labels/Background").transform;
      var label = Label.gameObject;
      label.transform.AddTo(parent);

      Label.fontStyle = FontStyles.Bold;
      Label.fontSize = 22;
      Label.alignment = TextAlignmentOptions.Right;
      label.Rect().localPosition = new(x: -65, y: 0);
    }

    private void UpdateToggleIsOn()
    {
      Toggle.isOn = ViewModel.HasFeature.Value;
    }

    private void UpdateToggle()
    {
      Toggle.interactable = ViewModel.CanSelectFeature.Value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      ViewModel.TryShowTooltip();
    }
  }

  internal class LegendaryFeatureSelectorVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(LegendaryEnchantmentScoreAllocatorVM));

    private readonly BlueprintFeature Feature;
    private readonly LegendaryGiftState State;
    private readonly InfoSectionVM InfoVM;

    public LegendaryFeatureSelectorVM(BlueprintFeature feature, LegendaryGiftState state, InfoSectionVM infoVM)
    {
      Feature = feature;
      State = state;
      InfoVM = infoVM;

      AddDisposable(State.AvailableGifts.Subscribe(_ => UpdateEligibility()));

      Name = feature.Name;
    }

    public override void DisposeImplementation() { }

    internal void TryShowTooltip()
    {
      InfoVM.SetTemplate(TooltipTemplate());
    }

    public TooltipBaseTemplate TooltipTemplate()
    {
      return new TooltipTemplateFeature(Feature);
    }

    internal void ToggleFeature(bool selected)
    {
      if (selected)
        State.TrySelectFeature(Feature);
      else
        State.TrySelectFeature(Feature);
    }

    private void UpdateEligibility()
    {
      HasFeature.Value = State.IsFeatureSelected(Feature) || State.Controller.Preview.HasFact(Feature);
      CanSelectFeature.Value = State.IsFeatureSelected(Feature) || State.CanSelectFeature(Feature);
    }

    internal readonly string Name;

    internal readonly BoolReactiveProperty HasFeature = new();
    internal readonly BoolReactiveProperty CanSelectFeature = new();
  }
}
