using UnityEngine;
using TMPro;

public class CardController : MonoBehaviour
{
  public TextMeshProUGUI cardText;

  // CardDataを受け取って表示を更新するメソッド
  public void SetCard(CardData data)
  {
    // データの数字とマークをテキストに反映
    cardText.text = data.suit + "\n" + data.rank;

    // もしマークが赤系なら文字色を変える、といった処理もここでできます
    if (data.suit == "♥" || data.suit == "♦")
    {
      cardText.color = Color.red;
    }
    else
    {
      cardText.color = Color.black;
    }
  }
}