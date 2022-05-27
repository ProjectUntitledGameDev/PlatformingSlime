using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour
{
    public Transform spawn;

    public void ResetPlayer(GameObject player)
    {
        fadeInOrOut = true;
        StartCoroutine(FadeToBlack(player, false, 0));
    }
    private float fadeSpeed = 5;
    private float fadeAmount;
    private bool fadeInOrOut;
    public GameObject blackScreen;
    IEnumerator FadeToBlack(GameObject player, bool loadLevel, int sceneIndex)
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
                player.transform.position = spawn.position;
                fadeInOrOut = false;
                StartCoroutine(FadeToBlack(player, false, 0));
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
