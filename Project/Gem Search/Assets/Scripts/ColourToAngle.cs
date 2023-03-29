using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ColourToAngle : MonoBehaviour
{
  public Gradient Colour;
  public Light Sun;
  public float Value;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    Value = Mathf.InverseLerp(0, 180, Sun.transform.eulerAngles.x);
    Color colour = Colour.Evaluate(Value);
    Sun.color = colour;
  }
}
