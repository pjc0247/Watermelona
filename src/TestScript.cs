using UnityEngine;
using System.Collections;

public class TestScript : Watermelona.GameObject {
    public class LoginPacket
    {
        public string id { get; set; }
        public bool result { get; set; }
    }
    public class MovePacket
    {
        public string id { get; set; }
        public Vector2 position { get; set; }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 400, 400));

        if (GUILayout.Button("Publish"))
            Publish();

        GUILayout.EndArea();
    }

    void Publish()
    {
        Watermelona.PubSub.Publish(new LoginPacket()
        {
            id = "pjc0247",
            result = true
        });
    }

    [Watermelona.Subscriber(typeof(LoginPacket))]
    public void OnLoginResponse(LoginPacket packet)
    {
        Debug.Log("OnLoginResponse");
        Debug.Log(packet.id + " / " + packet.result.ToString());
    }

    [Watermelona.Subscriber(typeof(MovePacket))]
    public void OnMoveNoti(MovePacket packet)
    {
        Debug.Log("OnMoveNoti");
        Debug.Log(packet.id + " / " + packet.position.ToString());
    }
}
