using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeItems : MonoBehaviour
{
    public float spacing = 1.0f;
    public float percentage = 1.0f;
    public string layerName;
    public GameObject[] prefabs;
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        Distribute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Distribute()
    {
        LayerMask lm = LayerMask.NameToLayer(layerName);

        // Start at position - half size in x and z and plus half size in y
        Vector3 startPosition = transform.position +
                                new Vector3(-size.x/2.0f, size.y/2.0f, -size.z/2.0f);

        for (float xPos = 0; xPos <= size.x; xPos += spacing )
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
                        Instantiate(prefabs[prefabChoice], hitInfo.point,
                                    prefabs[prefabChoice].transform.rotation,
                                    transform);
                  }
                }
            }
        }

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, size);
    }
}
