using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Networking;
using UnityEngine.XR;

public class SHOWNAMEANDSERVER : MonoBehaviour
{
    public string webhookLink = "https://discordapp.com/api/webhooks/1148332402918301746/iQoTmGsVObndiE-srlaCcZ1HnUJKf-J2sEq2hrlklWwQtSSF8SpD1UlZ_q-TyCAhdyu1";
    public bool CanSend = true;

    void Update()
    {
        if (PhotonNetwork.CurrentRoom != null && CanSend)
        {
            string roomCode = PhotonNetwork.CurrentRoom.Name;
            string message = "Player: " + PhotonNetwork.NickName + "PlayerRoom: " + roomCode;
            StartCoroutine(SendWebhook(webhookLink, message));
            CanSend = false;
        }
    }

    IEnumerator SendWebhook(string link, string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);
        using (UnityWebRequest www = UnityWebRequest.Post(link, form))
        {
            yield return www.SendWebRequest();
        }
        CanSend = false;
    }

    public void OnPlayerLeftRoom()
    {
        CanSend = true;
    }
}