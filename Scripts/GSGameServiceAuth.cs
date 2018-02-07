using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class GSGameService
{
    protected override void DoLogin(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        onFinish(result);
    }

    protected override void DoRegisterOrLogin(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        // If has player with username Login if not register
    }

    protected override void DoGuestLogin(string deviceId, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        if (string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(deviceId))
            result.error = GameServiceErrorCode.EMPTY_USERNMAE_OR_PASSWORD;
        onFinish(result);
    }

    protected override void DoValidateLoginToken(string playerId, string loginToken, bool refreshToken, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        onFinish(result);
    }

    protected override void DoSetProfileName(string playerId, string loginToken, string profileName, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        onFinish(result);
    }

    protected override void DoRegister(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        onFinish(result);
    }
}
