using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public RoomBlkManager _roomBlkSpawner;
    // Start is called before the first frame update
    void Start()
    {
       // _roomBlkSpawner = GetComponent<RoomBlkManager>();
       

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnTriggerEntered()
    {
        _roomBlkSpawner.MoveRoom();
    }
}
