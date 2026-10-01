using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class RespawnManager : MonoBehaviour
{
    public GameObject[] Cars;
    public GameObject P1;
    public GameObject P2;
    public int max;


    private void OnEnable()
    {
        int randomValue = Random.Range(0, max);
        GameObject Car1 = Cars[randomValue];
        Car1.transform.position = P1.transform.position;
        Car1.transform.rotation = P1.transform.rotation;
        randomValue++;
        if (randomValue > max)
        {
            randomValue--;
            randomValue--;
        }
        GameObject Car2 = Cars[randomValue];
        Car2.transform.position = P2.transform.position;
        Car2.transform.rotation = P2.transform.rotation;
    }
}