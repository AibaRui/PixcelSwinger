using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AudioData")]
public class AudioData : ScriptableObject
{
    [Header("��")]
    [SerializeField] private List<Data> _audios = new List<Data>();

    public List<Data> Audios => _audios;

    public enum AudioKinds
    {
        /// <summary>�`���[�g���A���X�e�[�W��BGM</summary>
        BGM_TutorialEria,
        /// <summary>Eria1��BGM</summary>
        BGM_Eria1,
        /// <summary>Eria2��BGM</summary>
        BGM_Eria2,
        /// <summary>Eria3��BGM</summary>
        BGM_Eria3,




        /// <summary>����</summary>
        GSE_Walk,
        /// <summary>����</summary>
        GSE_Run,
        /// <summary>�W�����v</summary>
        GSE_Jump,
        /// <summary>�㏸/summary>
        GSE_UpAir,
        /// <summary>����</summary>
        GSE_DownAir,
        /// <summary>���n</summary>
        GSE_Landing,
        /// <summary>WallRun</summary>
        GSE_WallRun,
        /// <summary>���C���[��ł�</summary>
        GSE_WairFire,
        /// <summary>���C���[Hit</summary>
        GSE_WairHit,
        /// <summary>Swing</summary>
        GSE_Swing,
        /// <summary>Grapple</summary>
        GSE_Grapple,
        /// <summary>Swing�I���</summary>
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
    [Header("������")]
    [SerializeField] private AudioClip _audioClip;

    [Header("���̎��")]
    [SerializeField] private AudioData.AudioKinds _audioKinds;

    public AudioClip AudioClip => _audioClip;

    public AudioData.AudioKinds AudioKind => _audioKinds;
}
