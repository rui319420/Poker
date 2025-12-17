using System.Collections.Generic; // Listを使うために必要
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  // TypeScriptの CardData[] に相当します
  public List<CardData> deck = new List<CardData>();

  void Start()
  {
    CreateDeck();
    ShuffleDeck();

    // 確認：最初の1枚をログに出してみる
    Debug.Log("最初のカードは: " + deck[0].suit + deck[0].rank);
  }

  // 52枚のカードを作成する
  void CreateDeck()
  {
    string[] suits = { "♥", "♦", "♣", "♠" };
    deck.Clear();

    foreach (string s in suits)
    {
      for (int r = 1; r <= 13; r++)
      {
        deck.Add(new CardData(s, r));
      }
    }
  }

  // 山札をシャッフルする（TypeScriptにはないC#の定番ロジック）
  void ShuffleDeck()
  {
    for (int i = 0; i < deck.Count; i++)
    {
      CardData temp = deck[i];
      int randomIndex = Random.Range(i, deck.Count);
      deck[i] = deck[randomIndex];
      deck[randomIndex] = temp;
    }
  }
}