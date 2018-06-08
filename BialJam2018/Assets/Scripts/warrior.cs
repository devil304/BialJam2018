using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class warrior : MonoBehaviour {
    public float hp;
    protected NavMeshAgent nma;
    public Transform[] hymm;
    public Transform target;
	// Use this for initialization
	void Start () {
        hymm = new Transform[3];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
        hymm[2] = GameObject.FindGameObjectWithTag("Statute").transform;
        StartCoroutine(findAndKill());
    }
	
	// Update is called once per frame
	void Update () {
        nma.destination = target.position;
        
	}
    IEnumerator findAndKill()
    {
        if (Vector3.Distance(this.transform.position, hymm[1].position) < Vector3.Distance(this.transform.position, hymm[0].position))
        {
            if(2*Vector3.Distance(this.transform.position, hymm[1].position)< Vector3.Distance(this.transform.position, hymm[2].position))
            {
                target = hymm[1];
            }
            else
            {
                target = hymm[2];
            }
            
        }
        else
        {
            if (2*Vector3.Distance(this.transform.position, hymm[0].position) < Vector3.Distance(this.transform.position, hymm[2].position))
            {
                target = hymm[0];
            }
            else
            {
                target = hymm[2];
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(findAndKill());
    }
}
