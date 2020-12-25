using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    public GameObject _loadingScreen;
    public GameObject _mainCanvas;
    public string _levelName;
    // Start is called before the first frame update
    void Start()
    {
        _loadingScreen.SetActive(false);
        _mainCanvas.SetActive(true);
    }
    public void Load()
    {
        _mainCanvas.SetActive(false);
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_levelName);
        while (asyncLoad.isDone)
        {
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
