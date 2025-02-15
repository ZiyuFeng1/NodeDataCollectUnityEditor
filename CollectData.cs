using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



namespace Ziyu_DataCollectionScript
{

    public class CollectData : EditorWindow
    {
        //奉來！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！--
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！

        //補秘方象
        GameObject node = null;
        


        //隠贋方象

        //光嶽双燕
        List<GameObject> childList = new List<GameObject>();
        List<GameObject> AllInstances=new List<GameObject>();
        List<GameObject> AllPrefab=new List<GameObject>();
        List<string> AllPrefabPath=new List<string>();
        List<GameObject> childListPrefab=new List<GameObject>();
        Renderer[] rendArray;
        List<Material> materials = new List<Material>();
        List<string> materialsName = new List<string>();



        //光嶽柴方
        int instanceCount = 0;
        int prefabCount = 0;
        int childCount = 0;
        int materialCount=0;

        int vertexInstance = 0;
        int surfaceInstance = 0;
        
       

        //械楚
        private Vector2 _scrollPosition;

        //UI中医
        string filterInput;
        bool isOpen = false;



        //痕方！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！

        //  UI中医
        [MenuItem("Ziyu's Tool/Data Collection &q")]
        private static void ShowWindow()
        {
            CollectData window = GetWindow<CollectData>();
            window.titleContent = new GUIContent("方象辺鹿");
            window.Show();
            window.position = new Rect(new Vector2(600, 25), new Vector2(600, 600));
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("俶勣由柴佚連議麗悶准泣", GUILayout.Width(200));

            EditorGUI.BeginChangeCheck();
            node = EditorGUILayout.ObjectField(node, typeof(GameObject), true, GUILayout.Width(350)) as GameObject;
         
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            if (EditorGUI.EndChangeCheck())
            {
                Init();
            }
            
            GUILayout.Button("麗悶方象", GUILayout.Width(595));
      


            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和Instance方朕葎:",GUILayout.Width(500));
            GUILayout.Label(instanceCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和Prefab方朕葎:", GUILayout.Width(500));
            GUILayout.Label(prefabCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和GameObject方朕葎:", GUILayout.Width(500));
            GUILayout.Label(childCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和Material方朕葎:",GUILayout.Width(500));
            GUILayout.Label(materialCount.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和Vertex方朕葎:", GUILayout.Width(500));
            GUILayout.Label(vertexInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("乎准泣和Triangles方朕葎:",GUILayout.Width(500) );
            GUILayout.Label(surfaceInstance.ToString(), GUILayout.Width(80));
            GUILayout.EndHorizontal();





            GUILayout.Space(20);
           
            if(node!=null)
            {
                GUILayout.Label("乎准泣Prefab双燕��", GUILayout.Width(600));
                isOpen = EditorGUILayout.ToggleLeft("頁倦宥狛購囚忖臥孀Prefab", isOpen, GUILayout.Width(400));
                if (isOpen)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("萩補秘購囚忖", GUILayout.Width(280));
                    filterInput = GUILayout.TextField(filterInput, GUILayout.Width(300));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                }
            }
                       
            
          
           
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            foreach (GameObject ob in AllPrefab)
            {
                if(ob.name.IndexOf(filterInput)==-1)
                {
                    continue;
                }
                GUILayout.BeginHorizontal();
                
                if(GUILayout.Button(ob.name,GUILayout.Width(200)))
                {
                    Selection.objects = SelectInstance(ob);
                    
                }
                GUILayout.Box("競泣方葎    " + GetVertexTriPrefab(ob).vertex, GUILayout.Width(190));
                GUILayout.Box("眉叔中方葎    " + GetVertexTriPrefab(ob).triangle, GUILayout.Width(190));
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
         
        }



        //耕協痕方！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        private void onEnable()
        {
            Init();
        }
        private void Update()
        {

        }




        //兜兵晒/泡仟 ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        private void Init()
        {
            childList.Clear();
            AllPrefab.Clear();
            AllInstances.Clear();
            AllPrefabPath.Clear();
            childListPrefab.Clear();
            materials.Clear();
            materialsName.Clear();
            prefabCount = 0;
            instanceCount = 0;
            childCount = 0;
            vertexInstance = 0;
            surfaceInstance = 0;
            materialCount=0;
            filterInput = "";


            Count();
        }


        //恷嶮柴方
        private void Count()  //由柴乎准泣和侭嗤徨麗悶方朕
        {
            //譜崔双燕方象
            GetAllChild(node);
            GetVertexTri();
            GetInstanceList(node);
            GetPrefabList();
            GetAllMaterials();



            //資函柴方
            childCount = childList.Count;
            instanceCount = AllInstances.Count;
            prefabCount = AllPrefab.Count;
            materialCount = materials.Count;



        }


        //徨麗悶  ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        private void GetAllChild(GameObject node)
        {

            if (node != null)
            {
                for (int i = 0; i < node.transform.childCount; i++)
                {                    

                    if (node.transform.GetChild(i).childCount > 0)
                    {
                        GetAllChild(node.transform.GetChild(i).gameObject);
                    }                   

                    childList.Add(node.transform.GetChild(i).gameObject);             //資函輝念麗悶准泣和侭嗤徨麗悶��繍侭嗤徨麗悶紗秘徨麗悶双燕

                }
            }
        }
        private void GetVertexTri()         //柴麻競泣方嚥眉叔中
        {
            foreach (GameObject obj in childList)
            {
                Component[] filters;
                filters = obj.GetComponentsInChildren<MeshFilter>();
                foreach (MeshFilter f in filters)
                {
                    vertexInstance += f.sharedMesh.vertexCount;
                    surfaceInstance += f.sharedMesh.triangles.Length / 3;
                }
            }
        }

        //糞箭晒斤��Instance  ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        private void GetInstanceList(GameObject node)         //柴麻乎麗悶准泣和侭嗤instant議prefab徨麗周��繍侭嗤乎窃麗悶紗秘instance双燕嶄
        {
            
            for (int i = 0; i < node.transform.childCount; i++)
            {

                if (PrefabBool(node.transform.GetChild(i).gameObject))
                {
                    
                    AllInstances.Add(node.transform.GetChild(i).gameObject);
                }

                else if (node.transform.GetChild(i).childCount > 0)
                {
                    GetInstanceList(node.transform.GetChild(i).gameObject);
                }

            }
        }


        //圓崙悶Prefab    ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！

        private void GetPrefabList()             //繍准泣麗悶和徨麗悶prefab殊霞��飛殊霞葎音揖prefab夸繍凪恵秘仟議list嶄恬葎prefabList��飛嶷鹸夸柳狛
        {
            
            foreach (GameObject obj in AllInstances)
            {
                string prefabPath=GetPrefabPath(obj);
                if(!AllPrefabPath.Contains(prefabPath))
                {
                    AllPrefabPath.Add(prefabPath);
                    AllPrefab.Add(obj);
                }
                 

            }
        }
        private VerTri GetVertexTriPrefab(GameObject obj)         //柴麻競泣方嚥眉叔中
        {
            VerTri tri = new VerTri();
            foreach (GameObject ob in GetAllChildPrefab(obj))
            {
                Component[] filters;
                filters = ob.GetComponentsInChildren<MeshFilter>();
                foreach (MeshFilter f in filters)
                {
                    tri.vertex += f.sharedMesh.vertexCount;
                    tri.triangle += f.sharedMesh.triangles.Length / 3;
                }
            }
            return tri;
        }
        private List<GameObject> GetAllChildPrefab(GameObject obj)         
        {

            if (obj != null)
            {
                List <GameObject> list= new List<GameObject>();
                for (int i = 0; i < obj.transform.childCount; i++)
                {

                    if (obj.transform.GetChild(i).childCount > 0)
                    {
                        GetAllChildPrefab(obj.transform.GetChild(i).gameObject);
                    }

                    list.Add(obj.transform.GetChild(i).gameObject);             //資函輝念麗悶准泣和侭嗤徨麗悶��繍侭嗤徨麗悶紗秘徨麗悶双燕

                }
                return list;
            }
            return null;
        }

        //可嵎Material    ！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！


        

        private void GetAllMaterials()
        {
            materials.Clear();
            foreach(GameObject obj in childList)
            {
                rendArray = obj.transform.GetComponentsInChildren<Renderer>(true);
                for (int i = 0; i < rendArray.Length; i++)
                {
                    Material[] mats = rendArray[i].materials;
                    for (int j = 0; j < mats.Length; j++)
                    {
                        if(!materialsName.Contains(mats[j].name))
                        {
                            materials.Add(mats[j]);
                            materialsName.Add(mats[j].name);
                        }
                       
                    }
                }
            }
            
          
        }








        //光嶽垢醤痕方！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        //！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
        private static bool PrefabBool(GameObject obj)           //登僅麗悶頁倦葎Instance Prefab
        {
            if (obj != null)
            {
                var type = PrefabUtility.GetPrefabAssetType(obj);
                var status = PrefabUtility.GetPrefabInstanceStatus(obj);
                if (type == PrefabAssetType.NotAPrefab || status == PrefabInstanceStatus.NotAPrefab)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
                return false;
        }

        private string GetPrefabPath(GameObject gobj)                //貫糞箭晒麗悶資函坿彿創揃抄
        {
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gobj))
            {
                return UnityEditor.AssetDatabase.GetAssetPath(gobj);
            }
            if (UnityEditor.PrefabUtility.IsPartOfPrefabInstance(gobj))
            {
                var assetPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(gobj);
                return UnityEditor.AssetDatabase.GetAssetPath(assetPrefab);
            }
            return null;

        }

        private GameObject[] SelectInstance(GameObject obj)
        {
            List<GameObject> sel = new List<GameObject>();
            string assetPath= GetPrefabPath(obj);
            foreach(GameObject instance in AllInstances)
            {
                string instancePath =GetPrefabPath(instance);
                if(assetPath== instancePath)
                {
                    sel.Add(instance);
                }
            }
            return sel.ToArray();
        }

  

       





    }

    class VerTri
    {
        public int vertex;
        public int triangle;
        public VerTri()
        {
            vertex = 0;
            triangle = 0;
        }

    }
}

