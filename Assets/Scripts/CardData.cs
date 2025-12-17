using UnityEngine;

// [System.Serializable] をつけると、UnityのInspectorで見えるようになります
[System.Serializable]
public class CardData
{
  public string suit; // マーク (♥, ♦, ♣, ♠)
  public int rank;    // 数字 (1～13)

  // コンストラクタ（TypeScriptの constructor と同じ役割）
  public CardData(string s, int r)
  {
    suit = s;
    rank = r;
  }
}