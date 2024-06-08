using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class UIActions : MonoBehaviour
{
    public TMP_Text FirstWinningWordLetter;
    public TMP_Text SecondWinningWordLetter;
    public TMP_Text ThirdWinningWordLetter;
    public TMP_Text FourthWinningWordLetter;
    string WinningWordsChar;
    public GameObject LvlCompletePopup;
    public TMP_Text LvlCompletePopupText;

    public GameObject GridParent;

    public SpriteRenderer StarOne;
    public SpriteRenderer StarTwo;
    public SpriteRenderer StarThree;

    public Sprite emptyStar;
    public Sprite completeStar;

    // Start is called before the first frame update
    void Awake()
    {
        // Make sure these references are set up in the Inspector
        if (FirstWinningWordLetter == null)
            Debug.LogError("FirstWinningWordLetter is not assigned in the Inspector!");
        if (SecondWinningWordLetter == null)
            Debug.LogError("SecondWinningWordLetter is not assigned in the Inspector!");
        if (ThirdWinningWordLetter == null)
            Debug.LogError("ThirdWinningWordLetter is not assigned in the Inspector!");
        if (FourthWinningWordLetter == null)
            Debug.LogError("FourthWinningWordLetter is not assigned in the Inspector!");
    }

    public void UpdateWinningWordLabel(int pos, string WinningWordsChar)
    {

        if (pos == 0)
        {
            FirstWinningWordLetter.text = WinningWordsChar;
        }
        if (pos == 1)
        {
            SecondWinningWordLetter.text = WinningWordsChar;
        }
        if (pos == 2)
        {
            ThirdWinningWordLetter.text = WinningWordsChar;
        }
        if (pos == 3)
        {
            FourthWinningWordLetter.text = WinningWordsChar;
        }
    }


    public void ShowPopup(int lvlStars)
    {
        GridParent.SetActive(false);
        LvlCompletePopup.SetActive(true);
        LvlCompletePopupText.text = "Level Complete";
        if (lvlStars == 4)
        {
            StarOne.sprite = completeStar;
            StarTwo.sprite = completeStar;
            StarThree.sprite = completeStar;
        }else if (lvlStars == 3)
        {

            StarOne.sprite = completeStar;
            StarTwo.sprite = completeStar;
            StarThree.sprite = emptyStar;
        }
        else if (lvlStars == 2)
        {

            StarOne.sprite = completeStar;
            StarTwo.sprite = completeStar;
            StarThree.sprite = emptyStar;
        }
        else if (lvlStars == 1)
        {

            StarOne.sprite = completeStar;
            StarTwo.sprite = emptyStar;
            StarThree.sprite = emptyStar;
        }
    }
    
}
