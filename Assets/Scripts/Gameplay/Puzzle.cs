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
using UnityEngine.XR;
using Unity.Burst.Intrinsics;
using System.Linq;

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

    public int[] WinnindWordArr;

    public UIActions UIActions;

    public Transform gridParentTransform;
    private int correctLetterCount;

    public string sortingLayerName = "Grid"; // Name of the sorting layer for the grid
    public int sortingOrder = 10; // Sorting order for the grid elements

    public int currentProg = 0;
    void Start()
    {  

        // Ensure the parent transform has a sorting layer and order if applicable
        SpriteRenderer parentSpriteRenderer = gridParentTransform.GetComponent<SpriteRenderer>();
        if (parentSpriteRenderer != null)
        {
            parentSpriteRenderer.sortingLayerName = sortingLayerName;
            parentSpriteRenderer.sortingOrder = sortingOrder;
        }

        gridCellSprites = new Sprite[16];
        letterAscii = new int[16];
        letterPos = new int[4];
        WinnindWordArr = new int[4];
        // Ensure UIActions is assigned, either via the Inspector or dynamically
        if (UIActions == null)
        {
            UIActions = FindObjectOfType<UIActions>();
            if (UIActions == null)
            {
                Debug.LogError("UIActions component not found in the scene!");
            }
        }
        gridCellSprites = DefineWinningArr(letterSprites);
        Init();
        for (int i = 0; i < 999; i++)
            Shuffle();
    }

    //Define levels word and add it to the sprite arr 
    public Sprite[] DefineWinningArr(Sprite[] letterSprites)
    {

        Levels Level = new Levels();
        WinningWord = Level.WinningLetters(currentLevel);
        Debug.Log(WinningWord);

        for (int i = 0; i < 4; i++) {   
            currentLetterAscii = (int)WinningWord[i];
            //ascii for small a is 65//97
            letterAscii[i] = currentLetterAscii - 65;
        }

        for (int i=0; i<15; i++)
        {
            if (i<4)
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
    public int CheckWinCond(int x, int y, string name)
    {
        var WinnindWordArrSum = WinnindWordArr.Sum(itm => itm);
        var correctLetterCount = 0;

        if (WinnindWordArrSum<5) { 
            if (x == 0 && y == 3 && name == "Cell0")
            {
                UIActions.UpdateWinningWordLabel(0, WinningWord[0].ToString());
                //Debug.Log(WinningWord[0]);
                WinnindWordArr[0] = 1;
                correctLetterCount += 1;
            }
            if (x == 1 && y == 3 && name == "Cell1")
            {   
                UIActions.UpdateWinningWordLabel(1, WinningWord[1].ToString());
                ///Debug.Log(WinningWord[1].ToString());
                WinnindWordArr[1] = 1;
                correctLetterCount += 1;
            }
            if (x == 2 && y == 3 && name == "Cell2")
            {
                UIActions.UpdateWinningWordLabel(2, WinningWord[2].ToString());
                //Debug.Log(WinningWord[2]);
                WinnindWordArr[2] = 1;
                correctLetterCount += 1;
            }
            if (x == 3 && y == 3 && name == "Cell3")
            {
                UIActions.UpdateWinningWordLabel(3, WinningWord[3].ToString());
               //Debug.Log(WinningWord[3]);
                WinnindWordArr[3] = 1;
                correctLetterCount += 1;
            }
        }

        return correctLetterCount ;
    }


    void Init()
    {

        int n = 0;
        for (int y = 3; y >= 0; y--)
            for (int x = 0; x < 4; x++)
            {
                if (n<16) { 
                    NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                    box.transform.SetParent(gridParentTransform, false);
                    // Set the sorting layer and order for each box
                    SpriteRenderer boxSpriteRenderer = box.GetComponent<SpriteRenderer>();
                    if (boxSpriteRenderer != null)
                    {
                        boxSpriteRenderer.sortingLayerName = sortingLayerName;
                        boxSpriteRenderer.sortingOrder = sortingOrder;
                    }
                    box.name = "Cell"+ n;
                    box.Init(x, y, n + 1, gridCellSprites[n], ClickToSwap);
                    boxes[x, y] = box;
                    n++;

                }
            }
    }

    void ClickToSwap(int x, int y, string objName, bool swapFlag)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        Swap(x, y, dx, dy, objName, swapFlag);

    }

    void Swap(int x, int y, int dx, int dy, string objName, bool swapFlag)
    {
            var from = boxes[x, y];
            var target = boxes[x + dx, y + dy];

            //swap this 2 boxes
            boxes[x, y] = target;
            boxes[x + dx, y + dy] = from;

            //update pos 2 boxes
            from.UpdatePos(x + dx, y + dy, swapFlag);
            target.UpdatePos(x, y, swapFlag);
            if (swapFlag)
            {
                currentProg = CheckWinCond(x + dx, y + dy, objName);
            }
            
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
                    Swap(i, j, (int)pos.x, (int)pos.y, "", (bool)false);
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

}
