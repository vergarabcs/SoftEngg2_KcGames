using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenController : MonoBehaviour
{
    public GameObject menu;
    public GameObject panuto;
    DataController dc;

    private void Start()
    {
        dc = GameObject.FindObjectOfType<DataController>();
        panuto.active = false;
        menu.active = true;
    }

    public void DisplayInstruction()
    {
        menu.active = false;
        panuto.active = true;
    }

    public void StartGame()
    {
        DisplayInstruction();
        dc.isPractice = false;
    }

    public void StartPractice()
    {
        DisplayInstruction();
        dc.isPractice = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
