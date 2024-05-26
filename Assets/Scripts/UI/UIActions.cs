using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    public TMP_Text FirstWinningWordLetter;
    public TMP_Text SecondWinningWordLetter;
    public TMP_Text ThirdWinningWordLetter;
    public TMP_Text FourthWinningWordLetter;
    string WinningWordsChar;
    //public GameObject LvlCompletePopup = new GameObject();
    public TMP_Text LvlCompletePopupText;


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
        //GameObject LvlCompletePopup = new GameObject();
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
        //LvlCompletePopup.SetActive(true);
        //LvlCompletePopupText.text = "You got "+lvlStars.ToString()+" stars";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
