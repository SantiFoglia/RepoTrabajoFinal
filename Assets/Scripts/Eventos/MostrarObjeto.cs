using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarObjeto : MonoBehaviour
{
    [SerializeField] private GameObject cubito;

    public void ActivarCubito()
    {
        cubito.SetActive(true);
    }

    public void DecirHola()
    {
        Debug.Log("HOLAAA");
    }
    public void DañarJugador(int daño)
    {
        Jugador.vida -= daño;
    }
}
