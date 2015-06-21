using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour 
{
    public Material buildMaterial, selectMaterial, destroyMaterial;
    private Material defaultMaterial;

	void Start ()
    {
        defaultMaterial = new Material( GetComponent<MeshRenderer>().material );
	}

	void Update () 
    {
	    
	}
}
