//#define IOS_DEVELOPMENT
#define ANDROID_DEVELOPMENT

using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Multiplayer;


public delegate void InvitationCallback();
public delegate void TurnBasedNotificationCallback();

public static class ConnectivityManager
{
    public static cIAP EIAP;
    public static cCloudGameState ECloudGameState;
    public static cSocial ESocial;

    public enum eANDROID_CLOUD_SLOTS
    {
        COINS = 0
    }

	//-----------------------------------------------
    //  CONSTRUCTOR INFO
    //-----------------------------------------------
    static ConnectivityManager()
    {
        Debug.Log("Constructor");
    }

    public static void InitializeConnectivity()
    {
        InitiliazeCloudServices();

        // 1) InAppPurchase --> lee IAP_XML e inicializa Soomla 
        EIAP = new cIAP("iap_items");

        // 2) CloudGameState --> Para guardar el estado del juego (nº monedas, objetos comprados)
        ECloudGameState = new cCloudGameState();

        // 3) Social --> Logros, rankings...
        ESocial = new cSocial();
    }

    private static void InitiliazeCloudServices()
    {
        InvitationCallback myInvitationCallback = new InvitationCallback(onInvitation);
        TurnBasedNotificationCallback myTurnBasedNotificationCallback = new TurnBasedNotificationCallback(onTurnBasedNotification);
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        // registers a callback to handle game invitations received while the game is not running.
        //.WithInvitationDelegate(myInvitationCallback)
        // registers a callback for turn based match notifications received while the
        // game is not running.
        //.WithMatchDelegate(myTurnBasedNotificationCallback)
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private static void onInvitation()
    {
        // empty for now :P
    }

    private static void onTurnBasedNotification()
    {
        // empty for now :p
    }


    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // SIGN IN: Autentifica al jugador con el servicio al que pertenece (GooglePlay o iOS GameCenter)
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static bool SocialAuthenticate()
    {
        bool autenticate_success = false;

        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("You've successfully loged in!");
                autenticate_success = true;
            }
            else
            {
                Debug.Log("Login failed for some reason");
                autenticate_success = false;
            }
        });

        return autenticate_success;
    }

    // ------------------------------------------------------------------------------------------------
    // SAVED GAMES METHODS
    // ------------------------------------------------------------------------------------------------
    public static void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, 
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLongestPlaytime,
                                                            OnSavedGameOpened);

    }

    public static void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    // -------------------------------------------------------------------------------------------
    // SAVE GAME: Guarda el GameState en la nube.
    //        byte[] savedData: array de bytes que contiene mi GameState propio de OMF serializado
    // -------------------------------------------------------------------------------------------
    public static void SaveGameState(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved gameState at " + DateTime.Now);
        /*
        if (savedImage != null)
        {
            // This assumes that savedImage is an instance of Texture2D
            // and that you have already called a function equivalent to
            // getScreenshot() to set savedImage
            // NOTE: see sample definition of getScreenshot() method below
            byte[] pngData = savedImage.EncodeToPNG();
            builder = builder.WithUpdatedPngCoverImage(pngData);
        }*/
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Guarda las monedas del jugador
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static void SaveStateToTheCloud(byte[] save_state, eANDROID_CLOUD_SLOTS cloud_slot)
    {
        #if ANDROID_DEVELOPMENT
                //((PlayGamesPlatform)Social.Active).UpdateState(cloud_slot, save_state, this);

        #endif

        #if IOS_DEVELOPMENT

        #endif
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Convierte el objeto GameState en un array de Bytes para ser enviado a la nube (Serializa)(GoogleServices)
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static byte[] SerializeGameState(cCloudGameState cloudGameState)
    {
        byte[] serializedData;
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter serializer = new BinaryFormatter();

        serializer.Serialize(memoryStream, cloudGameState);
        serializedData = memoryStream.GetBuffer();

        return serializedData;
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Convierte el array de bytes de GameState a un objeto GameState (Deserializa)
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static cCloudGameState DeserializeGameStateByteArray(byte[] cloudGameStateByteArray)
    {
        MemoryStream memoryStream = new MemoryStream(cloudGameStateByteArray);
        BinaryFormatter deserializer = new BinaryFormatter();

        cCloudGameState result = (cCloudGameState)deserializer.Deserialize(memoryStream);
        return result;
    }

    public static void FillGameState()
    {
        ECloudGameState.total_cocktails = 0;
        ECloudGameState.total_frogs = 1;
        ECloudGameState.total_mangos = 65;
        ECloudGameState.total_meters = 10000;

        ECloudGameState.list_CloudItems.Add(new CloudItem());
        ECloudGameState.list_CloudItems.Add(new CloudItem());
    }

    public static void ObtainGameStateFromCloud()
    {

    }


#region ACHIEVEMENTS

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Reporta un logro conseguido por el jugador para que sea desbloqueado
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static bool ReportAchievement(string achievement, double progress)
    {
        bool achievement_success = false;
        Social.ReportProgress(achievement, progress, (bool success) =>
        {
            if (success)
            {
                achievement_success = true;
                Debug.Log("Logro desbloqueado");
            }
            // Logro desbloqueado!
        });

        return achievement_success;
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Reporta un logro incremental conseguido por el jugador para que sea desbloqueado
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static bool ReportIncrementalAchievement(string incremental_achievement, int incremental)
    {
        bool incremental_achievement_success = false;
        #if ANDROID_DEVELOPMENT
            ((PlayGamesPlatform)Social.Active).IncrementAchievement(incremental_achievement, incremental, (bool success) =>
            {
                if (success)
                {
                    incremental_achievement_success = true;
                }
                // Logro desbloqueado!
            });
        #endif

        #if IOS_DEVELOPMENT


        #endif

        return incremental_achievement_success;
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Reporta un resultado en una tabla de rankings
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static void ReportScore(int score, string leaderboard)
    {
        #if ANDROID_DEVELOPMENT
            Social.ReportScore(score, leaderboard, (bool success) =>
            {
                // score reportado!
            });
        #endif

        #if IOS_DEVELOPMENT


        #endif
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Muestra los logros del jugador
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static void ShowAchievements()
    {
        #if ANDROID_DEVELOPMENT
            Social.ShowAchievementsUI();
        #endif

        #if IOS_DEVELOPMENT


        #endif
    }

    // --------------------------------------------------------------------------------------------------------------------------------------------------
    // Muestra los logros del jugador
    // --------------------------------------------------------------------------------------------------------------------------------------------------
    public static void ShowLeaderBoard()
    {
        #if ANDROID_DEVELOPMENT
            Social.ShowLeaderboardUI();
        #endif

        #if IOS_DEVELOPMENT

        #endif
    }
#endregion ACHIEVEMENTS

    
}