using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObstacleCollision : MonoBehaviour
{
    public Animator playerAnimator;
    public TextMeshProUGUI coinsUI, coinsGO, coinsPS;
    public int coins = 0;
    public AudioSource coinSound;
    public GameManager gm;
    public GameObject gameOver, winMenu;
    public Animator gameOverAnim, musicAnimator, winMenuAnim;
    public AudioSource loseSound, winSound, fortniteMusic, levelMusic, damageSound;
    public Button pauseButton;
    public static bool levelOver = false;
    private bool dead = false;
    private bool won = false;
    public bool replay = false;


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            if (!GameManager.paused)
            {
                playerAnimator.SetTrigger("Die");
                gm.pauseGame();
                gameOver.SetActive(true);
                gameOverAnim.SetTrigger("Show");
                musicAnimator.SetTrigger("SemiFadeOut");
                loseSound.Play();
                damageSound.Play();
                pauseButton.enabled = false;
                if (PlayerController.score > ScoreBehaviour.highScore)
                {
                    ScoreBehaviour.highScore = PlayerController.score;
                }
                CoinsBehaviour.totalCoins = CoinsBehaviour.totalCoins + coins;
            }
            else
            {
                dead = true;
            }
        }

        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            coins++;
            coinsUI.text = coins.ToString();
            coinsGO.text = coins.ToString();
            coinsPS.text = coins.ToString();
            coinSound.Play();
        }

        if (col.gameObject.tag == "Win")
        {
            if (!GameManager.paused)
            {
                levelOver = true;
                winMenu.SetActive(true);
                winMenuAnim.SetTrigger("Show");
                levelMusic.Pause();
                winSound.Play();
                Invoke("playFortniteMusic", 2f);
                pauseButton.enabled = false;
                if (PlayerController.score > ScoreBehaviour.highScore)
                {
                    ScoreBehaviour.highScore = PlayerController.score;
                }
                CoinsBehaviour.totalCoins = CoinsBehaviour.totalCoins + coins;
            }
        }
    }

    void playFortniteMusic()
    {
        fortniteMusic.Play();
    }

    void Update()
    {
        if(dead && !GameManager.paused)
        {
            if (replay)
            {
                gm.pauseGame();
            }
            playerAnimator.SetTrigger("Die");
            gameOver.SetActive(true);
            gameOverAnim.SetTrigger("Show");
            musicAnimator.SetTrigger("SemiFadeOut");
            loseSound.Play();
            damageSound.Play();
            pauseButton.enabled = false;
        }
        if (won && !GameManager.paused)
        {
            levelOver = true;
            winMenu.SetActive(true);
            winMenuAnim.SetTrigger("Show");
            levelMusic.Pause();
            winSound.Play();
            Invoke("playFortniteMusic", 2f);
            pauseButton.enabled = false;
            if (PlayerController.score > ScoreBehaviour.highScore)
            {
                ScoreBehaviour.highScore = PlayerController.score;
            }
            CoinsBehaviour.totalCoins = CoinsBehaviour.totalCoins + coins;
        }
        replay = false;
    }

    public void setReplay()
    {
        replay = true;
    }
}
