using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private GameObject camara;
    private Animator anim;

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


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        camara = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        tocaPiso = Physics.CheckSphere(detectaPiso.position, distanciaPiso, mascaraPiso);

        if (tocaPiso && velocity.y <0)
        {
            velocity.y = -2f;
            anim.SetBool("salto", false);
        }

        //if (!tocaPiso)
        //{
        //    anim.SetBool("salto", true);
        //}

        if (Input.GetKeyDown(KeyCode.Space) && tocaPiso)
        {

            velocity.y = Mathf.Sqrt(alturaDeSalto * -2 * gravedad);
            anim.SetBool("salto", true);

        }

        Roll();

        velocity.y += gravedad * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(hor, 0, ver).normalized;

        if(direccion.magnitude <=0) anim.SetFloat("Movimientos", 0, 0.1f, Time.deltaTime);

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

    void Roll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && tocaPiso)
        {

            anim.SetTrigger("roll");
        }


    }

}
