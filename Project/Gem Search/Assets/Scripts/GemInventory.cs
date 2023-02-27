using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GemInventory : MonoBehaviour
{
    public InventorySlot[] InventorySlots;

    private Dictionary<GemDefinition, int> _storage;
    private List<GemDefinition> _craftingInput;
    private List<GemDefinition> _sensor;
    private GemDefinition _craftingOutput;

    // Start is called before the first frame update
    void Start()
    {
        _storage = new Dictionary<GemDefinition, int>();
        _craftingInput = new List<GemDefinition>();
        _sensor = new List<GemDefinition>();
        _craftingOutput = null;

        RegisterInventorySlots();
    }

    private void RegisterInventorySlots()
    {
        foreach(var slot in InventorySlots)
        {
            slot.SetInventory(this);
        }
    }

    public void Add(GemDefinition gd)
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

    public void Remove(GemDefinition gd)
    {
        if (_storage.ContainsKey(gd) && _storage[gd] > 0)
        {
            _storage[gd] = _storage[gd] - 1;
        }
    }

    public void MoveFromInventoryToCraftingInput(GemDefinition gd)
    {

    }

    public void MoveFromInventoryToSensor(GemDefinition gd)
    {

    }

    public void MoveFromCraftingInputToInventory(GemDefinition gd)
    {

    }

    public void MoveFromCraftingOutputToInventory(GemDefinition gd)
    {

    }

    public void MoveFromCraftingToSensor(GemDefinition gd)
    {

    }
}
