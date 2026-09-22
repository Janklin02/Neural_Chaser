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
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Play Started");
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
        Debug.Log(Steer);
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
        Debug.Log(Steer);
    }
}
