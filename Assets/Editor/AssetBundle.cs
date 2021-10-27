using System.Collections;
using System.Collections.Generic;
using UnityEngine;//运行期间用
using UnityEditor;//编辑状态用

public class AssetBundle : Editor
{

    public static Object[] Objs = new Object[] { };

    [MenuItem("Assets/AssetsBundle/BuildSelectObjects")]
    static void BuildSelect()
    {
        //获取所有选中的对象
        Objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        //弹出一个编辑窗口；
        AssetBundleWindow.ShowWindow();
    }

    /// <summary>
    /// 开始打包；
    /// </summary>
    public static void StartBuild()
    {
        Debug.Log("开始打包！");
        string path = AssetBundleWindow.AsbPath;
        Debug.Log("选择路径：" + path);

        //设置出asb[]
        AssetBundleBuild abb = new AssetBundleBuild();
        abb.assetNames = new string[Objs.Length];
        for (int i = 0; i < Objs.Length; i++)
        {
            abb.assetNames[i] = AssetDatabase.GetAssetPath(Objs[i]);
        }
        if (AssetBundleWindow.IsWindows)
        {
            //设置路径；
            Debug.Log("将要打包到Windows");
            abb.assetBundleName = AssetBundleWindow.AssetBudleName + "_windows.UnityAsb";
            //开始打包；
            BuildPipeline.BuildAssetBundles(path, new AssetBundleBuild[] { abb }, BuildAssetBundleOptions.DisableWriteTypeTree, BuildTarget.StandaloneWindows64);
        }
        if (AssetBundleWindow.IsAndorid)
        {
            //设置路径；
            Debug.Log("将要打包到安卓");
            abb.assetBundleName = AssetBundleWindow.AssetBudleName + "_android.UnityAsb";
            //开始打包；
            BuildPipeline.BuildAssetBundles(path, new AssetBundleBuild[] { abb }, BuildAssetBundleOptions.None, BuildTarget.Android);
        }
        if (AssetBundleWindow.IsApple)
        {
            //设置路径：
            Debug.Log("将要打包到tvOS");
            abb.assetBundleName = AssetBundleWindow.AssetBudleName + "_tvOS.UnityAsb";
            //开始打包；
            BuildPipeline.BuildAssetBundles(path, new AssetBundleBuild[] { abb }, BuildAssetBundleOptions.None, BuildTarget.tvOS);
        }
        Debug.Log("打包完成！");
    }
}
