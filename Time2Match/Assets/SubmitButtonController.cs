using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SubmitButtonController : MonoBehaviour {
    LevelController lc;
    Button btn;
    DataController dc;
    public Sprite nextSprite;

    // Use this for initialization
    void Start () {
        dc = GameObject.FindObjectOfType<DataController>();
        lc = GameObject.FindObjectOfType<LevelController>();
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
	}

    private static bool isCorrect = false;
    void OnClick()
    {
        isCorrect = lc.CheckAnswer();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick2);
        ChangeSpriteToNext();
    }

    private void ChangeSpriteToNext()
    {
        Image img = GetComponent<Image>();
        img.sprite = nextSprite;
    }

    void OnClick2()
    {
        print("onclick2:" + isCorrect);
        dc.DidAnswer(isCorrect);
    }
}
