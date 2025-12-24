using UnityEngine;

public class CardController : MonoBehaviour
{
  public CardData myData; // カードのデータ（マークと数字）

  // 選択されているかどうかのフラグ（trueならHOLD状態）
  public bool isSelected = false;

  public void SetCard(CardData data)
  {
    myData = data;
    // 初期化時は選択状態を解除
    isSelected = false;
  }

  // ★重要：マウスでクリックされた時の処理
  // これが動くためには、プレハブに「Box Collider 2D」が必要です！
  void OnMouseDown()
  {
    // コンソールに文字を出してテスト（動いたら消してもOK）
    Debug.Log("クリックされました！: " + myData.suit + myData.rank);

    // 選択状態を反転（ONならOFFに、OFFならONに）
    isSelected = !isSelected;

    // 見た目の変化：選択されたら少し上にずらす
    if (isSelected)
    {
      transform.localPosition += Vector3.up * 0.5f; // 上に0.5動かす
    }
    else
    {
      transform.localPosition -= Vector3.up * 0.5f; // 元に戻す
    }
  }
}