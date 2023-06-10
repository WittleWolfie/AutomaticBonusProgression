using UnityEngine;
using UnityEngine.EventSystems;

namespace AutomaticBonusProgression.UI.Leveling.Legendary
{
  internal class CostToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    internal GameObject Cost;

    public void OnPointerEnter(PointerEventData eventData)
    {
      Cost?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      Cost?.SetActive(false);
    }
  }
}
