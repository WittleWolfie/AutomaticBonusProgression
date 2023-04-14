using AutomaticBonusProgression.Util;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.CharacterInfo.Sections.Abilities;
using UnityEngine;

namespace AutomaticBonusProgression.UI
{/// <summary>
 /// Class storing prefab objects used to instantiate elements and containers.
 /// </summary>
 /// 
 /// <remarks>
 /// Each Prefab is a copy of some original prefab. This allows edit any components on the object once to create
 /// a prefab, rather than using a base game prefab and editing components each time it is used. 
 /// </remarks>
  internal static class Prefabs
  {
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Prefabs));

    internal static GameObject Button;
    internal static GameObject DataGrid;

    internal static CharInfoFeaturePCView Feature;

    internal static void Create()
    {
      Logger.Log("Creating prefabs");

      CreateButton();
      CreateDataGrid();

      CreateFeature();
    }

    private static void CreateButton()
    {
      Button = GameObject.Instantiate(UITool.StaticCanvas.ChildObject("ChangeVisualPCView/Window/BackToStashButton/OwlcatButton"));
    }

    private static void CreateDataGrid()
    {
      DataGrid = GameObject.Instantiate(
        UITool.StaticCanvas.ChildObject(
          "ServiceWindowsPCView/Background/Windows/SpellbookPCView/SpellbookScreen/MainContainer/KnownSpells/StandardScrollView/"));
    }

    private static void CreateFeature()
    {
      Feature = GameObject.Instantiate(
        UITool.StaticCanvas.ChildObject(
          "ServiceWindowsPCView/Background/Windows/CharacterInfoPCView/CharacterScreen/Abilities")
            .GetComponent<CharInfoAbilitiesPCView>().m_WidgetEntityView.m_WidgetEntityView);
    }
  }
}
