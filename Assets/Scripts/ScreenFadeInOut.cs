using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;
    private bool sceneEnding = false;
    private Image img;
    private int sceneToEndTo; // is set to -1 to QUIT

    void Awake()
    {
        img = GetComponent<Image>();
        img.color = Color.black;
    }

    void Update()
    {
        if (sceneStarting)
        {
            StartScene();
        }
        if (sceneEnding)
        {
            EndScene();
        }
    }

    void FadeToClear()
    {
        img.color = Color.Lerp(img.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        img.color = Color.Lerp(img.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if (img.color.a <= 0.05f)
        {
            img.color = Color.clear;
            img.enabled = false;
            sceneStarting = false;
        }
    }

    void EndScene()
    {
        FadeToBlack();

        if (img.color.a >= 0.95f)
        {
            if (sceneToEndTo < 0)
                Application.Quit();
            Application.LoadLevel(sceneToEndTo);
        }

        Debug.Log("Fading out");
    }

    public void StartEndScene(int Scene)
    {
        sceneToEndTo = Scene;
        sceneEnding = true;
        sceneStarting = false;
        img.enabled = true;

        Debug.Log("Button clicked");
    }
}
