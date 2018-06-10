using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jebemnato : MonoBehaviour {
    public GameObject gmo;
    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            for(int i = 0; i< 6; i++)
            {

                Instantiate(gmo);
            }
        }
    }
}
