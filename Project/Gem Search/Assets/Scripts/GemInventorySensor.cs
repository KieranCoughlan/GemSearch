using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventorySensor : GemInventoryArea
{
  private const int MAX_SLOTS = 4; 

  public GemInventorySensor() : base(GemInventoryAreaType.Sensor, MAX_SLOTS)
  {
    
  }

  public override bool CanAdd(GemDefinition gd, AddFromSource source)
  {
    GemDefinition sameLevel = _contents.Find(p => p.Level == gd.Level);
    return sameLevel == null;
  }
}