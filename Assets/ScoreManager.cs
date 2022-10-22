using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;  // DOTween を使うため

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }





//   public void SetScore()
//    {
//        int tempScore = _player2Score; // 追加前のスコア


//        _player2Score += addScore;
//        if (_player2Score >= 0)
//        {
//            // DOTween.To() を使って連続的に変化させる
//            DOTween.To(() => tempScore, // 連続的に変化させる対象の値
//                x => tempScore = x, // 変化させた値 x をどう処理するかを書く
//                _player2Score, // x をどの値まで変化させるか指示する
//                0.5f)   // 何秒かけて変化させるか指示する
//                .OnUpdate(() => _scoreP2.text = tempScore.ToString())   // 数値が変化する度に実行する処理を書く
//                .OnComplete(() => _scoreP2.text = _player2Score.ToString());   // 数値の変化が完了した時に実行する処理を書く
//        }
//        else
//        {
//            _player2Score = 0;
//        }
//        _scoreP2.text = _player2Score.ToString();
//    }
//}
}
