using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class AudioObject : MonoBehaviour
{

    [SerializeField] private float time = 0;
    [SerializeField] private bool destroyOnAwake = true;
    
    private void OnEnable()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetInt("VolumeMusic", 1);
        if (!destroyOnAwake) return;
        if(time != 0) time = GetComponent<AudioSource>().clip.length;
        else Init(time);
    }

    public void Init(float time)
    {
        StopAllCoroutines();
        this.time = time;
        Destroy();
    }

    public void Destroy()
    {
        StartCoroutine(DestroyGameObject());
    }
    
    private IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}