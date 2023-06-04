using AutomaticBonusProgression.Util;
using HarmonyLib;
using Kingmaker;
using Kingmaker.UI;
using Kingmaker.UI.MVVM._PCView.InfoWindow;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Spellbook.KnownSpells;
using Kingmaker.UI.MVVM._PCView.Tooltip.Bricks;
using Owlcat.Runtime.Core.Utils;
using Owlcat.Runtime.UI.Controls.Button;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutomaticBonusProgression.UI
{
  /// <summary>
  /// Class storing prefab objects used to instantiate elements and containers.
  /// </summary>
  /// 
  /// <remarks>
  /// Each Prefab is a copy of some original object. This allows edit any components on the object once to create
  /// a prefab, rather than using a base game object and editing components each time it is used. 
  /// </remarks>
  internal static class Prefabs
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Prefabs));

    internal static OwlcatButton Button;
    internal static TextMeshProUGUI Text;
    internal static Image Image;
    internal static ToggleWorkaround Checkbox;

    internal static GameObject EnchantmentContainer;
    internal static SpellbookKnownSpellPCView Enchantment;

    internal static OwlcatButton CreateButton(string label)
    {
      var button = GameObject.Instantiate(Button);
      button.gameObject.ChildObject("Text").GetComponent<TextMeshProUGUI>().SetText(label);
      return button;
    }

    internal static Image CreateImage(Sprite sprite, int height, int width)
    {
      var image = GameObject.Instantiate(Image).GetComponent<Image>();
      image.gameObject.Rect().sizeDelta = new Vector2(width, height);
      image.sprite = sprite;
      return image;
    }

    internal static void InitStatic()
    {
      Logger.Log("Initializing prefabs for StaticCanvas");

      InitStaticBasics();
      InitEnchantmentPrefabs();
    }

    internal static void InitMainMenu()
    {
      Logger.Log("Initializing prefabs for MainMenu");

      if (Checkbox is null)
      {
        var checkbox = GameObject.Instantiate(
          UITool.MainMenu.ChildObject("ChargenPCView/ContentWrapper/SpellbookView/EdgeWindow/KnownSpells/Toggle"));
        GameObject.DontDestroyOnLoad(checkbox);
        Checkbox = checkbox.GetComponent<ToggleWorkaround>();
      }
    }

    private static void InitStaticBasics()
    {
      var button = GameObject.Instantiate(UITool.StaticCanvas.ChildObject("ChangeVisualPCView/Window/BackToStashButton/OwlcatButton"));
      Button = button.GetComponent<OwlcatButton>();

      var text = GameObject.Instantiate(UITool.StaticCanvas.ChildObject("ChangeVisualPCView/Window/Header/Header"));
      Text = text.GetComponent<TextMeshProUGUI>();

      var image = new GameObject("image-prefab", typeof(RectTransform));
      Image = image.AddComponent<Image>();
    }

    private static void InitEnchantmentPrefabs()
    {
      EnchantmentContainer = GameObject.Instantiate(
        UITool.StaticCanvas.ChildObject(
          "ServiceWindowsPCView/Background/Windows/SpellbookPCView/SpellbookScreen/MainContainer/KnownSpells"));
      // Destroy doesn't work for some reason.. just disable
      EnchantmentContainer.gameObject.ChildObject("Toggle").SetActive(false);

      Enchantment = GameObject.Instantiate(EnchantmentContainer.GetComponent<SpellbookKnownSpellsPCView>().m_KnownSpellView);
      Enchantment.gameObject.DestroyChildren(
        "Metamagic", "RemoveButton", "Icon/ArtBackImage", "Icon/Decoration", "Icon/Domain", "Icon/MythicArtFrame", "Icon/ArtArrowImage", "Level");
    }

    internal static TooltipBrickEntityHeaderView ItemInfoBlock;

    internal static void InitFade()
    {
      Logger.Log("Initializing prefabs for FadeCanvas");

      InitInfoBlocks();
    }

    private static void InitInfoBlocks()
    {
      var infoWindow = UITool.FadeCanvas.ChildObject("InfoWindowPCViewBig").GetComponent<InfoWindowPCView>();
      ItemInfoBlock = GameObject.Instantiate(infoWindow.m_BricksConfig.BrickEntityHeaderView);
      ItemInfoBlock.gameObject.DestroyChildren(
        "IconBlock/IconArrow", "TextBlock/SideBySideText (1)", "IconBlock/Icon/DecorationIcon (2)", "IconBlock/Icon/Icon (1)");
    }

    [HarmonyPatch(typeof(MainMenu))]
    static class MainMenu_Patch
    {
      [HarmonyPatch(nameof(MainMenu.Awake)), HarmonyPostfix]
      static void Awake()
      {
        try
        {
          InitMainMenu();
        }
        catch (Exception e)
        {
          Logger.LogException("MainMenu_Patch.Awake", e);
        }
      }
    }
  }
}
