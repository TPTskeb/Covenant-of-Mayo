using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;
    public int X;
    public int Y;

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene");
            return;
        }
        RoomController.instance.RegisterRoom(this);
    }
void OnDrawGizmos()
{
    Gizmos.DrawWireCube(transform.position, new UnityEngine.Vector3(Width,Height, 0));
}
 public UnityEngine.Vector3 GetroomCetre()
 {
    return new UnityEngine.Vector3 (X * Width, Y * Height );
 }
}
