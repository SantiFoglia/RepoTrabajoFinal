using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Enemigos
{
    public GameObject prefabRoca;
    public Transform spawnRoca;
    public Transform puntoMiraRay;
    GameObject Roca;

    public float cooldawnLanzaRoca;
    float tiempoParaLanzarRoca;

    void Start()
    {
        vida = 50;
        nombre = "Golem";
        velocidad = 2;
        rangoVision = 100f;
        rangoAtaqueBasico = 3f;
        dañoContacto = 10;
        anim = GetComponent<Animator>();
        _jugador = GameObject.FindGameObjectWithTag("Player");

        spawnRoca = GetComponent<Transform>().Find("spawnRoca");
        puntoMiraRay = GetComponent<Transform>().Find("puntoMiraRay");

        TemporizadorLanzarRoca();
        

        
    }

    // Update is called once per frame
    void Update()
    {   
        AtaqueBasico();
        AtaqueEspecial();
        TemporizadorLanzarRoca();
        detectarJugador();
        mirarJugador();
        seguirJugador();

        if(vida <= 0)
        {
            enemigoMuriendo = true;
            Destroy(gameObject);
        }
    }

    override public void AtaqueEspecial()
    {
        if (jugadorCerca && tiempoParaLanzarRoca <= 0 && !estaRangoCerca)
        {
            anim.SetTrigger("Attack01");

            tiempoParaLanzarRoca = cooldawnLanzaRoca;

            StartCoroutine(tiempoAnimacionLanzarRoca());
        }
    }
    override public void AtaqueBasico()
    {
        RaycastHit ray;

        if (Physics.Raycast(puntoMiraRay.transform.position, Vector3.forward, out ray, rangoAtaqueBasico))
        {
            
            if (ray.transform.tag == "Player")
            {
                estaRangoCerca = true;
                anim.SetTrigger("Attack02");
                
            }
        }
        else
        {
            estaRangoCerca = false;
        }
    }
    void TemporizadorLanzarRoca()
    {
        tiempoParaLanzarRoca -= Time.deltaTime;
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

    IEnumerator tiempoAnimacionLanzarRoca()
    {
        
        estaAtacando = true;
        yield return new WaitForSeconds(1.6f);

        Roca = Instantiate(prefabRoca, spawnRoca.transform.position, spawnRoca.transform.rotation);
        Roca.GetComponent<Rigidbody>().AddForce(spawnRoca.forward * 40, ForceMode.Impulse);
        estaAtacando = false;
    }
}
