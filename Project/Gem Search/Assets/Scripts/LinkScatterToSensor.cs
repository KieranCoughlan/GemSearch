using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkScatterToSensor : MonoBehaviour
{
    public ItemScatterer ItemScatter;
    public ProximitySensor ProximitySensor;

    // Update is called once per frame
    void Update()
    {
        ProximitySensor.Target = ItemScatter.nearestItem != null ? ItemScatter.nearestItem.transform : null;
    }
}
