using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Spawn : MonoBehaviour
{
    [SerializeField] private AssetReference obj;
    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        Addressables.InitializeAsync().Completed += OnAddressableInitialized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnGameObject();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Addressables.ReleaseInstance(spawnedObject);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadScene();
        }
    }

    private void OnAddressableInitialized(AsyncOperationHandle<IResourceLocator> locator)
    {
        ReadAllkeys();
    }

    private void SpawnGameObject()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("66e03580230e64343ab03f483201c020");
        handle.Completed += (task) =>
        {
            Instantiate(task.Result);
        };
    }

    private void LoadScene()
    {
        Debug.Log("Scene Loaded");
    }

    private void ReadAllkeys()
    {
        foreach(var locator in Addressables.ResourceLocators)
        {
            foreach (var key in locator.Keys)
            {
                if(locator.Locate(key, typeof(Object), out var locations))
                {
                    foreach(var location in locations)
                    {
                        Debug.Log($"key: {key}, First Location: {location.PrimaryKey}, Current Location: {location.InternalId}");
                    }
                }
            }
        }
    }
}
