using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    public GameObject giftPrefab; // 生成するGiftオブジェクトのプレハブ
    public float spawnInterval = 2f; // Giftオブジェクトを生成する間隔
    public float spawnRangeX = 4f; // Giftオブジェクトを生成するX座標の範囲
    public string[] texts; // ランダムに表示するテキストの配列

    private float timer; // 経過時間

    private bool isSpawnEnabled = true; // giftの生成が有効かどうか

    // 毎フレーム呼び出される関数
    void Update()
    {
        // 経過時間を更新する
        timer += Time.deltaTime;

        // giftの生成が有効な場合に生成する
        if (isSpawnEnabled)
        {

            // 指定した間隔でGiftオブジェクトを生成する
            if (timer > spawnInterval)
            {
                // Giftオブジェクトを生成する位置をランダムに決定する
                float posX = Random.Range(-spawnRangeX, spawnRangeX);
                Vector3 spawnPos = new Vector3(posX, transform.position.y, 0f);

                // Giftオブジェクトを生成する
                GameObject gift = Instantiate(giftPrefab, spawnPos, Quaternion.identity);

                // Giftオブジェクト内のTextMeshProコンポーネントを取得する
                TextMesh textMeshPro = gift.GetComponentInChildren<TextMesh>();

                // TextMeshProのテキストをランダムに変更する
                if (textMeshPro != null && texts.Length > 0)
                {
                    int randomIndex = Random.Range(0, texts.Length);
                    textMeshPro.text = texts[randomIndex];
                }
                    // Rigidbody2Dコンポーネントを取得する
                Rigidbody2D rb = gift.GetComponent<Rigidbody2D>();

                // Rigidbody2Dのmassをランダムに変更する
                if (rb != null)
                {
                    rb.gravityScale = Random.Range(0.1f, 1.0f);
                }


                // 経過時間をリセットする
                timer = 0f;
            }
        }
            // GameManagerクラスのタイマーが0以下になった場合に生成を停止する
        if (GameManager.instance.timer <= 0f)
        {
            isSpawnEnabled = false;
        }
    }
    public void launching_gift(Vector3 pos)
    {
        GameObject gift = Instantiate(giftPrefab, pos + Vector3.up * 1.2f, Quaternion.identity);
        Rigidbody2D rb = gift.GetComponent<Rigidbody2D>();
        // Giftオブジェクト内のTextMeshProコンポーネントを取得する
        TextMesh textMeshPro = gift.GetComponentInChildren<TextMesh>();
        // GameManagerのインスタンスからcollectedStringを取得し、最後の文字を取得する
        char lastChar = GameManager.instance.collectedString[GameManager.instance.collectedString.Length - 1];
        // 最後の文字をテキストに設定する
        textMeshPro.text = lastChar.ToString();
        rb.gravityScale=1;

        rb.AddForce(Vector2.up * 500f);
    }
        
}