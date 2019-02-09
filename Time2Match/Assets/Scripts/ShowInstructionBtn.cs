using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructionBtn : MonoBehaviour {
    public GameObject panuto;
    private void Start()
    {
        panuto.active = false;
    }

    public void ShowInstruction()
    {
        
        panuto.active = true;
    }

    public void HideInstruction()
    {
        panuto.active = false;
    }
}
