using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScatterer : MonoBehaviour
{
  public Vector3 size;
  public float spacing = 3.0f;
  public GameObject item;
  public float probability = 0.1f;
  public string groundLayerName = "Ground";
  public Transform player;
  public GameObject nearestItem;

  private bool[] _levelsDetected = new bool[] { true, false, false, false };
  
  // Start is called before the first frame update
  void Start()
  {
    Distribute();
  }

  // Update is called once per frame
  void Update()
  {
    FindNearest();
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireCube(transform.position, size);
  }

  private void Distribute()
  {
    Vector3 start = new Vector3(transform.position.x - size.x / 2.0f,
                                transform.position.y + size.y / 2.0f,
                                transform.position.z - size.z / 2.0f);

    LayerMask lm = LayerMask.GetMask(groundLayerName);

    for (float xOffset = 0.0f; xOffset < size.x; xOffset += spacing)
    {
      for (float zOffset = 0.0f; zOffset < size.z; zOffset += spacing)
      {
        Vector3 thisPos = start + new Vector3(xOffset, 0.0f, zOffset);

        // Check pobability
        if (Random.value < probability)
        {
          // Check for ground
          RaycastHit hit = new RaycastHit();
          if (Physics.Raycast(thisPos, Vector3.down, out hit,
                              size.y, lm))
          {
            Instantiate(item, hit.point,
                        Quaternion.identity,
                        transform);
          }
        }
      }
    }
  }

  private void FindNearest()
  {
    GameObject foundNearest = null;
    float nearestDist = 0.0f;

    for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
    {
      Transform thisChildTransform = transform.GetChild(childIndex);
      int childLevel = GetChildGemLevel(thisChildTransform);

      if (_levelsDetected[childLevel] == false)
        continue;

      float thisDistToPlayer = (player.position - thisChildTransform.position).magnitude;

      if (foundNearest == null || thisDistToPlayer < nearestDist)
      {
        foundNearest = thisChildTransform.gameObject;
        nearestDist = thisDistToPlayer;
      }
    }

    nearestItem = foundNearest;
  }

  private int GetChildGemLevel(Transform child)
  {
    RandomGem rg = child.GetComponent<RandomGem>();

    if (rg == null || rg.GemDefinition == null)
      return 0;

    return rg.GemDefinition.Level;
  }

  public void SetLevelDetected(int level, bool canDetect)
  {
    if (level > -0 && level < 4)
      _levelsDetected[level] = canDetect;
  }

}