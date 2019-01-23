using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;

public partial class GSGameService
{
    protected override void DoStartStage(string playerId, string loginToken, string stageDataId, UnityAction<StartStageResult> onFinish)
    {
        var result = new StartStageResult();
        var data = new GSRequestData();
        data.AddString("stageDataId", stageDataId);
        var request = GetGSEventRequest("StartStage", data);
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
                    
                    result.stamina = JsonUtility.FromJson<PlayerStamina>(stamina.JSON);
                    result.session = session;
                }
            }
            onFinish(result);
        });
    }

    protected override void DoFinishStage(string playerId, string loginToken, string session, ushort battleResult, int deadCharacters, UnityAction<FinishStageResult> onFinish)
    {
        var result = new FinishStageResult();
        var data = new GSRequestData();
        data.AddString("session", session);
        data.AddNumber("battleResult", battleResult);
        data.AddNumber("deadCharacters", deadCharacters);
        var request = GetGSEventRequest("FinishStage", data);
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
                    var rating = scriptData.GetInt("rating").Value;
                    var clearStage = scriptData.GetGSData("clearStage");
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
                    result.rewardPlayerExp = rewardPlayerExp;
                    result.rewardCharacterExp = rewardCharacterExp;
                    result.rewardSoftCurrency = rewardSoftCurrency;
                    result.rating = rating;
                    result.clearStage = JsonUtility.FromJson<PlayerClearStage>(clearStage.JSON);
                    result.player = JsonUtility.FromJson<Player>(player.JSON);
                }
            }
            onFinish(result);
        });
    }

    protected override void DoReviveCharacters(string playerId, string loginToken, UnityAction<CurrencyResult> onFinish)
    {
        var result = new CurrencyResult();
        var request = GetGSEventRequest("ReviveCharacters");
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
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoSelectFormation(string playerId, string loginToken, string formationName, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var data = new GSRequestData();
        data.AddString("formationName", formationName);
        var request = GetGSEventRequest("SelectFormation", data);
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
                    var player = scriptData.GetGSData("player");
                    result.player = JsonUtility.FromJson<Player>(player.JSON);
                }
            }
            onFinish(result);
        });
    }

    protected override void DoSetFormation(string playerId, string loginToken, string characterId, string formationName, int position, UnityAction<FormationListResult> onFinish)
    {
        var result = new FormationListResult();
        var data = new GSRequestData();
        data.AddString("characterId", characterId);
        data.AddString("formationName", formationName);
        data.AddNumber("position", position);
        var request = GetGSEventRequest("SetFormation", data);
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
                    var list = scriptData.GetGSDataList("list");
                    foreach (var entry in list)
                    {
                        result.list.Add(JsonUtility.FromJson<PlayerFormation>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }
}
