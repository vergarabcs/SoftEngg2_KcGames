using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("StartNextScene", 1.0f);
	}
	
	void StartNextScene()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
