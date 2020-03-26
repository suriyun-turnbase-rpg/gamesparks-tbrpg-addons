using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public partial class GSGameService
{
    protected override void DoCreateClan(string playerId, string loginToken, string clanName, UnityAction<CreateClanResult> onFinish)
    {
        var result = new CreateClanResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoFindClan(string playerId, string loginToken, string clanName, UnityAction<ClanListResult> onFinish)
    {
        var result = new ClanListResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanJoinRequest(string playerId, string loginToken, string clanId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanJoinAccept(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanJoinDecline(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanMemberDelete(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanJoinRequestDelete(string playerId, string loginToken, string clanId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoGetClanMemberList(string playerId, string loginToken, UnityAction<FriendListResult> onFinish)
    {
        var result = new FriendListResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanOwnerTransfer(string playerId, string loginToken, string targetPlayerId, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoClanTerminate(string playerId, string loginToken, UnityAction<GameServiceResult> onFinish)
    {
        var result = new GameServiceResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }

    protected override void DoGetClan(string playerId, string loginToken, UnityAction<ClanResult> onFinish)
    {
        var result = new ClanResult();
        result.error = GameServiceErrorCode.NOT_AVAILABLE;
        onFinish(result);
    }
}
