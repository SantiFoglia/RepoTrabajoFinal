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
            ManagerSonido.unicaInstancia.musica1.volume = 0.5f;
            ManagerSonido.unicaInstancia.musica2.volume = 0.3f;
            ManagerSonido.unicaInstancia.musica3.volume = 0.3f;
            ManagerSonido.unicaInstancia.PlayMusica1(musicaJefe);
        }
    }
}
