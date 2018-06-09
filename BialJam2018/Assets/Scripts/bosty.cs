using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosty : MonoBehaviour {

    private List<GameObject> targets = new List<GameObject>();
    boombox.boost ut;
    private void Start()
    {
        ut = this.gameObject.GetComponentInParent<boombox>().uu;
    }
    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == 9 && !targets.Contains(other.gameObject))
        {
            Debug.Log("eee");
            targets.Add(other.gameObject);
            other.gameObject.SendMessage("Kanapka_Z_Dzemem", ut);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 9 && targets.Contains(other.gameObject))
        {
            boombox.boost us;
            us = new boombox.boost();
            us.h = 0;
            us.t = 0;
            us.r = 0;
            targets.Remove(other.gameObject);
            other.gameObject.SendMessage("Kanapka_Z_Dzemem", us);
        }
    }
}
