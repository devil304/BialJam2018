using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class boombox : MonoBehaviour
{
    public float hp;
    private NavMeshAgent nma;
    private Transform[] hymm;
    public Transform target;
    public float range,tor,pr,pt,ph;
    bool dod;
    private float hpp,preh;
    public GameObject[] gates;
    public int LoS;
    public GameObject asdas, asdas1;
    int rand;
    List<GameObject> targets = new List<GameObject>();
    public struct boost
    {
        public float r, t, h;
    }
    public boost uu;
    // Use this for initialization
    void Start()
    {
        preh = hp;
        hymm = new Transform[3];
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
        hymm[2] = GameObject.FindGameObjectWithTag("Statute").transform;
        StartCoroutine(findAndKill());
        target = hymm[2];
        this.gameObject.GetComponentInChildren<SphereCollider>().radius = range;
        uu = new boost();
        uu.h = ph;
        uu.r = pr;
        uu.t = pt;
        hpp = 0;
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
        target = gates[0].transform;
    }
    public void Kanapka(boombox.boost ho)
    {
        range += ho.r;
        tor /= ho.t;
        hpp = ho.h;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rh;
        Physics.Raycast(this.transform.position, target.position - this.transform.position, out rh,Vector3.Distance(this.transform.position, target.position)+1,9);
        if (rh.transform.gameObject.tag==target.gameObject.tag && Vector3.Distance(this.transform.position, target.position)<range)
        {
            this.transform.LookAt(target);
            for (int i=0;i<2;i++)
            {
                asdas.GetComponent<boom>().chydysz = true;
                asdas1.GetComponent<boom>().chydysz = true;
            }
            nma.destination = this.transform.position;
            
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                asdas.GetComponent<boom>().chydysz = false;
                asdas1.GetComponent<boom>().chydysz = false;
            }
            nma.destination = target.position;
        }
        hp += hpp;
        if (hp > preh)
        {
            hp = preh;
        }
        if (Vector3.Distance(this.transform.position, target.transform.position) < 4 * range)
        {
            this.gameObject.GetComponent<Animator>().SetInteger("aminac", 2);
            this.gameObject.GetComponent<NavMeshAgent>().speed = 6;
        }
        else if (this.GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
        {
            this.gameObject.GetComponent<Animator>().SetInteger("aminac", 1);
            this.gameObject.GetComponent<NavMeshAgent>().speed=3.5f;
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetInteger("aminac", 0);
        }
    }
    IEnumerator findAndKill()
    {
        RaycastHit[] rh;
        rh = new RaycastHit[2];
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1]);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0]);
        if (Vector3.Distance(this.transform.position, hymm[0].position) < LoS - range || Vector3.Distance(this.transform.position, hymm[1].position) < LoS - range)
        {
            if (rh[1].transform.gameObject.tag == "Player" && rh[0].transform.gameObject.tag == "Player")
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
            else if (rh[0].transform.gameObject.tag == hymm[0].gameObject.tag && nma.CalculatePath(hymm[1].position, nma.path))
            {
                target = hymm[1];
            }
        }
        else if (gates[0].GetComponent<gaty>().stan == 0)
        {
            if (gates[1].GetComponent<gaty>().stan == 0)
            {
                if (gates.Length == 3)
                {
                    if (gates[2].GetComponent<gaty>().stan == 0)
                    {
                        target = hymm[2];
                    }
                    else
                    {
                        target = gates[2].transform;
                    }
                }
                else
                {
                    target = hymm[2];
                }
            }
            else
            {
                target = gates[1].transform;
            }
        }
        else
        {
            target = gates[0].transform;
        }
        yield return new WaitForSeconds(tor);
        StartCoroutine(findAndKill());
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9 && !targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
            other.SendMessage("Kanapka", uu);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && targets.Contains(other.gameObject))
        {
            boost us;
            us = new boost();
            us.r = -uu.r;
            us.h = 0;
            us.t = 1/uu.t;
            targets.Remove(other.gameObject);
            other.SendMessage("Kanapka", us);
        }
    }
}
