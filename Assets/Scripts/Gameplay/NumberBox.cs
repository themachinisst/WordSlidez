using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberBox : MonoBehaviour
{

    public int index = 0;
    int x = 0;
    int y = 0;

    public UIActions UIActions;
    public Puzzle puzzle;

    public TimerBar timerBar;

    private Action<int, int, string, bool> swapFunc = null;

    public int correctlyPlaced = 0;
    public void Start()
    {
        // Ensure UIActions is assigned, either via the Inspector or dynamically
        if (UIActions == null)
        {
            UIActions = FindObjectOfType<UIActions>();
            if (UIActions == null)
            {
                Debug.LogError("UIActions component not found in the scene!");
            }
        }
    }

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int, string, bool> swapFunc)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j - 1, false);
        this.swapFunc = swapFunc;
    }

    public void UpdatePos(int i, int j, bool swapFlag)
    {
        x = i;
        y = j;
            
        //this.gameObject.transform.localPosition = new Vector2(i, j);
        StartCoroutine(Move(swapFlag));
    }

    IEnumerator Move(bool swapFlag)
    {
        float elapsedTime = 0;
        float duration = 0.2f;
        Vector2 start = this.gameObject.transform.localPosition;
        Vector2 end = new Vector2(x, y);

        while(elapsedTime < duration)
        {
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, (elapsedTime/duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.transform.localPosition = end;
        CheckAllLettersPos(swapFlag, 0);
    }

    public int CheckAllLettersPos(bool swapFlag, int endTimer)
    {

        GameObject FirstLetter = GameObject.Find("Cell0");
        GameObject SecondLetter = GameObject.Find("Cell1");
        GameObject ThirdLetter = GameObject.Find("Cell2");
        GameObject FourthLetter = GameObject.Find("Cell3");

        puzzle = new Puzzle();

        if (swapFlag) { 
            if (FirstLetter.transform.position.x == 0 && FirstLetter.transform.position.y == 1)
                correctlyPlaced += 1;
            if (FirstLetter.transform.position.x == 1 && FirstLetter.transform.position.y == 1)
                correctlyPlaced += 1;
            if (FirstLetter.transform.position.x == 2 && FirstLetter.transform.position.y == 1)
                correctlyPlaced += 1;
            if (FirstLetter.transform.position.x == 3 && FirstLetter.transform.position.y == 1)
                correctlyPlaced += 1;
        }

        if (swapFlag && FirstLetter.transform.position.x == 0 && FirstLetter.transform.position.y == 1
            && SecondLetter.transform.position.x == 1 && SecondLetter.transform.position.y == 1
            && ThirdLetter.transform.position.x == 2 && ThirdLetter.transform.position.y == 1
            && FourthLetter.transform.position.x == 3 && FourthLetter.transform.position.y == 1
            )
            //|| endTimer == 1)
        {   
            
            UIActions.ShowPopup(correctlyPlaced);
        }


        return correctlyPlaced;

    }

    public bool IsEmpty()
    {
        return index == 16;
    }

    void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0) && swapFunc != null) 
        {
            swapFunc(x, y, name, true);
            // puzzle.CheckWinCond(x, y, name);
        }    
    }

}
