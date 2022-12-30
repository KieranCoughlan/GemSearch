using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
  public Light bulb;
  public Transform needle;
  public GameObject target;
  public float flashDuration = 0.1f;
  public float longestDelay = 2.0f;
  public float shortestDelay = 0.1f;
  public float maxDetectionRadus = 2;
  public float minDetectionRadus = 0.5f;

  private bool isOn = false;
  private bool aboutToTurnOn = false;
  private float targetNeedleAngle = 0;
  private float needleSpeed = 1;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (bulb != null)
      bulb.enabled = isOn;

    float distanceToTarget = (transform.position - target.transform.position).magnitude;

    if (distanceToTarget < maxDetectionRadus)
    {
      // Inside radius = determine flash delay and needle angle
      float distFromMax = maxDetectionRadus - distanceToTarget;
      float detectInter = maxDetectionRadus - minDetectionRadus;
      float proportion = Mathf.Min(1.0f, distFromMax / detectInter); // Cap At 1
      float delayDelta = shortestDelay - longestDelay;
      float delay = longestDelay + (delayDelta * Mathf.Pow(proportion, 2)); // Square proportion to get nonlinear response

      targetNeedleAngle = (distFromMax/maxDetectionRadus) * 180.0f;

      if (isOn == false && aboutToTurnOn == false)
      {
        aboutToTurnOn = true;
        Invoke("TurnLightOn", delay);
      }
    }
    else
    {
      targetNeedleAngle = 0;
    }

    if (needle != null)
    {
      float actualAngle = Mathf.Lerp(needle.transform.localRotation.eulerAngles.z, targetNeedleAngle, Time.deltaTime * needleSpeed);

      needle.transform.localRotation = Quaternion.Euler(0, 0, actualAngle);
      //needle.transform.localRotation = Quaternion.Lerp(needle.transform.localRotation,
      //                                       Quaternion.Euler(0, 0, targetNeedleAngle),
      //                                       Time.deltaTime * needleSpeed); 
    }
  }

  void TurnLightOn()
  {
    Invoke("TurnLightOff", flashDuration);
    aboutToTurnOn = false;
    isOn = true;
  }

  void TurnLightOff()
  {
    isOn = false;
  }
}
