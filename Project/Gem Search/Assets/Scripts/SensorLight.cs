using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLight : MonoBehaviour
{
    public Material MaterialOn;
    public Material MaterialOff;

    public bool IsOn = false;

    private MeshRenderer _mr;
    private Light _bulb;
    private AudioSource _as;

    // Start is called before the first frame update
    void Start()
    {
        _mr = GetComponent<MeshRenderer>();
        _bulb = GetComponentInChildren<Light>();
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TurnOn()
    {
        _bulb.enabled = true;
        _mr.material = MaterialOn;
        _as.Play();
    }

    public void TurnOff()
    {
        _bulb.enabled = false;
        _mr.material = MaterialOff;
    }
}
