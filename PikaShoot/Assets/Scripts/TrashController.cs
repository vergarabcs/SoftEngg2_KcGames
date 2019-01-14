using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashController : MonoBehaviour {

    public string text;
    private DataController dc;
	// Use this for initialization
	void Start () {
        dc = FindObjectOfType<DataController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetText(string v)
    {
        GameObject tmP = transform.GetChild(0).gameObject;
        tmP.GetComponent<TextMeshPro>().SetText(v);
        this.text = v;
    }

    private Collider collider;
    private bool isFirstTime = true;
    private void OnTriggerEnter(Collider other)
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            collider = other;
            Invoke("CheckAndNext", 1.0f);
        }
    }

    void CheckAndNext()
    {
        this.isFirstTime = true;
        Collider other = collider;
        if (other.CompareTag("Player"))
        {
            Question q = dc.questionPool[dc.questionIndex];
            if (q.answer == this.text)
            {
                dc.DidAnswer(true);
            }
            else
            {
                dc.DidAnswer(false);
            }
        }
    }
}
