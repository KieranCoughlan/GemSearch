using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDig : MonoBehaviour
{
  public string GroundLayer = "Ground";
  public GameObject DiggingPrefab;
  public ItemScatterer ItemScatterer;
  public GameObject GemFoundPrefab;
  public Transform GemFoundLocation;

  public float MaxDistToItem = 0.35f;

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
      Instantiate(DiggingPrefab, hitInfo.point, DiggingPrefab.transform.rotation);
      Invoke("DigResult", 3);
    }
  }

  private void DigResult()
  {
    float distToNearest = (ItemScatterer.nearestItem.transform.position - transform.position).magnitude;

    if (distToNearest > MaxDistToItem)
      return;

    // Capture the gem definition
    RandomGem rg = ItemScatterer.nearestItem.GetComponent<RandomGem>();
    GemDefinition gd = rg.GemDefinition;

    // Remove the item from ItemScatterer
    Destroy(ItemScatterer.nearestItem);

    // TODO: Add to our inventory

    // Spawn the Gem Found Prefab to let the player know what they found
    GameObject gfb = Instantiate(GemFoundPrefab, GemFoundLocation);
    GemFoundMessage gfm = gfb.GetComponent<GemFoundMessage>();
    GameObject gem = Instantiate(gd.Prefab, gfm.GemParent);
    gem.name = gd.name;
  }
}
