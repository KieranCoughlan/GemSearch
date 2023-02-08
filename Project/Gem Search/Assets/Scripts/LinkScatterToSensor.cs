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
        if (ItemScatter.nearestItem != null)
        {
            ProximitySensor.Target = ItemScatter.nearestItem.transform;
        }

    }
}
