using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gem Search/Gem Recipe")]
public class GemRecipe : ScriptableObject
{
  public GemDefinition Input0;
  public GemDefinition Input1;
  public GemDefinition Input2;

  public GemDefinition Output;

  public bool MatchesInput(IEnumerable<GemDefinition> inputs)
  {
    return inputs.Contains<GemDefinition>(Input0) && 
           inputs.Contains<GemDefinition>(Input1) && 
           inputs.Contains<GemDefinition>(Input2);
  }
}
