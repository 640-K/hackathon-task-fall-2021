using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{
    public GameObject loadingScreen;
    public string targetScene;
    public Image progressBar;
    

    

    public UnityEvent onTransition;

    AsyncOperation loadSceneOperation = null;
    public void Transition()
    {
        if (loadSceneOperation != null) return;

        onTransition.Invoke();

        loadingScreen.SetActive(true);
        loadSceneOperation = SceneManager.LoadSceneAsync(targetScene);
       loadSceneOperation.allowSceneActivation = false;

        StartCoroutine(SceneProgress(loadSceneOperation));
    }


    IEnumerator SceneProgress(AsyncOperation operation)
    {
        progressBar.fillAmount = 0;
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += Mathf.Min(operation.progress / 0.9f - progressBar.fillAmount, 0.025f);
            yield return new WaitForSeconds(.025f);
        }


        operation.allowSceneActivation = true;
        Destroy(this);
    }
}
