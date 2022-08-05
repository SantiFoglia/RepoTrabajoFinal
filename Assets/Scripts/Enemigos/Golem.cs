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

    

    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
        nombre = "Golem";
        velocidad = 3;
        rangoVision = 100f;
        anim = GetComponent<Animator>();

        TemporizadorLanzarRoca();
        spawnRoca = GetComponent<Transform>().Find("spawnRoca");
        puntoMiraRay = GetComponent<Transform>().Find("puntoMiraRay");
        _jugador = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        AtaqueBasico();
        AtaqueEspecial();
        TemporizadorLanzarRoca();
        DetectarJugador();
        mirarJugador();
        seguirJugador();
    }

    override public void AtaqueEspecial()
    {
        RaycastHit ray;

        if (Physics.Raycast(puntoMiraRay.transform.position, Vector3.forward, out ray, rangoAtaqueEspecial))
        {
            if (ray.transform.tag == "Player" && tiempoParaLanzarRoca <= 0 && !estaRangoCerca)
            {
                anim.SetTrigger("Attack01");

                tiempoParaLanzarRoca = cooldawnLanzaRoca;

                StartCoroutine(tiempoAnimacionLanzarRoca());
            }
        }
    }
    override public void AtaqueBasico()
    {
        RaycastHit ray;

        if (Physics.Raycast(puntoMiraRay.transform.position, Vector3.forward, out ray, rangoAtaqueBasico))
        {
            estaRangoCerca = true;
            if (ray.transform.tag == "Player")
            {
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
    public override void DetectarJugador()
    {
        base.DetectarJugador();
    }
    public override void mirarJugador()
    {
        base.mirarJugador();
    }
    public override void seguirJugador()
    {
        base.seguirJugador();
    }
    public override void Morir()
    {
        base.Morir();
    }

    IEnumerator tiempoAnimacionLanzarRoca()
    {
        yield return new WaitForSeconds(1.6f);

        Roca = Instantiate(prefabRoca, spawnRoca.transform.position, spawnRoca.transform.rotation);
        Roca.GetComponent<Rigidbody>().AddForce(spawnRoca.forward * 40, ForceMode.Impulse);
    }
}
