using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody rb;

    #region Camara
    private Camera cam;
    private camara cm;
    private Vector3 camFwd;
    #endregion

    #region Movimiento
    [Range(1.0f, 10.0f)]
    public float velocidadCaminar;
    [Range(1.0f, 10.0f)]
    public float velocidadCaminarAtras;
    [Range(1.0f, 10.0f)]
    public float strafeSpeed;

    [Range(0.1f, 1.5f)]
    public float velocidadRotacion;

    [Range(2.0f, 10.0f)]
    public float fuerzaSalto;
    #endregion

    #region Animacion
    //private MyTpCharacter tpc;
    private bool caminar = false;
    private bool caminarIzquierda = false;
    private bool caminarDerecha = false;
    private bool caminarAtras = false;
    private bool saltar = false;
    #endregion

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        saltar = Input.GetButtonDown("Jump");

        camFwd = Vector3.Scale(cam.transform.forward, new Vector3(1, 1, 1)).normalized;
        Vector3 camFlatFwd = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 flatRight = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

        Vector3 m_CharForeward = Vector3.Scale(camFlatFwd, new Vector3(1, 0, 1)).normalized;
        Vector3 m_CharRight = Vector3.Scale(flatRight, new Vector3(1, 0, 1)).normalized;


        float w_speed;
        Vector3 move = Vector3.zero;
        if (cm.tipo == camara.TipoCamara.FreeLook)
        {
            w_speed = velocidadCaminar;
            move = ver * m_CharForeward * w_speed + hor * m_CharRight * velocidadCaminar;
            cam.transform.position += move * Time.deltaTime;

            
        }
        else if (true)
        {
            //w_speed = (ver > 0) = velocidadCaminar : velocidadCaminarAtras;
            //move = ver * m_CharForeward * w_speed + hor * m_CharRight * velocidadCaminar;
        }

        transform.position += move * Time.deltaTime;

        if (saltar)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        if (true)
        {

        }
        else if (true)
        {

        }
    }

    void Update()
    {
        
    }
}
