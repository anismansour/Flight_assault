namespace Edge.UnityStandard
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "EDGE/Version/Version")]
    [Serializable]
    public class Version : ScriptableObject
    {
        public int Major = 0;

        public int Minor = 1;

        public int Build = 1;

        public string User = "";

        public Product Product;

        public string Notes = "";

        public string Formal
        {
            get
            {
                return Product.name + " v" + VersionNumbers;
            }
        }

        public string VersionNumbers
        {
            get
            {
                return Major + "." + Minor + "." + Build;
            }
        }
    }

#if (UNITY_EDITOR)
    [CustomEditor(typeof(Version))]
    public class VersionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var version = (Version)target;
            GUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var v = (Product)EditorGUILayout.ObjectField("Product", version.Product, typeof(Product), true);
                    if (v != version.Product)
                    {
                        version.Product = v;
                        EditorUtility.SetDirty(version);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    if (string.IsNullOrEmpty(version.User))
                    {
                        version.User = UserName;
                        EditorUtility.SetDirty(target);
                    }

                    var user = EditorGUILayout.TextField("User", version.User);
                    if (user != version.User)
                    {
                        version.User = user;
                        EditorUtility.SetDirty(target);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Version");
                    var v = EditorGUILayout.IntField(version.Major);
                    if (v != version.Major)
                    {
                        version.Major = v;
                        EditorUtility.SetDirty(target);
                    }

                    v = EditorGUILayout.IntField(version.Minor);
                    if (v != version.Minor)
                    {
                        version.Minor = v;
                        EditorUtility.SetDirty(target);
                    }

                    v = EditorGUILayout.IntField(version.Build);
                    if (v != version.Build)
                    {
                        version.Build = v;
                        EditorUtility.SetDirty(target);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    EditorStyles.textField.wordWrap = true;
                    var v = EditorGUILayout.TextArea(version.Notes, GUILayout.Height(100));
                    if (v != version.Notes)
                    {
                        version.Notes = v;
                        EditorUtility.SetDirty(target);
                    }
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Next Version", GUILayout.Height(40)))
                    {
                        var v2 = new Version();
                        v2.Major = version.Major;
                        v2.Minor = version.Minor;
                        v2.Build = version.Build + 1;
                        v2.Product = version.Product;
                        var pathCurrent = AssetDatabase.GetAssetPath(version);
                        var pathFolder = pathCurrent.Substring(0, pathCurrent.LastIndexOf("/") + 1);
                        AssetDatabase.CreateAsset(v2, pathFolder + v2.Formal + ".asset");
                        Selection.activeObject = v2;
                        v2.Product.Latest = v2;
                        EditorUtility.SetDirty(v2.Product);
                        EditorUtility.SetDirty(v2);
                    }

                    if (GUILayout.Button("Copy Message", GUILayout.Height(40)))
                    {
                        GUIUtility.systemCopyBuffer = version.Formal + ":\r\n" + version.Notes;
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        public string UserName
        {
            get
            {
                var editorAssembly = Assembly.GetAssembly(typeof(UnityEditor.EditorWindow));
                var unityConnect = editorAssembly.CreateInstance(
                    "UnityEditor.Connect.UnityConnect", 
                    false, 
                    BindingFlags.NonPublic | BindingFlags.Instance, 
                    null, null, null, null);
                var type = unityConnect.GetType();
                var userinfo = type.GetProperty("userInfo").GetValue(unityConnect, null);
                var userinfoType = userinfo.GetType();
                return userinfoType.GetProperty("displayName").GetValue(userinfo, null) as string;
            }
        }
    }
#endif
}