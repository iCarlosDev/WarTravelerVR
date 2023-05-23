using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class CanonBarcos : MonoBehaviour
{


    [SerializeField] private float maxRotation;
    [SerializeField] private float minRotation;
    [SerializeField ]private float currentRotation;
    [SerializeField] private float tiempoRotacion;
    [SerializeField] private bool rotando;
    
    
    // Start is called before the first frame update
    void Start()
    {
       Rotacion();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, currentRotation, 0).normalized * Time.deltaTime *10);
        
        if (!rotando)
        {
            Rotacion();
        }
       
    }

    private void Rotacion()
    {
        currentRotation = Random.Range(maxRotation, minRotation + 1);
        tiempoRotacion = Random.Range(2f, 4f);
        StartCoroutine(TiempoRotacion());
        rotando = true;
    }

    IEnumerator TiempoRotacion()
    {
        yield return new WaitForSeconds(tiempoRotacion);
        rotando = false;
    }
}
