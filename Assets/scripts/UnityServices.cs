using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

using UnityEngine;
public class UnityServices : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private async void Start()
    {
        await UnityServices.InstantiateAsync();
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Player signed in anonymously");
        }
    }

    private static async Task InstantiateAsync()
    {
        throw new NotImplementedException();
    }

    private async  void SaveData(string key, string value)
    {
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { key, value }
        };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("Data saved successfuly");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private async void LoadData(string key)
    {
        try
        {
             Dictionary<string, string> data = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });
            if (data.TryGetValue(key, out var value))
            {
                Debug.Log($"Lcaded value: {value}");
            }
        }
        catch (Exception ex)                                          
        {
            Debug.LogError($"Error loading data: {ex.Message}");
        }

    }

}
