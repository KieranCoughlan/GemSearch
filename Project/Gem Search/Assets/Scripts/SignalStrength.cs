using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignalStrength : MonoBehaviour
{
  public GameObject Target;
  public GameObject Player;
  public TMP_Text signalStrengthTxt;
  public TMP_Text pointingTowardsTxt;
  public TMP_Text adjustedSignalStrengthTxt;
  public TMP_Text needleAngleTxt;
  public TMP_Text flashRateTxt;
  public TMP_Text lightOnTxt;
  public Light bulb;
  public Transform indicator;
  public float maxSignalStrength = 100.0f;
  public float minSignalStrength = 2.0f;
  public bool lightOn = false;
  public bool lightBusy = false;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Vector3 playerToTarget = Target.transform.position - Player.transform.position;
    float playerToTargetDist = playerToTarget.magnitude;
    float signalStrength = maxSignalStrength / (Mathf.Pow(playerToTargetDist, 2) + 1);

    float pointingTowards = Mathf.Max(0, Vector3.Dot(Player.transform.forward, playerToTarget.normalized));
    float adjustedSignalStrength = signalStrength * pointingTowards;
    float needleAngle = CalculateNeedleAngle(adjustedSignalStrength);
    float nextFlash = CalculateNextFlashTime(adjustedSignalStrength);

    if (nextFlash < 2.1f && lightBusy == false)
    {
      lightBusy = true;
      lightOn = true;
      Invoke("LightOff", 0.2f);
      Invoke("LightNotBusy", nextFlash + 0.2f);
    }

    bulb.enabled = lightOn;
    indicator.localRotation = Quaternion.Euler(0, 0, needleAngle);

    signalStrengthTxt.text = "Signal Strength: " + signalStrength.ToString();
    pointingTowardsTxt.text = "Pointing Towards: " + pointingTowards.ToString();
    adjustedSignalStrengthTxt.text = "Adjusted Signal Strength: " + adjustedSignalStrength.ToString();
    needleAngleTxt.text = "Needle Angle: " + needleAngle.ToString();
    flashRateTxt.text = "Flash Rate: " + nextFlash.ToString();
    lightOnTxt.text = "Light: " + (lightOn ? "On" : "Off");
  }

  private float CalculateNeedleAngle(float signalStrength)
  {
    float proportion = (signalStrength - minSignalStrength) / (maxSignalStrength - minSignalStrength);
    proportion = Mathf.Clamp(proportion, 0, 1);

    return 180.0f * proportion;
  }

  private float CalculateNextFlashTime(float signalStrength)
  {
    float proportion = (signalStrength - minSignalStrength) / (maxSignalStrength - minSignalStrength);
    proportion = Mathf.Clamp(proportion, 0, 1);

    return (2.0f * (1 - proportion)) + 0.1f;
  }

  private void LightOff()
  {
    lightOn = false;
  }

  private void LightNotBusy()
  {
    lightBusy = false;
  }
}
