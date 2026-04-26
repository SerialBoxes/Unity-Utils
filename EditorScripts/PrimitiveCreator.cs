#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace UnityUtils {

[InitializeOnLoad]
public class PrimitiveCreator : Editor {

    //OH MY GOD FOR ONCE IT WAS JUST WELL DOCUMENTED AND EASY!!!!
    //https://docs.unity3d.com/ScriptReference/MenuItem.html

    [MenuItem("GameObject/3D Object/Cone")]
    public static void Cone(MenuCommand menuCommand) {
        string name = "Cone";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Cube Beveled")]
    public static void CubeBeveled(MenuCommand menuCommand) {
        string name = "CubeBeveled";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Cube Sphere")]
    public static void CubeSphere(MenuCommand menuCommand) {
        string name = "CubeSphere";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Cylinder Quads")]
    public static void CylinderQuads(MenuCommand menuCommand) {
        string name = "CylinderQuads";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Diamond")]
    public static void Diamond(MenuCommand menuCommand) {
        string name = "Diamond";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Disc")]
    public static void Disc(MenuCommand menuCommand) {
        string name = "Disc";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Gear")]
    public static void Gear(MenuCommand menuCommand) {
        string name = "Gear";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Gömböc")]
    public static void Gömböc(MenuCommand menuCommand) {
        string name = "Gömböc";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Icosphere")]
    public static void Icosphere(MenuCommand menuCommand) {
        string name = "Icosphere";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Knot")]
    public static void Knot(MenuCommand menuCommand) {
        string name = "Knot";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Plane 10x10")]
    public static void Plane10x10(MenuCommand menuCommand) {
        string name = "Plane10x10";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Plane 100x100")]
    public static void Plane100x100(MenuCommand menuCommand) {
        string name = "Plane100x100";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Topology/Plane 500x500")]
    public static void Plane500x500(MenuCommand menuCommand) {
        string name = "Plane500x500";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Teapot Hero")]
    public static void TeapotHero(MenuCommand menuCommand) {
        string name = "TeapotHero";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Teapot LOD")]
    public static void TeapotLOD(MenuCommand menuCommand) {
        string name = "TeapotLOD";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    [MenuItem("GameObject/3D Object/Torus")]
    public static void Torus(MenuCommand menuCommand) {
        string name = "Torus";
        PlacePrimitive(FindPrimitive(name), menuCommand);
    }

    static PrimitiveCreator() {
        var ids = AssetDatabase.FindAssets("PrimitivePrefabsList t:PrimitivePrefabs");
        if (ids.Length == 0) {
            ids = AssetDatabase.FindAssets("PrimitivePrefabsList t:PrimitivePrefabs", new string[] { "Assets/Unity-Utils" });
        }

        if (ids.Length == 1) {
            var prefabsObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

            PrimitivePrefabs.instance = (PrimitivePrefabs)prefabsObject;
        }
        else {
            Debug.Log("Couldn't find prefabs :(");
        }
    }

    private static GameObject FindPrimitive(string name) {
        if (PrimitivePrefabs.instance == null) {
            Debug.Log("Primitive Prefabs SO static instance is null!!! Bug Roxy about this and try again it might work idk");
            return null;
        }

        IEnumerable<GameObject> prefabQuery = //variable storing results of the query
            from prefab in PrimitivePrefabs.instance.prefabs //sql style query
            where prefab.name.Equals(name)
            select prefab;
        return prefabQuery.FirstOrDefault();
    }

    private static void PlacePrimitive(GameObject prefab, MenuCommand menuCommand) {
        if (prefab == null) {
            return;
        }

        Vector3 position = Vector3.zero;
        if (SceneView.lastActiveSceneView != null) {
            position = SceneView.lastActiveSceneView.camera.transform.position
                       + SceneView.lastActiveSceneView.camera.transform.forward * 5f;
        }

        GameObject go = Instantiate(prefab, position, Quaternion.identity);
        go.name = prefab.name;

        //reparent only if this was a context click
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}
}
#endif
