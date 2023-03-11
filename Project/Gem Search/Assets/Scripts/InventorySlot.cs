using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
  public enum InventorySlotType { Storage, CraftingInput, CraftingOutput, Sensor }

  public InventorySlotType Type;
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

  public void SetDetails(GemDefinition gd, InventorySlotType type)
  {
    SetDetails(gd, 1, type);
  }

  public void SetDetails(GemDefinition gd, int count, InventorySlotType type)
  {
    GemDefinition = gd;
    GemCount = gd == null ? 0 : count;
    Type = type;
  }

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
        if (fromSlot.Type == InventorySlotType.Storage && Type == InventorySlotType.CraftingInput)
          _gemInventory.StorageToCraftingIn(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.Storage && Type == InventorySlotType.Sensor)
          _gemInventory.StorageToSensor(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.CraftingInput && Type == InventorySlotType.Storage)
          _gemInventory.CraftingInToStorage(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.CraftingInput && Type == InventorySlotType.Sensor)
          _gemInventory.CraftingInToSensor(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.CraftingOutput && Type == InventorySlotType.Storage)
          _gemInventory.CraftingOutToStorage(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.CraftingOutput && Type == InventorySlotType.CraftingInput)
          _gemInventory.CraftingOutToCraftingIn(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.CraftingOutput && Type == InventorySlotType.Sensor)
          _gemInventory.CraftingOutToSensor(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.Sensor && Type == InventorySlotType.Storage)
          _gemInventory.SensorToStorage(fromSlot.GemDefinition);
        else if (fromSlot.Type == InventorySlotType.Sensor && Type == InventorySlotType.CraftingInput)
          _gemInventory.SensorToCraftingIn(fromSlot.GemDefinition);
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

  public void SetInventory(GemInventory gi)
  {
    _gemInventory = gi;
  }
}
