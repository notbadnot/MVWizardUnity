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

    public int[,] BuildMap()
    {
        deltaX = minX < 0 ? -minX : 0;
        deltaY = minY < 0 ? -minY : 0;

        int[,] BuildingMap = new int[maxX + deltaX + 1, maxY + deltaY + 1];
        BuildingMap = FindWalkableCells(BuildingMap);
        BuildingMap = FindBlockedCells(BuildingMap);

        myMap = BuildingMap;
        return BuildingMap;
    }

    public int[,] FindWalkableCells(int[,] BuildingMap)
    {
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

        for (int i = minX + deltaX; i < maxX + deltaX + 1; i++)
        {
            for (int j = minY + deltaY; j < maxY + deltaY + 1; j++)
            {
                if (obstacleSpace.HasTile(new Vector3Int(i - deltaX, j - deltaY, 0)))
                {
                    BuildingMapWithWalkableCells[i, j] = -1;
                }
            }
        }
        return BuildingMapWithWalkableCells;
    }

    public List<Vector2Int> FindWay(Vector2 startPoint, Vector2 endPoint)
    {
        Vector3Int startPosInCellCoords = walkableSpace.WorldToCell(startPoint);
        Vector3Int endPosInCellCoords = walkableSpace.WorldToCell(endPoint);
        Vector2Int startPosCorrected = new Vector2Int(startPosInCellCoords.x + deltaX, startPosInCellCoords.y + deltaY);
        Vector2Int endPosCorrected = new Vector2Int(endPosInCellCoords.x + deltaX, endPosInCellCoords.y + deltaY);

        var path = AStarFromGoogle.FindPath(myMap, startPosCorrected, endPosCorrected);
        pointNumber = 0;

        return path;
    }
    public Vector2 GetNextPoint(List<Vector2Int> path, Vector2 endPoint)
    {
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
            nextPoint = endPoint;
        }
        return nextPoint;
    }


    public void VisualiseMap (int[,] previousMap, Vector2Int startPos, Vector2Int endPos) //need for Debug
    {

        int[,] moddeledMap = new int[previousMap.GetLength(0),previousMap.GetLength(1)];

        for (int i = 0; i < moddeledMap.GetLength(0); i++)
        {
            for (int j = 0; j < moddeledMap.GetLength(1); j++)
            {
                moddeledMap[i, j] = previousMap[i, j];
            }
        }

        moddeledMap[startPos.x, startPos.y] = -2;
        moddeledMap[endPos.x, endPos.y] = -3;

        string positions = "";
        for (int i = 0; i < moddeledMap.GetLength(0); i++)
        {
            for (int j = 0; j < moddeledMap.GetLength(1); j++)
            {
                positions = positions + (-moddeledMap[i, j] * 2);
            }
            positions = positions + "\n";
        }
    }

}
