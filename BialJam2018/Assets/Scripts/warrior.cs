using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class warrior : MonoBehaviour {
    public float hp,tor,zas;
    protected NavMeshAgent nma;
    public Transform[] hymm;
    private float hpp, preh;
    public Transform target;
    public Object anim;
    public GameObject[] gates;
    private int rand;
	void Start ()
    {
        preh = hp;
        hpp = 0;
        hymm = new Transform[3];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
        hymm[2] = GameObject.FindGameObjectWithTag("Statute").transform;
        rand = (int)Random.value * 5;
        switch (rand)
        {
            case 1:
                gates = new GameObject[2];
                gates[0] = GameObject.Find("gate1");
                gates[1] = GameObject.Find("gate2");
                break;
            case 2:
                gates = new GameObject[3];
                gates[0] = GameObject.Find("gate6");
                gates[1] = GameObject.Find("gate5");
                gates[2] = GameObject.Find("gate4");
                break;
            default:
                gates = new GameObject[2];
                gates[0] = GameObject.Find("gate3");
                gates[1] = GameObject.Find("gate4");
                break;
        }
        StartCoroutine(findAndKill());
        
    }
    public void Kanapka(boombox.boost ho)
    {
        tor /= ho.t;
        hpp = ho.h;
    }
    void Update () {
        if (Vector3.Distance(this.transform.position, target.position) < zas)
        {
            this.transform.LookAt(target);
            nma.destination = this.transform.position;
            

        }
        else
        {
            nma.destination = target.position;
        }
        hp += hpp;
        if (hp > preh)
        {
            hp = preh;
        }
    }
    IEnumerator findAndKill()
    {
        RaycastHit[] rh;
        rh = new RaycastHit[2];
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1]);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0]);
        if ((2 * Vector3.Distance(this.transform.position, hymm[2].position) < Vector3.Distance(this.transform.position, hymm[0].position) && 2 * Vector3.Distance(this.transform.position, hymm[2].position) < Vector3.Distance(this.transform.position, hymm[1].position)) || (rh[0].transform.tag != "Player" && rh[1].transform.tag != "Player"))
        {
           // target = hymm[2];
           
        }
        else if (rh[1].transform.gameObject.tag == hymm[1].gameObject.tag && rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
        {
            if (Vector3.Distance(this.transform.position, hymm[1].position) > Vector3.Distance(this.transform.position, hymm[0].position))
            {
                target = hymm[0];
            }
            else
            {
                target = hymm[1];
            }
        }
        else if (rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
        {
            target = hymm[0];
        }
        else
        {
            target = hymm[1];
        }
        yield return new WaitForSeconds(tor);
        StartCoroutine(findAndKill());
    }
}
