using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour
{
    public GameObject LampOn;
    public GameObject LampOff;

    public void Activate()
    {
        SetLampOn(true);
    }

    public void Deactivate()
    {
        SetLampOn(false);
    }

    private void SetLampOn(bool on)
    {
        LampOn.SetActive(on);
        LampOff.SetActive(!on);
    }
}
