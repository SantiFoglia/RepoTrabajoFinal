using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventoJefe : MonoBehaviour
{
    [SerializeField] private UnityEvent EmpezarPeleaJefe;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EmpezarPeleaJefe.Invoke();
        }
    }
}
