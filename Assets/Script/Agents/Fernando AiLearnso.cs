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
    private int _Laps;
    public Slider ThrottleBar;
    public Slider TurnBar;
    [SerializeField] public TextMeshProUGUI LapCounter;
    public TextMeshProUGUI Reward;
    private Vector2 StartPos;
    public List<GameObject> StartPoints;
    private int Place;
    private Renderer _renderer;
    public float ThrottleRewardThreshHold;
    public float ThrottlePunishmentThreshHold;
    public CheckpointTracker[] CheckpointTracker;
    private int Checkpoints;
    private int _currentEpisode = 0;
    private float _cumulativeReward = 0f;

    private void FixedUpdate()
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

        Steer = 0f;
        _throttle = 0f;
        _Speed = 0f;

        for (int i = 0; i < CheckpointTracker.Length; i++)
        {
            CheckpointTracker[i].Restart();
        }
    }
    private void SetStart()
    {
        StartPos = transform.position;
    }
    private void Respawn()
    {
        transform.position = StartPos;
        transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float driverPositionX_Normalized = transform.position.x / 5f;
        float driverPositionY_Normalized = transform.position.y / 5f;

        float driverRotationY_Normalized = (transform.rotation.eulerAngles.y / 360f) * 2f - 1f;

        sensor.AddObservation(driverPositionX_Normalized);
        sensor.AddObservation(driverPositionY_Normalized);
        sensor.AddObservation(_throttle);
        sensor.AddObservation(_turnel);
        sensor.AddObservation(_maxThrottle);
        sensor.AddObservation(_minThrottle);
        sensor.AddObservation(ThrottleRewardThreshHold);
        sensor.AddObservation(ThrottlePunishmentThreshHold);
        sensor.AddObservation(Time.timeScale);
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
                ThrottleUp();
                ThrottleCheck();
                break;
            case 2:
                ThrottleDown();
                ThrottleCheck();
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    private void ThrottleUp()
    {
        _throttle += 0.1f;
    }
    private void ThrottleDown()
    {
        _throttle -= 0.1f;
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

    }

    public void SteerLeft()
    {
        Steer -= 0.25f;
        if ( Steer < SteerMin)
        {
            Steer = SteerMin;
        }
        TurnBar.value = Steer;
    }

    public void SteerRight ()
    {
        Steer += 0.25f;
        if ( Steer > SteerMax )
        {
            Steer = SteerMax;
            TurnBar.value = Steer;
        }
        TurnBar.value = Steer;
    }

    public void StopTurn()
    {
        Steer = 0;
    }

    public void go()
    {
        _Speed = topSpeed * _throttle;
        rb.linearVelocity = transform.up * _Speed;
        if (_Speed >= topSpeed)
        {
            _Speed = topSpeed;
        }

        float Value = Steer * (steeringDamper - _throttle);
        _turnel = steeringStrength * Value;
        rb.angularVelocity = _turnel;

        if (_throttle >= ThrottleRewardThreshHold)
        {
            //AddReward(1f / maxStep);
        }
        if (_throttle <= ThrottlePunishmentThreshHold)
        {
            //AddReward(-1f / maxStep);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            FinishedLap();
            Debug.Log("LineCrossed!");

        }
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            Checkpoints++;
            AddReward(2.0f * Checkpoints);
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
        //AddReward(-2.0f);
        _cumulativeReward = GetCumulativeReward();

        EndEpisode();
        Checkpoints = 0;
    }
}



