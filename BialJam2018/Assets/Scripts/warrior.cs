using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class warrior : MonoBehaviour {
    public float hp;
    protected NavMeshAgent nma;
    public Transform[] hymm;
	// Use this for initialization
	void Start () {
        hymm = new Transform[2];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
    }
	
	// Update is called once per frame
	void Update () {

        
	}
    IEnumerator findAndKill()
    {
        if (Vector3.Distance(this.transform.position, hymm[1].position) < Vector3.Distance(this.transform.position, hymm[0].position))
        {
            nma.destination = hymm[1].position;
        }
        else
        {
            nma.destination = hymm[0].position;
        }
        yield return new WaitForSeconds(5);
    }
}
