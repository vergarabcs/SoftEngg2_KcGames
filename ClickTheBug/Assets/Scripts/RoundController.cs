using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class RoundController : MonoBehaviour {
    GameController callBack;
    public GameObject prefab;
    private float speed;
    static GameObject[] gObjects;
    public Question question;

	// Use this for initialization
	void Start () {
	}

    public void Initiate(Question question, GameController callBack)
    {
        this.callBack = callBack;
        this.question = question;

        this.speed = question.speed;

        ManualStart();
    }

    private void ManualStart()
    {
        gObjects = new GameObject[question.choices.Length];
        setQuestion(question.question);
        int gOIndex = 0;
        for(int i=0; i<question.choices.Length; i++)
        {
            float x = i;
            float y = 0.0f;
            Vector3 vec = new Vector3(x, y);
            GameObject instance = Instantiate(prefab, vec, Quaternion.identity);
            instance.transform.parent = gameObject.transform;
            print(instance.name);
            BubbleController bubbleController = (BubbleController)instance.GetComponent("BubbleController");
            bubbleController.SetText(question.choices[gOIndex]);
            gObjects[gOIndex] = instance;
            gOIndex++;
        }

        for (int i = 0; i < gObjects.Length; i++)
        {
            float x = Random.RandomRange(-5.0f, 5.0f);
            float y = Random.RandomRange(-5.0f, 5.0f);
            GameObject curr = gObjects[i];
            Rigidbody2D rb2d = curr.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(x, y) * speed);
        }

        InvokeRepeating("RandomForce", 1.0f, 1.0f);
    }

    private void setQuestion(string question)
    {
        GameObject tmP = transform.GetChild(0).gameObject;
        tmP.GetComponent<TextMeshPro>().SetText(question);
    }

    

    //store random days/months into choices. 
    

    

    void RandomForce()
    {
        for(int i=0; i<gObjects.Length; i++)
        {
            float x = Random.RandomRange(-1.0f, 1.0f);
            float y = Random.RandomRange(-1.0f, 1.0f);
            GameObject curr = gObjects[i];
            Rigidbody2D rb2d = curr.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(x, y));
        }
    }

    void Update()
    {
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Ended)
            {
                // Construct a ray from the current touch coordinates
                InputEvent(touch.position);
            }
        }
    }

    void InputEvent(Vector3 inPos)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(inPos);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit != null && hit.collider != null && hit.collider.CompareTag("Bubble"))
        {
            BubbleController bc = (BubbleController)hit.collider.gameObject.GetComponent("BubbleController");
            Destroy(gameObject);
            if (bc.text == question.answer)
            {
                Debug.Log("Correct" + hit.collider.name);
                callBack.DidAnswer(true);
            }
            else
            {
                Debug.Log("Wrong");
                callBack.DidAnswer(false);
            }
        }
    }
}
