using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GSGameService : BaseGameService
{
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
