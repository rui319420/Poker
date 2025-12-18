using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  public List<CardData> deck = new List<CardData>();

  // 画面上のカード（CardController）をインスペクターから紐付けます
  public CardController targetCard;

  void Start()
  {
    CreateDeck();
    ShuffleDeck();
    DeckCards(5);

    // 山札の0番目（1枚目）のデータを、画面のカードにセットする
    if (deck.Count > 0)
    {
      targetCard.SetCard(deck[0]);
    }
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

  void DealCards(int count)
  {
    for (int i = 0; i < count; i++)
    {
      // 山札の先頭からデータを取得
      CardData data = deck[i];

      // プレハブを生成
      GameObject newCard = Instantiate(cardPrefab, handParent);

      // 生成したカードの表示を更新
      CardController controller = newCard.GetComponent<CardController>();
      if (controller != null)
      {
        controller.suit = data.suit;
        controller.rank = data.rank;
        controller.DisplayCard();
      }
    }
  }
}