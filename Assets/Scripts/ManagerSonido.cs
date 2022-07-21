using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSonido : MonoBehaviour
{
    public static ManagerSonido unicaInstancia;

    AudioSource _audioSource;

    private void Awake()
    {
        if (ManagerSonido.unicaInstancia == null)
        {
            ManagerSonido.unicaInstancia = this;
            DontDestroyOnLoad(gameObject);

            _audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
