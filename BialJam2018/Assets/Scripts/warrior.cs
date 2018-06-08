using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class warrior : MonoBehaviour {
    public float hp;
    protected NavMeshAgent nma;
	// Use this for initialization
	void Start () {
        nma = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        nma.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
}
