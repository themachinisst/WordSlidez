using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private string[] WinningWords = new string[5];
    public string WinningLetters(int LevelIndex)
    {
        //Level 1 Words
        //scritptable objects with lists
        if (LevelIndex == 1)
        {
            WinningWords[0] = "GRIN";
            WinningWords[1] = "POST";
            WinningWords[2] = "CHIN";
            WinningWords[3] = "TRAP";
            WinningWords[4] = "REEF";
        }

        //Level 2 Words
        if (LevelIndex == 2)
        {
            WinningWords[0] = "TRUE";
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
//add bg first
//count movecounta 
//add powerups in earlier stages
//check match3 games for powerups

//use json file for 'words' list
//restructure the code, make it intuitive, and avoid static values and loops
//make a doc for class~objects mindmap