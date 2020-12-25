using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIControl : MonoBehaviour
{
    public Scene _level;
    public bool menuIsActive = false;
    public Canvas MainCanvas;
    public Canvas DopCanvas;
    public UnityEngine.UI.Button OpenButton;
    public UnityEngine.UI.Button CloseButton;
    // Start is called before the first frame update
    void Start()
    {
        MainCanvas.gameObject.SetActive(true);
        DopCanvas.gameObject.SetActive(false);
    }
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Time.timeScale = 1;
    }
    public void CloseMenuButton()
    {
        MainCanvas.gameObject.SetActive(true);
        DopCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenMenuButton()
    {
        MainCanvas.gameObject.SetActive(false);
        DopCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
