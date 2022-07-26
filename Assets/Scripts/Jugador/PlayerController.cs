using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject camara;
    private Animator anim;
    private BoxCollider _boxCollider;

    //InputTeclas
    Dictionary<string, KeyCode> inputTeclas;

    //estadisticas
    public float velocidad;
    float velocidadFija;
    public float velocidadCorriendo;
    public float velocidadSaltando;
    public float alturaDeSalto;
    public float tiempoAlGirar;

    //datos piso
    public Transform detectaPiso;
    public float distanciaPiso;
    public LayerMask mascaraPiso;

    float velocidadGiro;
    float gravedad = -9.81f;
    Vector3 velocity;
    public bool tocaPiso;
    public bool estaRolleando;
    public bool estaCorriendo;
    public bool estaSaltando;

    //flechas
    public GameObject flechaPrefab;
    public GameObject flecha;
    public Transform spawnFlecha;
    public GameObject mira;
    private float cooldownDisparo = 0.6f;
    private float tiempoRestanteParaAtacar;
    private bool puedeAtacar;
    public bool estaAtacando;

    //apuntar
    public bool estaApuntando = false;
    CamaraController controladorCamara;
    public LayerMask LayerMaskObjAApuntar;
    public Collider[] arrayEnemigos;
    public int objAApuntar = 0;

    //costosStamina
    float costoDisparar = 10f;
    float costoCorrer = 0.01f;
    float costoRollear = 20f;
    float costoSaltar = 20f;
    float costoPoder = 0.2f;

    //pausar
    private bool pausaActivada;
    public GameObject menuPausa;
    public GameObject menuControles;

    //sonidos
    public AudioClip audioRoll;
    public AudioClip audioDispararFlecha;
    public AudioClip audioSaltar;
    public AudioClip audioPasos;

    //poder
    public bool poderActivado = false;
    public ParticleSystem auraPoder;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        CrearinputTeclas();
        velocidadFija = velocidad;
    }

    private void Update()
    {
        DetectarPiso();

        Roll();

        Salto();

        CooldownDisparo();

        Disparar();

        if (Jugador.poderAlmaGolem)
        {
            ActivarPoder();
        }

        UsarPocion();

        Apuntar();
        

        TogglePause();
    
        
    }

    private void FixedUpdate()
    {
        PoderActivado();
        Movimiento();
    }

    void CrearinputTeclas()
    {
        inputTeclas = new Dictionary<string, KeyCode>();

        inputTeclas.Add("saltar", KeyCode.Space);
        inputTeclas.Add("rollear", KeyCode.LeftControl);
        inputTeclas.Add("correr", KeyCode.LeftShift);
        inputTeclas.Add("disparar", KeyCode.Mouse0);
        inputTeclas.Add("apuntar", KeyCode.Mouse1);
        inputTeclas.Add("cambiarObjD", KeyCode.E);
        inputTeclas.Add("cambiarObjI", KeyCode.Q);
        inputTeclas.Add("pausa", KeyCode.Escape);
        inputTeclas.Add("poder", KeyCode.F);
        inputTeclas.Add("usarPocion", KeyCode.R);
    }
    void DetectarPiso()
    {
        tocaPiso = Physics.CheckSphere(detectaPiso.position, distanciaPiso, mascaraPiso);

        if (tocaPiso && velocity.y < 0)
        {
            velocity.y = -2f;
            anim.SetBool("salto", false);

        }

        if (!tocaPiso)
        {
            estaSaltando = true;
            velocidad = velocidadSaltando;

        }
        else
        {
            estaSaltando = false;
            velocidad = velocidadFija;
        }
    }
    void Roll()
    {
        if (Input.GetKeyDown(inputTeclas["rollear"]) && tocaPiso && Jugador.stamina>costoRollear)
        {
            StartCoroutine(tiempoAnimacionRoll());
            Jugador.jugadorInvulnerable = true;
            _boxCollider.enabled = false;
            estaRolleando = true;
            puedeAtacar = false;
            anim.SetTrigger("roll");

            ManagerSonido.unicaInstancia.PlayEfectoSonido(audioRoll);

            Jugador.stamina -= costoRollear;
        }


    }
    void Salto()
    {
        if (Input.GetKeyDown(inputTeclas["saltar"]) && tocaPiso && Jugador.stamina > costoSaltar && !estaRolleando)
        {
            velocity.y = Mathf.Sqrt(alturaDeSalto * -2 * gravedad);
            anim.SetBool("salto", true);

            ManagerSonido.unicaInstancia.PlayEfectoSonido(audioSaltar);

            Jugador.stamina -= costoSaltar;

        }
    }
    void Movimiento()
    {
        velocity.y += gravedad * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(hor, 0, ver).normalized;

        if (direccion.magnitude <= 0) anim.SetFloat("Movimientos", 0, 0.1f, Time.deltaTime);
        {

            if (direccion.magnitude >= 0.1f)
            {
                //calcular nuevo angulo
                float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.transform.eulerAngles.y;
                //aplicar ese angulo suavizado en el eje y a una velocidad, un giro y un tiempo dado
                float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, objetivoAngulo, ref velocidadGiro, tiempoAlGirar);
                transform.rotation = Quaternion.Euler(0, angulo, 0);

                if (Input.GetKey(inputTeclas["correr"]) && !estaApuntando && Jugador.stamina > costoCorrer && !estaAtacando)
                {
                    //ahora se va a mover hacia el lado donde giramos
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * velocidadCorriendo * Time.deltaTime);

                    anim.SetFloat("Movimientos", 1f, 0.1f, Time.deltaTime);


                    estaCorriendo = true;
                    Jugador.stamina -= costoCorrer;
                    

                }
                else if (!estaAtacando)
                {
                    //ahora se va a mover hacia el lado donde giramos
                    Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                    controller.Move(mover.normalized * velocidad * Time.deltaTime);

                    anim.SetFloat("Movimientos", 0.5f, 0.1f, Time.deltaTime);

                    estaCorriendo = false;
                }


            }
        }
        
    }
    void Disparar()
    {
        if (Input.GetKeyDown(inputTeclas["disparar"]) && puedeAtacar && tocaPiso && !estaRolleando && Jugador.stamina > costoDisparar)
        {
            puedeAtacar = false;
            estaAtacando = true;
            tiempoRestanteParaAtacar = cooldownDisparo;
            anim.SetTrigger("ataque");
            StartCoroutine(tiempoAnimacionAtaque());
            Jugador.stamina -= costoDisparar;
            ManagerSonido.unicaInstancia.PlayEfectoSonido(audioDispararFlecha);

        }
    }
    void CooldownDisparo()
    {
        tiempoRestanteParaAtacar -= Time.deltaTime;
        if (tiempoRestanteParaAtacar <= 0)
        {
            puedeAtacar = true;
        }
    }
    void Apuntar()
    {


        if (Input.GetKeyDown(inputTeclas["apuntar"]) && !Enemigos.enemigoMuriendo)
        {
            arrayEnemigos = Physics.OverlapSphere(transform.position, 40f, LayerMaskObjAApuntar);

            velocidad -= 3;

        }
        

        if (Input.GetKey(inputTeclas["apuntar"]) && !Enemigos.enemigoMuriendo)
        {

            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            estaApuntando = true;
            anim.SetBool("apuntando", true);

            anim.SetFloat("movimientoApuntandoEjeY", ver, 0.1f, Time.deltaTime);
            anim.SetFloat("movimientoApuntandoEjeX", hor, 0.1f, Time.deltaTime);

            if (Input.GetKeyDown(inputTeclas["cambiarObjD"]))
            {
                objAApuntar++;
                
                if (objAApuntar == arrayEnemigos.Length)
                {
                    objAApuntar = 0;
                }
            }

            if (Input.GetKeyDown(inputTeclas["cambiarObjI"]))
            {
                objAApuntar--;
                
                if (objAApuntar < 0)
                {
                    objAApuntar = arrayEnemigos.Length - 1;
                }
            }


        }
        else
        {
            estaApuntando = false;
            anim.SetBool("apuntando", false);
        }
        if (Input.GetKey(inputTeclas["apuntar"]) && Enemigos.enemigoMuriendo)
        {
            arrayEnemigos = Physics.OverlapSphere(transform.position, 40f, LayerMaskObjAApuntar);
        }

        if (Input.GetKeyUp(inputTeclas["apuntar"]) && !Enemigos.enemigoMuriendo)
        {
            arrayEnemigos = Physics.OverlapSphere(transform.position, 40f, LayerMaskObjAApuntar);

            velocidad += 3;

            objAApuntar = 0;
        }
        
    }
    void ActivarPoder()
    {
        if (Input.GetKeyDown(inputTeclas["poder"]) && !poderActivado && Jugador.mana > 0)
        {
            auraPoder.Play();
            poderActivado = true;

        }
        else if (Input.GetKeyDown(inputTeclas["poder"]) && poderActivado || Jugador.mana <= 0)
        {
            auraPoder.Stop();
            poderActivado = false;
        }

        
    }
    void PoderActivado()
    {
        if (poderActivado)
        {
            Flecha.da�o = 20;
            Jugador.mana -= costoPoder;
        }
        else
        {
            Flecha.da�o = 10;
        }
    }
    void UsarPocion()
    {
        if (Input.GetKeyDown(inputTeclas["usarPocion"]) && Jugador.pocionesVida > 0 && Jugador.vida < Jugador.vidaMax)
        {
            Jugador.pocionesVida -= 1;

            if (Jugador.vida >= Jugador.vidaMax-50)
            {
                Jugador.vida = Jugador.vidaMax;
            }
            else
            {
                Jugador.vida += 50;
            }
        }
    }
    public void TogglePause()
    {
        if (Input.GetKeyDown(inputTeclas["pausa"]))
        {
            if (pausaActivada)
            {
                menuPausa.SetActive(false);
                menuControles.SetActive(false);
                Time.timeScale = 1;
                pausaActivada = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                menuPausa.SetActive(true);
                Time.timeScale = 0;
                pausaActivada = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }

    IEnumerator tiempoAnimacionAtaque()
    {
        yield return new WaitForSeconds(0.4f);
        flecha = Instantiate(flechaPrefab, spawnFlecha.position, spawnFlecha.rotation);
        flecha.GetComponent<Rigidbody>().AddForce(spawnFlecha.forward * 50, ForceMode.Impulse);
        estaAtacando = false;

    }

    IEnumerator tiempoAnimacionRoll()
    {
        yield return new WaitForSeconds(1.0f);
        Jugador.jugadorInvulnerable = false;
        estaRolleando = false;
        _boxCollider.enabled = true;


    }
}
