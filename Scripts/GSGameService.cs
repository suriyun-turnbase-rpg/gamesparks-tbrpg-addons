using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;
using GameSparks.Api.Requests;
#if UNITY_EDITOR
using UnityEditor;
#endif


public partial class GSGameService : BaseGameService
{
    private void Awake()
    {
        StartCoroutine(WaitForGS());
    }
    
    IEnumerator WaitForGS()
    {
        while (!GS.Available)
            yield return 0;

        Debug.Log("Gamesparks ready.");
    }

#if UNITY_EDITOR
    [ContextMenu("Export Game Database")]
    public void ExportGameDatabase()
    {
        var gameInstance = FindObjectOfType<GameInstance>();
        if (gameInstance == null)
        {
            Debug.LogError("Cannot export game database, no game instance found");
            return;
        }
        var gameDatabase = gameInstance.gameDatabase;
        if (gameDatabase == null)
        {
            Debug.LogError("Cannot export game database, no game database found");
            return;
        }
        gameDatabase.Setup();
        var itemsJson = "";
        var currenciesJson = "";
        var staminasJson = "";
        var formationsJson = "";
        var stagesJson = "";
        var lootBoxesJson = "";

        foreach (var item in gameDatabase.Items)
        {
            if (!string.IsNullOrEmpty(itemsJson))
                itemsJson += ",";
            itemsJson += "\"" + item.Key + "\":" + item.Value.ToJson();
        }
        itemsJson = "{" + itemsJson + "}";

        foreach (var entry in gameDatabase.Currencies)
        {
            if (!string.IsNullOrEmpty(currenciesJson))
                currenciesJson += ",";
            currenciesJson += "\"" + entry.Key + "\":" + entry.Value.ToJson();
        }
        currenciesJson = "{" + currenciesJson + "}";

        foreach (var entry in gameDatabase.Staminas)
        {
            if (!string.IsNullOrEmpty(staminasJson))
                staminasJson += ",";
            staminasJson += "\"" + entry.Key + "\":" + entry.Value.ToJson();
        }
        staminasJson = "{" + staminasJson + "}";

        foreach (var entry in gameDatabase.Formations)
        {
            if (!string.IsNullOrEmpty(formationsJson))
                formationsJson += ",";
            formationsJson += "\"" + entry.Key + "\":" + entry.Value.ToJson();
        }
        formationsJson = "{" + formationsJson + "}";

        foreach (var entry in gameDatabase.Stages)
        {
            if (!string.IsNullOrEmpty(stagesJson))
                stagesJson += ",";
            stagesJson += "\"" + entry.Key + "\":" + entry.Value.ToJson();
        }
        stagesJson = "{" + stagesJson + "}";

        foreach (var entry in gameDatabase.LootBoxes)
        {
            if (!string.IsNullOrEmpty(lootBoxesJson))
                lootBoxesJson += ",";
            lootBoxesJson += "\"" + entry.Key + "\":" + entry.Value.ToJson();
        }
        lootBoxesJson = "{" + lootBoxesJson + "}";

        var jsonCombined = "{\"items\":" + itemsJson + "," +
            "\"currencies\":" + currenciesJson + "," +
            "\"staminas\":" + staminasJson + "," +
            "\"formations\":" + formationsJson + "," +
            "\"stages\":" + stagesJson + "," +
            "\"lootBoxes\":" + lootBoxesJson + "}";

        var cloudCodes = "var gameDatabase = " + jsonCombined + ";";
        var path = EditorUtility.SaveFilePanel("Export Game Database", Application.dataPath, "GameDatabase", "json");
        if (path.Length > 0)
            File.WriteAllText(path, cloudCodes);
    }
#endif

    protected override void DoGetAuthList(string playerId, string loginToken, UnityAction<AuthListResult> onFinish)
    {
        var result = new AuthListResult();
        onFinish(result);
    }

    protected override void DoGetItemList(string playerId, string loginToken, UnityAction<ItemListResult> onFinish)
    {
        var result = new ItemListResult();
        onFinish(result);
    }

    protected override void DoGetCurrencyList(string playerId, string loginToken, UnityAction<CurrencyListResult> onFinish)
    {
        var result = new CurrencyListResult();
        onFinish(result);
    }

    protected override void DoGetStaminaList(string playerId, string loginToken, UnityAction<StaminaListResult> onFinish)
    {
        var result = new StaminaListResult();
        onFinish(result);
    }

    protected override void DoGetFormationList(string playerId, string loginToken, UnityAction<FormationListResult> onFinish)
    {
        var result = new FormationListResult();
        onFinish(result);
    }

    protected override void DoGetUnlockItemList(string playerId, string loginToken, UnityAction<UnlockItemListResult> onFinish)
    {
        var result = new UnlockItemListResult();
        onFinish(result);
    }

    protected override void DoGetClearStageList(string playerId, string loginToken, UnityAction<ClearStageListResult> onFinish)
    {
        var result = new ClearStageListResult();
        onFinish(result);
    }
}
