using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventoCambiarNivel : MonoBehaviour
{
    [SerializeField] private UnityEvent CambioEscenario;

    private void OnTriggerEnter(Collider other)
    {
        CambioEscenario.Invoke();
    }
}
