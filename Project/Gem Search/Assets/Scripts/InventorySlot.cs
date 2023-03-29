using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
  public GemDefinition GemDefinition;
  public int GemCount;

  public Image SlotImage;
  public GameObject CountTextBadge;
  public TMP_Text CountText;

  public Vector2 DragIconSize = new Vector2(40, 40);

  private Canvas _canvas;
  private GameObject _draggingIcon;
  private bool _isDragging;

  private GemInventory _gemInventory;

  private GemInventoryArea.GemInventoryAreaType _type;

  // Start is called before the first frame update
  void Start()
  {
    _canvas = GetComponentInParent<Canvas>();
    _isDragging = false;
  }

  void Update()
  {
    if (GemDefinition == null || GemCount < 1)
    {
      SlotImage.sprite = null;
    }
    else
    {
      SlotImage.sprite = GemDefinition.Sprite;
    }

    CountTextBadge.SetActive(GemDefinition != null && GemCount > 1);
    CountText.text = GemCount.ToString();
  }

  public void RegisterWithInventory(GemInventory inventory, GemInventoryArea.GemInventoryAreaType type)
  {
    _gemInventory = inventory;
    _type = type;
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    if (GemDefinition != null && GemCount > 0)
    {
      _draggingIcon = new GameObject("Icon");
      _draggingIcon.transform.SetParent(_canvas.transform, false);
      _draggingIcon.transform.SetAsLastSibling();

      var image = _draggingIcon.AddComponent<Image>();

      image.sprite = GemDefinition.Sprite;
      image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, DragIconSize.x);
      image.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, DragIconSize.y);
      image.GetComponent<RectTransform>().ForceUpdateRectTransforms();

      CanvasGroup canvasGroup = _draggingIcon.AddComponent<CanvasGroup>();
      canvasGroup.alpha = 0.75f;
      canvasGroup.blocksRaycasts = false;

      _isDragging = true;

      OnDrag(eventData);
    }
  }

  public void OnDrag(PointerEventData eventData)
  {
    if (_isDragging && _draggingIcon != null)
    {
      var rt = _draggingIcon.GetComponent<RectTransform>();
      Vector3 globalMousePos;
      if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas.GetComponent<RectTransform>(),
                                                                  eventData.position,
                                                                  eventData.pressEventCamera,
                                                                  out globalMousePos))
      {
        rt.position = globalMousePos;
      }
    }
  }

  public void OnDrop(PointerEventData eventData)
  {
    if (eventData.pointerDrag != null)
    {
      InventorySlot fromSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

      if (fromSlot != null && fromSlot._isDragging)
      {
        _gemInventory.DragBetween(fromSlot._type, _type, fromSlot.GemDefinition);
      }
    }
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    if (_isDragging && _draggingIcon != null)
    {
      Destroy(_draggingIcon);
    }
  }

  public void SetDetails(GemDefinition gd, int count)
  {
    GemDefinition = gd;
    GemCount = gd == null ? 0 : count;
  }
}
