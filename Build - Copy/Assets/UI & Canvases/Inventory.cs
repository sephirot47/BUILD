using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    private bool open;

	void Start () 
    {
        open = false;
        CanvasUtils.Hide(gameObject);
	}
	
	void Update () 
    {
    }

    public void TriggerOpenClose()
    {
        if (open) CanvasUtils.Hide(gameObject);
        else CanvasUtils.Show(gameObject);
        open = !open;
    }

    public bool IsOpen()
    {
        return open;
    }
}
