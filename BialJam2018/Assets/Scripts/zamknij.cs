using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zamknij : MonoBehaviour {
    public void nara(bool nudne)
    {
        if (nudne)
        {
            Application.Quit();
        }
    }
}
