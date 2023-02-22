using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
public class LayoutElementMatchSize : MonoBehaviour
{
  public enum MatchDimension { width, height }

  public RectTransform MatchItem;
  public MatchDimension Dimension;

  private LayoutElement _layoutElement;

  // Start is called before the first frame update
  void Start()
  {
    _layoutElement = GetComponent<LayoutElement>();
  }

  // Update is called once per frame
  void Update()
  {
    if (MatchItem == null || _layoutElement == null)
      return;

    if (Dimension == MatchDimension.width)
      _layoutElement.preferredWidth = MatchItem.rect.width;
    else
      _layoutElement.preferredHeight = MatchItem.rect.height;
  }
}
