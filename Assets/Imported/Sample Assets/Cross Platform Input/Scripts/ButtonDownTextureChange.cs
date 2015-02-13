using UnityEngine;

[RequireComponent(typeof(Button1))]
[RequireComponent(typeof(GUITexture))]
public class ButtonDownTextureChange : MonoBehaviour {

    private Button1 m_Button;
    private new GUITexture guiTexture;
    public Texture idleTexture;
    public Texture activeTexture;

    private bool down;

    void OnEnable() {
        m_Button = GetComponent<Button1> ();
        guiTexture = GetComponent<GUITexture> ();
    }


    void Update () {
        
        if (CrossPlatformInput.GetButtonDown (m_Button.buttonName) && !down) {
            guiTexture.texture = activeTexture;
            down = true;
        }
        if (CrossPlatformInput.GetButtonUp("NextCamera") && down)
        {
            guiTexture.texture = idleTexture;
            down = false;
        }


    }



	
}
