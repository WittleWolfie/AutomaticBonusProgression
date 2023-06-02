using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI.Attunement
{
  internal class EnchantmentGridView : ViewBase<EnchantmentsVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnchantmentGridView));

    internal static EnchantmentGridView Instantiate(Transform parent)
    {
      var transform = UnityEngine.Object.Instantiate(Prefabs.EnchantmentContainer).transform;
      var scrollView = transform.Find("StandardScrollView");

      // Create grid params
      var gridLayout = scrollView.GetComponentInChildren<GridLayoutGroupWorkaround>();
      gridLayout.startAxis = GridLayoutGroup.Axis.Horizontal;
      gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
      gridLayout.constraintCount = 4;
      gridLayout.cellSize = new(353, 75);

      // Text shown if there are no enchantments available
      var emptyText = UnityEngine.Object.Instantiate(Prefabs.Text);
      emptyText.transform.AddTo(scrollView);
      emptyText.transform.localPosition = Vector3.zero;
      emptyText.SetText(UITool.GetString("Attunement.Empty"));

      // Add the view & make sure viewport scales to fit
      var view = scrollView.gameObject.CreateComponent<EnchantmentGridView>(
        view =>
        {
          view.Grid = gridLayout.transform;
          view.EmptyText = emptyText.transform;
        });
      scrollView.transform.Find("Viewport").Rect().offsetMin = Vector2.zero;

      // Add before configuring layout params or else some may be overridden
      transform.AddTo(parent);
      //transform.gameObject.AddComponent<Image>().color = Color.blue;

      // Set up the size
      var rect = transform.Rect();
      rect.anchorMin = new(0.11f, 0.30f);
      rect.anchorMax = new(0.90f, 0.85f);
      rect.sizeDelta = Vector2.zero;

      return view;
    }

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
    }

    private void Refresh()
    {
      EmptyText.gameObject.SetActive(!ViewModel.AvailableEnchantments.Any());

      ViewModel.AvailableEnchantments.ForEach(
        feature =>
        {
          var view = EnchantmentView.Instantiate();
          view.Bind(feature);
          view.transform.AddTo(Grid);
        });
    }

    internal Transform Grid;
    internal Transform EmptyText;
  }

  internal class EnchantmentsVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EnchantmentsVM));

    internal List<EnchantmentVM> AvailableEnchantments = new();

    private Action OnRefresh;
    private UnitEntityData Unit => Game.Instance.SelectionCharacter.SelectedUnit.Value;
    private readonly ReactiveProperty<EnhancementType> Type;

    internal EnchantmentsVM(ReactiveProperty<EnhancementType> type)
    {
      Type = type;
      AddDisposable(Game.Instance.SelectionCharacter.SelectedUnit.Subscribe(_ => Refresh()));
      AddDisposable(Type.Subscribe(_ => Refresh()));
    }

    public override void DisposeImplementation()
    {
      foreach (var vm in AvailableEnchantments)
        vm.Dispose();
    }

    internal List<BlueprintBuff> GetActiveEnchantments()
    {
      return AvailableEnchantments.Where(vm => vm.CurrentState.Value == EnchantmentVM.State.Active).Select(vm => vm.Enchant).ToList();
    }

    internal List<BlueprintBuff> GetInactiveEnchantments()
    {
      return AvailableEnchantments.Where(vm => vm.CurrentState.Value != EnchantmentVM.State.Active).Select(vm => vm.Enchant).ToList();
    }

    private void Refresh()
    {
      Logger.Verbose(() => $"Refreshing Enchantment Grid: {Unit}, {Type.Value}");
      try
      {
        foreach (var vm in AvailableEnchantments)
          vm.Dispose();

        AvailableEnchantments.Clear();

        if (Unit is null)
          return;

        var legendaryFeature = Type.Value switch
        {
          EnhancementType.Armor => Unit.GetFeature(Common.LegendaryArmor),
          EnhancementType.Shield => Unit.GetFeature(Common.LegendaryShield),
          EnhancementType.MainHand => Unit.GetFeature(Common.LegendaryWeapon),
          EnhancementType.OffHand => Unit.GetFeature(Common.LegendaryOffHand),
          _ => throw new NotImplementedException(),
        };
        if (legendaryFeature is null)
          return;

        var attunement = legendaryFeature.GetComponent<AttunementBuffsComponent>();
        if (attunement is null)
          throw new InvalidOperationException($"Missing AttunementBuffsComponent: {legendaryFeature.Name}");

        var unitPart = Unit.Ensure<UnitPartEnhancement>();
        unitPart.ResetTempEnhancement(Type.Value);

        Logger.Verbose(() => $"Adding enchantments: {legendaryFeature.Name}");
        foreach (var buff in attunement.Buffs)
        {
          var bp = buff.Get();
          Logger.Verbose(() => $"Checking {bp.Name}");
          var effect = bp.GetComponent<AttunementEffect>();
          if (effect is null)
            throw new InvalidOperationException($"Missing AttunementEffect: {bp.Name}");

          if (legendaryFeature.GetRank() < effect.Cost)
            continue;

          AvailableEnchantments.Add(new EnchantmentVM(bp, effect, Unit));
        }
      }
      finally
      {
        OnRefresh?.Invoke();
      }
    }

    internal void Subscribe(Action onRefresh)
    {
      OnRefresh = onRefresh;
    }
  }
}
