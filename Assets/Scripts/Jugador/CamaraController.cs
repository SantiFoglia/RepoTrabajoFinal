using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform mira;
    public float velCamara = 120;
    public float sensibilidad = 150;

    private float mouseX;
    private float mouseY;
    private float rotX = 0;
    private float rotY = 0;

    PlayerController _playerController;

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.y;
        rotY = rot.x;
        _playerController = FindObjectOfType<PlayerController>();
        
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

        int objetivo = _playerController.objAApuntar;

        if (_playerController.estaApuntando && !Enemigos.enemigoMuriendo)
        {
            for (int i = 0; i < _playerController.arrayEnemigos.Length; i++)
            {
                if (_playerController.arrayEnemigos[i].CompareTag("Enemy") && !Enemigos.enemigoMuriendo)
                {
                    
                    transform.LookAt(_playerController.arrayEnemigos[objetivo].transform);
                    if (!_playerController.estaRolleando && !Enemigos.enemigoMuriendo)
                    {
                        _playerController.gameObject.transform.LookAt(_playerController.arrayEnemigos[objetivo].transform);
                    }

                }
            }
        }
    }

    private void LateUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, mira.position, velCamara * Time.deltaTime);

    }


}
