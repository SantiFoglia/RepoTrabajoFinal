using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{
    int da�o = 10;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !Jugador.jugadorInvulnerable)
        {
            Jugador.vida -= da�o;
            print("Pega");
        }
    }

}
