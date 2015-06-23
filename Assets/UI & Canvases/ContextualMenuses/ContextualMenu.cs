using UnityEngine;
using System.Collections;

public class ContextualMenu : MonoBehaviour, IGameStateListener
{
    [HideInInspector]
    public Buildable parentBuildable;

    public void Start() { }
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
        Destroy(gameObject);
        if (parentBuildable) parentBuildable.OnContextualMenuClosed();
    }

    public void OnGameStateChange(GameState previousState, GameState newState)
    {
        if (newState != GSM.Contextual) OnCloseButtonClicked();
    }
}
