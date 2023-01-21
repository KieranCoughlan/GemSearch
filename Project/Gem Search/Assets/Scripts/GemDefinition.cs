using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gem Search/Gem Definition")]
public class GemDefinition : ScriptableObject
{
  public GameObject prefab;
  public int level;
  public int value;
  public Sprite icon;
}
