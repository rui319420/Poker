using UnityEngine;
using TMPro; // テキスト操作に必要

public class CardController : MonoBehaviour
{
  // TypeScriptのプロパティのようなもの
  public string suit; // マーク
  public int rank;    // 数字
  public TextMeshProUGUI cardText; // 画面上のテキスト

  // 起動時に呼ばれる
  void Start()
  {
    DisplayCard();
  }

  // 表示を更新するメソッド
  public void DisplayCard()
  {
    cardText.text = suit + "\n" + rank;
  }
}