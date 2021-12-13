using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using Search;

public class AIMapComponent : MonoBehaviour // Bad, but fast for making prototype
{
    [SerializeField]public int minX;
    [SerializeField] public int maxX;
    [SerializeField] public int minY;
    [SerializeField] public int maxY;

    [SerializeField] public Tilemap walkableSpace;
    [SerializeField] public Tilemap obstacleSpace;

    public int deltaX;
    public int deltaY;

    public int pointNumber;


    public int[,] myMap;
    void Start()
    {
        
    }
    public int[,] BuildMap()
    {
        deltaX = minX < 0 ? -minX : 0;
        deltaY = minY < 0 ? -minY : 0;

        int[,] BuildingMap = new int[maxX + deltaX + 1, maxY + deltaY + 1];
        BuildingMap = FindWalkableCells(BuildingMap);
        BuildingMap = FindBlockedCells(BuildingMap);

        //BuildingMap = GetRightCooddinatesMap(BuildingMap);

        //delete soon

        string positions = "";


        /*for (int i = 0; i < BuildingMap.GetLength(0); i++)
        {
            //string positions = "" ;
            for (int j =0; j< BuildingMap.GetLength(1); j++)
            {
                positions = positions + (-BuildingMap[i, j] * 2);
                if (BuildingMap[i,j] == -1)
                {
                    Debug.DrawLine(walkableSpace.CellToWorld(new Vector3Int(i - deltaX, j - deltaY, 0)), walkableSpace.CellToWorld(new Vector3Int(i - deltaX, j - deltaY, 0)) + Vector3.back * 1000, Color.cyan, 100f); 
                }
            }

            //Debug.Log(positions);
            positions = positions + "\n";
        }
        Debug.Log(positions);*/


        myMap = BuildingMap;
        return BuildingMap;
    }

    public int[,] FindWalkableCells(int[,] BuildingMap)
    {

        //Debug.DrawLine(walkableSpace.CellToWorld(new Vector3Int(0, 0, 0)), walkableSpace.CellToWorld(new Vector3Int(0, 0, 0)) + Vector3.back * 1000, Color.cyan, 100f);
        //walkableSpace.SetColor(new Vector3Int(0, 0, 0), Color.red);
        //Debug.Log( walkableSpace.HasTile(new Vector3Int(0, 0, 0)));
        //walkableSpace.DeleteCells(new Vector3Int(2, 2, 0), new Vector3Int(0, 1, 0));
        var ttile = walkableSpace.GetTile(new Vector3Int(0, 0, 0));


        for (int i=minX + deltaX; i<maxX + deltaX+1; i++)
        {
            for (int j=minY + deltaY; j < maxY + deltaY + 1; j++)
            {
                if (walkableSpace.HasTile(new Vector3Int(i - deltaX, j - deltaY, 0)))
                {
                    BuildingMap[i, j] = 0;

                }
                else
                {
                    BuildingMap[i, j] = -1;
                }
            }
        }
        return BuildingMap;
    }

    public int[,] FindBlockedCells(int[,] BuildingMapWithWalkableCells)
    {
        //Debug.Log("Iamcalled");

        for (int i = minX + deltaX; i < maxX + deltaX + 1; i++)
        {
            for (int j = minY + deltaY; j < maxY + deltaY + 1; j++)
            {
                if (obstacleSpace.HasTile(new Vector3Int(i - deltaX, j - deltaY, 0)))
                {
                    BuildingMapWithWalkableCells[i, j] = -1;
                    //Debug.Log("Obstacle");
                }
            }
        }



        return BuildingMapWithWalkableCells;
    }

    public List<Vector2Int> FindWay(Vector2 startPoint, Vector2 endPoint)
    {
        //Debug.Log("Starting find path");
        Vector3Int startPosInCellCoords = walkableSpace.WorldToCell(startPoint);

        Vector3Int endPosInCellCoords = walkableSpace.WorldToCell(endPoint);
        Vector2Int startPosCorrected = new Vector2Int(startPosInCellCoords.x + deltaX, startPosInCellCoords.y + deltaY);
        Vector2Int endPosCorrected = new Vector2Int(endPosInCellCoords.x + deltaX, endPosInCellCoords.y + deltaY);

        /*Debug.Log("Start at " + startPoint + " that is " + startPosInCellCoords + " in cell coord and back it is " + walkableSpace.CellToWorld(startPosInCellCoords));
        Debug.Log("Start = " + startPoint + " in cell = " + startPosInCellCoords + " start corrected = " + startPosCorrected);

        Debug.Log("End at " + endPoint + " that is " + endPosInCellCoords + " in cell coord and back it is " + walkableSpace.CellToWorld(endPosInCellCoords));
        Debug.Log("End = " + endPoint + " in cell = " + endPosInCellCoords + " start corrected = " + endPosCorrected);*/


        //VisualiseMap(myMap, startPosCorrected, endPosCorrected);

        var path = AStarFromGoogle.FindPath(myMap, startPosCorrected, endPosCorrected);
        pointNumber = 0;


       


        return path;
    }
    public Vector2 GetNextPoint(List<Vector2Int> path, Vector2 endPoint)
    {
        //Debug.Log(path);
        //Debug.Log(path.Count);
        Vector2 nextPoint;
        if (path.Count >= 3) 
        {
            var nextPointInPath = path[1];
            Vector3Int nextPointInPathCorrected = new Vector3Int(nextPointInPath.x - deltaX, nextPointInPath.y - deltaY, 0);
            nextPoint = walkableSpace.CellToWorld(nextPointInPathCorrected);
            if ((nextPoint - (Vector2)gameObject.transform.position).magnitude > 0.5f)
            {
                nextPointInPath = path[2];
                nextPointInPathCorrected = new Vector3Int(nextPointInPath.x - deltaX, nextPointInPath.y - deltaY, 0);
                nextPoint = walkableSpace.CellToWorld(nextPointInPathCorrected);
            }
        }
        else
        {
            //nextPoint = endPoint;
            //nextPoint = path[0];
            //Debug.Log(path[0]);
            nextPoint = endPoint;
        }
        return nextPoint;
    }

    public int[,] GetRightCooddinatesMap(int[,] oldMap)
    {
        /*int[,] newMap = new int[oldMap.GetLength(1), oldMap.GetLength(0)];
        for (int i = 0; i < oldMap.GetLength(0); i++)
        {
            for (int j = 0; j < oldMap.GetLength(1); j++)
            {
                newMap[j, i] = oldMap[i, j];
            }
        }*/

        /*int[,] newMap = new int[oldMap.GetLength(1), oldMap.GetLength(0)];
        for (int i = 0; i < oldMap.GetLength(0); i++)
        {
            for (int j = 0; j < oldMap.GetLength(1); j++)
            {
                newMap[j, i] = oldMap[oldMap.GetLength(0) -1 - i, oldMap.GetLength(1) -1 - j];
            }
        }*/

        int[,] newMap = new int[oldMap.GetLength(0), oldMap.GetLength(1)];
        for (int i = 0; i < oldMap.GetLength(0); i++)
        {
            for (int j = 0; j < oldMap.GetLength(1); j++)
            {
                newMap[i, j] = oldMap[/*oldMap.GetLength(0) - 1 - */i, /*oldMap.GetLength(1) - 1 - */j];
            }
        }



        return newMap;
    }

    public void VisualiseMap (int[,] previousMap, Vector2Int startPos, Vector2Int endPos)
    {


        //Debug.Log(previousMap);

        int[,] moddeledMap = new int[previousMap.GetLength(0),previousMap.GetLength(1)];


        for (int i = 0; i < moddeledMap.GetLength(0); i++)
        {
            for (int j = 0; j < moddeledMap.GetLength(1); j++)
            {
                moddeledMap[i, j] = previousMap[i, j];
            }
        }


        //Debug.Log("Drawing map for start " + startPos + " and end " + endPos);
        //Debug.Log(moddeledMap);

        moddeledMap[startPos.x, startPos.y] = -2;
        moddeledMap[endPos.x, endPos.y] = -3;

        string positions = "";


        for (int i = 0; i < moddeledMap.GetLength(0); i++)
        {
            //string positions = "" ;
            for (int j = 0; j < moddeledMap.GetLength(1); j++)
            {
                positions = positions + (-moddeledMap[i, j] * 2);
            }

            //Debug.Log(positions);
            positions = positions + "\n";
        }
        //Debug.Log("Drawing map");
        //Debug.Log(positions);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
