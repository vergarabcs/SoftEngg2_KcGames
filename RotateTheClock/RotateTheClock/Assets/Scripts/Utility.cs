using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class Utility
{
    public static void reshuffle<T>(T[] arr)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < arr.Length; t++)
        {
            T tmp = arr[t];
            int r = Random.Range(t, arr.Length);
            arr[t] = arr[r];
            arr[r] = tmp;
        }
    }

    public static DateTime randomTime()
    {
        //48 15 minutes in one 12 hour cycle
        int offset = Random.Range(0, 48) * 15;
        DateTime dt = new DateTime();
        return dt.AddMinutes(offset);
    }

    public static string timeToString(int hour, int minute)
    {
        return string.Format("{0}:{1}", (hour == 0) ? "12" : hour.ToString()
            , (minute == 0) ? "00" : minute.ToString());
    }

    public static string timeToString(DateTime dt)
    {
        return timeToString(dt.Hour, dt.Minute);
    }

    public static int[] generateRandomDifficulty(int length)
    {
        List<int> difficulties = new List<int>();
        float easy = 0.5f;
        float average = 0.3f;
        float diff = 0.2f;
        for (int i = 0; i < length*easy; i++)
        {
            difficulties.Add(2);
        }
        for (int i = 0; i < length*average; i++)
        {
            difficulties.Add(3);
        }
        for (int i = 0; i < length*diff; i++)
        {
            difficulties.Add(4);
        }
        int[] diffArr = difficulties.ToArray();
        reshuffle<int>(diffArr);
        return diffArr;
    }

}