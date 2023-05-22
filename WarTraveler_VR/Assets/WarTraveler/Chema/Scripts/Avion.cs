using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Avion : MonoBehaviour
{

   
    public GameObject bomba;
    
    public float max_Speed;
    public float min_Speed;
  
    public float currentSpeed;

    private Transform objetivo;
    private Transform endMap;
    private Transform startMap;

    public Transform[] endPoints;
    public Transform[] startPoints;
    public List<Transform> objetivos;
    private bool bombDown;
    private bool atacar;

  
    
    
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] listaTarget = GameObject.FindGameObjectsWithTag("Barco");
        
        Debug.Log("Length " + listaTarget.Length);
        
        foreach (GameObject target in listaTarget)
        {
            objetivos.Add(target.GetComponent<Transform>());
        }
    }
    
    void Start()
    {
        
        
        currentSpeed = Random.Range(max_Speed, min_Speed + 1);
        
        
        Objetivo();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndMap"))
        {
            StartMission();
        }
        
        if  (other.gameObject.CompareTag("Barco"))
        {
            Invoke(nameof(AtacarBarco), Random.Range(0f, 1f));
            
        }
        if  (other.gameObject.CompareTag("Mar"))
        {
            Destroy(gameObject);
            
        }
        if  (other.gameObject.CompareTag("Avion"))
        {
            Destroy(gameObject);
            
        }
        
        
    }
    
    private void AtacarBarco()
    {
        Instantiate(bomba, transform.position, transform.rotation);
        Fuga();
    }

    private void StartMission()
    {
        bool atacar = Random.Range(0f, 101f) >= 50f;
        currentSpeed = Random.Range(max_Speed, min_Speed + 1);
        startMap = startPoints[Random.Range(0, startPoints.Length)];
        transform.position = startMap.position;

        if (atacar == true)
        {
            objetivo = objetivos[Random.Range(0, objetivos.Count)];
            transform.LookAt(objetivo);
        }
        else
        {
            
            Fuga();
        }
    }

    private void Objetivo()
    {
        objetivo = objetivos[Random.Range(0, objetivos.Count)];
        transform.LookAt(objetivo);
    }

    private void Fuga()
    {
        Debug.Log("FUGA");
        endMap = endPoints[Random.Range(0, endPoints.Length)];
        transform.LookAt(endMap);
    }
}
