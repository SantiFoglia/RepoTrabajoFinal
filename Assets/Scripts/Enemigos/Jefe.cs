using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jefe : Enemigos
{
    [SerializeField] private UnityEvent MuerteJefe;

    public List<Transform> spawnsRocas;
    public List<Transform> spawnsGolems;
    public GameObject prefabRoca;
    public GameObject prefabGolem;
    GameObject Roca;

    int fase = 1;

    //cooldawns
    public float cooldawnLanzaRoca;
    float tiempoParaLanzarRoca;
    public float cooldawnLanzaPelota;
    float tiempoParaLanzarPelota;
    public float cooldawnInvocarGolems;
    float tiempoParaInvocarGolems;

    void Start()
    {
        vida = 100;
        nombre = "Jefe";
        velocidad = 3;
        rangoVision = 100f;
        rangoAtaqueBasico = 3f;
        dañoContacto = 10;
        anim = GetComponent<Animator>();
        _jugador = GameObject.FindGameObjectWithTag("Player");

        TemporizadorLanzarRoca();
    }

    private void Update()
    {
        detectarJugador();
        mirarJugador();
        seguirJugador();
        TemporizadorLanzarRoca();
        AtaqueBasico();
        InvocarGolems();
        AtaqueEspecial();
        CambioFase();
        
        

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
        if (jugadorCerca)
        {
            base.seguirJugador();
            anim.SetFloat("Movimiento", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Movimiento", 0f, 0.1f, Time.deltaTime);
        }

    }
    void TemporizadorLanzarRoca()
    {
        tiempoParaLanzarRoca -= Time.deltaTime;
        tiempoParaInvocarGolems -= Time.deltaTime;
    }
    
    public override void AtaqueBasico()
    {
        if (jugadorCerca && tiempoParaLanzarRoca <= 0 && !estaRangoCerca && !estaAtacando)
        {
            anim.SetTrigger("AtaquePiedras");

            tiempoParaLanzarRoca = cooldawnLanzaRoca;

            StartCoroutine(tiempoAnimacionLanzarRoca());
        }
    }
    public void InvocarGolems()
    {
        if (jugadorCerca && tiempoParaInvocarGolems <= 0 && !estaRangoCerca && !estaAtacando)
        {
            anim.SetTrigger("InvocarGolems");

            tiempoParaInvocarGolems = cooldawnInvocarGolems;

            StartCoroutine(tiempoAnimacionInvocarGolems());
        }
    }
    public override void AtaqueEspecial()
    {

    }
    void CambioFase()
    {
        if (fase == 1 && vida <=50)
        {
            fase = 2;
            velocidad = 0;
            anim.SetTrigger("CambiarFase");
            
            StartCoroutine(tiempoAnimacionCambiarFase());
        }
    }
    IEnumerator tiempoAnimacionLanzarRoca()
    {

        estaAtacando = true;
        yield return new WaitForSeconds(1.8f);

        foreach (var spawn in spawnsRocas)
        {
            spawn.LookAt(_jugador.transform);
            Roca = Instantiate(prefabRoca, spawn.transform.position, spawn.transform.rotation);
            Roca.GetComponent<Rigidbody>().AddForce(spawn.forward * 40, ForceMode.Impulse);
            
        }
        
        estaAtacando = false;
    }
    IEnumerator tiempoAnimacionLanzarPelota()
    {

        estaAtacando = true;
        yield return new WaitForSeconds(1.6f);


        estaAtacando = false;
    }
    IEnumerator tiempoAnimacionInvocarGolems()
    {

        estaAtacando = true;
        yield return new WaitForSeconds(3.5f);

        foreach (var spawn in spawnsGolems)
        {
            Instantiate(prefabGolem, spawn.transform.position, spawn.transform.rotation);        
        }

        estaAtacando = false;
    }
    IEnumerator tiempoAnimacionCambiarFase()
    {

        estaAtacando = true;
        yield return new WaitForSeconds(3.5f);
        estaAtacando = false;
        velocidad = 5f;
        cooldawnLanzaRoca = 3f;
        cooldawnInvocarGolems = 30f;
    }


}
