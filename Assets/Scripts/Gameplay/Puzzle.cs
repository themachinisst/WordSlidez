using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{

    public NumberBox boxPrefab;

    public NumberBox[,] boxes = new NumberBox[4, 4];

    public Sprite[] letterSprites;

    int[] letterPos;

    int currentLetterAscii;

    int[] letterAscii;

    string WinningWord;

    public int currentLevel = 1;

    int WordCounter = 0;

    public Sprite[] gridSprites;

    public Sprite[] gridCellSprites;

    public string spriteNamePrefix = "Sprites/spritesheet_";

    public float timeLeft = 60.0f;

    public TMP_Text showTimer;
    public TMP_Text showTempGameStatus;

    void Start()    
    {

        gridCellSprites = new Sprite[16];
        letterAscii = new int[16];
        letterPos = new int[4];

        gridCellSprites = DefineWinningArr(letterSprites);
        Init();
        for (int i = 0; i < 999; i++)
            Shuffle();
    }

    public Sprite[] DefineWinningArr(Sprite[] letterSprites)
    {

        Levels Level = new Levels();
        WinningWord = Level.WinningLetters(currentLevel);
        Debug.Log(WinningWord);

        for (int i = 0; i < 4; i++) {   
            currentLetterAscii = (int)WinningWord[i];
            //ascii for small a is 97
            letterAscii[i] = currentLetterAscii - 97;
        }

        for (var i = 0; i < 4; i++)
            letterPos[i] = Random.Range(0,14);

        for (int i=0; i<15; i++)
        {
            if (Array.Exists(letterPos, element => element == i))
            {

                gridCellSprites[i] = letterSprites[letterAscii[WordCounter]];
                WordCounter++;
            }
            else
            {
                gridCellSprites[i] = letterSprites[26];
            }
        }
        gridCellSprites[15] = letterSprites[27];

        return gridCellSprites;
    }


    //To check winning status
    void CheckWinCond()
    {
        for (var i = 0; i < 4; i++) { 
            Debug.Log(letterSprites[letterAscii[i]]);
        }


        Debug.Log(boxes[0, 3]);
        foreach(Sprite arr in gridCellSprites)
        {
            //Debug.Log(arr);
        }
    }


    void Init()
    {

        int n = 0;
        for (int y = 3; y >= 0; y--)
            for (int x = 0; x < 4; x++)
            {
                if (n<16) { 
                    NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                    box.Init(x, y, n + 1, gridCellSprites[n], ClickToSwap);
                    boxes[x, y] = box;
                    n++;
                }
            }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        Swap(x, y, dx, dy);

    }

    void Swap(int x, int y, int dx, int dy)
    {
        var from = boxes[x, y];
        var target = boxes[x+dx, y+dy];

        //swap this 2 boxes
        boxes[x, y] = target;
        boxes[x+dx, y+dy] = from;

        //update pos 2 boxes
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int getDx(int x, int y)
    {
        //if right is empty
        if (x < 3 && boxes[x + 1, y].IsEmpty())
            return 1;

        //if left is empty
        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;

        return 0;
    }

    int getDy(int x, int y)
    {
        //if top is empty
        if (y < 3 && boxes[x, y + 1].IsEmpty())
            return 1;

        //if bottom is empty
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;

        return 0;
    }

    void Shuffle()
    {
        for(int i=0;i<4;i++)
            for(int j = 0; j < 4; j++)
            {
                if (boxes[i, j].IsEmpty())
                {
                    Vector2 pos = GetValidMove(i, j);
                    Swap(i, j, (int)pos.x, (int)pos.y);
                }
            }
    }

    private Vector2 lastMove;


    Vector2 GetValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();
        do {
            int n = Random.Range(0, 4);
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2)
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(IsValidRange(x + (int)pos.x) && IsValidRange(y + (int)pos.y)) || IsRepeatMove(pos));

        lastMove = pos;
        return pos;
    }

    bool IsValidRange(int n)
    {
        return n>=0 && n<=3;
    }


    bool IsRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        showTimer.text = (timeLeft).ToString("0");
    }
}
