using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  public GemInventory Inventory;

  public void OnInventory()
  {
    Inventory.ToggleInventory();
  }
}
