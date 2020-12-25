using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DopSceneManager : MonoBehaviour
{
    public VideoPlayer vid;
    bool hide = false;
    public string sceneName;
    public CanvasGroup canG;
    float t = 0;
    void Start() { vid.loopPointReached += CheckOver; canG = GetComponent<CanvasGroup>(); }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenue");
        hide = true;
    }
    private void Update()
    {
        if (Input.anyKey) hide = true; //scip logo and load menu
        if (hide)
        {
            t += Time.deltaTime;
            canG.alpha = Mathf.MoveTowards(1, 0, t);
            if (canG.alpha == 0) { UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName); Destroy(this); }
        }
    }

}
