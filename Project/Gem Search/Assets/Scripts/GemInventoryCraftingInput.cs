using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryCraftingInput : GemInventoryArea
{
  private const int MAX_SLOTS = 3; 

  public GemInventoryCraftingInput() : base(GemInventoryAreaType.CraftingInput, MAX_SLOTS)
  {
    
  }
}