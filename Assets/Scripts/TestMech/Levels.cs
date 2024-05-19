using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private string[] WinningWords = new string[5];

    public string WinningLetters(int LevelIndex)
    {
        //Level 1 Words
        if (LevelIndex == 1)
        {
            WinningWords[0] = "time";
            WinningWords[1] = "last";
            WinningWords[2] = "stop";
            WinningWords[3] = "drop";
            WinningWords[4] = "over";
        }

        //Level 2 Words
        if (LevelIndex == 2)
        {
            WinningWords[0] = "true";
            WinningWords[1] = "cart";
            WinningWords[2] = "post";
            WinningWords[3] = "cool";
            WinningWords[4] = "trap";
        }

        //Level 3 Words

        if (LevelIndex == 3)
        {
            WinningWords[0] = "band";
            WinningWords[1] = "cast";
            WinningWords[2] = "cost";
            WinningWords[3] = "port";
            WinningWords[4] = "fret";
        }

        return WinningWords[Random.Range(0, 4)];
    }
}
