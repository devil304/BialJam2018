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
        falus.AddForce(Vector3.forward*pren,ForceMode.Impulse);
        yield return new WaitForSeconds(this.gameObject.GetComponentInParent<boombox>().tor);
        if (chydysz)
        { StartCoroutine(jep());

        }
    }
}
