using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    public static StoreManager instance; // ゲームマネージャーのインスタンス
    public int score;　// スコア
    // ゲーム開始時に呼び出される関数
    private void Awake()
    {

    
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
        {
            Destroy(this.gameObject);
        }
   }
}
