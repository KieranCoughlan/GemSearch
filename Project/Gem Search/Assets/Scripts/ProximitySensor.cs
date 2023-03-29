using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
  public Transform Player;
  public Transform Target;
  public Transform NeedlePivot;
  public SensorLight DetectorLight;

  public float MaxSignalStrength = 100.0f;
  public float MinSignalStrength = 1.0f;

  public float LightOnDuration = 0.2f;
  public float LightOffDurationShort = 0.2f;
  public float LightOffDurationLong = 5.0f;

  private bool _lightIsOn = false;
  private float _lastLightChangeTime;

  // Start is called before the first frame update
  void Start()
  {
    _lastLightChangeTime = Time.time;
  }

  // Update is called once per frame
  void Update()
  {
    float adjustedSignalStrength = 0.0f;

    if (Target != null)
    {
      float distance = (Player.position - Target.position).magnitude;

      float signalStrength = MaxSignalStrength / ((distance * distance) + 0.9f);
      signalStrength = Mathf.Min(signalStrength, MaxSignalStrength);

      Vector3 playerToTarget = (Target.position - Player.position).normalized;
      float pointingTowards = Vector3.Dot(playerToTarget, Player.forward);
      pointingTowards = Mathf.Max(pointingTowards, 0.0f);

      adjustedSignalStrength = signalStrength * pointingTowards;
    }

    CalculateNeedleAngle(adjustedSignalStrength);
    CalculateBulbDelay(adjustedSignalStrength);
  }

  private void CalculateNeedleAngle(float adjustedSignalStrength)
  {
    float proportion = (adjustedSignalStrength - MinSignalStrength) /
                       (MaxSignalStrength - MinSignalStrength);
    proportion = Mathf.Clamp(proportion, 0, 1);

    float needleAngle = 180.0f * proportion;

    NeedlePivot.localRotation = Quaternion.Euler(0.0f, 0.0f, needleAngle);
  }

  private void CalculateBulbDelay(float adjustedSignalStrength)
  {
    bool lightShouldBeChanged = false;
    bool lightShouldBeOn = false;

    if (adjustedSignalStrength <= MinSignalStrength)
    {
      lightShouldBeChanged = true;
      lightShouldBeOn = false;
    }
    else
    {
      float timeSinceLastChange = Time.time - _lastLightChangeTime;

      if (_lightIsOn && (timeSinceLastChange > LightOnDuration))
      {
        lightShouldBeChanged = true;
        lightShouldBeOn = false;
      }
      else if (_lightIsOn == false)
      {
        float proportion = (adjustedSignalStrength - MinSignalStrength) /
                 (MaxSignalStrength - MinSignalStrength);
        proportion = Mathf.Clamp(proportion, 0, 1);
        float lightOffDuration = ((LightOffDurationLong - LightOffDurationShort) * (1 - proportion)) + LightOffDurationShort;

        if (timeSinceLastChange >= lightOffDuration)
        {
          lightShouldBeChanged = true;
          lightShouldBeOn = true;
        }
      }
    }

    if (lightShouldBeChanged == true)
    {
      if (_lightIsOn == true && (bool)lightShouldBeOn == false)
      {
        DetectorLight.TurnOff();
        _lightIsOn = false;
        _lastLightChangeTime = Time.time;
      }
      else if (_lightIsOn == false && (bool)lightShouldBeOn == true)
      {
        DetectorLight.TurnOn();
        _lightIsOn = true;
        _lastLightChangeTime = Time.time;
      }
    }
  }
}
