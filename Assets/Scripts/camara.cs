using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara : MonoBehaviour
{
    public Camera _cam;

    public enum TipoCamara { FreeLook, Locked}

    public TipoCamara tipo = TipoCamara.FreeLook;

    [Range(0.1f, 2.0f)]
    public float sensibilidad;
    public bool invertXAxis;
    public bool invertYAxis;

    public Transform lookAt;

    //#region Camera Transitions
    bool enTransicion;
    EstatusCamara EmpezarEstatus;
    EstatusCamara TerminarEstatus;
    float tiempoTransicion = 0.0f;

    public Transform aimCam;

    public struct EstatusCamara
    {
        public Vector3 posicion;
        public Vector3 rotacion;
        public Transform lookAt;
        public float time;
    }

    private void Awake()
    {
        if (tipo == TipoCamara.Locked)
        {
            _cam.transform.parent = transform;
        }
    }

    private void FixedUpdate()
    {
        if (!enTransicion)
        {
            float hor = Input.GetAxis("Mouse X");
            float ver = Input.GetAxis("Mouse Y");

            hor = (invertXAxis) ? (-hor) : hor;
            ver = (invertYAxis) ? (-ver) : ver;

            if (hor != 0)
            {
                if(tipo == TipoCamara.Locked)
                {
                    transform.Rotate(Vector3.up, hor * 90 * sensibilidad * Time.deltaTime);
                }
                else if (tipo == TipoCamara.FreeLook)
                {
                    _cam.transform.RotateAround(transform.position, transform.up, hor * 90 * sensibilidad * Time.deltaTime);
                }
            }

            if (ver != 0)
            {
                _cam.transform.RotateAround(transform.position, transform.right, ver * 90 * sensibilidad * Time.deltaTime);
            }

            _cam.transform.LookAt(lookAt);

            Vector3 ea = _cam.transform.rotation.eulerAngles;
            _cam.transform.rotation = Quaternion.Euler(new Vector3(ea.x, ea.y, 0));

        }
        else
        {
            float t = (Time.time - EmpezarEstatus.time) / (TerminarEstatus.time - EmpezarEstatus.time);
            _cam.transform.position = Vector3.Lerp(EmpezarEstatus.posicion, TerminarEstatus.posicion, t);
            _cam.transform.eulerAngles = Vector3.Lerp(EmpezarEstatus.rotacion, TerminarEstatus.rotacion, t);

            _cam.transform.LookAt(TerminarEstatus.lookAt);
            if (t>=1)
            {
                enTransicion = false;
            }
        }
    }

    public void TransicionHacia(Vector3 posicionFinal, Vector3 RotacionFinal, Transform finalLookAt, float duracion)
    {
        EmpezarEstatus.posicion = _cam.transform.position;
        EmpezarEstatus.rotacion = _cam.transform.rotation.eulerAngles;
        EmpezarEstatus.lookAt = lookAt;
        EmpezarEstatus.time = Time.time;

        TerminarEstatus.posicion = posicionFinal;
        TerminarEstatus.rotacion = RotacionFinal;
        TerminarEstatus.lookAt = finalLookAt;
        TerminarEstatus.time = EmpezarEstatus.time + duracion;

        tiempoTransicion = duracion;
        enTransicion = true;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TransicionHacia(aimCam.position, aimCam.rotation.eulerAngles, lookAt, 1.5f);
        }
    }
}
