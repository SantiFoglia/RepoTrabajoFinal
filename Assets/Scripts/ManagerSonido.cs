using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSonido : MonoBehaviour
{
    public static ManagerSonido unicaInstancia;

    public AudioSource efectoSonido;
    public AudioSource musica1;
    public AudioSource musica2;
    public AudioSource musica3;

    public float rangoSonidoBajo = 0.95f;
    public float rangoSonidoAlto = 1.05f;

    private void Awake()
    {
        if (ManagerSonido.unicaInstancia == null)
        {
            ManagerSonido.unicaInstancia = this;
 
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void PlayMusica1(AudioClip clip)
    {
        musica1.clip = clip;
        musica1.Play();
    }
    public void PlayMusica2(AudioClip clip)
    {
        musica2.clip = clip;
        musica2.Play();
    }
    public void PlayMusica3(AudioClip clip)
    {
        musica3.clip = clip;
        musica3.Play();
    }
    public void StopMusica1()
    {
        musica1.Stop();
    }
    public void StopMusica2()
    {
        musica2.Stop();
    }
    public void StopMusica3()
    {
        musica3.Stop();
    }
    public void PlayEfectoSonido(AudioClip clip)
    {
        efectoSonido.clip = clip;
        efectoSonido.Play();
    }
    public void PlayEfectoSonidoRandom(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(rangoSonidoBajo, rangoSonidoAlto);
        efectoSonido.pitch = randomPitch;
        efectoSonido.clip = clips[randomIndex];
        efectoSonido.Play();
    }
}
