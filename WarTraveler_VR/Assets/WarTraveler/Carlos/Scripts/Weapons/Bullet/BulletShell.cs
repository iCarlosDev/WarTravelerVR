using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f); //Destruimos el objeto después de 5s para no tener muchos instanciados.
    }
}
