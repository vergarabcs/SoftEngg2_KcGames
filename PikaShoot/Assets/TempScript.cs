using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempScript : MonoBehaviour {
   
	void Start () {
     
	}

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            Reload();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
