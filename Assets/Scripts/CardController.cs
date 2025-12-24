using UnityEngine;
// テキスト表示の機能は削除します

public class CardController : MonoBehaviour
{
  // カードデータを保持しておく（後でゲームの判定などに使うため）
  public CardData myData;

  public void SetCard(CardData data)
  {
    myData = data;
    // 絵柄はプレハブですでに決まっているので、表示を変える処理は不要です
  }
}