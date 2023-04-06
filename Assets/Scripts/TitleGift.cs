using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleGift : MonoBehaviour
{
    public float destroyY = -10f; // アイテムが画面外に出たと判定するY座標

    // Update is called once per frame
    void Update()
    {
                // アイテムが画面外に出た場合、削除する
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
        // プレイヤーがアイテムを取得した場合に呼び出される関数
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject); // アイテムを削除する
        }
    }
}
