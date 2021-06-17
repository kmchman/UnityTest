using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject parentObj;
    [SerializeField] private UITestItem itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Load Start");
        Addressables.LoadResourceLocationsAsync("Table").Completed += (handle) =>
        {
            int loadCnt = 0;
            var csvList = handle.Result.Where(type => type.ResourceType == typeof(UnityEngine.TextAsset)).ToList();
            Debug.Log($"csvList count : {csvList.Count}");
            
            foreach (var item in csvList)
            {
                //string fileName = Path.GetFileName(item.PrimaryKey);
                Addressables.LoadAssetAsync<UnityEngine.TextAsset>(item).Completed += (obj) =>
                {
                    var uiItem = Instantiate<UITestItem>(itemPrefab, parentObj.transform);
                    uiItem.SetData(item.PrimaryKey);
                    Debug.Log($" {item.PrimaryKey}");
                    loadCnt++;

                    if (loadCnt >= csvList.Count)
                        Debug.Log("complete");

                };
            }
        };
    }
}
