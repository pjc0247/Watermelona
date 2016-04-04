Watermelona
====

ObjectBased Publisher/Subscriber implementation. (for Unity)
<br>

Usage
----
아래의 예제는 게임에서 패킷을 받아서 각 오브젝트에게 전송하고,<br>
각 오브젝트들은 패킷을 받아서 처리하는 간단한 예제를 보여줍니다.
<br><br>
`Subscribe`를 지정하기만 하면 자동으로 구독되기 때문에, 패킷을 받아서 `switch~case`로 타입을 나눈 후, 알맞은 오브젝트들을 골라서 각각 다시 뿌려주는 번거로운 로직이 필요 없습니다.

__Network.cs__
```c#
void PacketReceivedFromNetwork(IPacket packet)
{
  PubSub.Publish(packet);
}
```

__GameState.cs__
```c#
// 게임 전역적으로 사용하는 상태 저장소입니다.
// 턴 시작/종료 패킷을 받아서 현재 턴 상태를 저장합니다.
class GameState : Watermelona.GameObject
{
  // 메소드에 서브스크라이버를 지정하면 자동으로 등록됩니다.
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
// 누구의 턴인지 알려주는 UI 오브젝트입니다.
// 턴 시작/종료 패킷을 받아서 UI를 표시하고 숨깁니다.
class TurnIndicatorUI : Watermelona.GameObject
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
