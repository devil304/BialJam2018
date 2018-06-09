using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour {
    private float timer = 0;
    public float rate;
    public float das1,das2;
    public float damage;
	// Use this for initialization
	public void strat (boom.lol da) {
        das1 = da.tor1;
        das2 = da.pren1;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 0) * timer * rate;
        this.transform.Translate(Vector3.forward*das2);
        if (timer > das1*2)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
	}
    private void OnCollisionEnter(Collision other)
    {
        try
        {
            other.gameObject.SendMessage("Damage", damage);
        }
        catch
        {

        }
    }
}
