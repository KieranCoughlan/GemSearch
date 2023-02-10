using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemFoundMessage : MonoBehaviour
{

  public Transform GemParent;
  public TMP_Text Message;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (GemParent.childCount != 1)
    {
      Message.text = "";
    }
    else
    {
      Message.text = string.Format("You found a {0} spirit gem!", GemParent.GetChild(0).gameObject.name);
    }
  }
}
