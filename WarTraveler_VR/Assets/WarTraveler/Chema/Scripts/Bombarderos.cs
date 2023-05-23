using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombarderos : MonoBehaviour
{

    public float max_Speed;
    public float min_Speed;
   
    public float currentSpeed;

    public Transform[] endPoints;
    public Transform[] startPoints;
    
    private Transform BombEnd;
    private Transform BombStart;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = Random.Range(max_Speed, min_Speed + 1);
        StartFlight();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void StartFlight()
    {
        currentSpeed = Random.Range(max_Speed, min_Speed + 1);
        BombStart = endPoints[Random.Range(0, endPoints.Length)];
        transform.position = BombStart.position;
        OnFlight();
       
    }

    private void OnFlight()
    {
        BombEnd = startPoints[Random.Range(0, startPoints.Length)];
        transform.LookAt(BombEnd);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StartPoint"))
        {
            StartFlight();
        }
        
        
        
        
    }
}
