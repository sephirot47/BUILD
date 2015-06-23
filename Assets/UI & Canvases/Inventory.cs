using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour, IGameStateListener
{
	void Start () 
    {
        CanvasUtils.Hide(gameObject);
	}
	
	void Update ()  
    {
        if (Input.GetKeyUp(KeyCode.I))
            TriggerOpenClose();
    }

    public void TriggerOpenClose()
    {
        if(GSM.CurrentStateIs(GSM.Inventory)) GSM.GoTo(GSM.Playing);
        else GSM.GoTo(GSM.Inventory);
    }

    public void OnGameStateChange(GameState previousState, GameState newState)
    {
        Debug.Log("LaLALALLALALALL");
        if (newState == GSM.Inventory)  CanvasUtils.Show(gameObject);
        else  CanvasUtils.Hide(gameObject);
    }

    public void Open()
    {
        GSM.GoTo(GSM.Inventory);
    }

    public void Close()
    {
        GSM.GoTo(GSM.Playing);
    }
}
