using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour {
    private float timer = 0;
    public float rate;
    public float das1,das2;
    public float damage;
	// Use this for initialization
<<<<<<< HEAD
	public void strat (float dis) {
        das = dis;
=======
	public void strat (float da) {
        das1 = da;
>>>>>>> e6637f33ef8b3aee3c7c1dd145af0e51102b40e8
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
