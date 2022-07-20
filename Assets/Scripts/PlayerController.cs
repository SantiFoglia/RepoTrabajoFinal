using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    Vector3 cameraForeward;

    Vector2 m_Movement;
    Vector2 m_Camera;

    public float speed = 10f;
    public float speedRotacion = 100f;

    protected float m_AngleDiff;
    protected Quaternion m_TargetRotation;

    

    protected bool IsMoveInput
    {
        get { return !Mathf.Approximately(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).sqrMagnitude, 0f); }
    }

    //Quaternion targetRotacion;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        m_Camera.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //Movimiento();
    }

    private void FixedUpdate()
    {
        Movimiento();
        SetTargetRotation();
        if (IsMoveInput)
        {
           UpdateOrientation();
        }


        
    }

    

    void Movimiento()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(hor, 0, ver);

        cameraForeward = Vector3.Scale(cam.transform.forward, new Vector3(1, 1, 1).normalized);

        Vector3 camaraPlanaForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 planoDerecha = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);

        Vector3 mForward = Vector3.Scale(camaraPlanaForward, new Vector3(1, 0, 1)).normalized;
        Vector3 mDerecha = Vector3.Scale(planoDerecha, new Vector3(1, 0, 1)).normalized;

        Vector3 move = Vector3.zero;
        move = ver * mForward * speed + hor * mDerecha * speed;
        //cam.transform.position += move * Time.deltaTime;

        //transform.position += move * Time.deltaTime;

        rb.velocity = Vector3.Lerp(Vector3.zero, move, speed);

        ///Vector3 mousePos = Input.mousePosition;

        ///Ray mira = cam.ScreenPointToRay(mousePos);

        ///transform.LookAt(mousePos);

        ////Vector3 localInput = new Vector3(input.x, 0, input.y);
        ////targetRotacion = Quaternion.RotateTowards(transform.rotation, targetRotacion, speedRotacion * Time.deltaTime);
        ////transform.rotation = targetRotacion;


        //transform.Translate(move * Time.deltaTime);

        /////Quaternion rotacion = Quaternion.EulerAngles(input);
        /////Vector3 localInput = new Vector3(0, Input.GetAxis("Mouse X") * 30, 0);
        /////transform.Rotate(localInput);
        
        
    }

    void SetTargetRotation()
    {
        Vector2 moveInput = m_Movement;
        Vector3 localMovementDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        Vector3 forward = Quaternion.Euler(0f,0f , 0f) * Vector3.forward;
        forward.y = 0f;
        forward.Normalize();

        Quaternion targetRotation;

        if (Mathf.Approximately(Vector3.Dot(localMovementDirection, Vector3.forward), -1.0f))
        {
            targetRotation = Quaternion.LookRotation(-forward);
        }
        else
        {
            Quaternion cameraToInputOffset = Quaternion.FromToRotation(Vector3.forward, localMovementDirection);
            targetRotation = Quaternion.LookRotation(cameraToInputOffset * forward);
        }

        Vector3 resultingForward = targetRotation * Vector3.forward;

        float angleCurrent = Mathf.Atan2(transform.forward.x, transform.forward.z) * Mathf.Rad2Deg;
        float targetAngle = Mathf.Atan2(resultingForward.x, resultingForward.z) * Mathf.Rad2Deg;

        m_AngleDiff = Mathf.DeltaAngle(angleCurrent, targetAngle);
        m_TargetRotation = targetRotation;
    }

    void UpdateOrientation()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 localInput = new Vector3(moveInput.x, 0f, moveInput.y);
        
        m_TargetRotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, speedRotacion * Time.deltaTime);

        transform.rotation = m_TargetRotation;
    }
}
