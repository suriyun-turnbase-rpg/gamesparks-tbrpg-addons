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
                    onFinish(result);
                }
                else
                {
                    var stamina = scriptData.GetGSData("stamina");
                    var session = scriptData.GetString("session");

                    var resultStamina = new PlayerStamina();
                    PlayerStamina.CloneTo(JsonUtility.FromJson<DbPlayerStamina>(stamina.JSON), resultStamina);
                    result.stamina = resultStamina;
                    result.session = session;
                    onFinish(result);
                }
            }
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
                    onFinish(result);
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
                        var resultEntry = new PlayerItem();
                        PlayerItem.CloneTo(JsonUtility.FromJson<DbPlayerItem>(entry.JSON), resultEntry);
                        result.rewardItems.Add(resultEntry);
                    }
                    foreach (var entry in createItems)
                    {
                        var resultEntry = new PlayerItem();
                        PlayerItem.CloneTo(JsonUtility.FromJson<DbPlayerItem>(entry.JSON), resultEntry);
                        result.createItems.Add(resultEntry);
                    }
                    foreach (var entry in updateItems)
                    {
                        var resultEntry = new PlayerItem();
                        PlayerItem.CloneTo(JsonUtility.FromJson<DbPlayerItem>(entry.JSON), resultEntry);
                        result.updateItems.Add(resultEntry);
                    }
                    result.deleteItemIds.AddRange(deleteItemIds);
                    foreach (var entry in updateCurrencies)
                    {
                        var resultEntry = new PlayerCurrency();
                        PlayerCurrency.CloneTo(JsonUtility.FromJson<DbPlayerCurrency>(entry.JSON), resultEntry);
                        result.updateCurrencies.Add(resultEntry);
                    }
                    result.rewardPlayerExp = rewardPlayerExp;
                    result.rewardCharacterExp = rewardCharacterExp;
                    result.rewardSoftCurrency = rewardSoftCurrency;
                    result.rating = rating;
                    var resultClearedStage = new PlayerClearStage();
                    PlayerClearStage.CloneTo(JsonUtility.FromJson<DbPlayerClearStage>(clearStage.JSON), resultClearedStage);
                    result.clearStage = resultClearedStage;
                    var resultPlayer = new Player();
                    Player.CloneTo(JsonUtility.FromJson<DbPlayer>(player.JSON), resultPlayer);
                    result.player = resultPlayer;
                    onFinish(result);
                }
            }
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
                    onFinish(result);
                }
                else
                {
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");
                    foreach (var entry in updateCurrencies)
                    {
                        var resultEntry = new PlayerCurrency();
                        PlayerCurrency.CloneTo(JsonUtility.FromJson<DbPlayerCurrency>(entry.JSON), resultEntry);
                        result.updateCurrencies.Add(resultEntry);
                    }
                    onFinish(result);
                }
            }
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
                    onFinish(result);
                }
                else
                {
                    var list = scriptData.GetGSDataList("list");
                    foreach (var entry in list)
                    {
                        var resultEntry = new PlayerFormation();
                        PlayerFormation.CloneTo(JsonUtility.FromJson<DbPlayerFormation>(entry.JSON), resultEntry);
                        result.list.Add(resultEntry);
                    }
                    onFinish(result);
                }
            }
        });
    }
}
