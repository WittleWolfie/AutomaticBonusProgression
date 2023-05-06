using AutomaticBonusProgression.Components;
using AutomaticBonusProgression.UnitParts;
using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.UI;
using Kingmaker.UI.Common;
using Kingmaker.UI.FullScreenUITypes;
using Kingmaker.UI.MVVM._PCView.ChangeVisual;
using Kingmaker.UI.MVVM._PCView.InGame;
using Kingmaker.UI.MVVM._PCView.Tooltip.Bricks;
using Kingmaker.UI.Tooltip;
using Kingmaker.UnitLogic;
using Owlcat.Runtime.UI.Controls.Button;
using Owlcat.Runtime.UI.Controls.Other;
using Owlcat.Runtime.UI.MVVM;
using System;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

namespace AutomaticBonusProgression.UI.Attunement
{
  /// <summary>
  /// TODO:
  ///  - Close this whenever a full screen UI is opened?
  /// </summary>
  internal class AttunementView : ViewBase<AttunementVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementView));

    #region Static
    private static AttunementView BaseView;
    internal static readonly ReactiveProperty<AttunementVM> AttunementVM = new();

    internal static void ShowWindow(EnhancementType type)
    {
      AttunementVM.Value?.Close();
      AttunementVM.Value = new(DisposeWindow, type);
    }

    internal static void DisposeWindow()
    {
      AttunementVM.Value?.Dispose();
      AttunementVM.Value = null;
    }
    #endregion

    private Transform Window;
    private OwlcatButton CloseButton;
    private TextMeshProUGUI Header;

    private EnchantmentGridView Enchantments;
    private TextMeshProUGUI EnhancementBonus;

    private TooltipBrickEntityHeaderView Equipment;

    private OwlcatButton MainHand;
    private OwlcatButton OffHand;
    private OwlcatButton Armor;
    private OwlcatButton Shield;

    private OwlcatButton Apply;

    private readonly List<Transform> Children = new();

    public override void BindViewImplementation()
    {
      gameObject.SetActive(true);

      Enchantments.Bind(new(ViewModel.Type));
      AddDisposable(Game.Instance.UI.EscManager.Subscribe(ViewModel.Close));
      AddDisposable(CloseButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.Close()));
      AddDisposable(MainHand.OnLeftClickAsObservable().Subscribe(_ => ViewModel.SetType(EnhancementType.MainHand)));
      AddDisposable(OffHand.OnLeftClickAsObservable().Subscribe(_ => ViewModel.SetType(EnhancementType.OffHand)));
      AddDisposable(Armor.OnLeftClickAsObservable().Subscribe(_ => ViewModel.SetType(EnhancementType.Armor)));
      AddDisposable(Shield.OnLeftClickAsObservable().Subscribe(_ => ViewModel.SetType(EnhancementType.Shield)));
      AddDisposable(Apply.OnLeftClickAsObservable().Subscribe(_ => ViewModel.Apply(Enchantments.ViewModel)));

      Refresh();
      ViewModel.Subscribe(Refresh);
    }

    public override void DestroyViewImplementation()
    {
      gameObject.SetActive(false);

      Children.ForEach(child => DestroyImmediate(child.gameObject));
      Children.Clear();
    }

    internal void Initialize()
    {
      Window = gameObject.ChildObject("Window").transform;
      CloseButton = gameObject.ChildObject("Window/Close").GetComponent<OwlcatButton>();
      Header = gameObject.ChildObject("Window/Header").GetComponentInChildren<TextMeshProUGUI>();

      Enchantments = EnchantmentGridView.Instantiate(Window);
      EnhancementBonus = CreateEnhancementBonus();

      MainHand = CreateAttunementTypeButton(UITool.GetString("Weapon"), 0);
      OffHand = CreateAttunementTypeButton(UITool.GetString("OffHand"), 1);
      Armor = CreateAttunementTypeButton(UITool.GetString("Armor"), 2);
      Shield = CreateAttunementTypeButton(UITool.GetString("Shield"), 3);

      Apply = CreateApplyButton(UITool.GetString("Attunement.Apply"));
    }

    // Use for prefabs that don't exist during first init
    internal void LateInit()
    {
      Equipment = GameObject.Instantiate(Prefabs.ItemInfoBlock);
      Equipment.transform.AddTo(transform);

      var rect = Equipment.GetComponent<RectTransform>();
      rect.localPosition = new(x: 0, y: -325);
      rect.sizeDelta = new(x: 700, y: 90);
    }

    private void Refresh()
    {
      Header.text = ViewModel.GetHeader();
      UITool.DecorateTitle(Header);

      MainHand.SetInteractable(ViewModel.Type.Value != EnhancementType.MainHand);
      OffHand.SetInteractable(ViewModel.Type.Value != EnhancementType.OffHand);
      Armor.SetInteractable(ViewModel.Type.Value != EnhancementType.Armor);
      Shield.SetInteractable(ViewModel.Type.Value != EnhancementType.Shield);
      Apply.SetInteractable(ViewModel.CanAttune());

      switch (ViewModel.Type.Value)
      {
        case EnhancementType.MainHand:
          BindEquippedItem(ViewModel.Unit.Body.PrimaryHand.Weapon);
          break;
        case EnhancementType.OffHand:
          var weapon = Common.GetSecondaryWeapon(ViewModel.Unit);
          if (weapon is null)
            BindUnequipped();
          else
            BindEquippedItem(weapon);
          break;
        case EnhancementType.Armor:
          if (ViewModel.Unit.Body.Armor.HasArmor)
            BindEquippedItem(ViewModel.Unit.Body.Armor.Armor);
          else
            BindUnarmored();
          break;
        case EnhancementType.Shield:
          if (ViewModel.Unit.Body.SecondaryHand.HasShield)
            BindEquippedItem(ViewModel.Unit.Body.SecondaryHand.Shield);
          else
            BindUnequipped();
          break;
      }

      var unitPartEnhancement = ViewModel.Unit.Ensure<UnitPartEnhancement>();
      if (EnhancementSubscription is not null)
        RemoveDisposable(EnhancementSubscription);
      EnhancementSubscription =
        unitPartEnhancement.TempEnhancement.Subscribe(
          bonus =>
            EnhancementBonus.text =
              string.Format(
                UITool.GetString("Attunement.Enhancement"),
                bonus,
                unitPartEnhancement.GetMax(ViewModel.Type.Value)));
      AddDisposable(EnhancementSubscription);
    }

    // Used since we need a subscription per unit for UnitPartEnhancement. Prevents continually adding subscriptions.
    private IDisposable EnhancementSubscription;

    private void BindEquippedItem(ItemEntity item)
    {
      Equipment.m_ItemContainer.SetActive(true);
      Equipment.m_OtherContainer.SetActive(false);

      var icon = item.Icon ?? item.Blueprint.Icon;
      Equipment.m_ImageContainer.SetActive(icon is not null);
      Equipment.m_Image.sprite = icon;

      Equipment.m_MainTitle.text = item.Name;

      var tooltipData = UIUtilityItem.GetItemTooltipData(item);
      Equipment.m_Title.text = tooltipData.GetText(TooltipElement.Subname);

      Equipment.m_RightLabel.text = tooltipData.GetText(TooltipElement.Twohanded);
    }

    private void BindUnarmored()
    {
      Equipment.m_ItemContainer.SetActive(true);
      Equipment.m_OtherContainer.SetActive(false);
      Equipment.m_ImageContainer.SetActive(false);

      Equipment.m_MainTitle.text = UITool.GetString("Attunement.Armor.None");
      Equipment.m_Title.text = UITool.GetString("Attunement.Armor.Light");
    }

    private void BindUnequipped()
    {
      Equipment.m_ItemContainer.SetActive(true);
      Equipment.m_OtherContainer.SetActive(false);
      Equipment.m_ImageContainer.SetActive(false);

      Equipment.m_MainTitle.text = UITool.GetString("Attunement.Unequipped");

      Equipment.m_Title.text = null;
      Equipment.m_RightLabel.text = null;
    }

    private OwlcatButton CreateAttunementTypeButton(string label, int position)
    {
      var button = Prefabs.CreateButton(label);

      var buttonTransform = button.transform;
      buttonTransform.AddTo(transform);

      // Each button has a height of 50
      var offset = position * 50;
      buttonTransform.localPosition = new(x: -700, y: -260 - offset);

      return button;
    }

    private OwlcatButton CreateApplyButton(string label)
    {
      var button = Prefabs.CreateButton(label);

      var buttonTransform = button.transform;
      buttonTransform.AddTo(transform);

      buttonTransform.localPosition = new(x: 700, y: -410);

      return button;
    }

    private TextMeshProUGUI CreateEnhancementBonus()
    {
      var label = GameObject.Instantiate(Prefabs.Text);
      label.fontSize = 32;
      label.enableAutoSizing = true;

      var labelTransform = label.transform;
      labelTransform.AddTo(transform);

      labelTransform.localPosition = new(x: 0, y: -235);

      return label;
    }

    #region Setup
    [HarmonyPatch(typeof(InGameStaticPartPCView))]
    static class InGameStaticPartPCView_Patch
    {
      [HarmonyPatch(nameof(InGameStaticPartPCView.Initialize)), HarmonyPostfix]
      static void Initialize(InGameStaticPartPCView __instance)
      {
        try
        {
          Logger.Log("Initializing WindowView BaseView");
          Prefabs.InitStatic();
          BaseView = Create(__instance.m_ChangeVisualPCView);
        }
        catch (Exception e)
        {
          Logger.LogException("InGameStaticPartPCView_Patch.Initialize", e);
        }
      }

      [HarmonyPatch(nameof(InGameStaticPartPCView.BindViewImplementation)), HarmonyPostfix]
      static void BindViewImplementation(InGameStaticPartPCView __instance)
      {
        try
        {
            Logger.Log("Binding to AttunementVM");
            __instance.AddDisposable(AttunementVM.Subscribe(BaseView.Bind));
        }
        catch (Exception e)
        {
            Logger.LogException("InGameStaticPartPCView_Patch.BindViewImplementation", e);
        }
      }

      internal static AttunementView Create(ChangeVisualPCView changeVisualView)
      {
        var obj = Instantiate(changeVisualView.gameObject);
        obj.transform.AddTo(changeVisualView.transform.parent);
        obj.MakeSibling("ServiceWindowsPCView");

        obj.DestroyComponents<ChangeVisualPCView>();
        // TODO: Add as components!
        obj.DestroyChildren(
          "Window/InteractionSlot",
          "Window/Inventory",
          "Window/Doll",
          "Window/BackToStashButton",
          "Window/ChangeItemsPool");

        var view = obj.AddComponent<AttunementView>();
        view.Initialize();
        return view;
      }
    }

    [HarmonyPatch(typeof(FadeCanvas))]
    static class FadeCanvas_Patch
    {
      [HarmonyPatch(nameof(FadeCanvas.Initialize)), HarmonyPostfix]
      static void Initialize()
      {
        try
        {
          Prefabs.InitFade();
          BaseView.LateInit();
        }
        catch (Exception e)
        {
          Logger.LogException("FadeCanvas_Patch.Initialize", e);
        }
      }
    }
    #endregion
  }

  internal class AttunementVM : BaseDisposable, IViewModel
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementVM));

    private readonly Action DisposeAction;
    private Action OnRefresh;

    internal UnitEntityData Unit => Game.Instance.SelectionCharacter.SelectedUnit.Value;
    internal readonly ReactiveProperty<EnhancementType> Type = new();

    internal AttunementVM(Action disposeAction, EnhancementType type)
    {
      DisposeAction = disposeAction;
      Type.Value = type;
      EventBus.RaiseEvent<IFullScreenUIHandler>(h => h.HandleFullScreenUiChanged(state: true, FullScreenUIType.Unknown));

      AddDisposable(Game.Instance.SelectionCharacter.SelectedUnit.Subscribe(unit => Refresh()));
      AddDisposable(Type.Subscribe(type => Refresh()));
    }

    public override void DisposeImplementation()
    {
      DisposeAction();
      EventBus.RaiseEvent<IFullScreenUIHandler>(h => h.HandleFullScreenUiChanged(state: false, FullScreenUIType.Unknown));
    }

    internal void Close()
    {
      DisposeImplementation();
    }

    internal string GetHeader()
    {
      return Type.Value switch
      {
        EnhancementType.MainHand => UITool.GetString("Attunement.Weapon"),
        EnhancementType.OffHand => UITool.GetString("Attunement.OffHand"),
        EnhancementType.Armor => UITool.GetString("Attunement.Armor"),
        EnhancementType.Shield => UITool.GetString("Attunement.Shield"),
        _ => throw new InvalidOperationException($"Unknown enhancement type: {Type}"),
      };
    }

    internal void SetType(EnhancementType type)
    {
      if (Type.Value != type)
        Type.Value = type;
    }

    internal void Apply(EnchantmentsVM enchantmentsVM)
    {
      // Can only apply once a day
      switch (Type.Value)
      {
        case EnhancementType.Armor:
          Unit.Resources.Spend(Common.LegendaryArmorResource, 1);
          break;
        case EnhancementType.Shield:
          Unit.Resources.Spend(Common.LegendaryShieldResource, 1);
          break;
        case EnhancementType.MainHand:
          Unit.Resources.Spend(Common.LegendaryWeaponResource, 1);
          break;
        case EnhancementType.OffHand:
          Unit.Resources.Spend(Common.LegendaryOffHandResource, 1);
          break;
      }

      foreach (var enchant in enchantmentsVM.GetInactiveEnchantments())
      {
        Logger.Verbose(() => $"Removing {enchant.Name} from {Unit.CharacterName}");
        Unit.RemoveFact(enchant);
      }

      foreach (var enchant in enchantmentsVM.GetActiveEnchantments())
      {
        Logger.Verbose(() => $"Applying {enchant.Name} to {Unit.CharacterName}");
        Unit.AddBuff(enchant, Unit);
      }

      Refresh();
    }

    internal bool CanAttune()
    {
      return Type.Value switch
      {
        EnhancementType.Armor => Unit.Resources.HasEnoughResource(Common.LegendaryArmorResource, 1),
        EnhancementType.Shield => Unit.Resources.HasEnoughResource(Common.LegendaryShieldResource, 1),
        EnhancementType.MainHand => Unit.Resources.HasEnoughResource(Common.LegendaryWeaponResource, 1),
        EnhancementType.OffHand => Unit.Resources.HasEnoughResource(Common.LegendaryOffHandResource, 1),
        _ => throw new NotImplementedException(),
      };
    }

    private void Refresh()
    {
      Logger.Verbose(() => $"Refreshing Attunement Window: {Unit}, {Type.Value}");
      OnRefresh?.Invoke();
    }

    internal void Subscribe(Action onRefresh)
    {
      OnRefresh = onRefresh;
    }
  }

  internal class ShowAttunement : GameAction
  {
    internal EnhancementType Type;

    public override string GetCaption()
    {
      return "Shows the attunement window";
    }

    public override void RunAction()
    {
      AttunementView.ShowWindow(Type);
    }
  }
}
