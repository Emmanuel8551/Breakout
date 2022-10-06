using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] internal GameManager gameManager;
    [SerializeField] private List<State> states;
    [SerializeField] private State current; // Sets the first state in editor

    private void Start()
    {
        // States loading is trough editor
        InitializeStates();
        current.Enter();
    }

    private void InitializeStates()
    {
        for (int i = 0; i < states.Count; i++)
            states[i].Init();
    }

    public void Change (string name)
    {
        current.Exit();
        current = GetState(name);
        current.Enter();
    }

    private State GetState (string name)
    {
        for (int i = 0; i < states.Count; i++)
            if (states[i].name == $"{name}State") return states[i];
        return null;
    }
}

public class State : MonoBehaviour
{
    private AudioManager audioManager => stateMachine.gameManager.audioManager;
    // Q: Must be deactivated
    [SerializeField] protected StateMachine stateMachine;

    public void PlaySound (string name)
    {
        audioManager.PlaySound(name);
    }

    public virtual void Init () { }

    public virtual void Enter ()
    {
        gameObject.SetActive(true);
    }

    public virtual void Exit ()
    {
        gameObject.SetActive(false);
    }

    protected void Change (string name)
    {
        stateMachine.Change(name);
    }
}
