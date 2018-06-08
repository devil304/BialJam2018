using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    public Rigidbody falus;
    public bool chydysz;
    public float pren;
    IEnumerator jep()
    {
        Instantiate(falus);
        falus.SendMessage("Start", this.gameObject.GetComponentInParent<boombox>().range);
        falus.gameObject.GetComponent<Rigidbody>().velocity=Vector3.forward*pren;
        yield return new WaitForSeconds(this.gameObject.GetComponentInParent<boombox>().tor);
        if (chydysz)
        {
            StartCoroutine(jep());
        }
    }
}
