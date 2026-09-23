using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float Speed;
    public float topSpeed;
    public float breakPower;
    public float steeringStrength;
    public float steeringDamper;
    private float turnel;
    private float turnar;
    private float throttle;
    private float minThrottle = 0;
    public float maxThrottle;
    public int tCountO;
    private int tCount;
    public int bCountO;
    private int bCount;
    public GameObject RespawnPoint;
    [SerializeField] Rigidbody2D rb;
    private int Laps;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Play Started");
        Respawn();
        Laps = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
        if (Input.GetKey(KeyCode.A))
        {
            TurnL();
        }
        if (Input.GetKey(KeyCode.D))
        {
            TurnR();
        }
        if (Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.D)))
        {
            StopTurn();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Brake();
        }
        go();
        Debug.Log(Laps);
    }

    public void go()
    {
        Speed = topSpeed * throttle;
        rb.linearVelocity = transform.up * Speed;
        if (Speed >= topSpeed)
        {
            Speed = topSpeed;
        }
    }
    public void Accelerate()
    {
        if (tCount == 0)
        {
            throttle += 0.1f;
        }
        tCount++;
        if (tCount >= tCountO)
        {
            tCount = 0;
        }
        if (throttle >= maxThrottle)
        {
            throttle = maxThrottle;
        }
    }
    public void Brake()
    {
        if (bCount == 0)
        {
            throttle -= breakPower;
        }
        bCount++;
        if (bCount >= bCountO)
        {
            bCount = 0;
        }
        if (throttle <= minThrottle)
        {
            throttle = minThrottle;
        }
    }
    public void TurnL()
    {
        float Steer = steeringDamper - throttle;

            turnel = steeringStrength * Steer;
        if (Steer == 0)
        {
            turnel = steeringStrength * 0.05f;
        }
            rb.angularVelocity = turnel;

    }
    public void StopTurn()
    {
        rb.angularVelocity = 0;
    }

    public void TurnR()
    {
        float Steer = steeringDamper - throttle;

        turnel = steeringStrength * Steer;
        if (Steer == 0)
        {
            turnel = steeringStrength * 0.05f;
        }
        rb.angularVelocity = turnel * -1;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Hit Wall");
            Respawn();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            Debug.Log("LapComplete");
            Lap();
        }
    }

    public void Lap()
    {
        Laps++;
    }
    public void Respawn()
    {
        transform.position = RespawnPoint.transform.position;
        Speed = 0;
        throttle = 0;
    }
}
