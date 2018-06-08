using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBaker : MonoBehaviour {

    // Use this for initialization
    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
    void Update()
    {
        
    }
    void beBaked()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
             surfaces[i].BuildNavMesh();
        }

    }
}
