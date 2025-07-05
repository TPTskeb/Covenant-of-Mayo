using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 50, dungeonHeight = 50;

    [SerializeField, Range(0, 10)]
    private int offset = 1;

    [SerializeField]
    private bool randomWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        tilemapVisualizer.Clear(); // Always clear previous tiles
        CreateRooms();
    }

    private void CreateRooms()
    {
        var bounds = new BoundsInt(startPosition.x, startPosition.y, 0, dungeonWidth, dungeonHeight, 1);

        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(bounds, minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = CreateSimpleRooms(roomsList);

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);
       
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
{
    HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
    for (int i = 0; i < roomsList.Count; i++)
    {
        var roomBounds = roomsList[i];
        var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
        var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
        foreach (var position in roomFloor)
        {
            if (position.x >= (roomBounds.xMin + offset) &&
                position.x <= (roomBounds.xMax - offset) &&
                position.y >= (roomBounds.yMin + offset) &&
                position.y <= (roomBounds.yMax - offset))
            {
                floor.Add(position);
            }
        }
    }
    return floor;
}

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosesPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        return corridor;
    }
    private Vector2Int FindClosesPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomsList)
        {
            Debug.Log($"[Room] Room bounds: {room.min} size: {room.size}");

            for (int x = offset; x < room.size.x - offset; x++)
            {
                for (int y = offset; y < room.size.y - offset; y++)
                {
                    Vector2Int pos = new Vector2Int(room.xMin + x, room.yMin + y);
                    floor.Add(pos);
                }
            }
        }

        return floor;
    }
}
