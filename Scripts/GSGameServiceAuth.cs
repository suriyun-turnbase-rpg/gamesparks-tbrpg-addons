using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameSparks.Core;
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
                player.Id = accountResponse.UserId;
                player.ProfileName = accountResponse.DisplayName;
                if (accountResponse.ScriptData != null)
                {
                    if (accountResponse.ScriptData.ContainsKey("exp"))
                        player.Exp = accountResponse.ScriptData.GetInt("exp").Value;
                    if (accountResponse.ScriptData.ContainsKey("selectedFormation"))
                        player.SelectedFormation = accountResponse.ScriptData.GetString("selectedFormation");
                    if (accountResponse.ScriptData.ContainsKey("selectedArenaFormation"))
                        player.SelectedArenaFormation = accountResponse.ScriptData.GetString("selectedArenaFormation");
                    if (accountResponse.ScriptData.ContainsKey("arenaScore"))
                        player.arenaScore = accountResponse.ScriptData.GetInt("arenaScore").Value;
                    if (accountResponse.ScriptData.ContainsKey("highestArenaRank"))
                        player.highestArenaRank = accountResponse.ScriptData.GetInt("highestArenaRank").Value;
                    if (accountResponse.ScriptData.ContainsKey("highestArenaRankCurrentSeason"))
                        player.highestArenaRankCurrentSeason = accountResponse.ScriptData.GetInt("highestArenaRankCurrentSeason").Value;
                }
                result.player = player;
                onFinish(result);
            }
            else
            {
                Debug.LogError("GameSparks error while request accout details: " + accountResponse.Errors.JSON);
                result.error = GameServiceErrorCode.UNKNOW;
                onFinish(result);
            }
        });
    }

    protected override void DoLogin(string username, string password, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        result.player = new Player();
        var request = new AuthenticationRequest();
        request.SetUserName(username);
        request.SetPassword(password);
        request.Send((authResponse) =>
        {
            if (!authResponse.HasErrors)
                RequestAccountDetails(result, onFinish);
            else
            {
                Debug.LogError("GameSparks error while login: " + authResponse.Errors.JSON);
                result.error = GameServiceErrorCode.UNKNOW;
                if (authResponse.Errors.ContainsKey("DETAILS") && authResponse.Errors.GetString("DETAILS").Equals("UNRECOGNISED"))
                    result.error = GameServiceErrorCode.INVALID_USERNAME_OR_PASSWORD;
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
        result.player = new Player();
        var request = new DeviceAuthenticationRequest();
        request.Send((authResponse) =>
        {
            if (!authResponse.HasErrors)
                RequestAccountDetails(result, onFinish);
            else
            {
                Debug.LogError("GameSparks error while guest login: " + authResponse.Errors.JSON);
                result.error = GameServiceErrorCode.UNKNOW;
                onFinish(result);
            }
        });
    }

    protected override void DoValidateLoginToken(string playerId, string loginToken, bool refreshToken, UnityAction<PlayerResult> onFinish)
    {
        var result = new PlayerResult();
        if (GS.Authenticated && !string.IsNullOrEmpty(playerId))
        {
            result.player = new Player();
            RequestAccountDetails(result, onFinish);
            return;
        }
        result.error = GameServiceErrorCode.INVALID_LOGIN_TOKEN;
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
                Debug.LogError("GameSparks error while set profile name: " + authResponse.Errors.JSON);
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
        request.SetDisplayName(" ");
        request.SetUserName(username);
        request.SetPassword(password);
        request.Send((authResponse) =>
        {
            player.Id = authResponse.UserId;

            if (!authResponse.HasErrors)
                onFinish(result);
            else
            {
                Debug.LogError("GameSparks error while register: " + authResponse.Errors.JSON);
                result.error = GameServiceErrorCode.UNKNOW;
                if (authResponse.Errors.ContainsKey("USERNAME") && authResponse.Errors.GetString("USERNAME").Equals("TAKEN"))
                    result.error = GameServiceErrorCode.EXISTED_USERNAME;
                onFinish(result);
            }
        });
    }
}
