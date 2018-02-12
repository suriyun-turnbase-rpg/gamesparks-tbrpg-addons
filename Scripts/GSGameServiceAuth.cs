using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Api.Requests;

public partial class GSGameService
{
    protected void RequestAccountDetails(PlayerResult result, UnityAction<PlayerResult> onFinish)
    {
        var player = result.player;
        var request = new AccountDetailsRequest();
        request.Send((accountResponse) =>
        {
            if (!accountResponse.HasErrors)
            {
                player.ProfileName = accountResponse.DisplayName;
                if (accountResponse.ScriptData != null)
                {
                    if (accountResponse.ScriptData.ContainsKey("exp"))
                        player.Exp = accountResponse.ScriptData.GetInt("exp").Value;
                    if (accountResponse.ScriptData.ContainsKey("selectedFormation"))
                        player.SelectedFormation = accountResponse.ScriptData.GetString("selectedFormation");
                }
                result.player = player;
                onFinish(result);
            }
            else
            {
                result.error = GameServiceErrorCode.UNKNOW;
                onFinish(result);
            }
        });
    }

    protected override void DoLogin(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var player = result.player = new Player();
        var request = new AuthenticationRequest();
        request.SetUserName(username);
        request.SetPassword(password);
        request.Send((authResponse) =>
        {
            player.Id = authResponse.UserId;
            player.LoginToken = authResponse.AuthToken;

            if (!authResponse.HasErrors)
                RequestAccountDetails(result, onFinish);
            else
            {
                result.error = GameServiceErrorCode.UNKNOW;
                if (authResponse.Errors.ContainsKey("DETAILS") && authResponse.Errors.GetString("DETAILS").Equals("UNRECOGNISED"))
                    result.error = GameServiceErrorCode.EMPTY_USERNMAE_OR_PASSWORD;
                onFinish(result);
            }
        });
    }

    protected override void DoRegisterOrLogin(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        DoRegister(username, password, (registerResult) =>
        {
            if (registerResult.Success)
                DoLogin(username, password, onFinish);
            else
                onFinish(registerResult);
        });
    }

    protected override void DoGuestLogin(string deviceId, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var player = result.player = new Player();
        var request = new DeviceAuthenticationRequest();
        request.Send((authResponse) =>
        {
            player.Id = authResponse.UserId;
            player.LoginToken = authResponse.AuthToken;

            if (!authResponse.HasErrors)
                RequestAccountDetails(result, onFinish);
            else
            {
                result.error = GameServiceErrorCode.UNKNOW;
                onFinish(result);
            }
        });
    }

    protected override void DoValidateLoginToken(string playerId, string loginToken, bool refreshToken, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var player = result.player = new Player();
        result.error = GameServiceErrorCode.INVALID_PLAYER_DATA;
        onFinish(result);
    }

    protected override void DoSetProfileName(string playerId, string loginToken, string profileName, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var player = result.player = Player.CurrentPlayer;
        var request = new ChangeUserDetailsRequest();
        request.SetDisplayName(profileName);
        request.Send((authResponse) =>
        {
            player.ProfileName = profileName;

            if (!authResponse.HasErrors)
                onFinish(result);
            else
            {
                result.error = GameServiceErrorCode.UNKNOW;
                onFinish(result);
            }
        });
    }

    protected override void DoRegister(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        var player = result.player = new Player();
        var request = new RegistrationRequest();
        request.SetUserName(username);
        request.SetPassword(password);
        request.Send((authResponse) =>
        {
            player.Id = authResponse.UserId;
            player.LoginToken = authResponse.AuthToken;

            if (!authResponse.HasErrors)
                onFinish(result);
            else
            {
                result.error = GameServiceErrorCode.UNKNOW;
                if (authResponse.Errors.ContainsKey("USERNAME") && authResponse.Errors.GetString("USERNAME").Equals("TAKEN"))
                    result.error = GameServiceErrorCode.EXISTED_USERNAME;
                onFinish(result);
            }
        });
    }
}
