using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cebula : MonoBehaviour
{
    public float hp;
    protected NavMeshAgent nma;
    public Transform[] hymm; 
    private Transform target;
    public float range;
    public float tor;
    private float hpp, preh;
    public Rigidbody rb;
    public Transform transsexualista;
    public RaycastHit[] rh;
    public GameObject[] gates;
    public float shootforce;
    public int LoS;
    bool lookat=false;
    int rand;
    private void Awake()
    {
        hymm = new Transform[3];
        rh = new RaycastHit[2];
    }
    void Start()
    {
        preh = hp;
        hpp = 0;
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 2; i++)
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
        target = gates[0].transform;
        StartCoroutine(findAndKill());
        StartCoroutine(shootAndKill());
    }
    private void Update()
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
    public void Kanapka(boombox.boost ho)
    {
        range += ho.r;
        tor /= ho.t;
        hpp = ho.h;
    }
    IEnumerator shootAndKill()
    {
        RaycastHit rh2;
        Debug.Log("preshot");
        Physics.Raycast(this.transform.position, target.position - this.transform.position, out rh2, Vector3.Distance(this.transform.position, target.position) + 1, 9);
        if ((rh2.transform.gameObject.tag == "Statute"||rh2.transform.gameObject.tag == "Player") && Vector3.Distance(this.transform.position, target.position) < range)
        {
            lookat = true;
            nma.destination = this.transform.position;
            Rigidbody tmp = Instantiate(rb, transsexualista.position, transsexualista.rotation);
            tmp.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward*shootforce,ForceMode.Impulse);
            yield return new WaitForSeconds(tor);
            StartCoroutine(shootAndKill());
            Debug.Log("postshot");
        }
        else
        {
            lookat = false;
            nma.destination = target.position;
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
        if (Vector3.Distance(this.transform.position, hymm[0].position) < LoS-range || Vector3.Distance(this.transform.position, hymm[1].position) < LoS-range)
        {
            // target = hymm[2];

            if (rh[1].transform.gameObject.tag == "Player"  && rh[0].transform.gameObject.tag == "Player")
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
}
