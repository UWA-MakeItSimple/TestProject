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
    private GameObject instance2;
    private GameObject instance3;
    private GameObject instance4;
    private string path;
    GameObject instance;
    private AssetBundle ab0;

    byte[] MyDecription(byte[] binary)
    {
        byte[] decrypted = new byte[1024];
        return decrypted;
    }

    IEnumerator loadAsset()
    {
        string sp1 = Application.streamingAssetsPath + "/test01";
        string sp2 = Application.streamingAssetsPath + "/test02";
        ab0 = AssetBundle.LoadFromFile(sp1);
        AssetBundleCreateRequest ab1 = AssetBundle.LoadFromFileAsync(sp2);
        yield return ab1;
        Instantiate(ab1.assetBundle.LoadAsset("Cube"));
        AssetBundleRequest abrequest = ab0.LoadAssetAsync("Capsule", typeof(GameObject));
        yield return abrequest;
        GameObject gpPrefabs = (GameObject)Instantiate(abrequest.asset);
        GameObject[] obj = ab0.LoadAllAssets<GameObject>();
        foreach (var item in obj)
        {
            Instantiate(item);
        }
    }

    IEnumerator loadstream()
    {
        var fileStream = new FileStream(Application.persistentDataPath + "//test02", FileMode.Open, FileAccess.Read);
        var LoadFromStreamBundle = AssetBundle.LoadFromStream(fileStream);
        var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);
        yield return bundleLoadRequest;
        LoadFromStreamBundle.Unload(true);
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
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
        ab0.Unload(true);
        ab1.assetBundle.Unload(true);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("\n\tLoad\t\n", GUILayout.Width(200), GUILayout.Height(150)))
        {
            StartCoroutine(loadAsset());
            StartCoroutine(loadnet());
            StartCoroutine(loadstream());

            blue = Resources.Load("blue") as Material;
            haha = Resources.Load("Picture") as Texture2D;

            CreateCube(blue);
            CreateCube(haha);

            go = Resources.Load("Cube") as GameObject;
            instance1 = Instantiate(go);

            ResourceRequest request = Resources.LoadAsync("Cube");
            Instantiate(request.asset);

            Object[] Gos = Resources.LoadAll("");
            foreach (var t in Gos)
            {
                Debug.Log(t.name);
            }

            instance1.GetComponent<MeshRenderer>().material = blue;
            instance2 = Instantiate(go, instance1.transform, true);
            Vector3 pos = new Vector3(0, 2, 0);
            instance3 = Instantiate(go, instance1.transform.position, instance1.transform.rotation);
            instance4 = Instantiate(go, pos, instance1.transform.rotation, instance1.transform);
        }
        if (GUILayout.Button("SetActive", GUILayout.Width(200), GUILayout.Height(150)))
        {
            instance1.SetActive(true);
            instance2.SetActive(true);
            instance3.SetActive(true);
            instance4.SetActive(true);

            instance1.SetActive(false);
            instance2.SetActive(false);
            instance3.SetActive(false);
            instance4.SetActive(false);
        }
        if (GUILayout.Button("UnloadAsset", GUILayout.Width(200), GUILayout.Height(150)))
        {
            //blue = null;
            Resources.UnloadAsset(haha);
            Resources.UnloadAsset(blue);
            Destroy(go);
            Destroy(instance1);
            Destroy(instance2);
            Destroy(instance3);
            Destroy(instance4);
            ab0.Unload(true);

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

    void Update()
    {
        // AudioSource.Play
        var audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        // GameObject.AddComponent
        gameObject.AddComponent<Light>();
        //CharacterController.Move
        var cc = gameObject.GetComponent<CharacterController>();
        cc.Move(new Vector3(0, 0, 0));
        //ParticleSystem.Simulate
        var pc = gameObject.GetComponent<ParticleSystem>();
        pc.Simulate(Time.deltaTime * 5f, false, false, false);
        //Application.internetReachability
        var ira=Application.internetReachability;
        var gbs=SystemInfo.batteryStatus;
        var bgl=SystemInfo.batteryLevel;
        Debug.Log(ira + "/" + gbs + "/" + bgl);
    }
}
