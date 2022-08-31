using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Jefe : Enemigos
{
    [SerializeField] private UnityEvent MuerteJefe;

    public List<Transform> spawnsRocas;
    public List<Transform> spawnsGolems;
    public Transform spawnPelota;
    public GameObject prefabRoca;
    public GameObject prefabGolem;
    public GameObject prefabPelota;
    GameObject Roca;

    public ParticleSystem Lluvia;
    public ParticleSystem Rayos;
    public ParticleSystem Aura;

    public Material Tormenta;
    public Material Cielo;

    public AudioClip musicaTormenta;
    public AudioClip musicaTormenta2;
    public AudioClip musicaVictoria;

    public Light luz;
    public Light luzRoja;

    int vidaMax;
    int fase = 1;
    bool jefeMuerto = false;

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
        vidaMax = vida;
        nombre = "Jefe";
        velocidad = 3;
        rangoVision = 100f;
        rangoAtaqueBasico = 3f;
        dañoContacto = 10;
        anim = GetComponent<Animator>();
        _jugador = GameObject.FindGameObjectWithTag("Player");

        Temporizador();
    }

    private void Update()
    {
        detectarJugador();
        mirarJugador();
        seguirJugador();
        Temporizador();
        AtaqueBasico();
        InvocarGolems();
        AtaqueEspecial();
        CambioFase();

        if (fase==2)
        {
            OscurecerEscenario();
            AumentarTamaño();
            RenderSettings.skybox = Tormenta;
        }

        if (vida <=0 && !jefeMuerto)
        {
            MuerteJefe.Invoke();
            enemigoMuriendo = true;
            RenderSettings.skybox = Cielo;
            Lluvia.Stop();
            Rayos.Stop();
            EsclarecerEscenario();
            anim.SetTrigger("Morir");
            jefeMuerto = true;          
            
            StartCoroutine(tiempoAnimacionMuerte());
            
        }

        if (vida <= 0 && jefeMuerto)
        {
            ManagerSonido.unicaInstancia.musica1.volume -= Time.deltaTime / 15;
            ManagerSonido.unicaInstancia.musica2.volume -= Time.deltaTime / 20;
            ManagerSonido.unicaInstancia.musica3.volume -= Time.deltaTime / 20;
        }

    }

    public override void detectarJugador()
    {
        base.detectarJugador();
    }
    public override void mirarJugador()
    {
        if (jugadorCerca && !jefeMuerto)
        {
            base.mirarJugador();
        }

    }
    public override void seguirJugador()
    {
        if (jugadorCerca && !jefeMuerto)
        {
            base.seguirJugador();
            anim.SetFloat("Movimiento", 1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Movimiento", 0f, 0.1f, Time.deltaTime);
        }

    }
    void Temporizador()
    {
        tiempoParaLanzarRoca -= Time.deltaTime;
        tiempoParaInvocarGolems -= Time.deltaTime;
        tiempoParaLanzarPelota -= Time.deltaTime;
    }
    public override void AtaqueBasico()
    {
        if (jugadorCerca && tiempoParaLanzarRoca <= 0 && !estaRangoCerca && !estaAtacando && !jefeMuerto)
        {
            anim.SetTrigger("AtaquePiedras");

            tiempoParaLanzarRoca = cooldawnLanzaRoca;

            StartCoroutine(tiempoAnimacionLanzarRoca());
        }
    }
    public void InvocarGolems()
    {
        if (jugadorCerca && tiempoParaInvocarGolems <= 0 && !estaRangoCerca && !estaAtacando && !jefeMuerto)
        {
            anim.SetTrigger("InvocarGolems");

            tiempoParaInvocarGolems = cooldawnInvocarGolems;

            StartCoroutine(tiempoAnimacionInvocarGolems());
        }
    }
    public override void AtaqueEspecial()
    {
        if (jugadorCerca && tiempoParaLanzarPelota <= 0 && !estaRangoCerca && !estaAtacando && fase ==2 && !jefeMuerto)
        {
            anim.SetTrigger("InvocarPelota");

            tiempoParaLanzarPelota = cooldawnLanzaPelota;

            StartCoroutine(tiempoAnimacionLanzarPelota());
        }
    }
    void CambioFase()
    {
        if (fase == 1 && (vidaMax/vida) >= 2)
        {
            fase = 2;
            velocidad = 0;
            anim.SetTrigger("CambiarFase");

            ManagerSonido.unicaInstancia.PlayMusica2(musicaTormenta);
            ManagerSonido.unicaInstancia.PlayMusica3(musicaTormenta2);

            StartCoroutine(tiempoAnimacionCambiarFase());
        }
    }
    void OscurecerEscenario()
    {
        if (luz.intensity >= 0.4f)
        {
            luz.intensity -= Time.deltaTime/3;
        }
        
    }
    void EsclarecerEscenario()
    {
        if (luz.intensity <= 1f)
        {
            luz.intensity += Time.deltaTime / 3;
        }
    }
    void AumentarTamaño()
    {
        if (transform.localScale.x <= 4f)
        {
            transform.localScale += new Vector3(Time.deltaTime/5, Time.deltaTime/5, Time.deltaTime/5);
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
        yield return new WaitForSeconds(3f);

        Instantiate(prefabPelota, spawnPelota.transform.position, spawnPelota.transform.rotation);


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
        tiempoParaLanzarRoca = cooldawnLanzaRoca;
        tiempoParaInvocarGolems = cooldawnInvocarGolems;
        tiempoParaLanzarPelota = 5f;

        Lluvia.Play();
        Rayos.Play();
        Aura.Play();
        luzRoja.enabled = true;

    }
    IEnumerator tiempoAnimacionMuerte()
    {
        yield return new WaitForSeconds(3f);
        
        
        ManagerSonido.unicaInstancia.StopMusica2();
        ManagerSonido.unicaInstancia.StopMusica3();
        ManagerSonido.unicaInstancia.StopMusica1();
        ManagerSonido.unicaInstancia.musica1.volume = 0.1f;
        //ManagerSonido.unicaInstancia.PlayMusica1(musicaVictoria);
        ManagerSonido.unicaInstancia.PlayEfectoSonido(musicaVictoria);

        Destroy(gameObject,10);
    }

}
