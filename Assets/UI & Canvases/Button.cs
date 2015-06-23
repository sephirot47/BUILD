using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
    public float defaultAlpha = 0.5f, overAlpha = 0.9f;
    public GameObject buildablePrefab;
    public KeyCode boundKey;
    public Inventory parentInventory;

	void Start () 
    {
        CanvasUtils.SetAlpha(gameObject, defaultAlpha);
	}
	
	void Update () 
    {
        if ( GSM.CurrentStateIs(GSM.Contextual) ) return;

        if (GSM.CurrentStateIs(GSM.Inventory) && CanvasUtils.MouseOver(gameObject))
        {
            CanvasUtils.SetAlpha(gameObject, overAlpha);
            if (Input.GetMouseButtonUp(0)) OnClick();
        }
        else
        {
            CanvasUtils.SetAlpha(gameObject, defaultAlpha);
        }

        if (Input.GetKeyUp(boundKey)) OnKey();
	}

    void OnKey()
    {
        OnClick();
        parentInventory.Close();
    }

    void OnClick()
    {
        GC.PausePhysics();
        GC.player.GetComponent<PlayerBuilding>().OnBuildableChoosed( buildablePrefab.GetComponent<Buildable>() );
        GC.inventory.TriggerOpenClose();
    }
}
