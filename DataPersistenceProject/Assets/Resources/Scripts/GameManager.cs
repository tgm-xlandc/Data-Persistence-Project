using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Image blackFade;
    public GameObject rockPaperScissorsPanel;
    public GameObject revealPanel;
    public TMP_Text countdownText;
    public Image computerObjectImage;
    public Image playerObjectImage;
    public GameObject gameResultsPanel;
    public CanvasGroup gameResultsGroup;
    public TMP_Text winsCountText;
    public GameObject youLostPanel;
    public TMP_Text wonText;

    public Sprite[] sprites;

    public int winsCount;
    public int lostCount;
    public int gamesCount;
    public int winsCountFinal;
    public int selectedObjectPlayer;
    public int selectedObjectComputer;


    private void Start()
    {
        selectedObjectPlayer = -1;
        lostCount = 0;
        winsCount = 0;
        gamesCount = 0;
        winsCountFinal = 0;
        StartCoroutine(FadeBlackOut());
        StartCoroutine(GameCoroutine());
    }

    IEnumerator FadeBlackOut()
    {
        float timeElapsed = 0.0f;
        float lerpDuration = 2.0f;

        while (timeElapsed < lerpDuration)
        {
            blackFade.color = new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, Mathf.Lerp(1.0f, 0.0f, timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        blackFade.gameObject.SetActive(false);
    }

    public void SelectObject(int type)
    {
        selectedObjectPlayer = type;
        selectedObjectComputer = Random.Range(0, 3);
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator GameCoroutine()
    {
        while(true)
        {
            rockPaperScissorsPanel.SetActive(true);
            //Wait for player to select object
            while (selectedObjectPlayer == -1)
            {
                yield return null;
            }
            rockPaperScissorsPanel.SetActive(false);

            playerObjectImage.sprite = sprites[selectedObjectPlayer];
            computerObjectImage.sprite = sprites[3];

            revealPanel.SetActive(true);
            countdownText.gameObject.SetActive(true);
            //Countdown
            for (int i = 3; i >= 0; i--)
            {
                countdownText.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }
            countdownText.gameObject.SetActive(false);


            if (selectedObjectPlayer == selectedObjectComputer)
            {
                wonText.text = "It's a draw.";
                gameResultsPanel.transform.GetChild(gamesCount).GetChild(2).gameObject.SetActive(true);

            }
            else
            {
                if (selectedObjectPlayer == 0)
                {
                    if (selectedObjectComputer == 1)
                    {
                        wonText.text = "You lost.";
                        lostCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        wonText.text = "You won.";
                        winsCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(0).gameObject.SetActive(true);
                    }
                }
                else if (selectedObjectPlayer == 1)
                {
                    if (selectedObjectComputer == 0)
                    {
                        wonText.text = "You won.";
                        winsCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(0).gameObject.SetActive(true);
                    }
                    else
                    {
                        wonText.text = "You lost.";
                        lostCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(1).gameObject.SetActive(true);
                    }
                }
                else if (selectedObjectPlayer == 2)
                {
                    if (selectedObjectComputer == 0)
                    {
                        wonText.text = "You lost.";
                        lostCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        wonText.text = "You won.";
                        winsCount++;
                        gameResultsPanel.transform.GetChild(gamesCount).GetChild(0).gameObject.SetActive(true);
                    }
                }


            }
            gamesCount++;

            computerObjectImage.sprite = sprites[selectedObjectComputer];
            wonText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            revealPanel.SetActive(false);
            wonText.gameObject.SetActive(false);

            //Show game results
            gameResultsPanel.SetActive(true);

            float timeElapsed = 0.0f;
            float lerpDuration = 1.5f;

            while (timeElapsed < lerpDuration)
            {
                gameResultsGroup.alpha = Mathf.Lerp(0.0f, 1.0f, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(2.0f);

            timeElapsed = 0.0f;
            while (timeElapsed < lerpDuration)
            {
                gameResultsGroup.alpha = Mathf.Lerp(1.0f, 0.0f, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            gameResultsPanel.SetActive(false);
            selectedObjectPlayer = -1;

            if (gamesCount == 3)
            {
                for(int i = 0;i<=2;i++)
                {
                    for(int j = 0; j<=2;j++)
                    {
                        gameResultsPanel.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                    }
                }
                
                if (lostCount == 0 && winsCount == 0)
                    Debug.Log("It's a draw");
                else
                {
                    if(lostCount>winsCount)
                    {
                        //Check if highscore
                        if (winsCountFinal > PlayerPrefs.GetInt("Highscore", 0))
                        {
                            PlayerPrefs.SetInt("Highscore", winsCountFinal);
                            PlayerPrefs.SetString("HighscorePlayer",PlayerPrefs.GetString("PlayerName", "Player"));
                        }
                        youLostPanel.SetActive(true);
                        break;
                    }
                    else if (lostCount<winsCount)
                    {
                        winsCountFinal++;
                        winsCountText.text = "Wins: "+winsCountFinal.ToString();
                    }
                }

            }
        }
    }
}
