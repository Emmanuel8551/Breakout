using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : State
{
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball ball;
    [SerializeField] private Ball.Color bcolor;
    [SerializeField] private Paddle.Color pcolor;
    [SerializeField] private Paddle.Size size;
    [SerializeField] private GameObject pauseScreen;
    private bool paused = false;

    public override void Init()
    {
        base.Init();
        ball.SetSkin(bcolor);
        paddle.SetSkin(pcolor, size);
        ball.Serve(Random.Range(-10, 10), Random.Range(3, 4));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePause();

    }

    private void TogglePause ()
    {
        TogglePause(!paused);
    }

    private void TogglePause(bool _paused)
    {
        paused = _paused;
        paddle.enabled = !paused;
        ball.enabled = !paused;
        pauseScreen.SetActive(paused);
    }

    public override void Enter()
    {
        base.Enter();
        ball.gameObject.SetActive(true);
        paddle.gameObject.SetActive(true);
        LevelMaker.CreateMap();
    }

    public override void Exit()
    {
        base.Exit();
        ball.gameObject.SetActive(false);
        paddle.gameObject.SetActive(false);
    }
}

