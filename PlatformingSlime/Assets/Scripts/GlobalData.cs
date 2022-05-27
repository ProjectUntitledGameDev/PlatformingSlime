using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    public Transform spawn;
    private void Start()
    {
        blackScreen.SetActive(true);
        fadeInOrOut = false;
        StartCoroutine(FadeToBlack(null, false, false, 0));
    }
    public void ResetPlayer(GameObject player)
    {
        fadeInOrOut = true;
        StartCoroutine(FadeToBlack(player, false, true, 0));
    }
    private float fadeSpeed = 5;
    private float fadeAmount;
    private bool fadeInOrOut;
    public GameObject blackScreen;

    public void StartLevel(int level)
    {
        fadeInOrOut = true;
        StartCoroutine(FadeToBlack(null, true, false, level));
    }

    IEnumerator FadeToBlack(GameObject player, bool loadLevel, bool respawn, int sceneIndex)
    {
        blackScreen.SetActive(true);
        Color screenColour = blackScreen.GetComponent<Image>().color;
        if (fadeInOrOut)
        {
            while (screenColour.a < 1)
            {
                fadeAmount = screenColour.a + (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            if (loadLevel)
            {
                SceneManager.LoadScene(sceneIndex);
            }
            else 
            {
                if (respawn)
                {
                    player.transform.position = spawn.position;
                    fadeInOrOut = false;
                    StartCoroutine(FadeToBlack(player, false, false, 0));
                }
            }
        }
        else
        {
            while (screenColour.a > 0)
            {
                fadeAmount = screenColour.a - (fadeSpeed * Time.deltaTime);
                screenColour = new Color(screenColour.r, screenColour.g, screenColour.b, fadeAmount);
                blackScreen.GetComponent<Image>().color = screenColour;
                yield return null;
            }
            blackScreen.SetActive(false);
        }
    }

}
