using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    public float radius = 1f;
    public int segments = 100;
    public float lineWidth = 0.1f;
    public float totalTime = 10f; // Total time for the countdown

    private LineRenderer lineRenderer;
    public float timeLeft;

    public UIActions UIActions;
    public float seconds;
    public float minutes;
    public TMP_Text timerText;

    public NumberBox winCheck;

    /*
    Color c1 = new Color(173, 149, 96, 1);
    Color c2 = new Color(249, 239, 217, 1);
    */

    Color c1 = new Color(173, 149, 96, 1);
    Color c2 = new Color(249, 239, 217, 1);

    void Start()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = false;

        timeLeft = totalTime;
        winCheck = new NumberBox();
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            ProgressBar();
        }
        else
        {
            if(winCheck != null)
                winCheck.CheckAllLettersPos(false, 1);
            //Debug.Log(winCheck.CheckAllLettersPos(false));
        }
    }

    public float returnCurTime()
    {
        return timeLeft;
    }

    [System.Obsolete]
    void ProgressBar()
    {
        int currentSegments = Mathf.CeilToInt((timeLeft / totalTime) * segments);
        lineRenderer.positionCount = currentSegments + 1;

        float angleIncrement = Mathf.PI / segments;
        for (int i = 0; i <= currentSegments; i++)
        {
            float angle = Mathf.PI - (i * angleIncrement);
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, -Mathf.Sin(angle) * radius, 0f);
            lineRenderer.SetPosition(i, position);

            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.SetColors(c1, c2);
        }
    
    }
}
