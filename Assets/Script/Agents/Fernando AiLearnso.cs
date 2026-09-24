using Newtonsoft.Json.Bson;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class FernandoAiLearnso : Agent
{
    [SerializeField] private Transform _goal;
    [SerializeField] private float _Speed;
    [SerializeField] public float topSpeed;
    [SerializeField] public float steeringStrength;
    [SerializeField] public float steeringDamper;
    [SerializeField] private float _turnel;
    [SerializeField] private float _throttle;
    [SerializeField] private float _minThrottle = 0;
    [SerializeField] private float _maxThrottle = 1;
    [SerializeField] public int tCountO;
    [SerializeField] private int _tCount;
    [SerializeField] public int bCountO;
    [SerializeField] private int _bCount;
    [SerializeField] public GameObject RespawnPoint;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int _Laps;
    [SerializeField] public Slider ThrottleBar;
    [SerializeField] public TextMeshProUGUI LapCounter;
    public GameObject[] StartPoints;

    private Renderer _renderer;

    private int _currentEpisode = 0;
    private float _cumulativeReward = 0f;

    private void Update()
    {
        go();
    }
    public override void Initialize()
    {
        Debug.Log("Initialising...");

        _renderer = GetComponent<Renderer>();
        _currentEpisode = 0;
        _cumulativeReward = 0f;

        Respawn();
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("New Episode Is Begining, Dont Touch That Dial.");

        _currentEpisode++;
        _cumulativeReward = 0f;

        Respawn();
    }

    private void Respawn()
    {
        int randomIndex = Random.Range(0, StartPoints.Length);
        GameObject chosenObject = StartPoints[randomIndex];
        transform.position = chosenObject.transform.position;
        transform.rotation = chosenObject.transform.rotation;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //Placeholder: What Agent Sees Goes Here Later
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ThrottleAgent(actions.DiscreteActions);

        TurnAgent (actions.DiscreteActions);
    }

    public void ThrottleAgent(ActionSegment<int> act)
    {
        var action = act[0];

        switch (action)
        {
            case 1:
                _throttle =+ 0.1f;
                ThrottleCheck();
                break;
            case 2:
                _throttle =- 0.1f;
                ThrottleCheck();
                break;
        }
    }

    public void TurnAgent(ActionSegment<int> act)
    {
        var action = act[0];

        switch (action)
        {
            case 1:
                SteerLeft();
                break;
            case 2:
                SteerRight();
                break;
            case 3:
                StopTurn();
                break;
        }
    }

    public void ThrottleCheck()
    {
        if (_throttle > _maxThrottle)
        {
            _throttle = 1f;
        }
        if (_throttle < _minThrottle)
        {
            _throttle = 0f;
        }
    }

    public void SteerLeft()
    {
        float Steer = steeringDamper - _throttle;
        _turnel = steeringStrength * Steer;
        if (Steer == 0)
        {
            _turnel = steeringStrength * 0.05f;
        }
        rb.angularVelocity = _turnel;
    }

    public void SteerRight ()
    {
        float Steer = steeringDamper - _throttle;
        _turnel = steeringStrength * Steer;
        if (Steer == 0)
        {
            _turnel = steeringStrength * 0.05f;
        }
        rb.angularVelocity = _turnel * -1;
    }

    public void StopTurn()
    {
        rb.angularVelocity = 0;
    }

    public void go()
    {
        _Speed = topSpeed * _throttle;
        rb.linearVelocity = transform.up * _Speed;
        if (_Speed >= topSpeed)
        {
            _Speed = topSpeed;
        }
    }
}
