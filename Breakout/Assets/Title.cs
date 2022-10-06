using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title : State
{
    [SerializeField] private GameObject canvas;
    private enum button { play, highscores }
    private button current;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            ToggleButton();
        if (Input.GetKeyDown(KeyCode.Space))
            if (current == button.play)
                stateMachine.Change("Play");
    }

    public override void Enter()
    {
        base.Enter();
        canvas.SetActive(true);
        SetButton(button.play);
    }

    public override void Exit()
    {
        base.Exit();
        canvas.SetActive(false);
    }

    private void ToggleButton ()
    {
        if (current == button.play) ToggleButton(button.highscores);
        else if (current == button.highscores) ToggleButton(button.play);
    }

    private void ToggleButton (button value)
    {
        SetButton(value);
        stateMachine.gameManager.audioManager.PlaySound("paddle_hit");
    }

    private void SetButton (button value)
    {
        current = value;
        UpdateButtonsUI();
    }

    private void UpdateButtonsUI ()
    {
        // Q: Canvas is active
        Text play = canvas.transform.Find("Play").GetComponent<Text>();
        Text highscores = canvas.transform.Find("Highscores").GetComponent<Text>();
        play.color = current == button.play ? Color.cyan : Color.white;
        highscores.color = current == button.highscores ? Color.cyan : Color.white;
    }

}
