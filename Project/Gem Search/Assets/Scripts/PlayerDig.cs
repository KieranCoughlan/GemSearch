using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDig : MonoBehaviour
{
  public string GroundLayer = "Ground";
  public GameObject DiggingPrefab;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
        
  }

  public void OnDig(InputValue input)
  {
    Debug.Log("Dig");

    Vector3 rayPos = transform.position + (transform.forward * 2) + (Vector3.up * 2);
    LayerMask lm = LayerMask.GetMask(GroundLayer);
    RaycastHit hitInfo = new RaycastHit();
    if (Physics.Raycast(rayPos, Vector3.down, out hitInfo, 10.0f, lm))
    {
      Instantiate(DiggingPrefab, hitInfo.point, Quaternion.identity);
    }
  }
}
