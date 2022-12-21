using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlashDelegate : MonoBehaviour
{

    /// <summary>ターン開始時に呼ばれるメソッド</summary>
    public static event Action OnBeginSlash;
    /// <summary>ターン終了時に呼ばれるメソッド</summary>
    public static event Action OnEndSlash;

    static bool m_isTurnStarted = false;

    /// <summary>現在のターン数</summary>
    static int m_turnCount = 1;


    /// <summary>
    /// ターン開始時に呼ぶ
    /// </summary>
    public static void BeginSlash()
    {
        OnBeginSlash();
        m_isTurnStarted = true;
    }

    /// <summary>
    /// ターン終了時に呼ぶ
    /// </summary>
    public static void EndSlash()
    {
        //// ターンを開始せずに終了した場合はまず強制的にターンを開始する
        //if (!m_isTurnStarted)
        //{
        //    BeginTurn();
        //}

        OnEndSlash();
        m_isTurnStarted = false;
    }
}
