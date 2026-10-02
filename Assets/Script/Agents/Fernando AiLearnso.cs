using Newtonsoft.Json.Bson;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FernandoAiLearnso : Agent
{
    [SerializeField] public float maxStep;
    [SerializeField] private Transform _goal;
    [SerializeField] private float Steer;
    [SerializeField] private float SteerMax = 1;
    [SerializeField] private float SteerMin = -1;
    [SerializeField] private float _Speed;
    [SerializeField] public float topSpeed;
    [SerializeField] public float steeringStrength;
    [SerializeField] public float steeringDamper;
    [SerializeField] private float _turnel;
    [SerializeField] private float _throttle;
    [SerializeField] private float _minThrottle = 0;
    [SerializeField] private float _maxThrottle = 1;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private int _Laps;
    [SerializeField] public Slider ThrottleBar;
    [SerializeField] public TextMeshProUGUI LapCounter;
    public TextMeshProUGUI Reward;
    private Vector2 StartPos;
    public List<GameObject> StartPoints;
    private int Place;
    private Renderer _renderer;
    public float ThrottleRewardThreshHold;
    public float ThrottlePunishmentThreshHold;

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
        Invoke(nameof(SetStart), 0.5f);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("New Episode Is Begining, Dont Touch That Dial.");

        _currentEpisode++;
        _cumulativeReward = 0f;
        Invoke(nameof(Respawn), 0.5f);
    }
    private void SetStart()
    {
        StartPos = transform.position;
    }
    private void Respawn()
    {
        transform.position = StartPos;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float driverPositionX_Normalized = transform.position.x / 5f;
        float driverPositionY_Normalized = transform.position.y / 5f;

        float driverRotationY_Normalized = (transform.rotation.eulerAngles.y / 360f) * 2f - 1f;

        sensor.AddObservation(driverPositionX_Normalized);
        sensor.AddObservation(driverPositionY_Normalized);
        sensor.AddObservation(driverRotationY_Normalized);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        ThrottleAgent(actions.DiscreteActions);

        TurnAgent (actions.DiscreteActions);

        AddReward(-2f / maxStep);
        if (StepCount > maxStep)
        {
            Fail();
        }
        _cumulativeReward = GetCumulativeReward();
        Reward.text = _cumulativeReward.ToString();
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
            case 3:
                Debug.Log("Im Happy With How It Is");
                break;
            default:
                break;
        }
    }

    public void TurnAgent(ActionSegment<int> act)
    {
        var action = act[1];

        switch (action)
        {
            case 1:
                SteerLeft();
                break;
            case 2:
                SteerRight();
                break;
            case 3:
                Debug.Log("Im Happy With How It Is");
                break;
            default:
                break;
        }
    }

    public void ThrottleCheck()
    {
        ThrottleBar.value = _throttle;
        if (_throttle > _maxThrottle)
        {
            _throttle = 1f;

        }
        if (_throttle < _minThrottle)
        {
            _throttle = 0f;
        }
        if ( _throttle > ThrottleRewardThreshHold)
        {
            AddReward(1.5f / maxStep);
        }
        if ( _throttle < ThrottlePunishmentThreshHold)
        {
            AddReward(-1.5f / maxStep);
        }
    }

    public void SteerLeft()
    {
        Steer =- 0.25f;
        if ( Steer < SteerMin)
        {
            Steer = SteerMin;
        }
    }

    public void SteerRight ()
    {
        Steer =+ 0.25f;
        if ( Steer > SteerMax )
        {
            Steer = SteerMax;
        }
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

        float Value = (steeringDamper - _throttle) * Steer;
        _turnel = steeringStrength * Value;
        if (Value == 0)
        {
            _turnel = steeringStrength * 0.05f;
        }
        rb.angularVelocity = _turnel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            FinishedLap();
            Debug.Log("LineCrossed!");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Fail();
        }
    }

    private void FinishedLap()
    {
        AddReward(5.0f);
        Debug.Log(_cumulativeReward);
        _cumulativeReward = GetCumulativeReward();
        _Laps++;
        LapCounter.text = _Laps.ToString();
        Debug.Log(_cumulativeReward);

        EndEpisode();
    }

    private void Fail()
    {
        AddReward(-1.0f);
        _cumulativeReward = GetCumulativeReward();

        EndEpisode();
    }
}



