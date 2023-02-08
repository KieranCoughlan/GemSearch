using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gem Search/Gem Definition")]
public class GemDefinition : ScriptableObject
{
    public int Level;
    public int Value;
    public GameObject Prefab;
    public Sprite Icon;
}
