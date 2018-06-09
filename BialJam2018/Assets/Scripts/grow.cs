using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour {
    public float timer = 0,rate;
    private float das;
    public float damage;
	// Use this for initialization
	public void Start (float dis) {
        das = dis;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1) * timer * rate;
        if (this.gameObject.GetComponent<Rigidbody>().velocity.z * timer > das)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
	}
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.SendMessage("GetRekt", damage);
        Destroy(this.gameObject);
    }
}
