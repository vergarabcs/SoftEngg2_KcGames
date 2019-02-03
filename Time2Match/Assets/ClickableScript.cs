using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableScript : MonoBehaviour {
    private void OnMouseDown()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
