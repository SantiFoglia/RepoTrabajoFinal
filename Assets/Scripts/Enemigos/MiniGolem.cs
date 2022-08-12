using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGolem : Enemigos
{
    // Start is called before the first frame update
    void Start()
    {
        vida = 50;
        nombre = "MiniGolem";
        velocidad = 2;
        rangoVision = 30f;
        rangoAtaqueBasico = 1f;
        dañoContacto = 5;
        anim = GetComponent<Animator>();
        _jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        detectarJugador();
        mirarJugador();
        seguirJugador();
        AtaqueBasico();

        if (vida <= 0)
        {
            enemigoMuriendo = true;
            Destroy(gameObject);
        }
    }

    public override void detectarJugador()
    {
        base.detectarJugador();
        estaRangoCerca = Physics.CheckSphere(gameObject.transform.position, rangoAtaqueBasico, layerJugador);
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
        if (jugadorCerca && !estaAtacando)
        {
            base.seguirJugador();
            anim.SetBool("estaCaminando", true);
        }
        else
        {
            anim.SetBool("estaCaminando", false);
        }

    }

    override public void AtaqueBasico()
    {
        if (estaRangoCerca)
        {
            anim.SetTrigger("estaAtacando");
        }
    }
}
