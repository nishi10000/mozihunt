using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int score = 0;
        
            score = StoreManager.instance.score;
        
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
    }

}
