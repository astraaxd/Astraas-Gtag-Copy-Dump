using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GorillaNetworking
{
    public class PlayFabAuthenticator : MonoBehaviour
    {
        public static volatile PlayFabAuthenticator instance;

        public bool isTestAccount;

        public string testAccountName;

        public GorillaNetworkJoinTrigger testJoin;

        public string testRoomToJoin;

        public string testGameMode;

        public string _playFabPlayerIdCache;

        private string _displayName;

        public string userID;

        private string orgScopedID;

        private string userToken;

        public GorillaComputer gorillaComputer;

        private byte[] m_Ticket;

        private uint m_pcbTicket;

        public Text debugText;

        public bool screenDebugMode;

        public bool loginFailed;

        public GameObject emptyObject;

        private HAuthTicket m_HAuthTicket;

        private byte[] ticketBlob = new byte[1024];

        private uint ticketSize;

        public string expectedTitleID;

        public List<GameObject> Cosmetics;

        public bool test1;
        public GameObject AssFart;


        protected Callback<GetAuthSessionTicketResponse_t> m_GetAuthSessionTicketResponse;
        private float countdown = 900f; // 30 minutes in seconds (60 seconds x 30 minutes)
        private float timer = 0f;
        private const float interval = 300f;
        public float level;
        public string Values;
        private float ValuesSetTo = 100;
        public void Awake()
        {
            level = PlayerPrefs.GetFloat("LEVEL");

            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "4f6fc5e8-9543-47cf-a557-37dc194d8f7b";
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdVoice = "8a90d276-2ec1-4b86-b7b8-7922b6bc6f2e";
            PlayFabSettings.TitleId = "BB988";
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                UnityEngine.Object.Destroy(base.gameObject);
            }
            byte[] payload = new byte[1];
            PlayFabHttp.SimplePostCall("https://BB988.playfabapi.com/", payload, delegate
            {
            }, delegate
            {
            });
            if (screenDebugMode)
            {
                debugText.text = "";
            }
            Debug.Log("doing steam thing");
            OnGetAuthSessionTicketResponse();
            AuthenticateWithPlayFab();
            PlayFabSettings.DisableFocusTimeCollection = true;







        }


        public void GetValues()
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = _playFabPlayerIdCache,
                Keys = null
            }, delegate (GetUserDataResult result)
            {
                if (result.Data == null || !result.Data.ContainsKey("PlayerLevel"))
                {

                }
                else
                {
                    // PlayerLevel data found, so get its value and store it in the playerLevelValue variable
                    Values = result.Data["PlayerLevel"].Value;
                    if (Values == "COCONUT 1")
                    {
                        ValuesSetTo = 100;

                    }
                    else if (Values == "COCONUT 2")
                    {
                        ValuesSetTo = 100;

                    }
                    else if (Values == "COCONUT 3")
                    {
                        ValuesSetTo = 100;

                    }
                    if (Values == "PINEAPPLE 1")
                    {
                        ValuesSetTo = 125;

                    }
                    else if (Values == "PINEAPPLE 2")
                    {
                        ValuesSetTo = 125;

                    }
                    else if (Values == "PINEAPPLE 3")
                    {
                        ValuesSetTo = 125;

                    }
                    if (Values == "WATERMELON 1")
                    {
                        ValuesSetTo = 150;

                    }
                    else if (Values == "WATERMELON 2")
                    {
                        ValuesSetTo = 150;

                    }
                    else if (Values == "WATERMELON 3")
                    {
                        ValuesSetTo = 150;

                    }
                    if (Values == "BANNANA 1")
                    {
                        ValuesSetTo = 175;

                    }
                    else if (Values == "BANNANA 2")
                    {
                        ValuesSetTo = 175;

                    }
                    else if (Values == "BANNANA 3")
                    {
                        ValuesSetTo = 175;

                    }
                    if (Values == "GOLDEN BANNANA 1")
                    {
                        ValuesSetTo = 200;

                    }
                    else if (Values == "GOLDEN BANNANA 2")
                    {
                        ValuesSetTo = 200;

                    }
                    else if (Values == "GOLDEN BANNANA 3")
                    {
                        ValuesSetTo = 200;

                    }
                }
            }, null);
        }


        public void GetUserLevel(string myPlayFabeId)
        {
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = myPlayFabeId,
                Keys = null
            }, delegate (GetUserDataResult result)
            {
                if (result.Data == null || !result.Data.ContainsKey("PlayerLevel"))
                {
                    // PlayerLevel data not found, so create a new entry with a default value
                    string playerLevelValue = "COCONUT 1";
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", playerLevelValue }
                }
                    },
                    null, null);
                    PlayerPrefs.SetString("RANK", playerLevelValue);
                }
                else
                {
                    // PlayerLevel data found, so get its value
                    string REAL = result.Data["PlayerLevel"].Value;
                    PlayerPrefs.SetString("RANK", REAL);
                }
                if (result.Data == null || !result.Data.ContainsKey("Player1v1MMR"))
                {
                }
                else
                {

                }

            }, delegate
            {
            });
        }


        private void Update()
        {

            if (test1)
            {
                if (PlayerPrefs.GetFloat("LEVEL") == 50)
                {

                    PlayerPrefs.SetFloat("LEVEL", 0);
                    Look();
                    Debug.Log("15MINS PLAYED! COUNTDOWN DONE!");

                }
                else if (PlayerPrefs.GetFloat("LEVEL") == 0)
                {
                    float level2 = PlayerPrefs.GetFloat("LEVEL");

                    PlayerPrefs.SetFloat("LEVEL", 50);
                    Debug.Log("15MINS PLAYED!");

                }
                else if (PlayerPrefs.GetFloat("LEVEL") == 100)
                {
                    PlayerPrefs.SetFloat("LEVEL", 0);

                }

                test1 = false;
            }
            countdown -= Time.deltaTime;
            timer += Time.deltaTime;

            if (countdown <= 0f)

            {
                if (PlayerPrefs.GetFloat("LEVEL") == 50)
                {

                    PlayerPrefs.SetFloat("LEVEL", 0);
                    Look();
                    Debug.Log("15MINS PLAYED! COUNTDOWN DONE!");
                    countdown = 5f;
                }
                else if (PlayerPrefs.GetFloat("LEVEL") == 0)
                {
                    float level2 = PlayerPrefs.GetFloat("LEVEL");

                    PlayerPrefs.SetFloat("LEVEL", 50);
                    Debug.Log("15MINS PLAYED!");
                    countdown = 5f;
                }

            }
            else if (timer >= interval)
            {
                timer = 0f;
            }

            float playerLevel = PlayerPrefs.GetFloat("LEVEL");



        }
        public void Look()
        {
            PlayerPrefs.SetFloat("LEVEL", 0);
            string playerLevelValue = "";

            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = _playFabPlayerIdCache,
                Keys = null
            }, delegate (GetUserDataResult result)
            {
                if (result.Data == null || !result.Data.ContainsKey("PlayerLevel"))
                {

                }
                else
                {
                    // PlayerLevel data found, so get its value and store it in the playerLevelValue variable
                    playerLevelValue = result.Data["PlayerLevel"].Value;
                    PlayerPrefs.SetString("RANK", playerLevelValue);

                }

                // Use the playerLevelValue variable as needed

            }, null);

            if (playerLevelValue == "COCONUT 1")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "COCONUT 2" }
                }
                },
                null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "COCONUT 2")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "COCONUT 3" }
                }
                },
                                            null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "COCONUT 3")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "PINEAPPLE 1" }
                }
                },
                null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });


            }
            else if (playerLevelValue == "PINEAPPLE 1")
            {

                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "PINEAPPLE 2" }
                }
                },
                null, null);
                GetValues();
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "PINEAPPLE 2")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "PINEAPPLE 3" }
                }
                },
                                            null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "PINEAPPLE 3")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "WATERMELON 1" }
                }
                },
                                            null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "WATERMELON 1")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "WATERMELON 2" }
                }
                },
                                            null, null);
                GetValues();
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "WATERMELON 2")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "WATERMELON 3" }
                }
                },
                                            null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "WATERMELON 3")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "BANNANA 1" }
                }
                },
                null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "BANNANA 1")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "BANNANA 2" }
                }
                },
                null, null);
                GetValues();
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "BANNANA 2")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "BANNANA 3" }
                }
                },
                                            null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "BANNANA 3")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "GOLDEN BANNANA 1" }
                }
                },
                null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }

            else if (playerLevelValue == "GOLDEN BANNANA 1")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "GOLDEN BANNANA 2" }
                }
                },
                null, null);
                GetValues();
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "GOLDEN BANNANA 2")
            {
                PlayerPrefs.SetFloat("LEVEL", 0);
                PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                {
                    Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", "GOLDEN BANNANA 3" }
                }
                },
                null, null);
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrency",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
            else if (playerLevelValue == "GOLDEN BANNANA 3")
            {
                PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest()
                {
                    FunctionName = "addCurrencyGOLDEN",
                    FunctionParameter = new { },
                    GeneratePlayStreamEvent = true
                }, result =>
                {
                    Debug.Log(result.FunctionResult);
                }, error =>
                {
                    Debug.Log(error.ErrorMessage);
                });

            }
        }
        public void AuthenticateWithPlayFab()
        {
            if (!loginFailed)
            {
                Debug.Log("authenticating with plafyab!");
                if (SteamManager.Initialized)
                {
                    Debug.Log("trying to auth with steam");
                    m_HAuthTicket = SteamUser.GetAuthSessionTicket(ticketBlob, ticketBlob.Length, out ticketSize);
                }
            }
        }

        private void RequestPhotonToken(LoginResult obj)
        {
            LogMessage("PlayFab authenticated. Requesting photon token...");
            _playFabPlayerIdCache = obj.PlayFabId;
            PlayFabClientAPI.GetPhotonAuthenticationToken(new GetPhotonAuthenticationTokenRequest
            {
                PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime
            }, AuthenticateWithPhoton, OnPlayFabError);
        }

        private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
        {
            LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");
            AuthenticationValues authenticationValues = new AuthenticationValues();
            authenticationValues.AuthType = CustomAuthenticationType.Custom;
            authenticationValues.AddAuthParameter("username", _playFabPlayerIdCache);
            authenticationValues.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);
            PhotonNetwork.AuthValues = authenticationValues;
            GetPlayerDisplayName(_playFabPlayerIdCache);
            GetUserLevel(_playFabPlayerIdCache);
            PlayFabClientAPI.GetUserData(new GetUserDataRequest
            {
                PlayFabId = _playFabPlayerIdCache,
                Keys = null
            }, delegate (GetUserDataResult result)
            {
                if (result.Data == null || !result.Data.ContainsKey("PlayerLevel"))
                {
                    // PlayerLevel data not found, so create a new entry with a default value
                    string playerLevelValue = "COCONUT 1";
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
                    {
                        Data = new Dictionary<string, string>()
                {
                    { "PlayerLevel", playerLevelValue }
                }
                    },
                    null, null);
                    PlayerPrefs.SetString("RANK", playerLevelValue);
                }
                else
                {
                    // PlayerLevel data found, so get its value

                }
            }, delegate
            {
            });
            GetValues();
            PlayFabClientAPI.ExecuteCloudScript(new ExecuteCloudScriptRequest
            {
                FunctionName = "AddOrRemoveDLCOwnership",
                FunctionParameter = new { }
            }, delegate
            {
                Debug.Log("got results! updating!");
                GorillaTagger.Instance.offlineVRRig.GetUserCosmeticsAllowed();
            }, delegate (PlayFabError error)
            {
                Debug.Log("Got error retrieving user data:");
                Debug.Log(error.GenerateErrorReport());
                GorillaTagger.Instance.offlineVRRig.GetUserCosmeticsAllowed();
            });
            if (CosmeticsController.instance != null)
            {
                Debug.Log("itinitalizing cosmetics");
                CosmeticsController.instance.Initialize();
            }
            if (gorillaComputer != null)
            {
                gorillaComputer.OnConnectedToMasterStuff();
            }
            if (PhotonNetworkController.Instance != null)
            {
                PhotonNetworkController.Instance.InitiateConnection();
            }
        }

        private void OnPlayFabError(PlayFabError obj)
        {
            LogMessage(obj.ErrorMessage);
            Debug.Log(obj.ErrorMessage);
            loginFailed = true;
            if (obj.ErrorMessage == "The account making this request is currently banned")
            {
                using (Dictionary<string, List<string>>.Enumerator enumerator = obj.ErrorDetails.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<string, List<string>> current = enumerator.Current;
                        if (current.Value[0] != "Indefinite")
                        {
                            gorillaComputer.GeneralFailureMessage("YOU HAVE BEEN BANNED. YOU WILL NOT BE ABLE TO PLAY UNTIL THE BAN EXPIRES.\nREASON: " + current.Key + "\nHOURS LEFT: " + (int)((DateTime.Parse(current.Value[0]) - DateTime.UtcNow).TotalHours + 1.0));
                        }
                        else
                        {
                            gorillaComputer.GeneralFailureMessage("YOU HAVE BEEN BANNED INDEFINITELY.\nREASON: " + current.Key);
                        }
                    }
                    return;
                }
            }
            if (obj.ErrorMessage == "The IP making this request is currently banned")
            {
                using (Dictionary<string, List<string>>.Enumerator enumerator2 = obj.ErrorDetails.GetEnumerator())
                {
                    if (enumerator2.MoveNext())
                    {
                        KeyValuePair<string, List<string>> current2 = enumerator2.Current;
                        if (current2.Value[0] != "Indefinite")
                        {
                            gorillaComputer.GeneralFailureMessage("THIS IP HAS BEEN BANNED. YOU WILL NOT BE ABLE TO PLAY UNTIL THE BAN EXPIRES.\nREASON: " + current2.Key + "\nHOURS LEFT: " + (int)((DateTime.Parse(current2.Value[0]) - DateTime.UtcNow).TotalHours + 1.0));
                        }
                        else
                        {
                            gorillaComputer.GeneralFailureMessage("THIS IP HAS BEEN BANNED INDEFINITELY.\nREASON: " + current2.Key);
                        }
                    }
                    return;
                }
            }
            gorillaComputer.GeneralFailureMessage(gorillaComputer.unableToConnect);
        }

        public void LogMessage(string message)
        {
        }

        private void GetPlayerDisplayName(string playFabId)
        {
            PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest
            {
                PlayFabId = playFabId,
                ProfileConstraints = new PlayerProfileViewConstraints
                {
                    ShowDisplayName = true
                }
            }, delegate (GetPlayerProfileResult result)
            {
                _displayName = result.PlayerProfile.DisplayName;
            }, delegate (PlayFabError error)
            {
                Debug.LogError(error.GenerateErrorReport());
            });
        }

        public void SetDisplayName(string playerName)
        {
            if (_displayName == null || (_displayName.Length > 4 && _displayName.Substring(0, _displayName.Length - 4) != playerName))
            {
                PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
                {
                    DisplayName = playerName
                }, delegate
                {
                    _displayName = playerName;
                }, delegate (PlayFabError error)
                {
                    Debug.LogError(error.GenerateErrorReport());
                });
            }
        }

        public void ScreenDebug(string debugString)
        {
            Debug.Log(debugString);
            if (screenDebugMode)
            {
                Text text = debugText;
                text.text = text.text + debugString + "\n";
            }
        }

        public void ScreenDebugClear()
        {
            debugText.text = "";
        }

        public string GetSteamAuthTicket()
        {
            Array.Resize(ref ticketBlob, (int)ticketSize);
            StringBuilder stringBuilder = new StringBuilder();
            byte[] array = ticketBlob;
            foreach (byte b in array)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        private void OnGetAuthSessionTicketResponse()
        {
            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = PlayFabSettings.DeviceUniqueIdentifier
            }, RequestPhotonToken, OnPlayFabError);
        }
    }
}