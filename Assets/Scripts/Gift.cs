using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public int point = 10; // アイテムのポイント
    public float destroyY = -10f; // アイテムが画面外に出たと判定するY座標

    private TextMesh textMesh; // TextMeshコンポーネント

    // 文字とオーディオクリップの対応を定義するDictionary
    [SerializeField] private Dictionary<char, int> audioDict = new Dictionary<char, int>()
    {
        {'あ', 0}, // ここに文字とオーディオクリップの対応を追加する
        {'り', 1},
        {'が', 2},
        {'と', 3},
        {'う', 4},
        {'い', 5},
        {'ね', 6},
    };


    private void Start()
    {
        // Giftオブジェクト内のTextMeshProコンポーネントを取得する
        textMesh = GetComponentInChildren<TextMesh>();
        
        textMesh.gameObject.GetComponent<MeshRenderer>().sortingOrder = 2;

    }
// プレイヤーがアイテムを取得した場合に呼び出される関数
void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        GameManager.instance.AddScore(point); // スコアを加算する
        
        if (textMesh != null)
        {
            string text = textMesh.text;
            
            GameManager.instance.AddCollectedText(text);
            
            // 文字に対応するオーディオクリップを再生する
            foreach (char c in text)
            {
                if (audioDict.ContainsKey(c))
                {
                    //Debug.Log(audioDict[c]);
                    AudioManager.instance.PlaySFX(audioDict[c]);
                }
            }
        }
        
        Destroy(gameObject); // アイテムを削除する
    }
}

    // 毎フレーム呼び出される関数
    void Update()
    {
        // アイテムが画面外に出た場合、削除する
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}