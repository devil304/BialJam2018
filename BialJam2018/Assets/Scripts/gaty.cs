using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gaty : MonoBehaviour {
    public int stan;
    public float hp;
    private float hpp;
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
            stan = 0;
            
            foreach(Transform child in this.transform)
            {
                if (child.name != "Cube.007")
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
	}
}
