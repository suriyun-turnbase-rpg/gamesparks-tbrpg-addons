using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public partial class GSGameService
{
    protected override void DoLevelUpItem(string playerId, string loginToken, string itemId, Dictionary<string, int> materials, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        data.AddString("itemId", itemId);
        var materialsObject = new GSRequestData();
        foreach (var material in materials)
        {
            materialsObject.AddNumber(material.Key, material.Value);
        }
        data.AddObject("materials", materialsObject);
        var request = GetGSEventRequest("LevelUpItem", data);
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
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");
                    
                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    result.deleteItemIds.AddRange(deleteItemIds);
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoEvolveItem(string playerId, string loginToken, string itemId, Dictionary<string, int> materials, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        data.AddString("itemId", itemId);
        var materialsObject = new GSRequestData();
        foreach (var material in materials)
        {
            materialsObject.AddNumber(material.Key, material.Value);
        }
        data.AddObject("materials", materialsObject);
        var request = GetGSEventRequest("EvolveItem", data);
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
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    result.deleteItemIds.AddRange(deleteItemIds);
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoSellItems(string playerId, string loginToken, Dictionary<string, int> items, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        var itemsObject = new GSRequestData();
        foreach (var item in items)
        {
            itemsObject.AddNumber(item.Key, item.Value);
        }
        data.AddObject("items", itemsObject);
        var request = GetGSEventRequest("SellItems", data);
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
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    result.deleteItemIds.AddRange(deleteItemIds);
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoEquipItem(string playerId, string loginToken, string characterId, string equipmentId, string equipPosition, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        data.AddString("characterId", characterId);
        data.AddString("equipmentId", equipmentId);
        data.AddString("equipPosition", equipPosition);
        var request = GetGSEventRequest("EquipItem", data);
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
                    var updateItems = scriptData.GetGSDataList("updateItems");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoUnEquipItem(string playerId, string loginToken, string equipmentId, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        data.AddString("equipmentId", equipmentId);
        var request = GetGSEventRequest("UnEquipItem", data);
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
                    var updateItems = scriptData.GetGSDataList("updateItems");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                }
            }
            onFinish(result);
        });
    }

    protected override void DoGetAvailableLootBoxList(UnityAction<AvailableLootBoxListResult> onFinish)
    {
        var result = new AvailableLootBoxListResult();
        var request = GetGSEventRequest("GetAvailableLootBoxList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetStringList("list");
                result.list = list;
            }
            onFinish(result);
        });
    }

    protected override void DoGetAvailableIapPackageList(UnityAction<AvailableIapPackageListResult> onFinish)
    {
        var result = new AvailableIapPackageListResult();
        var request = GetGSEventRequest("GetAvailableIapPackageList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetStringList("list");
                result.list = list;
            }
            onFinish(result);
        });
    }

    protected override void DoOpenLootBox(string playerId, string loginToken, string lootBoxDataId, int packIndex, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        var data = new GSRequestData();
        data.AddString("lootBoxDataId", lootBoxDataId);
        data.AddNumber("packIndex", packIndex);
        var request = GetGSEventRequest("OpenLootBox", data);
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
                    var createItems = scriptData.GetGSDataList("createItems");
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");

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
                }
            }
            onFinish(result);
        });
    }

    protected override void DoOpenIapPackage_iOS(string playerId, string loginToken, string iapPackageDataId, string receipt, UnityAction<ItemResult> onFinish)
    {
        var request = new IOSBuyGoodsRequest();
        request.SetReceipt(receipt);
        request.Send((response) =>
        {
            IapResponse(response, onFinish);
        });
    }

    protected override void DoOpenIapPackage_Android(string playerId, string loginToken, string iapPackageDataId, string data, string signature, UnityAction<ItemResult> onFinish)
    {
        var request = new GooglePlayBuyGoodsRequest();
        request.SetSignedData(data);
        request.SetSignature(signature);
        request.Send((response) =>
        {
            IapResponse(response, onFinish);
        });
    }

    private void IapResponse(BuyVirtualGoodResponse response, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();

        if (!response.HasErrors)
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
                    var createItems = scriptData.GetGSDataList("createItems");
                    var updateItems = scriptData.GetGSDataList("updateItems");
                    var deleteItemIds = scriptData.GetStringList("deleteItemIds");
                    var updateCurrencies = scriptData.GetGSDataList("updateCurrencies");

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
                }
            }
            onFinish(result);
        }
        else
        {
            Debug.LogError("GameSparks error while request accout details: " + response.Errors.JSON);
            result.error = GameServiceErrorCode.UNKNOW;
            onFinish(result);
        }
    }

    protected override void DoEarnAchievementReward(string playerId, string loginToken, string achievementId, UnityAction<EarnAchievementResult> onFinish)
    {
        var result = new EarnAchievementResult();
        var data = new GSRequestData();
        data.AddString("achievementId", achievementId);
        var request = GetGSEventRequest("EarnAchievementReward", data);
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
                    var rewardSoftCurrency = scriptData.GetInt("rewardSoftCurrency").Value;
                    var rewardHardCurrency = scriptData.GetInt("rewardHardCurrency").Value;
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
                    result.rewardSoftCurrency = rewardSoftCurrency;
                    result.rewardHardCurrency = rewardHardCurrency;
                    result.player = JsonUtility.FromJson<Player>(player.JSON);
                }
            }
            onFinish(result);
        });
    }

    protected override void DoConvertHardCurrency(string playerId, string loginToken, int requireHardCurrency, UnityAction<HardCurrencyConversionResult> onFinish)
    {
        var result = new HardCurrencyConversionResult();
        var data = new GSRequestData();
        data.AddNumber("requireHardCurrency", requireHardCurrency);
        var request = GetGSEventRequest("ConvertHardCurrency", data);
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
                    var receiveSoftCurrency = scriptData.GetInt("receiveSoftCurrency").Value;
                    foreach (var entry in updateCurrencies)
                    {
                        result.updateCurrencies.Add(JsonUtility.FromJson<PlayerCurrency>(entry.JSON));
                    }
                    result.requireHardCurrency = requireHardCurrency;
                    result.receiveSoftCurrency = receiveSoftCurrency;
                }
            }
            onFinish(result);
        });
    }
}
