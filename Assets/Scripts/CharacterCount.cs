using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCount : MonoBehaviour
{
    public ParticleSystem GoalparticleSystem; // 再生するパーティクルシステム
    [SerializeField] private Dictionary<string, int> pointDict = new Dictionary<string, int>()
    {
        {"いいね", 300},
        {"ありがとう", 600},
    };
    public SpriteRenderer positiveSpriteRenderer;
    public SpriteRenderer negativeSpriteRenderer;

    void Start()
    {
        positiveSpriteRenderer.enabled = false;
        negativeSpriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
    {
        bool isPositive = false; // 初期値をfalseに設定

        foreach (KeyValuePair<string, int> pair in pointDict)
        {
            string targetString = pair.Key;
            int point = pair.Value;

            if (GameManager.instance.collectedString.Contains(targetString))
            {
                int count = GameManager.instance.collectedString.Split(targetString).Length - 1;
                GameManager.instance.AddScore(count * point);
                GoalparticleSystem.Play();
                isPositive = true; // 条件を満たす場合にtrueに設定
            }
        }

        // 条件を満たす場合のみPositivePlaySound()を呼び出す
        if (isPositive)
        {
            PositivePlaySound();
            positiveSpriteRenderer.enabled = true; // positiveSpriteRendererを表示する
            negativeSpriteRenderer.enabled = false; // negativeSpriteRendererを非表示にする
            // 3秒後にスプライトを非表示にする
            Invoke("HideSprite", 2f);
        } else {
            NegativePlaySound();
            positiveSpriteRenderer.enabled = false; // positiveSpriteRendererを非表示にする
            negativeSpriteRenderer.enabled = true; // negativeSpriteRendererを表示する
            // 3秒後にスプライトを非表示にする
            Invoke("HideSprite", 2f);
        }

        GameManager.instance.ClearCollectedText();
    }
}
    private void HideSprite()
    {
        positiveSpriteRenderer.enabled = false;
        negativeSpriteRenderer.enabled = false;
    }
    public AudioSource positiveAudioSource;
    public AudioSource negativeAudioSource;


    public void PositivePlaySound()
    {
        positiveAudioSource.Play();
    }
    public void NegativePlaySound()
    {
        negativeAudioSource.Play();
    }
}