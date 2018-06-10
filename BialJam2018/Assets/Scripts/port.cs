using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class port : MonoBehaviour {
    public float hp;
	// Use this for initialization
	void Start () {
		
	}
    void Damage(float dmg)
    {
        hp -= dmg;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
