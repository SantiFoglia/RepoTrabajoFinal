using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jefe : Enemigos
{
    [SerializeField] private UnityEvent MuerteJefe;

    void Start()
    {
        vida = 30;
        nombre = "Jefe";
        velocidad = 3;
        rangoVision = 100f;
        rangoAtaqueBasico = 3f;
        dañoContacto = 10;
        anim = GetComponent<Animator>();
        _jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        detectarJugador();
        mirarJugador();
        seguirJugador();

        if (vida <=0)
        {
            MuerteJefe.Invoke();
            enemigoMuriendo = true;
            Destroy(gameObject);
        }

    }

    public override void detectarJugador()
    {
        base.detectarJugador();
    }
    public override void mirarJugador()
    {
        if (jugadorCerca)
        {
            base.mirarJugador();
        }

    }
    public override void seguirJugador()
    {
        if (jugadorCerca /*&& !estaAtacando*/)
        {
            base.seguirJugador();
            anim.SetFloat("Movimiento", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Movimiento", 0f, 0.1f, Time.deltaTime);
        }

    }
}
