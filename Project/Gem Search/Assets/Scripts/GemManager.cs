using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
  public GemDefinition[] GemDefinitions;

  public GemDefinition RandomGem()
  {
    return GemDefinitions[Random.Range(0, GemDefinitions.Length)];
  }
}
