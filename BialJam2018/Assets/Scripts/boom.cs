﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour {
    private float timer = 0;
    public Rigidbody falus;
    public bool chydysz;
    public float pren,iss;
    public struct lol
    {
        public float tor1, pren1;
    }
    lol hu;
    private void Start()
    {
        hu = new lol();
        hu.tor1 = this.gameObject.GetComponentInParent<schod>().tors;
        hu.pren1 = pren;
        iss = hu.tor1;
    }
    void Update()
    {
        if (chydysz)
        {
            if (timer > hu.tor1)
            {
                Rigidbody asd = Instantiate(falus, this.transform.position, this.transform.rotation);
                asd.transform.LookAt(this.gameObject.GetComponentInParent<boombox>().target,Vector3.up);
                asd.gameObject.SendMessage("strat", hu);
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
