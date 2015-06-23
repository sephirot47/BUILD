using UnityEngine;

interface IGameStateListener
{
    void OnGameStateChange(GameState previousState, GameState newState);
}
