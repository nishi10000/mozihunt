using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public GameObject giftPrefab; // 打ち上げるGiftのプレハブ

    public TitleSpawner giftSpawner; // GiftSpawnerコンポーネント

    public float initialDelay = 3f; // 最初の遅延時間
    public float launchInterval = 2f; // 打ち上げる間隔

    private float launchTimer; // 打ち上げるタイマー
    private bool hasLaunched; // 最初の打ち上げが完了したかどうかのフラグ

    // 毎フレーム呼び出される関数
    void Update()
    {
        // 最初の遅延時間が経過していない場合は何もしない
        if (Time.time < initialDelay)
        {
            return;
        }

        // 打ち上げるタイマーを更新する
        launchTimer += Time.deltaTime;

        // 打ち上げる間隔が経過した場合、giftオブジェクトを打ち上げる
        if (launchTimer >= launchInterval)
        {
            launchTimer = 0f;
            giftSpawner.launching_gift(transform.position);
        }
    }
}