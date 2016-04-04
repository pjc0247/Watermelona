Watermelona
====

ObjectBased Publisher/Subscriber implementation. (for Unity)
<br>
앱 내에서 발생하는 각종 이벤트들을 다수의 게임 오브젝트에 전달할 수 있도록 해줍니다.

Usage
----
�븘�옒�쓽 �삁�젣�뒗 寃뚯엫�뿉�꽌 �뙣�궥�쓣 諛쏆븘�꽌 媛� �삤釉뚯젥�듃�뿉寃� �쟾�넚�븯怨�,<br>
媛� �삤釉뚯젥�듃�뱾��� �뙣�궥�쓣 諛쏆븘�꽌 泥섎━�븯�뒗 媛꾨떒�븳 �삁�젣瑜� 蹂댁뿬以띾땲�떎.
<br><br>
`Subscribe`瑜� 吏��젙�븯湲곕쭔 �븯硫� �옄�룞�쑝濡� 援щ룆�릺湲� �븣臾몄뿉, �뙣�궥�쓣 諛쏆븘�꽌 `switch~case`濡� ����엯�쓣 �굹�늿 �썑, �븣留욎�� �삤釉뚯젥�듃�뱾�쓣 怨⑤씪�꽌 媛곴컖 �떎�떆 肉뚮젮二쇰뒗 踰덇굅濡쒖슫 濡쒖쭅�씠 �븘�슂 �뾾�뒿�땲�떎.

__Network.cs__
```c#
void PacketReceivedFromNetwork(IPacket packet)
{
  PubSub.Publish(packet);
}
```

__GameState.cs__
```c#
// 寃뚯엫 �쟾�뿭�쟻�쑝濡� �궗�슜�븯�뒗 �긽�깭 ����옣�냼�엯�땲�떎.
// �꽩 �떆�옉/醫낅즺 �뙣�궥�쓣 諛쏆븘�꽌 �쁽�옱 �꽩 �긽�깭瑜� ����옣�빀�땲�떎.
class GameState : Watermelona.GameObject
{
  // 硫붿냼�뱶�뿉 �꽌釉뚯뒪�겕�씪�씠踰꾨�� 吏��젙�븯硫� �옄�룞�쑝濡� �벑濡앸맗�땲�떎.
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
// �늻援ъ쓽 �꽩�씤吏� �븣�젮二쇰뒗 UI �삤釉뚯젥�듃�엯�땲�떎.
// �꽩 �떆�옉/醫낅즺 �뙣�궥�쓣 諛쏆븘�꽌 UI瑜� �몴�떆�븯怨� �닲源곷땲�떎.
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
