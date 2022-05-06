using System.Collections;
using System.Collections.Generic;
using UnityEngine;//运行期间用
using UnityEditor;//编辑状态用

/// <summary>
/// 弹出窗口；
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    [MenuItem("AssetsBundle/ExportAB")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetBundleWindow));
    }
    /// <summary>
    /// asb的名字；
    /// </summary>
    public static string AssetBudleName;
    /// <summary>
    /// asb包的路径；
    /// </summary>
    public static string AsbPath;
    /// <summary>
    /// 是否在Windows下打包；
    /// </summary>
    public static bool IsWindows = false;
    /// <summary>
    /// 是否在安卓下打包；
    /// </summary>
    public static bool IsAndorid = false;
    /// <summary>
    /// 是否在苹果下打包；
    /// </summary>
    public static bool IsApple = false;


    AssetBundleWindow()
    {
        titleContent = new GUIContent("资源打包");
    }


    private void OnGUI()
    {
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 15;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        if (AssetBundle.Objs.Length > 0)
        {
            GUILayout.Label("当前总共选择：" + AssetBundle.Objs.Length + "个资源！");

            //绘制文件路径选择
            GUILayout.Space(10);
            if (GUILayout.Button("路径选择", GUILayout.Width(200)))
            {
                AsbPath = EditorUtility.SaveFolderPanel("请选择打包路径", Application.streamingAssetsPath, AssetBudleName);
                //这里开启窗口重绘制；
                Repaint();
            }

            GUI.skin.label.fontSize = 10;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            if (string.IsNullOrEmpty(AsbPath))
            {
                //这里绘制选择结果；
                GUILayout.Label("没有选择打包路径！");
            }
            else
            {
                //这里绘制选择结果；
                GUILayout.Label("当前选择打包路径：" + AsbPath);

                //放3个togle
                IsWindows = GUI.Toggle(new Rect(10, 100, 600, 20), IsWindows, "打包到Windows平台");

                IsAndorid = GUI.Toggle(new Rect(10, 120, 600, 20), IsAndorid, "打包到Android平台");

                IsApple = GUI.Toggle(new Rect(10, 140, 600, 20), IsApple, "打包到IOS平台");

                //绘制文件打包按钮
                GUILayout.Space(100);
                GUILayout.Label("请输入导出的包名：");
                //设置一个文字输入框；
                AssetBudleName = EditorGUILayout.TextField(AssetBudleName);

                GUILayout.Space(10);

                if (GUILayout.Button("开始打包", GUILayout.Width(200)))
                {
                    if (string.IsNullOrEmpty(AssetBudleName))
                    {
                        Debug.Log("请输入打包文件名！");
                    }
                    else
                    {
                        AssetBundle.StartBuild();
                    }
                }
            }


        }
        else
        {
            GUILayout.Label("当前未选择任何资源！");
        }
    }
}
