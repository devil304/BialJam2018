using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class patrzajta : MonoBehaviour {
    public Animator toto;
	// Use this for initialization
	void Start () {
        toto = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            toto.SetInteger("animka", 2);
        }else if (Input.GetKey(KeyCode.S))
        {
            toto.SetInteger("animka", 1);
        }
        else
        {
            toto.SetInteger("animka", 0);
        }
	}
}
