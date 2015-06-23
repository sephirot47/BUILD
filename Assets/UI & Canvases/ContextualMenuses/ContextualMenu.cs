using UnityEngine;
using System.Collections;

public class ContextualMenu : MonoBehaviour 
{
    [HideInInspector]
    public Buildable parentBuildable;

    public void Start() { GC.cMenu = this; }
	public void Update () 
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            OnCloseButtonClicked();
        }

        /*
        if (Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(0))
        {
            if (!CanvasUtils.MouseOver(gameObject)) OnCloseButtonClicked();
        }
        */
    }

    public void OnCloseButtonClicked()
    {
        GC.cMenu = null;
        Destroy(gameObject);
        if (parentBuildable) parentBuildable.OnContextualMenuClosed();
    }
}
