using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiculosManager : MonoBehaviour
{
    public GameObject[] vehiclesArray;
    public Transform startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnVehicles();
    }

    private void SpawnVehicles()
    {
        Instantiate(vehiclesArray[Random.Range(0, vehiclesArray.Length)], startPosition.position, startPosition.rotation);
    }
}
