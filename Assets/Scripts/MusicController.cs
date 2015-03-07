using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
	public AudioClip maintheme;
    public AudioClip[] themes;
    public AudioClip[] sfx;
    public float fadeSpeed = 2f;
    public float maxVol = 0.8f;

    private int sumThemes;
    private AudioClip lastTheme;
    private AudioSource _audio;
    private AudioSource sfxSource;
    private AudioSource sfxPause;

    private bool xfade = false;
    private float audioLastTime = 0f;

    // singleton
    private static MusicController musicController;
    public static MusicController Instance()
    {
        if (!musicController)
        {
            musicController = FindObjectOfType(typeof(MusicController)) as MusicController;
            if (!musicController)
                Debug.LogError("There needs to be one active ModalPanelscript on a GameObject in your scene.");
        }

        return musicController;
    }

    void Awake()
    {
        sumThemes = 1 + themes.Length;
        _audio = audio;
        lastTheme = maintheme;
        sfxSource = transform.Find("sfx").GetComponent<AudioSource>();
        _audio.volume = maxVol;
        sfxPause = transform.Find("pause").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_audio.isPlaying)
        {
            lastTheme = _audio.clip;
            do
            {
                int tmp = Random.Range(0, sumThemes * 2);
                if (tmp == 0)
                    _audio.clip = maintheme;
                else
                    _audio.clip = themes[tmp % 3];
            } while (lastTheme == _audio.clip);

            _audio.Play();
        }

        MusicFading();
    }

    public void PlaySFX(int index)
    {
        if (index == 1)
            fadeSpeed = 5f;

        xfade = true;
        sfxSource.clip = sfx[index];
        sfxSource.Play();
        Invoke("StopSFX", sfx[index].length);
    }

    void StopSFX()
    {
        xfade = false;
    }

    void MusicFading()
    {
        if (xfade)
        {
            audio.volume = Mathf.Lerp(audio.volume, 0f, fadeSpeed * Time.deltaTime);
            sfxSource.volume = Mathf.Lerp(sfxSource.volume, maxVol, fadeSpeed * Time.deltaTime);
        }
        else
        {
            audio.volume = Mathf.Lerp(audio.volume, maxVol, fadeSpeed * Time.deltaTime);
            sfxSource.volume = Mathf.Lerp(sfxSource.volume, 0f, fadeSpeed * Time.deltaTime);
        }
    }

    /* takes care of playing the pause music
     * appropriately! */
    public void PauseUnpauseMusic(bool paused, float speed)
    {
        if (paused)
        {
            if (!sfxPause.isPlaying)
                sfxPause.Play();

            audio.volume = Mathf.Lerp(audio.volume, 0f, speed * 1 / 60);
            sfxPause.volume = Mathf.Lerp(sfxPause.volume, maxVol, speed * 1 / 60);
        }
        else
        {
            audio.volume = Mathf.Lerp(audio.volume, maxVol, speed * 1 / 60);
            sfxPause.volume = Mathf.Lerp(sfxPause.volume, 0f, speed * 1 / 60);

            if (sfxPause.isPlaying && sfxPause.volume <= 0.05f)
                sfxPause.Stop();
        }
    }
}
