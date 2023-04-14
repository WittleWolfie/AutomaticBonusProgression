using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker;
using Kingmaker.UI;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.CharacterInfo.Sections.Abilities;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Abilities;
using Kingmaker.UnitLogic;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI.Attunement
{
  internal class EnchantmentsView : ViewBase<EnchantmentsVM>
  {
    internal static EnchantmentsView Instantiate(Transform parent)
    {
      var transform = UnityEngine.Object.Instantiate(Prefabs.DataGrid).transform;

      // Create grid params
      var gridLayout = transform.GetComponentInChildren<GridLayoutGroupWorkaround>();
      gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
      gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
      gridLayout.constraintCount = 3;
      gridLayout.cellSize = new(346, 60); // Standard size for UIFeature, might change later

      // Add the view & make sure viewport scales to fit
      var view = transform.gameObject.CreateComponent<EnchantmentsView>(view => { });
      transform.Find("Viewport").Rect().offsetMin = Vector2.zero;

      // Add before configuring layout params or else some may be overridden
      transform.AddTo(parent);

      // Set up the size
      var rect = transform.Rect();
      rect.anchorMin = new(0.12f, 0.3f);
      rect.anchorMax = new(0.89f, 0.85f);
      rect.sizeDelta = Vector2.zero;

      return view;
    }

    // Track children so they are not retained when the window is destroyed
    private readonly List<Transform> Children = new();

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);
      Refresh();
      // Subscribe to the view model which will call this whenever the view should update
      ViewModel.Subscribe(Refresh);
    }

    public override void DestroyViewImplementation()
    {
      gameObject.SetActive(false);

      Children.ForEach(child => GameObject.DestroyImmediate(child.gameObject));
      Children.Clear();
    }

    private void Refresh()
    {
      // TODO: Should there be a bind callback here?
      // TODO: How to handle clicks on items?
      // TODO: How can user refresh?

      // CharInfoFeatures are copied as is from Owlcat so there's no Element class or styling.
      ViewModel.CharInfoFeatures.ForEach(
        feature =>
        {
          var view = GameObject.Instantiate<CharInfoFeaturePCView>(Prefabs.Feature);
          view.Bind(feature);
          view.transform.AddTo(Grid);
          Children.Add(view.transform);
        });
    }

    internal Transform Grid;
  }

  internal class EnchantmentsVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnchantmentsVM));

    internal List<CharInfoFeatureVM> CharInfoFeatures = new();

    private Action OnRefresh;
    private UnitDescriptor Unit => SelectedUnit.Value;
    private readonly ReactiveProperty<UnitDescriptor> SelectedUnit = new();

    internal EnchantmentsVM()
    {
      AddDisposable(
        Game.Instance.SelectionCharacter.SelectedUnit.Subscribe(
          unit =>
          {
            SelectedUnit.Value = unit.Value;
            Refresh();
          }));
    }

    public override void DisposeImplementation() { }

    private void Refresh()
    {
      Logger.Verbose(() => $"Refreshing Enchantments: {Unit}");
      foreach (var vm in CharInfoFeatures)
        vm.Dispose();

      CharInfoFeatures.Clear();

      if (Unit is null)
        return;

      foreach (var feature in Unit.Progression.Features)
      {
        var attunement = feature.GetComponent<AttunementComponent>();
        if (attunement is null)
          continue;

        CharInfoFeatures.Add(new(feature));
      }

      OnRefresh?.Invoke();
    }

    internal void Subscribe(Action onRefresh)
    {
      OnRefresh = onRefresh;
    }
  }
}
