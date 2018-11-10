using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;

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
                    onFinish(result);
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
                    onFinish(result);
                }
            }
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
                    onFinish(result);
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
                    onFinish(result);
                }
            }
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
                    onFinish(result);
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
                    onFinish(result);
                }
            }
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
                    onFinish(result);
                }
                else
                {
                    var updateItems = scriptData.GetGSDataList("updateItems");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    onFinish(result);
                }
            }
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
                    onFinish(result);
                }
                else
                {
                    var updateItems = scriptData.GetGSDataList("updateItems");

                    foreach (var entry in updateItems)
                    {
                        result.updateItems.Add(JsonUtility.FromJson<PlayerItem>(entry.JSON));
                    }
                    onFinish(result);
                }
            }
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
                onFinish(result);
            }
        });
    }

    protected override void DoGetIAPPackageList(UnityAction<AvailableIAPPackageListResult> onFinish)
    {
        var result = new AvailableIAPPackageListResult();
        var request = GetGSEventRequest("GetIAPPackageList");
        request.Send((response) =>
        {
            GSData scriptData = response.ScriptData;
            if (scriptData != null && scriptData.ContainsKey("list"))
            {
                var list = scriptData.GetStringList("list");
                result.list = list;
                onFinish(result);
            }
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
                    onFinish(result);
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
                    onFinish(result);
                }
            }
        });
    }

    protected override void DoOpenIAPPackage_iOS(string playerId, string loginToken, string iapPackageDataId, string receipt, UnityAction<ItemResult> onFinish)
    {
    }

    protected override void DoOpenIAPPackage_Android(string playerId, string loginToken, string iapPackageDataId, string data, string signature, UnityAction<ItemResult> onFinish)
    {
    }
}
