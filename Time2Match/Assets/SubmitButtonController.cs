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
    public AudioSource source;

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
        PlayAudio(isCorrect);
        btn.onClick.RemoveAllListeners();
        if (isCorrect)
        {
            btn.onClick.AddListener(OnClick2);
        }
        else
        {
            btn.onClick.AddListener(OnClick3);
        }
        ChangeSpriteToNext();
    }

    private void PlayAudio(bool isCorrect)
    {
        if (isCorrect)
        {
            source.Play();
        }
    }

    //onclick3 is used only if wrong answer
    private void OnClick3()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick2);
        lc.CorrectAnimation();
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
