using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_Text highscorePlayerText;
    public TMP_Text highscoreText;
    public TMP_InputField playerNameInputField;
    public Image blackFade;
    public bool isFading;

    
    void Start()
    {
        isFading = false;

        int highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = highscore.ToString();

        string playerName = PlayerPrefs.GetString("HighscorePlayer", "Player");
        highscorePlayerText.text = playerName;
    }

 


    public void OnClickPlay()
    {
        if(isFading == false && playerNameInputField.text!=string.Empty)
            StartCoroutine(OnClickPlayCoroutine());
    }

    public IEnumerator OnClickPlayCoroutine()
    {
        isFading = true;
        PlayerPrefs.SetString("PlayerName", playerNameInputField.text);

        blackFade.gameObject.SetActive(true);

        float timeElapsed=0.0f;
        float lerpDuration = 2.0f;

        while (timeElapsed < lerpDuration)
        {
            blackFade.color = new Color(blackFade.color.r, blackFade.color.g, blackFade.color.b, Mathf.Lerp(0.0f,1.0f,timeElapsed/lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene("Game");
    }
}
