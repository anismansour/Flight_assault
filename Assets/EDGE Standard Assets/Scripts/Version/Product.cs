namespace Edge.UnityStandard
{
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "EDGE/Version/Product")]
    public class Product : ScriptableObject
    {
        public string Url;
        public Texture2D Logo;
        public Version Latest;
    }

#if (UNITY_EDITOR)
    [CustomEditor(typeof(Product))]
    public class ProductEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
           
            var product = (Product)target;

            EditorGUILayout.BeginHorizontal();
            {
                var url = EditorGUILayout.TextField("Url", product.Url);
                if (product.Url != url)
                {
                    product.Url = url;
                    EditorUtility.SetDirty(target);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                var logo = EditorGUILayout.ObjectField("Logo", product.Logo, typeof(Texture2D), true);
                if (product.Logo != logo)
                {
                    product.Logo = (Texture2D)logo;
                    EditorUtility.SetDirty(target);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                var v = (Version)EditorGUILayout.ObjectField("Latest", product.Latest, typeof(Version), true);
                if (v != product.Latest)
                {
                    product.Latest = v;
                    EditorUtility.SetDirty(product);
                }
            }
            EditorGUILayout.EndHorizontal();

            if (product.Latest == null)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Create Version 0.0.1", GUILayout.Height(40)))
                    {
                        var v = new Version();
                        v.Major = 0;
                        v.Minor = 0;
                        v.Build = 1;
                        v.Product = product;
                        var pathCurrent = AssetDatabase.GetAssetPath(product);
                        var pathFolder = pathCurrent.Substring(0, pathCurrent.LastIndexOf("/") + 1);
                        AssetDatabase.CreateAsset(v, pathFolder + v.Formal + ".asset");
                        Selection.activeObject = v;
                        product.Latest = v;
                        EditorUtility.SetDirty(v.Product);
                        EditorUtility.SetDirty(product);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
#endif
}