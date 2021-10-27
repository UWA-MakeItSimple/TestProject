using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;


public class Test : MonoBehaviour
{

    private Material blue;
    private Texture2D haha;
    private GameObject go;
    private GameObject instance1;
    private string path;
    GameObject instance;
    private AssetBundle ab0;

    // Use this for initialization
    private void Awake()
    {
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    byte[] MyDecription(byte[] binary)
    {
        byte[] decrypted = new byte[1024];
        return decrypted;
    }

    IEnumerator loadAsset()
    {
        Debug.Log("loadAsset in");
        string sp1 = Application.streamingAssetsPath + "/test01";
        string sp2 = Application.streamingAssetsPath + "/test02";
        Debug.Log("LoadFromFile before");
        ab0 = AssetBundle.LoadFromFile(sp1);
        Debug.Log("LoadFromFileAsync before");
        AssetBundleCreateRequest ab1 = AssetBundle.LoadFromFileAsync(sp2);
        yield return ab1;
        Debug.Log("Instantiate before");
        Instantiate(ab1.assetBundle.LoadAsset("Cube"));
        Debug.Log("LoadAssetAsync before");
        AssetBundleRequest abrequest = ab0.LoadAssetAsync("Capsule", typeof(GameObject));
        yield return abrequest;
        GameObject gpPrefabs = (GameObject)Instantiate(abrequest.asset);
        Debug.Log("LoadAllAssets before");
        GameObject[] obj = ab0.LoadAllAssets<GameObject>();
        foreach (var item in obj)
        {
            Instantiate(item);
        }
    }

    IEnumerator loadstream()
    {
        Debug.Log("Load Stream");
        var fileStream = new FileStream(Application.persistentDataPath + "//test02", FileMode.Open, FileAccess.Read);
        var LoadFromStreamBundle = AssetBundle.LoadFromStream(fileStream);
        var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);
        yield return bundleLoadRequest;
        LoadFromStreamBundle.Unload(true);
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("Cube");
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;
        Instantiate(prefab);

        myLoadedAssetBundle.Unload(false);
        fileStream.Close();
    }

    IEnumerator loadnet()
    {
        Debug.Log("Load Net");
        var uwr = UnityWebRequest.Get("http://myserver/myBundle.unity3d");
        yield return uwr.SendWebRequest();
        byte[] decryptedBytes = MyDecription(uwr.downloadHandler.data);
        AssetBundle ab0 = AssetBundle.LoadFromMemory(decryptedBytes);
        AssetBundleCreateRequest ab1 = AssetBundle.LoadFromMemoryAsync(decryptedBytes);
        yield return ab1;
        //ab0.Unload(true);
        ab1.assetBundle.Unload(true);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("\n\tLoad\t\n", GUILayout.Width(200), GUILayout.Height(150)))
        {
            //Debug.Log("Start Load");
            //StartCoroutine(loadAsset());
            //StartCoroutine(loadnet());
            //StartCoroutine(loadstream());


            //过了 没问题
            //Debug.Log("Resources.Load before");
            blue = Resources.Load("blue") as Material;
            haha = Resources.Load("huh") as Texture2D;
            CreateCube(blue);
            CreateCube(haha);
            //go = Resources.Load("Cube") as GameObject;
            //instance1 = Instantiate(go);

            //Debug.Log("Resources.LoadAsync before");
            //ResourceRequest request = Resources.LoadAsync("Cube");
            //Instantiate(request.asset);

            //Debug.Log("Resources.LoadAll before");
            //Object[] Gos = Resources.LoadAll("");
            //foreach (var t in Gos)
            //{
            //    Debug.Log(t.name);
            //}
            //instance1.GetComponent<MeshRenderer>().material = blue;

            //Debug.Log("instance2 before");
            //GameObject instance2 = Instantiate(go, instance1.transform, true);
            //Debug.Log("instance3 before");
            //Vector3 pos = new Vector3(0, 2, 0);
            //GameObject instance3 = Instantiate(go, instance1.transform.position, instance1.transform.rotation);
            //Debug.Log("instance4 before");
            //GameObject instance4 = Instantiate(go, pos, instance1.transform.rotation, instance1.transform);
        }
        if (GUILayout.Button("UnloadAsset", GUILayout.Width(200), GUILayout.Height(150)))
        {
            //blue = null;
            Resources.UnloadAsset(haha);
            Debug.Log(haha == null);
            //Destroy(go);
            //ab0.Unload(true);

        }
        if (GUILayout.Button("SetActive", GUILayout.Width(200), GUILayout.Height(150)))
        {
            //instance1.SetActive(false);
        }

        if (GUILayout.Button("CreateCube", GUILayout.Width(200), GUILayout.Height(150)))
        {
            CreateCube(haha);
        }
    }
    private Vector3 _position = new Vector3(-3, 0, 0);
    public void CreateCube(Material material)
    {
        _position += new Vector3(1.5f, 0, 0);
        var plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.position = _position;
        var planeRender = plane.GetComponent<MeshRenderer>();
        planeRender.material = material;
    }
    public void CreateCube(Texture2D textureTex2d)
    {
        _position += new Vector3(1.5f, 0, 0);
        var plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.position = _position;
        var planeMaterial = new Material(Shader.Find(@"Legacy Shaders/Diffuse"));
        var planeRender = plane.GetComponent<MeshRenderer>();
        planeRender.material = planeMaterial;
        planeMaterial.mainTexture = textureTex2d;
    }
}
