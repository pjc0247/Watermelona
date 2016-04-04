Watermelona
====

ObjectBased Publisher/Subscriber implementation. (for Unity)
<br>

Usage
----
�Ʒ��� ������ ���ӿ��� ��Ŷ�� �޾Ƽ� �� ������Ʈ���� �����ϰ�,<br>
�� ������Ʈ���� ��Ŷ�� �޾Ƽ� ó���ϴ� ������ ������ �����ݴϴ�.
<br><br>
`Subscribe`�� �����ϱ⸸ �ϸ� �ڵ����� �����Ǳ� ������, ��Ŷ�� �޾Ƽ� `switch~case`�� Ÿ���� ���� ��, �˸��� ������Ʈ���� ��� ���� �ٽ� �ѷ��ִ� ���ŷο� ������ �ʿ� �����ϴ�.

__Network.cs__
```c#
void PacketReceivedFromNetwork(IPacket packet)
{
  PubSub.Publish(packet);
}
```

__GameState.cs__
```c#
// ���� ���������� ����ϴ� ���� ������Դϴ�.
// �� ����/���� ��Ŷ�� �޾Ƽ� ���� �� ���¸� �����մϴ�.
class GameState : Watermelon.GameObject
{
  // �޼ҵ忡 ���꽺ũ���̹��� �����ϸ� �ڵ����� ��ϵ˴ϴ�.
  [Subscriber(typeof(TurnStart))]
  public void OnTurnStart(TurnStart packet)
  {
    turnState = packet.turnState;
  }

  [Subscriber(typeof(TurnEnd))]
  public void OnTurnEnd(TurnEnd packet)
  {
    turnState = TurnState.None;
  }
}
```

__TurnIndicatorUI.cs__
```c#
// ������ ������ �˷��ִ� UI ������Ʈ�Դϴ�.
// �� ����/���� ��Ŷ�� �޾Ƽ� UI�� ǥ���ϰ� ����ϴ�.
class TurnIndicatorUI : Watermelon.GameObject
{
  [Subscriber(typeof(TurnStart))]
  public void OnTurnStart(TurnStart packet)
  {
    turnText.FadeIn();
  }
  [Subscriber(typeof(TurnEnd))]
  public void OnTurnEnd(TurnEnd packet)
  {
    turnText.FadeOut();
  }
}
```
