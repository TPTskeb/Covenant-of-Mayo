using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public static class WallGenerator 
{
  public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
  {
    var basicWallPositons = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
    var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);
    CreateBasicWall(tilemapVisualizer, basicWallPositons, floorPositions);
    CreateCornerWalls(tilemapVisualizer, basicWallPositons, floorPositions);
  }

  private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositons, HashSet<Vector2Int> floorPositions)
     {
        foreach (var position in basicWallPositons)
        {
          string neighboursBinaryType = "";
      foreach (var direction in Direction2D.cardinalDirectionsList)
      {
        var neighbourPosition = position + direction;
        if (floorPositions.Contains(neighbourPosition))
        {
          neighboursBinaryType += "1";
        }
        else
        {
          neighboursBinaryType += "0";
        }
          }
          tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int>FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
  {
    HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
    foreach (var position in floorPositions)
    {
        foreach (var direction in directionList)
        {
            var neighbourPosition = position + direction;
            if(floorPositions.Contains(neighbourPosition) == false)
                wallPositions.Add(neighbourPosition);
        }
    }
    return wallPositions;
  }

}
