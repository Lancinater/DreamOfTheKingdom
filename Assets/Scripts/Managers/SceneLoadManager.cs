using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference currentScene;
    public AssetReference map;
    
    
    /// <summary>
    /// This method is called when the scene is loaded
    /// </summary>
    /// <param name="data"></param>
    public async void OnLoadRoomEvent(object data)
    {
        if (data is RoomDataSO)
        {
            var currentData = (RoomDataSO)data;
            // Debug.Log("Loading room: " + currentData.roomType);

            currentScene = currentData.sceneToLoad;
        }
        
        // Unload the current scene
        await UnloadSceneTask();
        // Load the scene
        await LoadSceneTask();
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
