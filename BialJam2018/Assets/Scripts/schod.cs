using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schod : MonoBehaviour {
    public float tors;
	// Use this for initialization
	void Start () {
        tors = this.gameObject.GetComponentInParent<boombox>().tor;	
	}
}
