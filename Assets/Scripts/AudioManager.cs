using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{    // シングルトンパターンで実装する
        public static AudioManager instance;
        public AudioSource[] audioSources; // オーディオソースの配列
        private void Awake()
    {
        // シングルトンパターンで実装するための処理
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // SFXを再生する
    public void PlaySFX(int sourceIndex)
    {
        if ( sourceIndex < audioSources.Length)
        {
            
            audioSources[sourceIndex].Play();
        }
    }
}
