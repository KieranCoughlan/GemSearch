using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using StarterAssets;

public class GemInventory : MonoBehaviour
{
  public GemManager GemManager;
  public GameObject InventoryCanvas;
  public StarterAssetsInputs StarterAssetInputs;

  private List<GemInventoryArea> _areas;
  private bool _inventoryOpen = false;

  private void Start()
  {
    _areas = GetComponents<GemInventoryArea>().ToList();
  }

  private void Update()
  {
    GemInventoryArea craftingIn = _areas.Find(p => p.Type == GemInventoryArea.GemInventoryAreaType.CraftingInput);
    GemInventoryArea craftingOut = _areas.Find(p => p.Type == GemInventoryArea.GemInventoryAreaType.CraftingOutput);
    GemDefinition gd = GemManager.CraftingInputsMatchRecipe(craftingIn.GetSlotEnumerator());

    if (gd != null && craftingOut.Add(gd, GemInventoryArea.AddFromSource.Direct))
    {
      craftingIn.DeleteAll();
    }
  }

  public void ToggleInventory()
  {
    _inventoryOpen = !_inventoryOpen;
    bool playerHasCursor = !_inventoryOpen;

    InventoryCanvas.SetActive(_inventoryOpen);
    StarterAssetInputs.cursorLocked = playerHasCursor;
    StarterAssetInputs.cursorInputForLook = playerHasCursor;
  }

  public void Add(GemDefinition gd)
  {
    GemInventoryArea storage = _areas.Find(p => p.Type == GemInventoryArea.GemInventoryAreaType.Storage);

    if (storage != null)
      storage.Add(gd, GemInventoryArea.AddFromSource.Direct);
  }

  public void DragBetween(GemInventoryArea.GemInventoryAreaType fromType, 
                          GemInventoryArea.GemInventoryAreaType toType, 
                          GemDefinition gd)
  {
    GemInventoryArea fromArea = _areas.Find(p => p.Type == fromType);
    GemInventoryArea toArea = _areas.Find(p => p.Type == toType);

    if (fromArea == null || toArea == null)
      return;

    if (toArea.Add(gd, GemInventoryArea.AddFromSource.Dragging))
      fromArea.Remove(gd);
  }
}