using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool poolTool;
    public List<CardDataSO> cardDataList;
    
    [Header("Card Library")]
    public CardLibrarySO newGameCardLibrary;
    public CardLibrarySO currentLibrary;

    private void Awake()
    {
        InitializeCardDataList();
        
        // Copy the new game card library to the current library (Could be adjusted according to the game design)
        foreach (var item in newGameCardLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
    }

    #region GetCardsFromSO

    private void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardDataSO>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardDataSO>> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            cardDataList = new List<CardDataSO>(obj.Result);
        }
        else
        {
            Debug.Log("Failed to load card data");
        }
    }

    #endregion

    public GameObject GetCardObject()
    {
        return poolTool.GetObjectFromPool();
    }
    
    public void DiscardCard(GameObject card)
    {
        poolTool.ReleaseObjectIntoPool(card);
    }
}
