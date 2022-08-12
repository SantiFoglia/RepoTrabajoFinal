using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jefe : Enemigos
{
    [SerializeField] private UnityEvent MuerteJefe;

    void Start()
    {
        vida = 50;
    }

    

    private void Update()
    {
        if (vida <=0)
        {
            MuerteJefe.Invoke();
            enemigoMuriendo = true;
            Destroy(gameObject);
        }

    }

    
}
