using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSelection : MonoBehaviour 
{
    PlayerBuilding playerBuilding;
    Vector2 initialPoint, finalPoint;
    bool dragging;

	void Start () 
    {
        playerBuilding = GetComponent<PlayerBuilding>();
	}
	
	void Update () 
    {
        if (playerBuilding.IsBuilding() || !GSM.CurrentStateIs(GSM.Playing)) return;

        List<Buildable> allBuildables = GC.GetAll<Buildable>();
        GameObject go = GetPointingBuildable();

        if(go != null)
        {
            Buildable targetBuildable = go.GetComponent<Buildable>();
            foreach (Buildable buildable in allBuildables)
            {
                buildable.OnMouseIsOut();
                if (MouseUp())
                {
                    if (targetBuildable != null)
                    {
                        if (buildable != targetBuildable && !MultiSelectionKey()) 
                            buildable.OnClickOutside();
                        
                        if (Input.GetMouseButtonUp(2)  && MultiSelectionKey() && buildable.IsSelected()) 
                            buildable.OnMiddleClick();
                    }
                    else buildable.OnClickOutside(); //Aprieta totalmente fuera (quiere deseleccionar todo)
                }
            }

            if (targetBuildable != null)
            {
                targetBuildable.OnMouseIsOver();
                if (Input.GetMouseButtonUp(0)) targetBuildable.OnLeftClick();
                if (Input.GetMouseButtonUp(1)) targetBuildable.OnRightClick();
                if (Input.GetMouseButtonUp(2)) targetBuildable.OnMiddleClick();
            }
        }
        else
        {
            foreach (Buildable buildable in allBuildables)
            {
                buildable.OnMouseIsOut();
                if (MouseUp()) buildable.OnClickOutside();
            }
        }
	}

    private GameObject GetPointingBuildable()
    {
        int layer = (1 << LayerMask.NameToLayer("BuildZone")) | (1 << LayerMask.NameToLayer("BuildIgnore"));
    
        Ray ray = playerBuilding.GetViewRay();
        RaycastHit hit;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 999999.9f, layer)) return hit.collider.gameObject;
        return null;
    }

    private bool MouseUp()
    {
        return Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2);
    }

    private bool MultiSelectionKey()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ||
               Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }
}
