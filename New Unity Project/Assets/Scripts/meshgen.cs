using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct TMeshData
{
    public Vector3[] vertices;
    public int[] triangles;
}

[System.Serializable]
public struct THorizontData
{
    public string name;
    public int color;     
    public TMeshData surfaceup;
    public TMeshData surfacedn;
    public TMeshData surfacesd;
}

[System.Serializable]
public struct TModelMeshData
{
    public THorizontData[] horizonts;
}


[System.Serializable]
public struct TModelBore
{
    public string name;
    public int id;
    public float x_up, y_up, z_up;
    public float x_dn, y_dn, z_dn;
}


[System.Serializable]
public struct TModelData3D
{
    public TModelBore[] bores;
    public TModelMeshData mesh;
}

[System.Serializable]
public struct TGMFdata
{
    public TModelData3D data3d;
}

[System.Serializable]
public struct TJSONdata
{
    public TGMFdata gmfdata;
}


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class meshgen : MonoBehaviour
{
    private TJSONdata jsondata;
    public string gmfFileName = "plotfull.json";
    public GameObject bore_prefab;
    //GameObject stol = new GameObject("Stol");
    //Instantiate(stol, new Vector3(0, 0, 0), Quaternion.identity);

    private void Generate()
    {
    }

    private void Awake()
    {
        LoadData();
        ScaleMesh();
    }

    private void LoadData()
    {
        string gmfFilePath = Path.Combine(Application.streamingAssetsPath, gmfFileName);
        print(gmfFilePath);

        if (File.Exists(gmfFilePath))
        {
            string gmfAsJson = File.ReadAllText(gmfFilePath);
            jsondata = JsonUtility.FromJson<TJSONdata>(gmfAsJson);
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void ScaleMesh()
    {

        float minx, maxx, miny, maxy, minz, maxz;
        float scalex = 0.02F, scaley = 0.05F, scalez = 0.02F;
        float x, y, z;
 
        minx = miny = minz = 1000000000.0F;
        maxx = maxy = maxz = -minx;

        for (int h = 0; h < jsondata.gmfdata.data3d.mesh.horizonts.Length; h++)
        {
            THorizontData horz = jsondata.gmfdata.data3d.mesh.horizonts[h];
            Vector3[] vertices;

            for (int j = 0; j < 3; j++)
            {
                vertices = horz.surfaceup.vertices;
                if (j == 1) vertices = horz.surfacedn.vertices;
                if (j == 2) vertices = horz.surfacesd.vertices;

                for (int i = 0; i < vertices.Length; i++)
                {
                    x = vertices[i].x *= scalex;
                    y = vertices[i].y *= scaley;
                    z = vertices[i].z *= scalez;
                    if (x > maxx) maxx = x;
                    if (x < minx) minx = x;
                    if (y != 0.0F && y > maxy) maxy = y;
                    if (y != 0.0F && y < miny) miny = y;
                    if (z > maxz) maxz = z;
                    if (z < minz) minz = z;
                }
            }
        }

        x = -(minx + maxx) * 0.5F;
        y = -miny;
        z = -(minz + maxz) * 0.5F;

        for (int h = 0; h < jsondata.gmfdata.data3d.mesh.horizonts.Length; h++)
        {
            THorizontData horz = jsondata.gmfdata.data3d.mesh.horizonts[h];
            Vector3[] vertices;

            for (int j = 0; j < 3; j++)
            {
                vertices = horz.surfaceup.vertices;

                if (j == 1) vertices = horz.surfacedn.vertices;
                if (j == 2) vertices = horz.surfacesd.vertices;

                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i].x += x;
                    if (vertices[i].y != 0.0F) vertices[i].y += y;
                    vertices[i].z += z;
                }
            }
        }

        for (int i = 0; i < jsondata.gmfdata.data3d.bores.Length; i++)
        {
            TModelBore Bore = jsondata.gmfdata.data3d.bores[i];

            jsondata.gmfdata.data3d.bores[i].x_dn *= scalex;
            jsondata.gmfdata.data3d.bores[i].x_dn += z;
            jsondata.gmfdata.data3d.bores[i].y_dn *= scalex;
            jsondata.gmfdata.data3d.bores[i].y_dn += x;
            jsondata.gmfdata.data3d.bores[i].z_dn *= scaley;
            jsondata.gmfdata.data3d.bores[i].z_dn += y;

            jsondata.gmfdata.data3d.bores[i].x_up *= scalex;
            jsondata.gmfdata.data3d.bores[i].x_up += z;
            jsondata.gmfdata.data3d.bores[i].y_up *= scalex;
            jsondata.gmfdata.data3d.bores[i].y_up += x;
            jsondata.gmfdata.data3d.bores[i].z_up *= scaley;
            jsondata.gmfdata.data3d.bores[i].z_up += y;

//            jsondata.gmfdata.data3d.bores[i] = Bore;
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        GeneratePlotBores();
        GeneratePlotMesh();
    }

    private void GeneratePlotBores()
    {
        for (int i = 0; i < jsondata.gmfdata.data3d.bores.Length; i++)
        {
            TModelBore Bore = jsondata.gmfdata.data3d.bores[i];
            Vector3 pos = new Vector3(Bore.y_dn, Bore.z_dn+1.0F, Bore.x_dn);
//            Vector3 pos = new Vector3((Bore.y_dn + Bore.y_up) * 0.5F, (Bore.z_dn + Bore.z_up) * 0.5F, (Bore.x_dn + Bore.x_up) * 0.5F);

            GameObject bore = Instantiate(bore_prefab, pos, Quaternion.identity);
            bore.transform.parent = transform;
        }
    }
    private void GeneratePlotMesh()
    { 
        for (int h = 0; h < jsondata.gmfdata.data3d.mesh.horizonts.Length; h++)
        {
            THorizontData horzmesh = jsondata.gmfdata.data3d.mesh.horizonts[h];
            string name;

            name = "mesh" + h;

            GameObject horz;
            horz = new GameObject(name);
            horz.transform.parent = transform;
            horz.AddComponent<MeshFilter>();

            Mesh mesh;

            CombineInstance[] combine = new CombineInstance[3];

            combine[0].mesh = mesh = new Mesh();
            combine[0].transform = Matrix4x4.identity;
            mesh.vertices = horzmesh.surfaceup.vertices;
            mesh.triangles = horzmesh.surfaceup.triangles;

            combine[1].mesh = mesh = new Mesh();
            combine[1].transform = Matrix4x4.identity;
            mesh.vertices = horzmesh.surfacedn.vertices;
            mesh.triangles = horzmesh.surfacedn.triangles;

            combine[2].mesh = mesh = new Mesh();
            combine[2].transform = Matrix4x4.identity;
            mesh.vertices = horzmesh.surfacesd.vertices;
            mesh.triangles = horzmesh.surfacesd.triangles;

            horz.GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Mesh"+h;
            mesh.CombineMeshes(combine);
            mesh.RecalculateNormals();

            MeshRenderer meshRenderer = horz.AddComponent<MeshRenderer>();
            meshRenderer.material = Resources.Load("OnePlaneCrossSection", typeof(Material)) as Material;
            Material material = meshRenderer.material;
            material.color = new Color32((byte)(horzmesh.color % (256 * 256)), (byte)((horzmesh.color / 256) % 256), (byte)(horzmesh.color / (256 * 256)), 255);
            material.SetColor("_CrossColor", new Color32((byte)(horzmesh.color % (256 * 256)), (byte)((horzmesh.color / 256) % 256), (byte)(horzmesh.color / (256 * 256)), 255));
           
            var foundObjects = FindObjectsOfType<GameObject>();
            GameObject quad = foundObjects[0];

            foreach (object o in foundObjects)
            {
                GameObject g = (GameObject)o;
                if (g.name=="Quad") quad = g;
            }

            OnePlaneCuttingController ctrl = horz.AddComponent<OnePlaneCuttingController>();
            ctrl.plane = quad;
            ctrl.transform.parent = transform;


            Rigidbody rigibody = horz.AddComponent<Rigidbody>();
            rigibody.isKinematic = true;
            
            MeshCollider meshCollider = horz.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;

            horz.transform.position = new Vector3(0F, 0.0F, 0F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
