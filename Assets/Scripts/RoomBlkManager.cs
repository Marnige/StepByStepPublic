using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomBlkManager : MonoBehaviour
{
    public List<GameObject> roomBlk;
    [SerializeField] private float offset = 24f;

    void Start()
    {   
        if(roomBlk != null && roomBlk.Count > 0)
        {
            roomBlk = roomBlk.OrderBy(r => r.transform.position.z).ToList();
        }
    }
    public void MoveRoom()
    {
        GameObject movedRoom = roomBlk[0];
        roomBlk.Remove(movedRoom);
        float newZ = roomBlk[roomBlk.Count - 1].transform.position.z + offset;
        movedRoom.transform.position = new Vector3(0,1, newZ);
        roomBlk.Add(movedRoom);

    }
}
