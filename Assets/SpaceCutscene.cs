using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpaceCutscene : MonoBehaviour {

    public Image img;
    public float fadeSpeed = 0.05f;
    public bool endingLevel = false;

    public void EnterShip()
    {
        GetComponent<Animator>().SetTrigger("open");
    }

    public void switchNPC()
    {
        transform.GetChild(0).GetComponent<NPCMoonCutscene>().Switch();
    }

    public void stillstandNPC()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Animator>().SetTrigger("StillStanding");
    }

    public void EndLevel()
    {
        endingLevel = true;
    }

    public void loadNext()
    {
        Application.LoadLevel(5);
    }

    void Update()
    {
        if (endingLevel)
            FadeToBlack();
    }

    void FadeToBlack()
    {
        img.color = Color.Lerp(img.color, Color.black, fadeSpeed * Time.deltaTime);
    }
}
