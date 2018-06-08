using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using UnityEngine.AI;
public class ecs : MonoBehaviour {

	
}
public class ecss : ComponentSystem
{
    struct warrior
    {
        public NavMeshAgent nmaW;
    }
    public Vector3 tmpdest;
    protected override void OnStartRunning()
    {
        tmpdest = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    protected override void OnUpdate()
    {
        foreach (var e in GetEntities<warrior>())
        {
            e.nmaW.destination = tmpdest;
        }
    }
}

