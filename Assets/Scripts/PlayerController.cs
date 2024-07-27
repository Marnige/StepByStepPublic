using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerController : MonoBehaviour
{
    public Rigidbody playerRb;
    [Header("Statistics")]
    [SerializeField] public float moveSpeed = 0.5f;
    [SerializeField] public float stepSize = 1f;
    [SerializeField] private float _avoidCooldownDuration = 1f;

    [Header("Transform References")]
    [SerializeField] private Transform _playerCenterPos;


    //Movement Logic methods

    private bool _stepForward = false;    //The command to step forward 

    private bool _leftStep;               //The command to take a left step
    private bool _rightStep;              //The command to take a right step

    private int _leftCounter;             //Alternating step logic, Left ver.
    private int _rightCounter;            //Atternating step logic, Right ver.
    private float _totalDistance = 0;     //Step Size Travelled 
    private Vector3 _previousLoc;         //Record previous location to calculate distance travelled per step

    //Avoid Logic

    private bool _avoidPressed;                      //Check if Spacebar is pressed
    private bool _avoidAvailable = true;             //Check if Avoid is available to be used again
	public bool avoidActivate = false;               //Activate Avoid
    public float avoidDistance = 0;                //The distance that avoid will be active
    private Vector3 _avoidPosition;                  //Record the distance when Avoid was last pressed
    private bool _avoidActivateRecordPos = false;    //Logic to start/stop _avoidPosition

    public SpawnManager spawnManager;
	
	
    void Awake()
    {
        _previousLoc = transform.position;
        _avoidPosition = transform.position;
        _leftCounter = 0;
        _rightCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {
        RecordDistance();

        MovementLogic();

        UseAvoid();
        AvoidActivateDist();

        if (_totalDistance >= stepSize)
        {
            _stepForward = false;
            _totalDistance = 0;
        }   
        else
        {
            MovePlayer();
        }
    
    }

    #region Movement Manager
    private void MovementLogic()
    {
        // Boolean for Left and Right Steps
        _leftStep = Input.GetKeyDown(KeyCode.LeftShift);
        _rightStep = Input.GetKeyDown(KeyCode.RightShift);
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _leftStep = false;
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            _rightStep = false;
        }
        // Movement Logic
        if (_leftStep && _leftCounter < 1 && !_rightStep)
        {
            _stepForward = true;
            _leftCounter++;

            if (_rightCounter == 1)
            {
                _rightCounter--;

            }
        }


        if (_rightStep && _rightCounter < 1 && !_leftStep)
        {
            _stepForward = true;
            _rightCounter++;

              if (_leftCounter == 1)
            {
                _leftCounter--;

            }
        }
    }
    private void MovePlayer()
     {
          if (_leftCounter + _rightCounter > 0 && _stepForward)
          {
            transform.Translate(0,0,moveSpeed);
          }
    }

    private void RecordDistance()
    {
        _totalDistance += Vector3.Distance(transform.position, _previousLoc);
        _previousLoc = transform.position;

    }
    #endregion
    
    #region Avoid Manager
    private void UseAvoid()
    {
        	if (_avoidAvailable == false)
		{
			return;
		}

        _avoidPressed = Input.GetKeyDown(KeyCode.Space);

        if (_avoidPressed && _avoidAvailable)
        {
            avoidActivate = true;
            _avoidActivateRecordPos = true;
            AvoidActivateDist();
            print("Avoid Used");
            StartCoroutine(StartCooldown());
        }

    }
	
	public IEnumerator StartCooldown()
	{
		_avoidAvailable = false;
        _avoidActivateRecordPos = false;

		yield return new WaitForSeconds(_avoidCooldownDuration);

		_avoidAvailable = true;
	}

    private void AvoidActivateDist()
    {
        if (_avoidActivateRecordPos)
        {
            _avoidPosition = transform.position;
            avoidDistance = 0;
        }

        avoidDistance = transform.position.z - _avoidPosition.z;
         //avoidDistance = Vector3.Distance(transform.position, _avoidPosition)
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }
}
