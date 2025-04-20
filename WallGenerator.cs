using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
  public static void CreateWalls(HashSet<Vector2Int>flooorPositions, TilemapVisualizer tilemapVisualizer)
  {
    var basicWallPositons = FindWallsInDirections(flooorPositions, Direction2D.cardinalDirectionsList);
    foreach (var position in basicWallPositons)
    {
        tilemapVisualizer.PaintSingleBasicWall(position);
    }
  }

  private static HashSet<Vector2Int>FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> DirectionList)
  {
    HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
    foreach (var position in floorPositions)
    {
        foreach (var direction in DirectionList)
        {
            var neighbourPosition = position + direction;
            if(floorPositions.Contains(neighbourPosition) == false)
                wallPositions.Add(neighbourPosition);
        }
    }
    return wallPositions;
  }

}
