using UnityEngine;
using System.Collections.Generic;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField]
    private Level level;

    private CombatGameState combatState = null;
    private IGameState state = null;


    private void Awake()
    {
        this.combatState = new CombatGameState();
        this.combatState.SetLevelData(level);


        this.SetState(this.combatState);
    }


    private void SetState(IGameState state)
    {
        this.state?.Out();
        this.state = state;
        this.state.In();
    }
}