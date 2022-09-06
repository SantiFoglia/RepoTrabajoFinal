using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventoJefe : MonoBehaviour
{
    [SerializeField] private UnityEvent EmpezarPeleaJefe;
    public AudioClip musicaJefe;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EmpezarPeleaJefe.Invoke();
            ManagerSonido.unicaInstancia.musica1.volume = 0.4f;
            ManagerSonido.unicaInstancia.musica2.volume = 0.1f;
            ManagerSonido.unicaInstancia.musica3.volume = 0.08f;
            ManagerSonido.unicaInstancia.PlayMusica1(musicaJefe);
        }
    }
}
