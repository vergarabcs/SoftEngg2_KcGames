using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {
    Button btn;
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    public void OnClick()
    {
        print("Function1");
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick2);
    }

    public void OnClick2()
    {
        print("Function2");
    }
}
