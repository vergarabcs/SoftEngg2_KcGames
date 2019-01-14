
using System;

public class QuestionGenerator {
    private static int QUESTION_LENGTH = 10;
    public Question[] GenerateQuestions()
    {
        Question[] qPool = new Question[10];
        int[] diffArr = Utility.generateRandomDifficulty(10);
        for (int i = 0; i < qPool.Length; i++)
        {
            qPool[i] = generateQuestion(diffArr[i]);
        }
        return qPool;
    }

    public Question generateQuestion(int length)
    {
        Question.takenTime.Clear();
        Question q = new Question();
        q.clocks = new DateTime[length];
        for (int i = 0; i < q.clocks.Length; i++)
        {

            DateTime curr = Utility.randomTime();
            while (Question.takenTime.Contains(Utility.timeToString(curr)))
            {
                curr = Utility.randomTime();
            }
            q.clocks[i] = curr;
            Question.takenTime.Add(Utility.timeToString(curr));
        }
        return q;
    }
}
