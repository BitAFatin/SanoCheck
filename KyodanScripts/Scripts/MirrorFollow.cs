using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 追従管理
/// </summary>
public class MirrorFollow : MonoBehaviour
{
    #region　変数の宣言

    private Transform attachedTarget; //追従対象
    private Vector3 localPos; //ローカル位置
    private Quaternion localRot; //ローカル回転

    #endregion

    //プロパティ
    public Transform AttachedTarget { get => attachedTarget; set => attachedTarget = value; }

    /// <summary>
    /// 追従設定
    /// </summary>
    /// <param name="parent"></param>
    public void Attach(Transform parent)
    {
        //targetに追従対象のTransformを入れる
        AttachedTarget = parent;

        //追従対象に対する相対位置をlocalPosに入れる
        localPos = parent.InverseTransformPoint(transform.position);

        //追従対象に対する相対角度をlocalRotに入れる
        localRot = Quaternion.Inverse(AttachedTarget.rotation) * transform.rotation;
    }


    private void LateUpdate()
    {
        if (AttachedTarget == null) return;

        //オブジェクトの位置を追従対象の相対位置にする
        transform.position = AttachedTarget.TransformPoint(localPos);

        //オブジェクトの回転を追従対象の相対角度にする
        transform.rotation = AttachedTarget.rotation * localRot;
    }
}
