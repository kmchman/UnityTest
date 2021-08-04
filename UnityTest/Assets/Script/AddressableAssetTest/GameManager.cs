using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject parentObj;
    [SerializeField] private UITestItem itemPrefab;
    [SerializeField] private UnityEngine.UI.Text versionText;

    // Start is called before the first frame update
    void Start()
    {
        versionText.text = Application.version;
        Debug.Log("Load Start");
        //versionText.text = PlyaerSetting.
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
                    if (obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                    {
                        var uiItem = Instantiate<UITestItem>(itemPrefab, parentObj.transform);
                        uiItem.SetData(item.PrimaryKey);
                        Debug.Log($" {item.PrimaryKey}");
                        
                    }
                    loadCnt++;
                    if (loadCnt >= csvList.Count)
                        Debug.Log("complete");
                };
            }
        };
    }
}
