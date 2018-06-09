using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cebul_fall : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(this.gameObject.GetComponent<Rigidbody>().velocity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.transform.SetParent(collision.gameObject.transform);

        try
        {
           collision.gameObject.SendMessage("Damage", 1);
        }
        catch
        {

        }
        Destroy(this.gameObject, 5);
    }
}
