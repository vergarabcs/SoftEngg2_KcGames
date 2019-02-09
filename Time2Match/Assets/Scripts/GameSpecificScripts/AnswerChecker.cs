using System;
using System.Collections.Generic;

public class AnswerChecker
{
    LevelController lc;
    SceneInitializer sceneOwner;
    Dictionary<ClockController, ChoiceController> matchMade = new Dictionary<ClockController, ChoiceController>();

    public AnswerChecker(LevelController lc, SceneInitializer sceneOwner)
    {
        this.lc = lc;
        this.sceneOwner = sceneOwner;
    }

    internal bool CheckAnswer()
    {
        if (matchMade.Count != sceneOwner.clockControllers.Count)
        {
            return false;
        }
        foreach (ClockController cc1 in matchMade.Keys)
        {
            ChoiceController cc2 = matchMade[cc1];
            if (!(cc1.hour == cc2.hour && cc1.minute == cc2.minute))
            {
                return false;
            }
        }
        return true;
    }

    internal void ShowCorrectAnswer()
    {
        StoreCorrectMappingToDict();

        foreach (ClockController clockC in matchMade.Keys)
        {
            ChoiceController choiceC = matchMade[clockC];
            clockC.line.enabled = true;
            clockC.line.SetPosition(0, clockC.radioButton.position);
            clockC.line.SetPosition(1, choiceC.radioButton.position);
        }
        //answerFeedback.text = "CORRECT!";
        ShowMarks();
    }

    public void ShowMarks()
    {
        ClearMarks();
        foreach (ClockController clockC in sceneOwner.clockControllers)
        {
            if (matchMade.ContainsKey(clockC))
            {
                ChoiceController choiceC = matchMade[clockC];
                bool isCorrect = clockC.hour == choiceC.hour && clockC.minute == choiceC.minute;
                choiceC.showMark(isCorrect);
                clockC.showMark(isCorrect);
            }
            else
            {
                clockC.showMark(false);
            }
        }
    }

    private void ClearMarks()
    {
        foreach (ClockController clockC in sceneOwner.clockControllers)
        {
            clockC.ClearMark();
        }
        foreach (ChoiceController choiceC in sceneOwner.choiceControllers)
        {
            choiceC.ClearMark();
        }
    }

    private void StoreCorrectMappingToDict()
    {
        matchMade.Clear();
        foreach (ClockController clockC in sceneOwner.clockControllers)
        {
            foreach (ChoiceController choiceC in sceneOwner.choiceControllers)
            {
                if (clockC.hour == choiceC.hour && clockC.minute == choiceC.minute)
                    Match(choiceC, clockC);
            }
        }
    }

    internal void Match(ChoiceController cc, ClockController clockController)
    {
        if (matchMade.ContainsKey(clockController))
        {
            matchMade[clockController] = cc;
        }
        else
        {
            matchMade.Add(clockController, cc);
        }
    }

    internal void unMatch(ClockController clockController)
    {
        this.matchMade.Remove(clockController);
    }
}