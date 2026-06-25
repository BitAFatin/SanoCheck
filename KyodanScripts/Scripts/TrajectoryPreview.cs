using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射前に弾道を予測するスクリプト
/// </summary>
public class TrajectoryPreview : MonoBehaviour
{
    #region 変数の宣言
    //予測線の最大距離
    [SerializeField] float maxDistance = 100f;
    //反射するオブジェクトのレイヤー
    [SerializeField] LayerMask reflectLayer;
    //無限ループ防止用の反射回数上限
    readonly int safetyReflectLimit = 100;

    Vector3 currentLinePos; //弾丸の位置
    Vector3 currentLineDir; //弾丸の方向

    [SerializeField] float lineWidth = 0.05f; //線の太さ

    [SerializeField] Material lineMaterial; //線のマテリアル

    private List<LineRenderer> lines = new List<LineRenderer>(); //弾道予測線用LineRendererのリスト
    private LineRenderer currentLine; //変更を加えるLineRendererを入れる変数
    #endregion

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="line"></param>
    void Initialization(LineRenderer line)
    {
        line.startWidth = lineWidth; //中心の線の太さ
        line.endWidth = lineWidth; //外側の線の太さ
        line.material = lineMaterial; //線を表示するマテリアルの設定
        line.startColor = Color.yellow; //線の中心の色
        line.endColor = Color.yellow; //線の外側の色
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward); //現在地から前方向に向かってRay（光線）を飛ばす
        DrawReflectionPath(ray.origin, ray.direction); //弾道線を引く処理を呼び出す
    }

    /// <summary>
    /// 弾道予測線を描く
    /// </summary>
    /// <param name="startPos">開始位置</param>
    /// <param name="direction">方向</param>
    void DrawReflectionPath(Vector3 startPos, Vector3 direction)
    {
        ClearLines();
        currentLine = CreateLine(); //未使用のLineRendererをcurrentLineに入れる
        currentLine.positionCount = 1; //頂点を追加
        currentLine.SetPosition(0, startPos); //現在地を始点にする

        int reflectionCount = 0; //反射回数を記録

        currentLinePos = startPos; //位置の引数をcurrentLinePosに入れる
        currentLineDir = direction; //方向の引数をcurrentLineDirに入れる

        //ループ防止上限より反射数が少ない間繰り返す
        while (reflectionCount < safetyReflectLimit)
        {
            //現在の進行方向にRayを飛ばして、反射対象に当たるか判定
            if (Physics.Raycast(currentLinePos, currentLineDir, out RaycastHit hit, maxDistance, reflectLayer))
            {
                //hitオブジェクトのWarpをgateに入れる
                Warp gate = hit.collider.GetComponentInParent<Warp>();

                if (gate != null)
                {
                    WarpTrajectory(hit, gate);

                    continue;
                }

                ReflectTrajectory(hit);

                //反射回数をカウント
                reflectionCount++;
            }
            //なににも当たらなかったら
            else
            {
                //最大距離まで線を引く
                AddLinePosition(currentLinePos + currentLineDir * maxDistance);

                break;
            }
        }

        //無限反射防止用の上限に到達
        if (reflectionCount >= safetyReflectLimit)
        {
                Debug.Log("上限です");
        }
    }

    /// <summary>
    /// 頂点を追加し線を引く
    /// </summary>
    /// <param name="point"></param>
    void AddLinePosition(Vector3 point)
    {
        currentLine.positionCount++; //頂点の追加

        //pointの地点まで線を引く
        currentLine.SetPosition(currentLine.positionCount - 1, point);
    }

    /// <summary>
    /// 弾道予測線をワープに触れた時にワープさせる
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="gate"></param>
    void WarpTrajectory(RaycastHit hit, Warp gate)
    {
        // ワープ地点を線に追加
        AddLinePosition(hit.point);

        // ワープ先へ移動
        currentLinePos = gate.GetWarpPoint().position;

        // 向きを変換
        currentLineDir = gate.GetWarpPoint().forward;

        //使うLineRendererを切り替える
        currentLine = CreateLine();

        //ワープ後の地点を追加
        currentLine.positionCount = 1;
        currentLine.SetPosition(0, currentLinePos);
    }

    /// <summary>
    /// 弾道予測線が鏡に触れた時に反射する
    /// </summary>
    /// <param name="hit"></param>
    void ReflectTrajectory(RaycastHit hit)
    {
        //反射処理用にMirrorスクリプトを取得
        Mirror mirror = hit.collider.GetComponentInParent<Mirror>();

        //反射した地点を頂点とし線を引く
        AddLinePosition(hit.point);

        //反射方向を次の進行方向として更新
        currentLineDir = mirror.GetReflectVectol(currentLineDir, hit.normal);
        currentLinePos = hit.point; //次のRay開始位置を反射地点に更新
    }

    /// <summary>
    /// 未使用のLineRendererが無かったら新しく作成し初期化したものを返す
    /// </summary>
    /// <returns></returns>
    LineRenderer CreateLine()
    {
        //未使用のLineRendererを探す
        foreach (LineRenderer line in lines)
        {
            if (!line.gameObject.activeSelf)
            {
                line.gameObject.SetActive(true);
                line.positionCount = 0;
                return line;
            }
        }

        GameObject obj = new GameObject("TrajectoryLine"); //TrajectoryLineという名前のオブジェクトを作る

        LineRenderer lineNew = obj.AddComponent<LineRenderer>(); //objにLineRendererを入れそれを変数に入れる

        Initialization(lineNew);

        lines.Add(lineNew); //リストに加える

        return lineNew;
    }

    /// <summary>
    /// リストの中身のLineRendererを非表示にする
    /// </summary>
    void ClearLines()
    {
        foreach (LineRenderer line in lines)
        {
            if(line != null)
            {
                line.gameObject.SetActive(false);
            }
        }
    }
}
