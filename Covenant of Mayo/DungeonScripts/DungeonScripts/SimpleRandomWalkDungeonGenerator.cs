using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool StartRandomlyEachIteration = false;

   protected override void RunProceduralGeneration()
{
    tilemapVisualizer.Clear(); // This should log "Clearing tiles..."
    HashSet<Vector2Int> floorPositions = RunRandomWalk();
    tilemapVisualizer.PaintFloorTiles(floorPositions);
}

    
        HashSet<Vector2Int> RunRandomWalk()
        {
            var currentPosition = startPosition;
            HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
            for(int i = 0; i < iterations; i++)
            {
                var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, walkLength);
                floorPositions.UnionWith(path);
                if(StartRandomlyEachIteration)
                    currentPosition = floorPositions.ElementAt(Random.Range(0,floorPositions.Count));
            }
            return floorPositions;
        }

   
}