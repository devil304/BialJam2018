using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour {
	// Use this for initialization
	public void zaladuj(string naz)
    {
        SceneManager.LoadScene(naz);
    }
}
