using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class FourStepObs_Handler : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform _fourStepCenterPos;
    [SerializeField] private GameObject _playerObject; 
    [SerializeField] private PlayerController _playerAvoid;
    [SerializeField] private GameOverHandler _gameOverFs;

    [Header("Statistics")]
    //[SerializeField] private float _avoidActivateDistance = 1f;


    private float _fsDistance;
    private float _fsDistInt;
    private bool _avoidWindow = false;
    private bool _avoidFsChecker = false;



    // Update is called once per frame
    void Update()
    {
        FSDistRec();

        if (_fsDistance == 1)
        {
            _avoidWindow = true;

            if (_avoidWindow && _playerAvoid.avoidDistance==0)
            {
                AvoidFSObs();
                _playerObject.transform.Translate(0,0,2);
                _avoidFsChecker = false;

            }
            
        }

        if (_fsDistInt == 0 && !_avoidFsChecker)
        {
            print("Game Over");

            _gameOverFs.GameOver();
        }
        //print(_playerAvoid.avoidDistance);

    }

    private void FSDistRec()
    {
        
       // _fsDistance = Vector3.Distance(_fourStepCenterPos.position, _playerObject.transform.position);
        _fsDistance = _fourStepCenterPos.position.z - _playerObject.transform.position.z;
        _fsDistInt = (int)_fsDistance;
        

        
       
    }
    
    private void AvoidFSObs()
    {
        _avoidFsChecker = true;
    }
        
       


}
