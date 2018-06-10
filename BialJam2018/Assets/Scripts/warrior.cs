using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class warrior : MonoBehaviour {
    public float hp,tor,zas;
    protected NavMeshAgent nma;
    public Transform[] hymm;
    private float hpp, preh;
    public Transform target;
    public Animator anim;
    public int LoS;
    int rand;
    public int DD;
    public RaycastHit rh2;
    GameObject[] gates;
    NavMeshPath nmp;
    void Start ()
    {
        preh = hp;
        nmp = new NavMeshPath();
        hpp = 0;
        hymm = new Transform[3];
        anim = this.GetComponent<Animator>();
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
                Debug.Log("1");
                gates[0] = GameObject.Find("gate1");
                gates[1] = GameObject.Find("gate2");
                break;
            case 2:
                Debug.Log("2");
                gates = new GameObject[3];
                gates[0] = GameObject.Find("gate6");
                gates[1] = GameObject.Find("gate5");
                gates[2] = GameObject.Find("gate4");
                break;
            default:
                Debug.Log("3-5");
                gates = new GameObject[2];
                gates[0] = GameObject.Find("gate3");
                gates[1] = GameObject.Find("gate4");
                break;
        }
        target = gates[0].transform;
        StartCoroutine(findAndKill());
        
    }
    public void Kanapka(boombox.boost ho)
    {
        tor /= ho.t;
        hpp = ho.h;
    }
    void Update () {
        if (nma.path == null)
        {
            anim.SetInteger("controller", 0);
        }
        if (Vector3.Distance(this.transform.position, target.position) < zas)
        {
            this.transform.LookAt(target);
            if(nma.destination != this.transform.position)
                nma.destination = this.transform.position;
            anim.SetInteger("controller", 2);
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                float prog = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;
                //Debug.Log("precheck");
                if (prog > 0.53 && prog < 0.55)
                {
                    Debug.Log("pre pcast");
                    Color x;
                    x = Color.blue;
                    Debug.DrawRay(this.transform.position + new Vector3(0, 2, 0), Vector3.forward, x, LoS);
                    Physics.Raycast(this.transform.position+new Vector3(0,2,0), Vector3.forward,out rh2,LoS);
                    if(rh2.transform.gameObject.tag == "Player")
                    {
                        Debug.Log("gracz");
                        rh2.transform.gameObject.SendMessage("Damage", 1 * DD);
                    }
                    else if(rh2.transform.gameObject.tag == "gate"){
                        Debug.Log("gate");
                        rh2.transform.gameObject.SendMessage("Damage", 2 * DD);
                    }
                }
            }

        }
        else
        {
            anim.SetInteger("controller", 1);
            if (nma.destination != target.position)
            {
                bool x = nma.CalculatePath(target.position, nmp);
                Debug.Log(x);
                nma.path= nmp;

            }
        }
        hp += hpp;
        if (hp > preh)
        {
            hp = preh;
        }
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void Damage(float dmg)
    {
        hp -= dmg;
    }
    IEnumerator findAndKill()
    {
        RaycastHit[] rh;
        rh = new RaycastHit[2];
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1]);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0]);
        if (Vector3.Distance(this.transform.position, hymm[0].position) < LoS  || Vector3.Distance(this.transform.position, hymm[1].position) < LoS )
        {
            if (rh[1].transform.gameObject.tag == "Player"&&nma.CalculatePath(hymm[1].position,nma.path) && rh[0].transform.gameObject.tag == "Player"&& nma.CalculatePath(hymm[0].position, nma.path))
            {
                if (Vector3.Distance(this.transform.position, hymm[1].position) > Vector3.Distance(this.transform.position, hymm[0].position))
                {
                    Debug.Log("t=g1");
                    target = hymm[0];
                }
                else
                {
                    Debug.Log("t=g2");
                    target = hymm[1];
                }
            }
            else if (rh[0].transform.gameObject.tag == hymm[0].gameObject.tag && nma.CalculatePath(hymm[0].position, nma.path))
            {
                Debug.Log("t=g1");
                target = hymm[0];
            }
            else if (rh[1].transform.gameObject.tag == hymm[1].gameObject.tag && nma.CalculatePath(hymm[1].position, nma.path))
            {
                Debug.Log("t=g2");
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
                        Debug.Log("t=portal");
                        target = hymm[2];
                    }
                    else
                    {
                        Debug.Log("t=gates[2]");
                        target = gates[2].transform;
                    }
                }
                else
                {
                    Debug.Log("t=portal");
                    target = hymm[2];
                }
            }
            else
            {
                Debug.Log("t=gates[1]");
                target = gates[1].transform;
            }
        }
        else
        {
            Debug.Log("t=gates[0]");
            target = gates[0].transform;
        }
        yield return new WaitForSeconds(tor);
        StartCoroutine(findAndKill());
    }
}
