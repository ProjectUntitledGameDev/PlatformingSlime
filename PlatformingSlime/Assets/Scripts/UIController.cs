using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public TextMeshProUGUI levelTxt;
    public int levelIndex;
    public GameObject panel;
    private GlobalData globalData;
    void Start()
    {
        globalData = GameObject.FindGameObjectWithTag("GlobalData").GetComponent<GlobalData>();
        if(levelTxt != null)
        {
            levelTxt.text = "Level " + levelIndex.ToString();
        }
    }

    public void OpenLevel()
    {
        globalData.StartLevel(levelIndex);
    }

    public void OpenOrClose(bool oOc)
    {
        panel.SetActive(oOc);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
