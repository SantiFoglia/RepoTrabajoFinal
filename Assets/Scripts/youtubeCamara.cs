using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class youtubeCamara : MonoBehaviour
{
    public Transform objASeguir;
    public float velCamara = 120;
    public float sensibilidad = 150;

    private float mouseX;
    private float mouseY;
    private float rotX = 0;
    private float rotY = 0;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.y;
        rotY = rot.x;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        rotX -= mouseY * sensibilidad * Time.deltaTime;
        rotY += mouseX * sensibilidad * Time.deltaTime;

        //para que no pueda superar esa velocidad y choque la camara contra el piso
        rotX = Mathf.Clamp(rotX, -60, 60);
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        


    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objASeguir.position, velCamara * Time.deltaTime);
    }


}
