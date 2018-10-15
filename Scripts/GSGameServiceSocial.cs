using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GSGameService
{
    protected override void DoGetHelperList(string playerId, string loginToken, UnityAction<FriendListResult> onFinish)
    {
        var result = new FriendListResult();
        var request = GetGSEventRequest("GetHelperList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetGSDataList("list");
                foreach (var entry in list)
                {
                    result.list.Add(JsonUtility.FromJson<Player>(entry.JSON));
                }
                onFinish(result);
            }
        });
    }

    protected override void DoGetFriendList(string playerId, string loginToken, UnityAction<FriendListResult> onFinish)
    {
        var result = new FriendListResult();
        var request = GetGSEventRequest("GetFriendList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetGSDataList("list");
                foreach (var entry in list)
                {
                    result.list.Add(JsonUtility.FromJson<Player>(entry.JSON));
                }
                onFinish(result);
            }
        });
    }

    protected override void DoGetFriendRequestList(string playerId, string loginToken, UnityAction<FriendListResult> onFinish)
    {
        var result = new FriendListResult();
        var request = GetGSEventRequest("GetFriendRequestList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetGSDataList("list");
                // TODO: Set list data
                onFinish(result);
            }
        });
    }

    protected override void DoFriendRequest(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        var data = new GSRequestData();
        data.AddString("targetPlayerId", targetPlayerId);
        var request = GetGSEventRequest("FriendRequest", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                    onFinish(result);
                }
                else
                {
                    onFinish(result);
                }
            }
        });
    }

    protected override void DoFriendAccept(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new PlayerResult();
        var data = new GSRequestData();
        data.AddString("targetPlayerId", targetPlayerId);
        var request = GetGSEventRequest("FriendAccept", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                    onFinish(result);
                }
                else
                {
                    var player = scriptData.GetGSData("player");
                    var resultPlayer = new Player();
                    Player.CloneTo(JsonUtility.FromJson<DbPlayer>(player.JSON), resultPlayer);
                    result.player = resultPlayer;
                    onFinish(result);
                }
            }
        });
    }

    protected override void DoFriendDecline(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new PlayerResult();
        var data = new GSRequestData();
        data.AddString("targetPlayerId", targetPlayerId);
        var request = GetGSEventRequest("FriendDecline", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                    onFinish(result);
                }
                else
                {
                    var player = scriptData.GetGSData("player");
                    var resultPlayer = new Player();
                    Player.CloneTo(JsonUtility.FromJson<DbPlayer>(player.JSON), resultPlayer);
                    result.player = resultPlayer;
                    onFinish(result);
                }
            }
        });
    }

    protected override void DoFriendDelete(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new PlayerResult();
        var data = new GSRequestData();
        data.AddString("targetPlayerId", targetPlayerId);
        var request = GetGSEventRequest("FriendDelete", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                    onFinish(result);
                }
                else
                {
                    var player = scriptData.GetGSData("player");
                    var resultPlayer = new Player();
                    Player.CloneTo(JsonUtility.FromJson<DbPlayer>(player.JSON), resultPlayer);
                    result.player = resultPlayer;
                    onFinish(result);
                }
            }
        });
    }
}
