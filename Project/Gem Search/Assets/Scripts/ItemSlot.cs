using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
  public void OnDrop(PointerEventData eventData)
  {
    Debug.Log("OnDrop");
    if (eventData.pointerDrag != null)
    {
      RectTransform itemRt = eventData.pointerDrag.GetComponent<RectTransform>();
      RectTransform slotRt = GetComponent<RectTransform>();
      itemRt.SetParent(slotRt, false);
      itemRt.anchoredPosition = Vector3.zero;
    }
  }
}
