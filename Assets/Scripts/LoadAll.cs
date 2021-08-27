using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Object o = Resources.Load("Cube");
        //Instantiate(o);
        Object obj = null;
        Instantiate(obj);
        //AssetBundle go = AssetBundle.LoadFromFile("C:/JJ/ResLoadHook/UnitySample/Assets/AssetBundles/test01");
        //Instantiate(go.LoadAsset("Cube"));
        string sp = Application.streamingAssetsPath + "/test/test02";
        AssetBundle go = AssetBundle.LoadFromFile(sp);
        Instantiate(go.LoadAsset("Cube") as GameObject);

        //AssetBundleCreateRequest request =  AssetBundle.LoadFromFileAsync("C:/JJ/ResLoadHook/UnitySample/Assets/AssetBundles/myassetbundle");
        //Instantiate( request.assetBundle.LoadAsset("Cube"));
        //AssetBundle go = AssetBundle.LoadFromFile("C:/JJ/ResLoadHook/UnitySample/Assets/AssetBundles/myassetbundle");
        //ResourceRequest go = AssetBundle.LoadFromFileAsync("");
        //Instantiate(go.LoadAsset("Cube") as GameObject);
    }
}
