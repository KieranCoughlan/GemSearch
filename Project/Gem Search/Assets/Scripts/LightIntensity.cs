using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour
{
  public Transform target;
  public Light pointLight;

  void Update()
  {
    // Get the distance between the game object and the target transform
    float distance = Vector3.Distance(transform.position, target.position);

    // Calculate the flash rate based on the square of the distance
    float flashRate = 0.1f/((distance * distance) + 1);

    // Set the point light's intensity to flash at the calculated rate
    pointLight.intensity = 100 * Mathf.PingPong(Time.time * flashRate, 1);
  }
}
