using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour {
    private float timer = 0;
    public float rate;
    public float das1;
    public float damage;
	// Use this for initialization
<<<<<<< HEAD
	public void Start () {
=======
	public void strat (float da) {
        das1 = da;
>>>>>>> ff40f1023d1bb92abda7a35f5adcc870f0df2d36
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 0) * timer * rate;
        if (timer > das1*2)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
	}
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag!="pass") {
            other.gameObject.SendMessage("GetRekt", damage);
        }
    }
}
