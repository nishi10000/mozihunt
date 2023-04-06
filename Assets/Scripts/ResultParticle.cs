using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultParticle : MonoBehaviour
{


    public ParticleSystem particleSystem1; // 再生するパーティクルシステム
    public float interval = 1f; // 再生間隔

    void Start()
    {
        StartCoroutine(PlayParticle());
    }

    private IEnumerator PlayParticle()
    {
        while (true)
        {
            // パーティクルを再生する
            particleSystem1.Play();

            // 次の再生まで一定時間待つ
            yield return new WaitForSeconds(interval);
        }
    }
}