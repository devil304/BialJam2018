using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float hp,hpp;

	// Use this for initialization
	void Start () {
        hpp = hp;
	}
    void Damage(float dmg)
    {
        hp -= dmg;
    }

	
	// Update is called once per frame
	void Update () {
        if (hp < 0)
        {
            
        }
	}
}
