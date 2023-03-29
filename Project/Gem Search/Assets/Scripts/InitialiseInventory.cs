using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseInventory : MonoBehaviour
{
  public GemInventory GemInventory;
  public GemManager GemManager;
  public int NumberOfGems;

  // Start is called before the first frame update
  void Start()
  {
    Invoke("AddRandomGem", 1.0f);
  }

  private void AddRandomGem()
  {
    GemInventory.Add(GemManager.RandomGem());
    NumberOfGems--;

    if (NumberOfGems > 0)
      Invoke("AddRandomGem", 1.0f);
  }

}
