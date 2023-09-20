using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

[LuaCallCSharp]
public class DuckHuntLuaWWise : MonoBehaviour
{
    // create a list to store the bank names
    private List<string> bankNames = new List<string>();

    // load bank
    public void LoadBank(string bankName)
    {
        Debug.Log("LoadBank: " + bankName);
        bankNames.Add(bankName);
        AkBankManager.LoadBank(bankName, false, false);
    }
    // unload bank
    public void UnloadBank(string bankName)
    {
        Debug.Log("UnloadBank: " + bankName);
        bankNames.Remove(bankName);
        AkBankManager.UnloadBank(bankName);
    }

    public void PostSound(string soundName)
    {
        //Debug.Log("PlaySound: " + soundName);
        AkSoundEngine.PostEvent(soundName, gameObject);
    }

    public void StopSound()
    {
        AkSoundEngine.StopAll();
    }

    // play by id
    public void PostSoundById(int soundId)
    {
        Debug.Log("PlaySoundById: " + soundId);
        AkSoundEngine.PostEvent((uint)soundId, gameObject);
    }

    private void OnDestroy()
    {
        StopSound();

        // unload all banks
        foreach (var bankName in bankNames)
        {
            Debug.Log("UnloadBank: " + bankName);
            AkBankManager.UnloadBank(bankName);
        }

        bankNames.Clear();
    }
}
