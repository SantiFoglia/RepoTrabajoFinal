using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoObjeto", menuName ="Objeto")]
public class Objetos : ScriptableObject
{
    public string nombreObjeto;
    public string descripcionObjeto;
    public Sprite dibujoObjeto;
    
    public void MostrarData()
    {

    }

}
