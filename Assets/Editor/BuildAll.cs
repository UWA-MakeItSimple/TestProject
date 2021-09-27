using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildAll : Editor
{
    [MenuItem("AssetsBundle/BuildAll")]
    static void BuildAllAsset()
    {
        BuildPipeline.BuildAssetBundles("Assets/Assetbundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
