using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    public int daño = 10;
    Rigidbody rb;
    Quaternion rotar;
    public float potenciaFinal;
    PlayerController Jugador;

    private void Start()
    {
        rotar = Quaternion.Euler(90, 0, 0);
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotar, 0.5f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().isTrigger = true;

    }

}
