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
        Instantiate(vehiclesArray[Random.Range(0, vehiclesArray.Length)], startPosition.position, startPosition.rotation);
        StartCoroutine(Spawn());
        
    }

    IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(7f);
        
        while (true)
        {
            yield return wait;
            Instantiate(vehiclesArray[Random.Range(0, vehiclesArray.Length)], startPosition.position, startPosition.rotation);
            yield return null;
        }
    }
    
    
}
