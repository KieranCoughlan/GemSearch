using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GemInventory))]
public class GemInventoryArea : MonoBehaviour
{
  public enum GemInventoryAreaType { Storage, CraftingInput, CraftingOutput, Sensor }
  public enum AddFromSource { Dragging, Direct }

  public InventorySlot[] InventorySlots;
  
  public GemInventoryAreaType Type 
  {
    get;
    private set;
  }

  internal List<GemDefinition> _contents;
  private int? _maxContentCount;
  private GemInventory _inventory;
  

  public GemInventoryArea(GemInventoryAreaType? type = null, int? maxContentCount = null)
  {
    Type = type ?? GemInventoryAreaType.Storage;
    _maxContentCount = maxContentCount;
  }

  void Start()
  {
    _inventory = GetComponent<GemInventory>();
    _contents = new List<GemDefinition>();
    RegisterSlots();
  }

  internal void DeleteAll()
  {
    _contents.Clear();
  }

  private void RegisterSlots()
  {
    foreach (var slot in InventorySlots)
    {
      slot.RegisterWithInventory(_inventory, Type);
    }
  }

  public virtual bool Add(GemDefinition gd, AddFromSource source = AddFromSource.Dragging)
  {
    bool canAdd = CanAdd(gd, source);
    if (canAdd)
      _contents.Add(gd);

    return canAdd;
  }

  public virtual bool CanAdd(GemDefinition gd, AddFromSource source)
  {
    if (_maxContentCount == null)
      return true;

    return _contents.Count < _maxContentCount.Value;
  }
  
  public virtual bool Remove(GemDefinition gd)
  {
    return _contents.Remove(gd);
  }

  public int Count(GemDefinition gd)
  {
    return _contents.FindAll(p => p == gd).Count;
  }

  public virtual IEnumerable<GemDefinition> GetSlotEnumerator()
  {
    return _contents.AsEnumerable<GemDefinition>();
  }

  public void Update()
  {
    IEnumerable<GemDefinition> slotEnumerator = GetSlotEnumerator();
    
    // Transfer into to crafting input slots
    for (int i = 0; i < InventorySlots.Count(); i++)
    {
      GemDefinition gd = null;

      if (i < slotEnumerator.Count())
        gd = slotEnumerator.ElementAt(i);

      // Transfer this info to the slot
      InventorySlots[i].SetDetails(gd, Count(gd));
    }
  }
}