using System.Collections;
using System.Collections.Generic;

public class GameState
{
    private List<GameState> adjacent;
    private bool omni;

    public GameState()
    {
        adjacent = new List<GameState>();
    }

    public void AddTransition(GameState gs)
    {
        adjacent.Add(gs);
    }

    public bool CanGoTo(GameState gs)
    {
        return omni || adjacent.Contains(gs);
    }

    public void SetOmniDirectional(bool omni)
    {
        this.omni = omni;
    }
}
