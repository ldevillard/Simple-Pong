using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;

    void Start()
    {
        if (!PlayerPrefs.HasKey("sound"))
            PlayerPrefs.SetInt("sound", 1);

        if (PlayerPrefs.GetInt("sound") == 1)
        {
            image.sprite = sprites[0];
            AudioManager.Instance.audioSource.volume = 1;
        }
        else
        {
            image.sprite = sprites[1];
            AudioManager.Instance.audioSource.volume = 0;
        }
    }

    public void UpdateSound()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            image.sprite = sprites[1];
            AudioManager.Instance.audioSource.volume = 0;
            PlayerPrefs.SetInt("sound", 0);
        }
        else
        {
            image.sprite = sprites[0];
            AudioManager.Instance.audioSource.volume = 1;
            PlayerPrefs.SetInt("sound", 1);
        }
    }
}
