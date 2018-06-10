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
    int rand;
    public GameObject asdas, asdas1;
    List<GameObject> targets = new List<GameObject>();
    Animator anim;
    NavMeshPath nmp;
    bool lookat = false;
    public struct boost
    {
        public float r, t, h;
    }
    public boost uu;
    public void Damage(float dmg)
    {
        Debug.Log("i dostał");
        hp -= dmg;
    }
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
        Debug.Log("przed");
        target = hymm[2];
        Debug.Log("po"+hymm[2].name);
        
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
        StartCoroutine(findAndKill());
        StartCoroutine(shootAndKill());
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
        hp += hpp;
        if (hp > preh)
        {
            hp = preh;
        }
        if (lookat)
        {

            this.transform.LookAt(target);
        }
    }
    IEnumerator shootAndKill()
    {
        RaycastHit rh2;
        Debug.Log("preshot");
        Physics.Raycast(this.transform.position + new Vector3(0, 2, 0), Vector3.forward, out rh2, LoS);
        Debug.DrawRay(this.transform.position + new Vector3(0, 2, 0), Vector3.forward, Color.blue, LoS);
        Debug.Log("preshot2");
        if ((rh2.transform.gameObject.tag == "Statute" || rh2.transform.gameObject.tag == "Player" || rh2.transform.gameObject.tag == "gate") && Vector3.Distance(this.transform.position, target.position) < range)
        {
            Debug.Log("wszedl do shotu");
            this.gameObject.GetComponent<NavMeshAgent>().speed = 6;
            anim.SetInteger("aminc", 2);
            lookat = true;
            nma.isStopped = true;
            for (int i = 0; i < 2; i++)
            {
                asdas.GetComponent<boom>().chydysz = true;
                asdas1.GetComponent<boom>().chydysz = true;
            }
            yield return new WaitForSeconds(tor);
            StartCoroutine(shootAndKill());
            Debug.Log("postshot");
        }
        else
        {
            Debug.Log("nie wszedl do shotu");
            this.gameObject.GetComponent<NavMeshAgent>().speed = 2;
            if (this.gameObject.GetComponent<NavMeshAgent>().pathStatus == NavMeshPathStatus.PathComplete)
            {
                this.gameObject.GetComponent<Animator>().SetInteger("aminc", 1);
            }
            else
            {
                this.gameObject.GetComponent<Animator>().SetInteger("aminc", 0);
            }
            lookat = false;
            nma.CalculatePath(target.position, nmp);
            nma.path = nmp;
            for (int i = 0; i < 2; i++)
            {
                asdas.GetComponent<boom>().chydysz = false;
                asdas1.GetComponent<boom>().chydysz = false;
            }
            yield return new WaitForSeconds(tor);
            StartCoroutine(shootAndKill());
            Debug.Log("postnoshot");
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
            if (rh[1].transform.gameObject.tag == "Player" && nma.CalculatePath(hymm[1].position, nma.path) && rh[0].transform.gameObject.tag == "Player" && nma.CalculatePath(hymm[0].position, nma.path))
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
            else if (rh[0].transform.gameObject.tag == "Player")
            {
                target = hymm[0];
            }
            else if (rh[1].transform.gameObject.tag == "Player")
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
