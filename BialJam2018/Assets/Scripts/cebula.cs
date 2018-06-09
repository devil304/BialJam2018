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
    public Rigidbody rb;
    public Transform transsexualista;
    public RaycastHit[] rh;
    public float shootforce;
    private void Awake()
    {
        hymm = new Transform[3];
        rh = new RaycastHit[2];
    }
    void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < 2; i++)
        {
            hymm[i] = temp[i].transform;
        }
        hymm[2] = GameObject.FindGameObjectWithTag("Statute").transform;
        target = hymm[2];
        StartCoroutine(findAndKill());
        StartCoroutine(shootAndKill());
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);

    }
    IEnumerator shootAndKill()
    {
        RaycastHit rh2;
        Debug.Log("preshot");
        Physics.Raycast(this.transform.position, target.position - this.transform.position, out rh2, Vector3.Distance(this.transform.position, target.position) + 1, 9);
        if ((rh2.transform.gameObject.tag == "Statute"||rh2.transform.gameObject.tag == "Player") && Vector3.Distance(this.transform.position, target.position) < range)
        {
            this.transform.LookAt(target);
            nma.destination = this.transform.position;
            Rigidbody tmp = Instantiate(rb, transsexualista.position, transsexualista.rotation);
            tmp.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward*shootforce,ForceMode.Impulse);
            yield return new WaitForSeconds(tor);
            StartCoroutine(shootAndKill());
            Debug.Log("postshot");
        }
        else
        {
            nma.destination = target.position;
            yield return new WaitForSeconds(tor);
            StartCoroutine(shootAndKill());
            Debug.Log("postnoshot");
        } 
    }
    IEnumerator findAndKill()
    {  
        Physics.Raycast(this.transform.position, hymm[1].position - this.transform.position, out rh[1], Vector3.Distance(this.transform.position, hymm[1].position) + 1, 9);
        Physics.Raycast(this.transform.position, hymm[0].position - this.transform.position, out rh[0], Vector3.Distance(this.transform.position, hymm[0].position) + 1, 9);
        if ((2 * Vector3.Distance(this.transform.position, hymm[2].position) + range < Vector3.Distance(this.transform.position, hymm[0].position) + range && 2 * Vector3.Distance(this.transform.position, hymm[2].position) < Vector3.Distance(this.transform.position, hymm[1].position)) || (rh[0].transform.tag != "Player" && rh[1].transform.tag != "Player"))
        {
            target = hymm[2];
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
