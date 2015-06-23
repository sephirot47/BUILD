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
        if (GC.cMenu) return;

        if (open) CanvasUtils.Hide(gameObject);
        else
        {
            if(GC.cMenu) GC.cMenu.OnCloseButtonClicked();
            CanvasUtils.Show(gameObject);
        }

        open = !open;
    }

    public bool IsOpen()
    {
        return open;
    }
}
