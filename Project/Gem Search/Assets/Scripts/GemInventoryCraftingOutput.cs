using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryCraftingOutput: GemInventoryArea
{
  private const int MAX_SLOTS = 1; 

  public GemInventoryCraftingOutput() : base(GemInventoryAreaType.CraftingOutput, MAX_SLOTS)
  {
    
  }

  public override bool CanAdd(GemDefinition gd, AddFromSource source)
  {
    if (source == AddFromSource.Dragging)
      return false;

    return base.CanAdd(gd, source);
  }
}