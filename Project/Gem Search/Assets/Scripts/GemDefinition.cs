using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gem Search/Gem Definition")]
public class GemDefinition : ScriptableObject
{
  public int Level;
  public int Value;
  public GameObject Prefab;
  public Texture2D Icon;

  public Sprite Sprite
  {
    get 
    { 
      if (_sprite == null)
      {
        _sprite = Sprite.Create(Icon, new Rect(0, 0, Icon.width, Icon.height), new Vector2(0.5f, 0.5f));
      }

      return _sprite;
    }
  }
  
  private Sprite _sprite;
}
