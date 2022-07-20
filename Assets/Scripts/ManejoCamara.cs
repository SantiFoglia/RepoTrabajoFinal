using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejoCamara : MonoBehaviour
{
    public Camera _cam;
    public Transform lookAt;

    public float sensibilidadCamara;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        _cam.transform.RotateAround(transform.position, transform.up, hor * 90 * sensibilidadCamara * Time.deltaTime);

        _cam.transform.LookAt(lookAt);

        Vector3 angulos = _cam.transform.rotation.eulerAngles;
        _cam.transform.rotation = Quaternion.Euler(new Vector3(angulos.x, angulos.y, 0));
    }

    
}
