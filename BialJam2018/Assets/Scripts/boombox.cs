using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boombox : MonoBehaviour
{
    public float hp;
    protected NavMeshAgent nma;
    public Transform[] hymm;
    private Transform target;
    // Use this for initialization
    void Start()
    {
        hymm = new Transform[3];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
        hymm[2] = GameObject.FindGameObjectWithTag("Statute").transform;
        StartCoroutine(findAndKill());
    }

    // Update is called once per frame
    void Update()
    {
        nma.destination = target.position;
    }
    IEnumerator findAndKill()
    {
        Debug.Log("iksde");
        RaycastHit[] rh;
        rh = new RaycastHit[2];
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1]);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0]);
        if((2*Vector3.Distance(this.transform.position,hymm[2].position)<Vector3.Distance(this.transform.position,hymm[0].position)&& 2 * Vector3.Distance(this.transform.position, hymm[2].position) < Vector3.Distance(this.transform.position, hymm[1].position))||(rh[0].transform.tag!="Player"&& rh[1].transform.tag != "Player"))
        {
            target.position = hymm[2].position;
        }
        else if(rh[1].transform.gameObject.tag == hymm[1].gameObject.tag && rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
        {
            if (Vector3.Distance(this.transform.position, hymm[1].position) > Vector3.Distance(this.transform.position, hymm[0].position))
            {
                target.position = hymm[0].position;
            }
            else
            {
                target.position = hymm[1].position;
            }
        }
        else if(rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
        {
            target.position = hymm[0].position;
        }
        else
        {
            target.position = hymm[1].position;
        }
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(findAndKill());
    }
}
