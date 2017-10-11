using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RulesScript : MonoBehaviour {

    //To get from Inspector
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    public GameObject livesCounter;
    public GameObject gameOverUI;
    public Transform canvas;
    public GameObject blockPanel;
    public AudioSource audioSource;
    public AudioClip[] sfx;
    public AudioClip[] bgm;

    //Local variables
    GameObject livesCounterInstance;
    AudioSource audioSourceInstance;
    BallScript ballScript;
    bool gameOver = false;
    float livesWidth;
    float livesHeight;
    private int _lives;
    public int lives {
        get { return this._lives; }
        set { this._lives = value; LivesChanged(); }
    }

    void Start () {
        //INSTANTIATE ALL ELEMENTS
        Instantiate(player1, canvas);
        Instantiate(player2, canvas);
        livesCounterInstance = (GameObject)Instantiate(livesCounter, canvas);
        audioSourceInstance = (AudioSource)Instantiate(audioSource, canvas);
        InstantiateBall();
        //Get livesCounter dimensions
        livesWidth = livesCounter.GetComponent<RectTransform>().rect.width;
        livesHeight = livesCounter.GetComponent<RectTransform>().rect.height;
        //Set variables
        lives = 3;
        audioSourceInstance.PlayOneShot((AudioClip)Resources.Load(@"Sounds\Musics\Start01"));
    }
    
    void Update () {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(0);
            }
        }
        // Debug Cheat : return to gain a life
        if (Input.GetKeyDown(KeyCode.Return))
        {
            lives++;
        }
    }

    void LostBall() //What to do when the ball gets out of boundaries
    {
        lives --;
        BGMDeath();
        if (lives<=0)
        {
            GameOver();
        }
        else
        {
            ballScript.OnLostBall -= LostBall;
            InstantiateBall();
        }
    }

    void InstantiateBall()
    {
        Instantiate(ball, canvas);
        ballScript = FindObjectOfType<BallScript>();
        ballScript.OnLostBall += LostBall;
        ballScript.OnPlayerHit += BGMPlayerHit;
        ballScript.OnBlockHit += BGMBlockHit;
        ballScript.OnEnnemyHit += BGMEnnemyHit;
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOver = true;
    }

    void LivesChanged()
    {
        livesCounterInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(livesWidth * lives, livesHeight);
    }

    void BGMPlayerHit()
    {
        audioSourceInstance.PlayOneShot(sfx[0]);
    }
    void BGMBlockHit()
    {
        audioSourceInstance.PlayOneShot(sfx[1]);
    }
    void BGMEnnemyHit()
    {
        audioSourceInstance.PlayOneShot(sfx[2]);
    }
    void BGMDeath()
    {
        audioSourceInstance.PlayOneShot(sfx[3]);
    }
    void BGMUpgrade()
    {
        audioSourceInstance.PlayOneShot(sfx[4]);
    }
    void BGMP1UP()
    {
        audioSourceInstance.PlayOneShot(sfx[5]);
    }
}
