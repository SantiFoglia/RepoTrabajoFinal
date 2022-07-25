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

    //estadisticas
    public float velocidad;
    public float velocidadCorriendo;
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

    //flechas
    public GameObject flechaPrefab;
    public GameObject flecha;
    public Transform spawnFlecha;
    public GameObject mira;
    private float cooldownDisparo = 0.6f;
    private float tiempoRestanteParaAtacar;
    private bool puedeAtacar;

    //apuntar
    public bool estaApuntando;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        DetectarPiso();

        Roll();

        Salto();

        Movimiento();

        CooldownDisparo();

        Disparar();

        Apuntar();

        
    }

    

    void DetectarPiso()
    {
        tocaPiso = Physics.CheckSphere(detectaPiso.position, distanciaPiso, mascaraPiso);

        if (tocaPiso && velocity.y < 0)
        {
            velocity.y = -2f;
            anim.SetBool("salto", false);
        }
    }
    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && tocaPiso)
        {
            StartCoroutine(tiempoAnimacionRoll());
            _boxCollider.enabled = false;
            estaRolleando = true;
            puedeAtacar = false;
            anim.SetTrigger("roll");
        }


    }
    void Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && tocaPiso)
        {

            velocity.y = Mathf.Sqrt(alturaDeSalto * -2 * gravedad);
            anim.SetBool("salto", true);

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

        if (direccion.magnitude >= 0.1f)
        {
            //calcular nuevo angulo
            float objetivoAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + camara.transform.eulerAngles.y;
            //aplicar ese angulo suavizado en el eje y a una velocidad, un giro y un tiempo dado
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, objetivoAngulo, ref velocidadGiro, tiempoAlGirar);
            transform.rotation = Quaternion.Euler(0, angulo, 0);

            if (Input.GetKey(KeyCode.LeftControl))
            {
                //ahora se va a mover hacia el lado donde giramos
                Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                controller.Move(mover.normalized * velocidadCorriendo * Time.deltaTime);

                anim.SetFloat("Movimientos", 1f, 0.1f, Time.deltaTime);
            }
            else
            {
                //ahora se va a mover hacia el lado donde giramos
                Vector3 mover = Quaternion.Euler(0, objetivoAngulo, 0) * Vector3.forward;
                controller.Move(mover.normalized * velocidad * Time.deltaTime);

                anim.SetFloat("Movimientos", 0.5f, 0.1f, Time.deltaTime);
            }


        }
    }
    void Disparar()
    {
        if (Input.GetMouseButtonDown(0) && puedeAtacar && tocaPiso && !estaRolleando)
        {
            puedeAtacar = false;
            tiempoRestanteParaAtacar = cooldownDisparo;
            anim.SetTrigger("ataque");
            StartCoroutine(tiempoAnimacionAtaque());

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
        

        if (Input.GetKey(KeyCode.Mouse1))
        {
            float hor = Input.GetAxis("Horizontal");
            float ver = Input.GetAxis("Vertical");

            estaApuntando = true;
            anim.SetBool("apuntando", true);

            anim.SetFloat("movimientoApuntandoEjeY", ver, 0.1f, Time.deltaTime);
            anim.SetFloat("movimientoApuntandoEjeX", hor, 0.1f, Time.deltaTime);
        }
        else
        {
            estaApuntando = false;
            anim.SetBool("apuntando", false);
        }
    }

    IEnumerator tiempoAnimacionAtaque()
    {
        yield return new WaitForSeconds(0.3f);
        flecha = Instantiate(flechaPrefab, spawnFlecha.position, spawnFlecha.rotation);
        flecha.GetComponent<Rigidbody>().AddForce(spawnFlecha.forward * 50, ForceMode.Impulse);
    }

    IEnumerator tiempoAnimacionRoll()
    {
        yield return new WaitForSeconds(1.0f);
        estaRolleando = false;
        _boxCollider.enabled = true;


    }
}
