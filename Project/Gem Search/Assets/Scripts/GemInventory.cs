using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventory : MonoBehaviour
{
  public GemDefinition[] InventoryItems;
  public int[] InventoryCounts;

  private Dictionary<GemDefinition, int> _inventory;

  // Start is called before the first frame update
  void Start()
  {
    _inventory = new Dictionary<GemDefinition, int>();
  }

  public void Add(GemDefinition gd)
  {
    if (_inventory.ContainsKey(gd))
    {
      _inventory[gd] = _inventory[gd] + 1;
    }
    else
    {
      _inventory.Add(gd, 1);
    }
    
    UpdatePublicArrays();
  }

  public void Remove(GemDefinition gd)
  {
    if (_inventory.ContainsKey(gd) && _inventory[gd] > 0)
    {
      _inventory[gd] = _inventory[gd] - 1;
      UpdatePublicArrays();
    }
  }

  private void UpdatePublicArrays()
  {
    List<GemDefinition> gemDefs = new List<GemDefinition>();
    List<int> gemCounts = new List<int>();

    foreach (var kvp in _inventory)
    {
      gemDefs.Add(kvp.Key);
      gemCounts.Add(kvp.Value);
    }

    InventoryItems = gemDefs.ToArray();
    InventoryCounts = gemCounts.ToArray();
  }

}
