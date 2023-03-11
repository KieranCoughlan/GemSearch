using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemInventory : MonoBehaviour
{
  public GemManager GemManager;
  public InventorySlot[] InventorySlots;

  public InventorySlot[] UIStorageSlots;
  public InventorySlot[] UICraftingInputSlots;
  public InventorySlot UICraftingOutputSlot;
  public InventorySlot[] UISensorSlots;

  private Dictionary<GemDefinition, int> _storage;
  private List<GemDefinition> _craftingInput;
  private List<GemDefinition> _sensor;
  private GemDefinition _craftingOutput;

  private const int CRAFTING_SLOTS_NUM = 3;
  private const int SENSOR_SLOTS_NUM = 4;

  // Start is called before the first frame update
  void Start()
  {
    _storage = new Dictionary<GemDefinition, int>();
    _craftingInput = new List<GemDefinition>();
    _sensor = new List<GemDefinition>();
    _craftingOutput = null;

    RegisterInventorySlots();
  }

  private void Update()
  {
    UpdateStorageSlots();
    UpdateCraftingSlots();
    UpdateSensorSlots();
  }

  private void UpdateStorageSlots()
  {
    for (int i = 0; i < GemManager.GemDefinitions.Length; i++)
    {
      // Get the gem definition
      GemDefinition gd = GemManager.GemDefinitions[i];

      // Look to see how many we have
      int gemCount = 0;
      if (_storage.ContainsKey(gd))
        gemCount = _storage[gd];

      // Transfer this info to the slot
      UIStorageSlots[i].SetDetails(gd, gemCount, InventorySlot.InventorySlotType.Storage);
    }
  }

  private void UpdateCraftingSlots()
  {
    // Transfer into to crafting input slots
    for (int i = 0; i < CRAFTING_SLOTS_NUM; i++)
    {
      GemDefinition gd = null;

      if (i < _craftingInput.Count)
        gd = _craftingInput[i];

      // Transfer this info to the slot
      UICraftingInputSlots[i].SetDetails(gd, InventorySlot.InventorySlotType.CraftingInput);
    }

    // Transfer to output slots
    UICraftingOutputSlot.SetDetails(_craftingOutput, InventorySlot.InventorySlotType.CraftingOutput);
    
    // TODO: Do crafting if we've three slots filled, a matching recipe and nothing in the output slot
  }

  private void UpdateSensorSlots()
  {
    // Transfer into to crafting input slots
    for (int i = 0; i < SENSOR_SLOTS_NUM; i++)
    {
      GemDefinition gd = null;

      if (i < _sensor.Count)
        gd = _sensor[i];

      // Transfer this info to the slot
      UISensorSlots[i].SetDetails(gd, InventorySlot.InventorySlotType.Sensor);
    }
  }

  private void RegisterInventorySlots()
  {
    List<InventorySlot> allSlots = new List<InventorySlot>();
    allSlots.AddRange(UIStorageSlots);
    allSlots.AddRange(UICraftingInputSlots);
    allSlots.Add(UICraftingOutputSlot);
    allSlots.AddRange(UISensorSlots);

    foreach (var slot in allSlots)
      slot.SetInventory(this);
  }

  public void AddStorage(GemDefinition gd)
  {
    if (_storage.ContainsKey(gd))
    {
      _storage[gd] = _storage[gd] + 1;
    }
    else
    {
      _storage.Add(gd, 1);
    }
  }

  public void RemoveStorage(GemDefinition gd)
  {
    if (_storage.ContainsKey(gd) && _storage[gd] > 0)
    {
      _storage[gd] = _storage[gd] - 1;
    }
  }

  private bool AddCraftingIn(GemDefinition gd)
  {
    // Can't add the same gem twice, can't have more than three
    if (_craftingInput.Contains(gd) == false && _craftingInput.Count < CRAFTING_SLOTS_NUM)
    {
      _craftingInput.Add(gd);
      return true;
    }

    return false;
  }

  private void RemoveCraftingIn(GemDefinition gd)
  {
    _craftingInput.Remove(gd);
  }

  private bool AddCraftingOut(GemDefinition gd)
  {
    if (_craftingOutput == null)
    {
      _craftingOutput = gd;
      return true;
    }

    return false;
  }

  private void RemoveCraftingOut()
  {
    _craftingOutput = null;
  }

  private bool AddSensor(GemDefinition gd)
  {
    // Do we have a gem of the same level? If so, remove it and put it to storage
    GemDefinition sameLevel = _sensor.Find(p => p.Level == gd.Level);
    if (sameLevel != null)
    {
      RemoveSensor(sameLevel);
      AddStorage(sameLevel);
    }

    // Can only have four total
    if (_sensor.Count < SENSOR_SLOTS_NUM - 1)
    {
      _sensor.Add(gd);
      return true;
    }

    return false;
  }

  private void RemoveSensor(GemDefinition gd)
  {
    _sensor.Remove(gd);
  }

  public void StorageToCraftingIn(GemDefinition gd)
  {
    if (AddCraftingIn(gd))
      RemoveStorage(gd);
  }

  public void StorageToSensor(GemDefinition gd)
  {
    if (AddSensor(gd))
      RemoveStorage(gd);
  }

  public void CraftingInToStorage(GemDefinition gd)
  {
    RemoveCraftingIn(gd);
    AddStorage(gd);
  }

  public void CraftingInToSensor(GemDefinition gd)
  {
    if (AddSensor(gd))
      RemoveCraftingIn(gd);
  }

  public void CraftingOutToStorage(GemDefinition gd)
  {
    RemoveCraftingOut();
    AddStorage(gd);
  }

  public void CraftingOutToCraftingIn(GemDefinition gd)
  {
    if (AddCraftingIn(gd))
      RemoveCraftingOut();
  }
  
  public void CraftingOutToSensor(GemDefinition gd)
  {
    if (AddSensor(gd))  
      RemoveCraftingOut();
  }

  public void SensorToStorage(GemDefinition gd)
  {
    RemoveSensor(gd);
    AddStorage(gd);
  }

  public void SensorToCraftingIn(GemDefinition gd)
  {
    if (AddCraftingIn(gd))
      RemoveSensor(gd);
  }
}
