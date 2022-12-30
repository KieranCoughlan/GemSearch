using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeItems : MonoBehaviour
{
    public float borderWidth;
    public float spacing;
    public float percentage;
    public string layerName;
    public GameObject prefab;
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
        // Find mesh extents
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Debug.Log(transform.position);
        Debug.Log(mr.bounds.center);
        Debug.Log(mr.bounds.extents);

        LayerMask lm = LayerMask.NameToLayer(layerName);

        //for (float )
        //Physics.Raycast()
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, size);
    }
}
