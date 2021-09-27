using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAsset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Test 1
        //AssetBundle.LoadFromFile("Assets/Assetbundles/test2prefaba");
        //AssetBundle.LoadFromFile("Assets/Assetbundles/test2texturea");

        //非异步
        AssetBundle ab = AssetBundle.LoadFromFile("Assets/Assetbundles/teststatic");
        Debug.Log(ab.isStreamedSceneAssetBundle);
        //Object sceneA = ab.LoadAsset("SceneA");
        SceneManager.LoadSceneAsync("TestForStaticCube");
    }

    IEnumerator LoadAsync()
    {
        //异步
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync("Assets/Assetbundles/test1scenea");
        yield return request;
        AssetBundle ab = request.assetBundle;

        //使用里面的资源
        Object[] obj = ab.LoadAllAssets<GameObject>();//加载出来放入数组中
        // 创建出来
        foreach (Object o in obj)
        {
            Instantiate(o);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
