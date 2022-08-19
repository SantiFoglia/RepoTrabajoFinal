using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcEjercicio : MonoBehaviour
{
    public PostProcessVolume volumen;
    private Bloom _bloom;

    void Start()
    {
        volumen.profile.TryGetSettings(out _bloom);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _bloom.intensity.value = 100;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            _bloom.intensity.value = 0;
        }
    }
}
