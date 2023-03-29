using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryStorage : GemInventoryArea
{
  public GemManager GemManager;
  public int Score = 0;

  public override IEnumerable<GemDefinition> GetSlotEnumerator()
  {
    return GemManager.GemDefinitions.AsEnumerable<GemDefinition>();
  }

  public override bool Add(GemDefinition gd, AddFromSource source = AddFromSource.Dragging)
  {
    Score += gd.Value;

    return base.Add(gd, source);
  }

  public override bool Remove(GemDefinition gd)
  {
    Score -= gd.Value;
    return base.Remove(gd);
  }
}