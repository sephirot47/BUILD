using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GSM : MonoBehaviour
{
    public static GameState Playing;
    public static GameState Paused;
    public static GameState Contextual;
    public static GameState Inventory;

    private static GameState currentState;
    
    public void Start()
    {
        Playing = new GameState(); Playing.SetOmniDirectional(true);
        Paused = new GameState(); Paused.SetOmniDirectional(true);
        Contextual = new GameState(); Contextual.SetOmniDirectional(true);
        Inventory = new GameState(); Inventory.SetOmniDirectional(true);

        currentState = Playing;
        Debug.Log(Playing);
    }

    public static bool CurrentStateIs(GameState state)
    {
        return currentState == state;
    }

    public static void GoTo(GameState state)
    {
        if ( CurrentStateIs(state) ) return;
        if (CanGoTo(state))
        {
            GameState lastState = currentState;
            currentState = state;
            List<IGameStateListener> gameStateListeners = GC.GetComponentsInWorldOfType<IGameStateListener>();
            foreach(IGameStateListener gsl in gameStateListeners)
            {
                gsl.OnGameStateChange(lastState, currentState);
            }
        }
    }

    public static bool CanGoTo(GameState state)
    {
        return currentState.CanGoTo(state);
    }
}
