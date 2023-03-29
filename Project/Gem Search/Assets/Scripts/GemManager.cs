using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
  public GemDefinition[] GemDefinitions;
  public GemRecipe[] GemRecipies;
  public GemDefinition RandomGem()
  {
    return GemDefinitions[Random.Range(0, GemDefinitions.Length)];
  }

  public GemDefinition CraftingInputsMatchRecipe(IEnumerable<GemDefinition> craftingInputs)
  {
    if (craftingInputs == null || craftingInputs.Count() != 3)
      return null;

    foreach (GemRecipe recipe in GemRecipies)
    {
      if (recipe.MatchesInput(craftingInputs))
        return recipe.Output;
    }

    return null;
  }
}
