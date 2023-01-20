using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeItems : MonoBehaviour
{
  public float spacing = 1.0f;
  public float percentage = 1.0f;
  public string layerName;
  public GameObject[] prefabs;
  public GameObject[] generatedItems;
  public Vector3 size;
  public GameObject Player;

  // Start is called before the first frame update
  void Start()
  {
    Distribute();
  }

  // Update is called once per frame
  void Update()
  {
    TrackPlayer();
  }

  private void Distribute()
  {
    LayerMask lm = LayerMask.GetMask(layerName);

    // Start at position - half size in x and z and plus half size in y
    Vector3 startPosition = transform.position +
                            new Vector3(-size.x / 2.0f, size.y / 2.0f, -size.z / 2.0f);

    List<GameObject> genItems = new List<GameObject>();

    for (float xPos = 0; xPos <= size.x; xPos += spacing)
    {
      for (float zPos = 0; zPos <= size.z; zPos += spacing)
      {
        Vector3 thisPos = startPosition + new Vector3(xPos, 0, zPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(thisPos, Vector3.down, out hitInfo,
                            size.y, lm))
        {
          if (Random.value < percentage)
          {
            // Spawn a prefab
            int prefabChoice = Random.Range(0, prefabs.Length);
            GameObject go = Instantiate(prefabs[prefabChoice], hitInfo.point,
                                        prefabs[prefabChoice].transform.rotation,
                                        transform);
            genItems.Add(go);
          }
        }
      }
    }

    generatedItems = genItems.ToArray();
  }

  void OnDrawGizmos()
  {
    // Draw a yellow sphere at the transform's position
    Gizmos.color = Color.yellow;

    Gizmos.DrawWireCube(transform.position, size);
  }

  public GameObject NearestItem(Vector3 to)
  {
    if (generatedItems == null || generatedItems.Length < 1)
      return null;
  
    GameObject nearest = generatedItems[0];
    float dist = (nearest.transform.position - to).magnitude;

    for (int i = 1; i < generatedItems.Length; i++)
    {
      GameObject thisItem = generatedItems[i];
      float thisDist = (thisItem.transform.position - to).magnitude;

      if (thisDist < dist)
      {
        nearest = thisItem;
        dist = thisDist;
      }
    }

    return nearest;
  }

  public void TrackPlayer()
  {
    if (Player == null)
      return;

    GameObject nearest = NearestItem(Player.transform.position);

    for (int i = 0; i < generatedItems.Length; i++)
    {
      SetIndicator(generatedItems[i], generatedItems[i] == nearest);
    }
  }

  private void SetIndicator(GameObject go, bool isNearest)
  {
    Transform indicator = go.transform.Find("ClosestIndicator");
    if (indicator != null)
      indicator.gameObject.SetActive(isNearest);
  }
}
