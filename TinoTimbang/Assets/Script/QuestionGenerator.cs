
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionGenerator
{
    private static int QUESTION_LENGTH = 10;
    private DataController dc;

    public Question[] GenerateQuestions()
    {
        dc = GameObject.FindObjectOfType<DataController>();
        int[] diff = Utility.generateRandomDifficulty(QUESTION_LENGTH);
        Question[] qPool = new Question[QUESTION_LENGTH];
        for(int i=0; i<qPool.Length; i++)
        {
            qPool[i] = generateQuestion(diff[i]);
        }
        return qPool;
    }

    public Question generateQuestion(int difficulty)
    {
        Question q = new Question();
        int[] index = new int[dc.prefabs.Length - 1];
        for(int i=1; i<= index.Length; i++)
        {
            index[i - 1] = i;
        }
        Utility.reshuffle(index);
        q.prefabs = new GameObject[dc.prefabs.Length - difficulty];
        q.questions = new GameObject[difficulty];

        for(int i=0; i<difficulty; i++)
        {
            q.questions[i] = dc.prefabs[index[i]];
        }

        q.prefabs[0] = dc.prefabs[0];
        for(int i=difficulty; i<index.Length; i++)
        {
            q.prefabs[i - difficulty + 1] = dc.prefabs[index[i]];
        }
        q.difficulty = difficulty;
        return q;
    }
}
