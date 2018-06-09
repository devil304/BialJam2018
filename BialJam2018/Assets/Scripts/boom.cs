using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    private float timer = 0;
    public Rigidbody falus;
    public bool chydysz;
    public float pren;
    private float tor1;
    private void Start()
    {
        tor1 = this.gameObject.GetComponentInParent<boombox>().tor;
    }
    void Update()
    {
        if (chydysz)
        {
            if (timer > this.gameObject.GetComponentInParent<boombox>().tor)
            {
                Rigidbody asd = Instantiate(falus, this.transform.position, this.transform.rotation);
                asd.gameObject.SendMessage("strat", tor1);
                asd.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * pren, ForceMode.Impulse);
                timer = 0;
            }
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }
}
