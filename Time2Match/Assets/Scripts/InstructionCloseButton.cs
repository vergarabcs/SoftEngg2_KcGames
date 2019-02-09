using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionCloseButton : MonoBehaviour {
    public void StartLevelScene()
    {
        SceneManager.LoadScene("LevelScene");
    }
}
