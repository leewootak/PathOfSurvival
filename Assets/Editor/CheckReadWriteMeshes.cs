using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckReadWriteMeshes : MonoBehaviour
{
    [MenuItem("Tools/Check Read/Write Enabled Meshes")]
    static void CheckMeshes()
    {
        string[] meshGUIDs = AssetDatabase.FindAssets("t:Mesh"); // 모든 메시 검색
        foreach (string guid in meshGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);

            if (mesh != null && !IsMeshReadable(mesh))
            {
                Debug.LogWarning($"Read/Write가 비활성화된 메시: {mesh.name} ({path})", mesh);
            }
        }
    }

    static bool IsMeshReadable(Mesh mesh)
    {
        return mesh != null && mesh.isReadable;
    }
}
