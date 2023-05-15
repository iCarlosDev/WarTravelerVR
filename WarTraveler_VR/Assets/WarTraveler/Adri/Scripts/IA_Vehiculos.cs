using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA_Vehiculos : MonoBehaviour
{

    public List<Transform> waypoints = new List<Transform>();
    public GameObject[] vehiculos;
    
    
    void Start()
    {
        IAVehiculo();
    }


    
    public void IAVehiculo()
    {
        GameObject car = Instantiate(vehiculos[Random.Range(0, vehiculos.Length)], waypoints[0].position, waypoints[0].rotation);
        car.GetComponent<NavMeshAgent>().SetDestination(waypoints[1].transform.position);
    }
}
