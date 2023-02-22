using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
  private Canvas _canvas;
  private RectTransform _transform;
  private CanvasGroup _canvasGroup;
  private GameObject _draggingIcon;

  private void Start()
  {
    _canvas = GetComponentInParent<Canvas>();
    _transform = GetComponent<RectTransform>();
    _canvasGroup = GetComponent<CanvasGroup>();
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    Debug.Log("Begin drag");

    _draggingIcon = new GameObject("Icon");
    _draggingIcon.transform.SetParent(_canvas.transform, false);
    _draggingIcon.transform.SetAsLastSibling();

    var image = _draggingIcon.AddComponent<Image>();

    image.sprite = GetComponent<Image>().sprite;
    //image.SetNativeSize();

    _canvasGroup = _draggingIcon.AddComponent<CanvasGroup>();
    _canvasGroup.alpha = 0.75f;
    _canvasGroup.blocksRaycasts = false;

    SetDraggedPosition(eventData);
  }

  private void SetDraggedPosition(PointerEventData eventData)
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

  public void OnDrag(PointerEventData eventData)
  {
    Debug.Log("On drag");
    SetDraggedPosition(eventData);
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    Debug.Log("End drag");
    Destroy(_draggingIcon);
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    Debug.Log("Pointer Down");
  }
}
