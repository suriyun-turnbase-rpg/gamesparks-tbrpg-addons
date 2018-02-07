using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GSGameService
{
    protected override void DoLevelUpItem(string playerId, string loginToken, string itemId, Dictionary<string, int> materials, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }

    protected override void DoEvolveItem(string playerId, string loginToken, string itemId, Dictionary<string, int> materials, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }

    protected override void DoSellItems(string playerId, string loginToken, Dictionary<string, int> items, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }

    protected override void DoEquipItem(string playerId, string loginToken, string characterId, string equipmentId, string equipPosition, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }

    protected override void DoUnEquipItem(string playerId, string loginToken, string equipmentId, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }

    protected override void DoGetAvailableLootBoxList(UnityAction<AvailableLootBoxListResult> onFinish)
    {
        var result = new AvailableLootBoxListResult();
        onFinish(result);
    }

    protected override void DoOpenLootBox(string playerId, string loginToken, string lootBoxDataId, int packIndex, UnityAction<ItemResult> onFinish)
    {
        var result = new ItemResult();
        onFinish(result);
    }
}
