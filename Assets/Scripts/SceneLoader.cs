using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    BoxCollider2D exitCollider;

    IEnumerator loadNextScene()
    {
        if (exitCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            yield return new WaitForSecondsRealtime(0.5f);

            // New Level with new coins will be loaded
            FindObjectOfType<ScenePersist>().destroyOutdatetScene();

            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentScene.buildIndex + 1);
            }
            else if (currentScene.buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void Start()
    {
        exitCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        StartCoroutine(loadNextScene());
    }
}
