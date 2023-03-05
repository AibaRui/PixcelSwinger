using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    [Header("音")]
    [SerializeField] private List<Data> _audios = new List<Data>();

    public List<Data> Audios => _audios;

    public enum AudioKinds
    {
        /// <summary>チュートリアルステージのBGM</summary>
        BGM_TutorialEria,
        /// <summary>Eria1のBGM</summary>
        BGM_Eria1,
        /// <summary>Eria2のBGM</summary>
        BGM_Eria2,
        /// <summary>Eria3のBGM</summary>
        BGM_Eria3,




        /// <summary>歩き</summary>
        GSE_Walk,
        /// <summary>走り</summary>
        GSE_Run,
        /// <summary>ジャンプ</summary>
        GSE_Jump,
        /// <summary>上昇/summary>
        GSE_UpAir,
        /// <summary>効果</summary>
        GSE_DownAir,
        /// <summary>着地</summary>
        GSE_Landing,
        /// <summary>WallRun</summary>
        GSE_WallRun,
        /// <summary>ワイヤーを打つ</summary>
        GSE_WairFire,
        /// <summary>ワイヤーHit</summary>
        GSE_WairHit,
        /// <summary>Swing</summary>
        GSE_Swing,
        /// <summary>Grapple</summary>
        GSE_Grapple,
        /// <summary>Swing終わり</summary>
        GSE_SwingEnd,



        /// <summary></summary>
        SE_Buttun,
        /// <summary></summary>
        SE_OpenInventory,
        /// <summary></summary>
        SE_CloseInventori,
        /// <summary></summary>
        SE_OnCursor,
        /// <summary></summary>
        SE_WarnigPanel,


    }

}


[System.Serializable]
public class Data
{
    [Header("流す曲")]
    [SerializeField] private AudioClip _audioClip;

    [Header("音の種類")]
    [SerializeField] private AudioData.AudioKinds _audioKinds;

    public AudioClip AudioClip => _audioClip;

    public AudioData.AudioKinds AudioKind => _audioKinds;
}
