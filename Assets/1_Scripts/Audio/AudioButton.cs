using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    private int volumeMusic;
    private Image _image;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    void Start()
    {
        _image = GetComponent<Image>();
        volumeMusic = PlayerPrefs.GetInt("VolumeMusic", 1);
        UpdateUI();
    }

    public void ChangeMusicVolume()
    {
        volumeMusic = volumeMusic == 0 ? 1 : 0;
        PlayerPrefs.SetInt("VolumeMusic", volumeMusic);
        UpdateUI();
    }

    private void UpdateUI()
    {
        _image.sprite = volumeMusic == 0 ? _spriteOff : _spriteOn;
    }
    
}
