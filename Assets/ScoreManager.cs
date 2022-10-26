using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  // DOTween を使うため

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text _scoreText;

    int _score = 0;

    void Start()
    {
        _scoreText.text = _score.ToString();
    }


    void Update()
    {
        
    }





    public void SetScore(int addScore)
    {
        int tempScore = _score; // 追加前のスコア


        _score += addScore;
        if (_score >= 0)
        {
            // DOTween.To() を使って連続的に変化させる
            DOTween.To(() => tempScore, // 連続的に変化させる対象の値
                x => tempScore = x, // 変化させた値 x をどう処理するかを書く
                _score, // x をどの値まで変化させるか指示する
                0.5f)   // 何秒かけて変化させるか指示する
                .OnUpdate(() => _scoreText.text = tempScore.ToString())   // 数値が変化する度に実行する処理を書く
                .OnComplete(() => _scoreText.text = _score.ToString());   // 数値の変化が完了した時に実行する処理を書く
        }
        else
        {
            _score = 0;
        }
        _scoreText.text = _score.ToString();
    }
}

