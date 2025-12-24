using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  public List<CardData> deck = new List<CardData>();

  // カードを並べる親オブジェクト（以前と同じ）
  public Transform handParent;

  // cardPrefab変数はもう使いません

  void Start()
  {
    CreateDeck();
    ShuffleDeck();
    DealCards(5);
  }

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

  void DealCards(int count)
  {
    for (int i = 0; i < count; i++)
    {
      if (i >= deck.Count) break;
      CardData data = deck[i];

      // 1. ファイル名を作る（例: "Deck05_Heart_A"）
      string suitName = "";
      switch (data.suit)
      {
        case "♥": suitName = "Heart"; break;
        case "♦": suitName = "Diamond"; break;
        case "♣": suitName = "Club"; break;
        case "♠": suitName = "Spade"; break;
      }

      string rankName = data.rank.ToString();
      if (data.rank == 1) rankName = "A";
      else if (data.rank == 11) rankName = "J";
      else if (data.rank == 12) rankName = "Q";
      else if (data.rank == 13) rankName = "K";

      string fileName = "Deck05/Deck05_" + suitName + "_" + rankName;

      // 2. Resourcesフォルダからプレハブを読み込む
      GameObject prefab = Resources.Load<GameObject>(fileName);

      if (prefab != null)
      {
        // 3. カードを生成
        GameObject newCard = Instantiate(prefab, handParent);

        // 4. データをセット
        CardController controller = newCard.GetComponent<CardController>();
        if (controller != null)
        {
          controller.SetCard(data);
        }
      }
      else
      {
        Debug.LogError("カードが見つかりません: " + fileName);
      }
    }
  }
}