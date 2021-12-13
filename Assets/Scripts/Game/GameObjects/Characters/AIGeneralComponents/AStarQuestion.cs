using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Search;

public class AStarQuestion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[,] map = new int[10, 10]
        {
            { 5,5,5,5,5,5,5,5,5,5 },
            { 5,0,0,0,0,0,0,0,0,5},
            { 5,0,0,0,0,0,0,0,0,5},
            { 5,0,0,5,5,5,5,0,0,5},
            { 5,0,0,5,0,0,5,0,0,5},
            { 5,0,0,5,0,0,5,0,0,5},
            { 5,0,0,5,0,0,5,0,0,5},
            { 5,0,0,0,0,0,0,0,0,5},
            { 5,0,0,0,0,0,0,0,0,5},
            { 5,5,5,5,5,5,5,5,5,5 },
        };
        Vector2Int from = new Vector2Int(5, 4);
        Vector2Int to = new Vector2Int(2, 4);
        var path = AStarFromGoogle.FindPath(map, @from, to);
        Debug.Log(path.Count);

        foreach (var pathPoint in path)
        {
            map[pathPoint.x, pathPoint.y] = 2;
        }



        for (int i = 0; i < map.GetLength(0); i++)
        {
            string positions = "";
            for (int j = 0; j < map.GetLength(1); j++)
            {
                positions = positions + map[i, j];
            }
            Debug.Log(positions);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
