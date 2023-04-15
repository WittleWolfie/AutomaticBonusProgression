using AutomaticBonusProgression.Util;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.CharacterInfo.Sections.Abilities;
using Kingmaker.UI.MVVM._PCView.ServiceWindows.Spellbook.KnownSpells;
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
    internal static GameObject EnchantmentContainer;

    internal static SpellbookKnownSpellPCView Enchantment;

    internal static void Create()
    {
      Logger.Log("Creating prefabs");

      CreateBasics();

      CreateEnchantmentPrefabs();
    }

    private static void CreateBasics()
    {
      Button = GameObject.Instantiate(UITool.StaticCanvas.ChildObject("ChangeVisualPCView/Window/BackToStashButton/OwlcatButton"));
    }

    private static void CreateEnchantmentPrefabs()
    {
      EnchantmentContainer = GameObject.Instantiate(
        UITool.StaticCanvas.ChildObject(
          "ServiceWindowsPCView/Background/Windows/SpellbookPCView/SpellbookScreen/MainContainer/KnownSpells"));

      Enchantment = EnchantmentContainer.GetComponent<SpellbookKnownSpellsPCView>().m_KnownSpellView;
      Enchantment.gameObject.DestroyChildren("Metamagic", "RemoveButton");
      Enchantment.gameObject.ChildObject("Level").SetActive(true);
    }
  }
}
