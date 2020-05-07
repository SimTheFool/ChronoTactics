using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField]
    private Level level = null;

    private CombatGameState combatState = null;
    private MenuGameState menuState = null;
    private IGameState state = null;


    void Awake()
    {
        this.combatState = new CombatGameState(this);
        this.menuState = new MenuGameState(this);

        this.SetCombatState();
    }

    void Update()
    {
        this.state?.Process();
    }

    private void SetState(IGameState state)
    {
        this.state?.Out();
        this.state = state;
        this.state?.In();
    }

    public void SetCombatState()
    {
        this.combatState.SetLevelData(this.level);
        this.SetState(this.combatState);
    }

    public void SetMenuState()
    {
        this.SetState(this.menuState);
    }
}