using AutomaticBonusProgression.Util;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker;
using Kingmaker.ElementsSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.UI;
using Kingmaker.UI.FullScreenUITypes;
using Kingmaker.UI.MVVM._PCView.ChangeVisual;
using Kingmaker.UI.MVVM._PCView.InGame;
using Kingmaker.UI.MVVM._PCView.Tooltip.Bricks;
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
  ///  - Actually bind view to current relevant equipment
  ///  - Implement support for the different attunement types
  ///  - Implement the Apply functionality
  ///  - Add Available / Remaining labels
  /// </summary>
  internal class AttunementView : ViewBase<AttunementVM>
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(AttunementView));

    #region Static
    private static AttunementView BaseView;
    internal static readonly ReactiveProperty<AttunementVM> AttunementVM = new();

    internal static void ShowWindow()
    {
      AttunementVM.Value?.Close();
      AttunementVM.Value = new(DisposeWindow);
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
      AddDisposable(Game.Instance.UI.EscManager.Subscribe(ViewModel.Close));
      AddDisposable(CloseButton.OnLeftClickAsObservable().Subscribe(_ => ViewModel.Close()));

      Header.text = ViewModel.GetHeader();
      Enchantments.Bind(new());

      // TODO: Actually set up binding to equipment
      Equipment.m_ItemContainer.SetActive(true);
      Equipment.m_OtherContainer.SetActive(false);
      Equipment.m_MainTitle.text = "Chainmail of Total Awesomeness +4";
      Equipment.m_Title.text = "Medium Armor - Chainmail";

      switch (ViewModel.Type)
      {
        case AttunementType.Weapon:
          MainHand.SetInteractable(false);
          break;
        case AttunementType.OffHand:
          OffHand.SetInteractable(false);
          break;
        case AttunementType.Armor:
          Armor.SetInteractable(false);
          break;
        case AttunementType.Shield:
          Shield.SetInteractable(false);
          break;
      }
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
    private readonly Action DisposeAction;

    internal AttunementType Type = AttunementType.Armor;

    internal AttunementVM(Action disposeAction)
    {
      DisposeAction = disposeAction;
      EventBus.RaiseEvent<IFullScreenUIHandler>(h => h.HandleFullScreenUiChanged(state: true, FullScreenUIType.Unknown));
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
      return Type switch
      {
        AttunementType.Weapon => UITool.GetString("Attunement.Weapon"),
        AttunementType.OffHand => UITool.GetString("Attunement.OffHand"),
        AttunementType.Armor => UITool.GetString("Attunement.Armor"),
        AttunementType.Shield => UITool.GetString("Attunement.Shield"),
        _ => throw new InvalidOperationException($"Unknown attunement type: {Type}"),
      };
    }
  }

  internal enum AttunementType
  {
    Weapon,
    OffHand,
    Armor,
    Shield
  }

  internal class ShowAttunement : GameAction
  {
    public override string GetCaption()
    {
      return "Shows the attunement window";
    }

    public override void RunAction()
    {
      AttunementView.ShowWindow();
    }
  }
}
