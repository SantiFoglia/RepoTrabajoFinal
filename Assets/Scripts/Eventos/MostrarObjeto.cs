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
    public void Da�arJugador(int da�o)
    {
        Jugador.vida -= da�o;
    }
}
