using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    public bool chydysz;
    IEnumerator jep()
    {

        yield return new WaitForSeconds(this.gameObject.GetComponentInParent<boombox>().tor);
        if (chydysz)
        { StartCoroutine(jep());

        }
    }
}
