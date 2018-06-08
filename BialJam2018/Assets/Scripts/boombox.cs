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
        hymm = new Transform[2];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
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
        if (Physics.Raycast(this.transform.position,hymm[0].position- this.transform.position,out RaycastHit transform))
        {
            target.position = hymm[1].position;
        }
        else
        {
            target.position = hymm[0].position;
        }
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(findAndKill());
    }
}
