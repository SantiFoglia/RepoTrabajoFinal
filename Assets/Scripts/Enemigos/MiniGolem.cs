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
    public override void Morir()
    {
        base.Morir();
    }

    override public void AtaqueBasico()
    {
        //RaycastHit ray;

        //if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out ray, rangoAtaqueBasico))
        //{

        //    if (ray.transform.tag == "Player")
        //    {
        //        estaRangoCerca = true;
        //        anim.SetTrigger("estaAtacando");

        //    }
        //}
        //else
        //{
        //    estaRangoCerca = false;
        //}

        if (estaRangoCerca)
        {
            anim.SetTrigger("estaAtacando");
        }
    }
}
