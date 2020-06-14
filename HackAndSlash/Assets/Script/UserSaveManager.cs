using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserSaveManager : MonoBehaviour
{
    private const string USER_KEY = "USER_KEY";
    private FirebaseDatabase _database;

    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hack-and-slash-io.firebaseio.com/");
        _database = FirebaseDatabase.DefaultInstance;
    }

    public async Task<User> LoadUser()
    {
        var dataSnapshot = await _database.GetReference(USER_KEY).GetValueAsync();
        if (!dataSnapshot.Exists)
        {
            return null;
        }
        return JsonUtility.FromJson<User>(dataSnapshot.GetRawJsonValue());
    }

    public void SaveGold(int gold)
    {
        _database.GetReference(USER_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(gold));
    }

    public async Task<bool> SaveExists()
    {
        var dataSnapshot = await _database.GetReference(USER_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }

    public void EraseSave()
    {
        _database.GetReference(USER_KEY).RemoveValueAsync();
    }
}
