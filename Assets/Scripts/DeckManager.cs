using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
  public List<CardData> deck = new List<CardData>();

  // カードを並べる親オブジェクト（Canvasの外に出した Hand を指定）
  public Transform handParent;

  void Start()
  {
    CreateDeck();
    ShuffleDeck();
    DealCards(5);
  }

  // ボタンから呼び出す関数
  public void OnClickShuffleButton()
  {
    // 安全対策：Handが設定されていなければ何もしない
    if (handParent == null) return;

    // 画面にあるカード情報を取得
    CardController[] currentCards = handParent.GetComponentsInChildren<CardController>();

    // まだカードがない場合は普通に配る
    if (currentCards.Length == 0)
    {
      CreateDeck();
      ShuffleDeck();
      DealCards(5);
      return;
    }

    System.Array.Sort(currentCards, (a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

    // --- ここから交換（ドロー）処理 ---

    // 1. デッキをリセットしてシャッフル
    CreateDeck();

    // 2. 「HOLD」しているカードと同じものは山札から抜いておく
    foreach (var card in currentCards)
    {
      // 親オブジェクト自体を間違って取得していたら無視
      if (card.transform == handParent) continue;

      if (card.isSelected)
      {
        deck.RemoveAll(d => d.suit == card.myData.suit && d.rank == card.myData.rank);
      }
    }
    ShuffleDeck();

    // 3. 選択されていないカードを交換する
    float spacing = 2.5f; // カードの間隔
    int cardIndex = 0;    // 何枚目のカードか

    for (int i = 0; i < currentCards.Length; i++)
    {
      // 親オブジェクトならスキップ
      if (currentCards[i].transform == handParent) continue;

      // 選択されていない（isSelected == false）なら交換
      if (!currentCards[i].isSelected)
      {
        // 古いカードを削除
        Destroy(currentCards[i].gameObject);

        // 新しいカードを山札から引く
        if (deck.Count > 0)
        {
          CardData nextCardData = deck[0];
          deck.RemoveAt(0);

          // ファイル名作成
          string suitName = "";
          switch (nextCardData.suit)
          {
            case "♥": suitName = "Heart"; break;
            case "♦": suitName = "Diamond"; break;
            case "♣": suitName = "Club"; break;
            case "♠": suitName = "Spade"; break;
          }
          string rankName = nextCardData.rank.ToString();
          if (nextCardData.rank == 1) rankName = "A";
          else if (nextCardData.rank == 11) rankName = "J";
          else if (nextCardData.rank == 12) rankName = "Q";
          else if (nextCardData.rank == 13) rankName = "K";

          string fileName = "Deck05/Deck05_" + suitName + "_" + rankName;
          GameObject prefab = Resources.Load<GameObject>(fileName);

          if (prefab != null)
          {
            // 新しいカードを生成
            GameObject newCard = Instantiate(prefab, handParent);

            // 位置をセット（左から順に並べる）
            // 5枚固定として計算: (カード番号 - 2) * 間隔
            float xPos = (cardIndex - 2.0f) * spacing;
            newCard.transform.localPosition = new Vector3(xPos, 0, 0);

            newCard.GetComponent<CardController>().SetCard(nextCardData);
          }
        }
      }
      else
      {
        // 選択されているカードはそのまま
        // 念のため位置だけ再計算して整列させても良いですが、そのままでもOK
        // 次回の操作のために選択状態を解除したくない場合はこのまま
        // （もし「交換したら選択解除」したいなら下の一行のコメントを外す）
        // currentCards[i].isSelected = false; 
        // currentCards[i].transform.localPosition -= Vector3.up * 0.5f; 
      }

      cardIndex++;
    }
  }

  void CreateDeck()
  {
    string[] suits = { "♥", "♦", "♣", "♠" };
    deck.Clear();
    foreach (string s in suits)
    {
      for (int r = 1; r <= 13; r++) deck.Add(new CardData(s, r));
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
    float spacing = 2.5f;
    for (int i = 0; i < count; i++)
    {
      if (i >= deck.Count) break;
      CardData data = deck[i];
      string suitName = "";
      switch (data.suit) { case "♥": suitName = "Heart"; break; case "♦": suitName = "Diamond"; break; case "♣": suitName = "Club"; break; case "♠": suitName = "Spade"; break; }
      string rankName = data.rank.ToString();
      if (data.rank == 1) rankName = "A"; else if (data.rank == 11) rankName = "J"; else if (data.rank == 12) rankName = "Q"; else if (data.rank == 13) rankName = "K";
      string fileName = "Deck05/Deck05_" + suitName + "_" + rankName;
      GameObject prefab = Resources.Load<GameObject>(fileName);

      if (prefab != null)
      {
        GameObject newCard = Instantiate(prefab, handParent);
        float xPos = (i - (count - 1) / 2.0f) * spacing;
        newCard.transform.localPosition = new Vector3(xPos, 0, 0);
        CardController controller = newCard.GetComponent<CardController>();
        if (controller != null) controller.SetCard(data);
      }
    }
  }
}