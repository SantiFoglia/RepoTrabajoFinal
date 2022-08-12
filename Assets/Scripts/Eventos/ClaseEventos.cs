 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClaseEventos : MonoBehaviour
{
    [SerializeField] private UnityEvent OnMyTrigger;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnMyTrigger.Invoke();
        }
    }
}
