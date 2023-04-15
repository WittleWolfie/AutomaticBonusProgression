using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.Util;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI.Attunement
{
  internal class EnchantmentGridView : ViewBase<EnchantmentsVM>
  {
    internal static EnchantmentGridView Instantiate(Transform parent)
    {
      var transform = UnityEngine.Object.Instantiate(Prefabs.EnchantmentContainer).transform;
      var scrollView = transform.Find("StandardScrollView");

      // Create grid params
      var gridLayout = scrollView.GetComponentInChildren<GridLayoutGroupWorkaround>();
      gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
      gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
      gridLayout.constraintCount = 4;
      gridLayout.cellSize = new(346, 60);

      // Add the view & make sure viewport scales to fit
      var view = scrollView.gameObject.CreateComponent<EnchantmentGridView>(view => view.Grid = gridLayout.transform);
      scrollView.transform.Find("Viewport").Rect().offsetMin = Vector2.zero;

      // Add before configuring layout params or else some may be overridden
      transform.AddTo(parent);
      //transform.gameObject.AddComponent<Image>().color = Color.blue;

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
      ViewModel.AvailableEnchantments.ForEach(
        feature =>
        {
          var view = EnchantmentView.Instantiate();
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

    internal List<EnchantmentVM> AvailableEnchantments = new();

    private Action OnRefresh;
    private UnitDescriptor Unit => SelectedUnit.Value;
    private readonly ReactiveProperty<EnhancementType> Type = new();
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

      // TODO: Actually make this change w/ buttons and shit
      Type.Value = EnhancementType.Armor;
    }

    public override void DisposeImplementation() { }

    private void Refresh()
    {
      Logger.Verbose(() => $"Refreshing Enchantments: {Unit}");
      foreach (var vm in AvailableEnchantments)
        vm.Dispose();

      AvailableEnchantments.Clear();

      if (Unit is null)
        return;

      foreach (var feature in Unit.Progression.Features)
      {
        var attunement = feature.GetComponent<AttunementComponent>();
        if (attunement is null)
          continue;

        Logger.Verbose(() => $"Adding enchantments: {feature.Name}");
        foreach (var buff in attunement.Buffs)
        {
          if (feature.GetRank() < buff.ranks)
            continue;

          var bp = buff.buff.Get();
          var enhancement = bp.GetComponent<EnhancementEquivalenceComponent>();
          if (enhancement is null)
            throw new InvalidOperationException($"Missing EnhancementEquivalentComponent: {bp.Name}");

          if (enhancement.Type != Type.Value)
            continue;

          // TODO: Need to probably refactor the whole requirements model.
          //  - Probably reduce to two components (EnhancementEquivalence & Requirements?)
          //  - Update the UnitPart to use the components, store the active buffs, and enforce requirements
          EnchantmentVM viewModel;
          if (Type.Value == EnhancementType.Armor)
          {
            var requireArmor = bp.GetComponent<RequireArmor>();
            if (requireArmor is null)
              viewModel = new(bp, enhancement.Enhancement);
            else
              viewModel = new(bp, enhancement.Enhancement, requireArmor.AllowedTypes);

          }
          else //if (Type.Value == EnhancementType.Shield)
          {
            var requireShield = bp.GetComponent<RequireShield>();
            if (requireShield is null)
              viewModel = new(bp, enhancement.Enhancement);
            else
              viewModel = new(bp, enhancement.Enhancement, requireShield.AllowedTypes);
          }
          AvailableEnchantments.Add(viewModel);
        }
      }

      OnRefresh?.Invoke();
    }

    internal void Subscribe(Action onRefresh)
    {
      OnRefresh = onRefresh;
    }

    // TODO: Probably create our own view
    private CharInfoFeatureVM Create(BlueprintBuff buff, int rank)
    {
      var viewModel = (CharInfoFeatureVM)FormatterServices.GetUninitializedObject(typeof(CharInfoFeatureVM));
      viewModel.Icon = buff.Icon;
      viewModel.DisplayName = buff.Name;
      viewModel.IsSquare = true;
      viewModel.IsActive = true;
      viewModel.Acronym = UIUtility.GetAbilityAcronym(buff.name);
      viewModel.Rank = rank;

      return viewModel;
    }
  }
}
