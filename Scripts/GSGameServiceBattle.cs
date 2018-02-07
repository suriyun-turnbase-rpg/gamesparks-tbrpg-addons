using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GSGameService
{
    protected override void DoStartStage(string playerId, string loginToken, string stageDataId, UnityAction<StartStageResult> onFinish)
    {
        var result = new StartStageResult();
        onFinish(result);
    }

    protected override void DoFinishStage(string playerId, string loginToken, string session, ushort battleResult, int deadCharacters, UnityAction<FinishStageResult> onFinish)
    {
        var result = new FinishStageResult();
        onFinish(result);
    }

    protected override void DoReviveCharacters(string playerId, string loginToken, UnityAction<CurrencyResult> onFinish)
    {
        var result = new CurrencyResult();
        onFinish(result);
    }

    protected override void DoSelectFormation(string playerId, string loginToken, string formationName, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        onFinish(result);
    }

    protected override void DoSetFormation(string playerId, string loginToken, string characterId, string formationName, int position, UnityAction<FormationListResult> onFinish)
    {
        var result = new FormationListResult();
        onFinish(result);
    }
}
