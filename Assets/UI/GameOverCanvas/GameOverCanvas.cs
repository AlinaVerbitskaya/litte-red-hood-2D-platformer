using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Animator))]

public class GameOverCanvas : MonoBehaviour
{
    Canvas gameOverCanvas;
    Animator canvasAnim;

    void Start()
    {
        gameOverCanvas = GetComponent<Canvas>();
        canvasAnim = GetComponent<Animator>();
        gameOverCanvas.enabled = false;
        EventManager.OnPlayerDeath += Activate;
    }

    private void Activate()
    {
        StartCoroutine(ReloadSceneOnKeyPress());
    }

    private IEnumerator ReloadSceneOnKeyPress()
    {
        gameOverCanvas.enabled = true;
        canvasAnim.SetTrigger("PlayerDeath");
        yield return new WaitForSeconds(3.4f);
        yield return new WaitUntil(() => Input.anyKeyDown);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
