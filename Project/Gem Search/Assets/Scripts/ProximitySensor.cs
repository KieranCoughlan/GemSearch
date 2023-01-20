using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
  public Light bulb;
  public AudioSource beep;
  public Transform needle;
  public DistributeItems distributor;
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

    if (distributor == null)
      return;

    GameObject target = distributor.NearestItem(transform.position);

    if (target == null)
      return;

    Vector3 offsetToTarget = transform.position - target.transform.position;

    float distanceToTarget = Vector3.Dot(transform.forward, offsetToTarget);

    if (distanceToTarget > 0 && distanceToTarget < maxDetectionRadus)
    {
      // Inside radius = determine flash delay and needle angle
      float distFromMax = maxDetectionRadus - distanceToTarget;
      float detectInter = maxDetectionRadus - minDetectionRadus;
      float proportion = Mathf.Min(1.0f, distFromMax / detectInter); // Cap At 1
      float delayDelta = shortestDelay - longestDelay;
      float delay = longestDelay + (delayDelta * Mathf.Pow(proportion, 2)); // Square proportion to get nonlinear response

      targetNeedleAngle = proportion * 180.0f;

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
    }
  }

  void TurnLightOn()
  {
    Invoke("TurnLightOff", flashDuration);
    aboutToTurnOn = false;
    isOn = true;

    if (beep != null && beep.isPlaying == false)
      beep.Play();
  }

  void TurnLightOff()
  {
    isOn = false;
  }

  private float MapRange(float input, float inputRangeStart, float inputRangeEnd, 
                                      float outputRangeStart, float outputRangeEnd, int power = 1)
  {
    float inputAdjusted = input - inputRangeStart;
    float inputRange = inputRangeEnd - inputRangeStart;
    float proportion = Mathf.Clamp(inputAdjusted / inputRange, 0.0f, 1.0f);
    


    return 0.0f;
  }
}
