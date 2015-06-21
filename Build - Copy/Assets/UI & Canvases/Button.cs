using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public float defaultAlpha = 0.5f, overAlpha = 0.9f;
    public GameObject buildablePrefab;

	void Start () 
    {
        CanvasUtils.Show(gameObject, defaultAlpha);
	}
	
	void Update () 
    {
	    if(CanvasUtils.MouseOver(gameObject))
        {
            CanvasUtils.Show(gameObject, overAlpha);
            if (Input.GetMouseButtonUp(0)) OnClick();
        }
        else
        {
            CanvasUtils.Show(gameObject, defaultAlpha);
        }
	}

    void OnClick()
    {
        GameObject.Instantiate(buildablePrefab, GC.player.transform.position, Quaternion.identity);
    }
}
