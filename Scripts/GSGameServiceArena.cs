using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;

public partial class GSGameService
{
    protected override void DoGetOpponentList(string playerId, string loginToken, UnityAction<FriendListResult> onFinish)
    {
        var result = new FriendListResult();
        var request = GetGSEventRequest("GetOpponentList");
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
            }
            onFinish(result);
        });
    }

    protected override void DoStartDuel(string playerId, string loginToken, string targetPlayerId, UnityAction<StartDuelResult> onFinish)
    {
        var result = new StartDuelResult();
        var data = new GSRequestData();
        data.AddString("targetPlayerId", targetPlayerId);
        var request = GetGSEventRequest("StartDuel", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                }
                else
                {
                    var stamina = scriptData.GetGSData("stamina");
                    var session = scriptData.GetString("session");
                    var opponentCharacters = scriptData.GetGSDataList("opponentCharacters");
                    
                    result.stamina = JsonUtility.FromJson<PlayerStamina>(stamina.JSON);
                    result.session = session;
                    foreach (var entry in opponentCharacters)
                    {
                        result.opponentCharacters.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoFinishDuel(string playerId, string loginToken, string session, ushort battleResult, int deadCharacters, UnityAction<FinishDuelResult> onFinish)
    {
        var result = new FinishDuelResult();
        var data = new GSRequestData();
        data.AddString("session", session);
        data.AddNumber("battleResult", battleResult);
        data.AddNumber("deadCharacters", deadCharacters);
        var request = GetGSEventRequest("FinishDuel", data);
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null)
            {
                if (scriptData.ContainsKey("error") && !string.IsNullOrEmpty(scriptData.GetString("error")))
                {
                    result.error = scriptData.GetString("error");
                }
                else
                {
                    var rewardItems = scriptData.GetGSDataList("rewardItems");
                    var createItems = scriptData.GetGSDataList("createItems");
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");
                    var rewardPlayerExp = scriptData.GetInt("rewardPlayerExp").Value;
                    var rewardCharacterExp = scriptData.GetInt("rewardCharacterExp").Value;
                    var rewardSoftCurrency = scriptData.GetInt("rewardSoftCurrency").Value;
                    var rewardHardCurrency = scriptData.GetInt("rewardHardCurrency").Value;
                    var rating = scriptData.GetInt("rating").Value;
                    var player = scriptData.GetGSData("player");

                    foreach (var entry in rewardItems)
                    {
                        result.rewardItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    foreach (var entry in createItems)
                    {
                        result.createItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    result.deleteItemIds.AddRange(deleteItemIds);
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                    result.rewardSoftCurrency = rewardSoftCurrency;
                    result.rewardHardCurrency = rewardHardCurrency;
                    result.rating = rating;
                    result.player = JsonUtility.FromJson<Player>(player.JSON);
                }
            }
            onFinish(result);
        });
    }
}
