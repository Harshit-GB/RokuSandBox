using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BirdController bird;
    public PipeManager pipeManager;

    private int score = 0;
    private bool isPlaying,showgameover;
    

    void Start()
    {
        bird = new BirdController();
        pipeManager = new PipeManager();
        isPlaying = true;
        bird.Initialize();
        pipeManager.Initialize();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)&&!isPlaying)
        {
            Restart();
        }
        if (!isPlaying) return;

       
        bird.SpawnBird();
        pipeManager.Spawn();
        

        if (pipeManager.CheckCollision(bird.Position))
        {
            isPlaying = false;
            GameOver();
            Debug.Log("Game Over");
            showgameover = true;
            //OnGUI();
        }

        else
        {
            if (pipeManager.PassedPipe(bird.Position))
            {
                UpdateScore();
            }
        }
    }

    void UpdateScore()
    {
        score++;
    }

    void GameOver()
    {
        Debug.Log("Game Over"+ score);
    }

    void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnGUI()
    {
        if (isPlaying)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 64;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.red;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width/2-150, 10, 180, 40), "Score: " + score,style);
        }
        if (showgameover)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 64;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.red;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 180, 40), "Score: " + score,style);
        }
    }
}