using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorLight : MonoBehaviour
{
  public Material MaterialOff;
  public Material MaterialOn;
  
  private bool _isOn = false;
  private MeshRenderer _mr;
  private AudioSource _as;
  private Light _bulb;

  // Start is called before the first frame update
  void Start()
  {
    _mr = GetComponent<MeshRenderer>();
    _as = GetComponent<AudioSource>();
    _bulb = GetComponentInChildren<Light>();
  }

  // Update is called once per frame
  void Update()
  {
    if (_isOn)
    {
      _mr.material = MaterialOn;
      _bulb.enabled = true;
    }
    else
    {
      _mr.material = MaterialOff;
      _bulb.enabled = false;
    }
  }

  public void TurnOn()
  {
    _as.Play();
    _isOn = true;
  }

  public void TurnOff()
  {
    _isOn = false;
  }
}
