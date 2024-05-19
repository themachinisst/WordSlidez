using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class MainMech : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject buttonPrefab;

    public object[,] gridBox = new object[4, 4];
    public int[,] numberArr = new int[4, 4];
    private int Index = 0;

    void Start()
    {
        GridSpawn();
    }

    private void GridSpawn()
    {
        for (int y = 3; y >= 0; y--)
            for (int x = 0; x < 4; x++)
            {
                object cell = Instantiate(buttonPrefab, new Vector2(x, y), Quaternion.identity);
                gridBox[x, y] = cell;
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
