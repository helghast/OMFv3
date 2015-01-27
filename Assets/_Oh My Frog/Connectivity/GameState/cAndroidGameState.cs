using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class AndroidGameState : OnStateLoadedListener
{
    public int coins;

    public void SaveState(int slot)
    {

        // serialize your game state to a byte array:
        //byte[] mySaveState = coins; //= ...;
        //((PlayGamesPlatform) Social.Active).UpdateState(slot, mySaveState, this);
    }

    public void OnStateSaved(bool success, int slot)
    {
        // handle success or failure
    }

    public void OnStateLoaded(bool success, int slot, byte[] data)
    {
        throw new System.NotImplementedException();
    }

    public byte[] OnStateConflict(int slot, byte[] localData, byte[] serverData)
    {
        throw new System.NotImplementedException();
    }
}
