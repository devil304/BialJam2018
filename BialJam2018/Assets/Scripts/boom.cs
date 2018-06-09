using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    public Rigidbody falus;
    public bool chydysz;
    public float pren;
    void Update()
    {

    }
        public IEnumerator jep()
    {
        Rigidbody fas = Instantiate(falus,this.transform.position,this.transform.rotation);
        fas.SendMessage("Start", this.gameObject.GetComponentInParent<boombox>().range);
        fas.gameObject.GetComponent<Rigidbody>().velocity=Vector3.forward*pren;
        yield return new WaitForSeconds(this.gameObject.GetComponentInParent<boombox>().tor);
        if (chydysz)
        {
            StartCoroutine(jep());
        }
    }
}
