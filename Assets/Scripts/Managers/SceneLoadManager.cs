using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;

    private Vector2Int currentRoomVector;
    
    [Header("Events")]
    public ObjectEventSO afterRoomLoadedEvent;
    
    
    /// <summary>
    /// This method is called when the scene is loaded
    /// </summary>
    /// <param name="data"></param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is Room)
        {
            Room currentRoom = data as Room;
            var currentData = currentRoom.roomData;
            Debug.Log("Loading room: " + currentData.roomType);
            currentRoomVector = new(currentRoom.column, currentRoom.row);
            Debug.Log("currentRoomVector: " + currentRoomVector);
            currentScene = currentData.sceneToLoad;
        }
        
        // Unload the current scene
        await UnloadSceneTask();
        // Load the scene
        await LoadSceneTask();
        
        
        afterRoomLoadedEvent.RaiseEvent(currentRoomVector, this);
    }

    /// <summary>
    /// Asynchronously load the scene
    /// </summary>
    private async Awaitable LoadSceneTask()
    {
        var s =currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;
        if(s.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    }
    
    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
    
    public async void LoadMap()
    {
        await UnloadSceneTask();

        currentScene = map;
        await LoadSceneTask();
    }
}
