using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boombox : MonoBehaviour
{
    public float hp;
    private NavMeshAgent nma;
    private Transform[] hymm;
    private Transform target;
    public float range,tor,pr,pt,ph;
    bool dod;
    private float hpp;
    List<GameObject> targets = new List<GameObject>();
    public struct boost
    {
        public float r, t, h;
    }
    public boost uu;
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
        target = hymm[2];
        uu = new boost();
        uu.h = ph;
        uu.r = pr;
        uu.t = pt;
        hpp = 0;
        this.gameObject.GetComponent<SphereCollider>().radius = range;
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
                this.transform.GetChild(i).GetComponent<boom>().chydysz = true;
            }
            nma.destination = this.transform.position;
            
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                this.transform.GetChild(i).GetComponent<boom>().chydysz = false;
            }
            nma.destination = target.position;
        }
        hp += hpp;
        if (hp > 100)
        {
            hp = 100;
        }
    }
    IEnumerator findAndKill()
    {
        RaycastHit[] rh;
        rh = new RaycastHit[2];
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1], Vector3.Distance(this.transform.position, hymm[1].position)+1, 9);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0], Vector3.Distance(this.transform.position, hymm[0].position)+1, 9);
        if((2*Vector3.Distance(this.transform.position,hymm[2].position)+range<Vector3.Distance(this.transform.position,hymm[0].position)+range&& 2 * Vector3.Distance(this.transform.position, hymm[2].position) < Vector3.Distance(this.transform.position, hymm[1].position))||(rh[0].transform.tag!="Player"&& rh[1].transform.tag != "Player"))
        {
            target = hymm[2];
        }
        else if(rh[1].transform.gameObject.tag == hymm[1].gameObject.tag && rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
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
        else if(rh[0].transform.gameObject.tag == hymm[0].gameObject.tag)
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
            boost bost1;
            bost1 = new boost();
            targets.Remove(other.gameObject);
            other.SendMessage("Kanapka", bost1);
        }
    }
}
