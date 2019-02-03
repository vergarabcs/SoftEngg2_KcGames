
using System;

public class QuestionGenerator {
    private static int QUESTION_LENGTH = 10;
    public Question[] GenerateQuestions()
    {
        Question[] qPool = new Question[QUESTION_LENGTH];
        int[] diffArr = Utility.generateRandomDifficulty(QUESTION_LENGTH);
        for (int i = 0; i < qPool.Length; i++)
        {
            qPool[i] = generateQuestion(diffArr[i]);
        }
        return qPool;
    }

    public Question generateQuestion(int difficulty)
    {
        Question q = new Question();
        q.clockHour = Utility.randomHour();
        q.textHour = Utility.randomHour();
        q.clockMin = Utility.randomMinute();
        q.textMin = Utility.randomMinute();
        q.difficulty = difficulty;

        if (difficulty == 0)
        {
            q.clockHour = q.textHour;
        }else if(difficulty == 1)
        {
            q.clockMin = q.textMin;
        }
        return q;
    }
}
