using UnityEngine;
using System.Collections;

public class Collider2dVisualizationTest : MonoBehaviour
{
    bool isOn = false;


    void Start()
    {
    }

    void Update()
    {
        // 新しく生成した当たり判定が表示できなかったので、毎フレーム削除と生成を繰り返す
        // 超重そうだけどデバッグ用の機能だしいいや
        if (isOn)
        {
            Collider2dVisualizationer.DeleteLineRenderer();
            Collider2dVisualizationer.CreateLineRenderer();
        }
    }

    /// <summary>
    /// レンダラーを表示
    /// </summary>
    public void On()
    {
        Collider2dVisualizationer.IsEnabled = true;
    }

    /// <summary>
    /// レンダラーを非表示
    /// </summary>
    public void Off()
    {
        Collider2dVisualizationer.IsEnabled = false;
    }

    /// <summary>
    /// コライダーを元にレンダラーを作成
    /// </summary>
    public void Create()
    {
        isOn = true;
        Collider2dVisualizationer.CreateLineRenderer();
    }

    /// <summary>
    /// 作成したレンダラーを削除
    /// </summary>
    public void Delete()
    {
        isOn = false;
        Collider2dVisualizationer.DeleteLineRenderer();
    }
}