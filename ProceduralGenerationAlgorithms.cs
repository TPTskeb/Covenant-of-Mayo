using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerationAlgorithms : MonoBehaviour
{
    [SerializeField] private TilemapVisualizer tileMapVisualizer;
    [SerializeField] private Vector2Int startPosition;
    [SerializeField] private int walkLength;

    public void GenerateDungeon()
    {
        tileMapVisualizer.Clear(); // ✅ Clears previous tiles before generating new ones

        HashSet<Vector2Int> floorPositions = SimpleRandomWalk(startPosition, walkLength);
        tileMapVisualizer.PaintFloorTiles(floorPositions);
    }

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), // UP
        new Vector2Int(1,0), // Right
        new Vector2Int(0,-1), // Down
        new Vector2Int(-1,0) // Left
    };

    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
