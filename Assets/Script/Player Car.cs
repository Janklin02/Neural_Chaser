using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Direction;
    public float speed;
    public float acceleration;
    public float breakPower;
    public float steeringStrength; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate();
        }
    }
    public void Accelerate()
    {
  
    }
    public void Brake()
    {

    }
    public void TurnL()
    {

    }
    public void TurnR()
    {

    }
}
