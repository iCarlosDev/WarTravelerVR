using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class IA_Vehiculo : MonoBehaviour
{

    public List<GameObject> waypoints = new List<GameObject>();
    public int waypointIndex;
    public NavMeshAgent NavMeshAgent;

    void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        waypoints.AddRange(GameObject.FindGameObjectsWithTag("VehicleWaypoint"));
    }

    void Update()
    {
        GoNextWaypoint();
    }
    

    private void GoNextWaypoint()
    {
        if (waypointIndex == waypoints.Count -1) return;

        if (Vector3.Distance(transform.position, NavMeshAgent.destination) < 0.3f)
        {
            waypointIndex++; 
            NavMeshAgent.SetDestination(waypoints[waypointIndex].transform.position);
        }
    }
}
