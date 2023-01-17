using UnityEngine;
using UnityEditor;
using static PrimitivesUtils;

public class Primitives : MonoBehaviour
{
    private const string cone = "Cone";
    private const string smoothCube = "Smooth Cube";
    private const string smoothIcosphere = "Smooth Icosphere";
    private const string smoothCylinder = "Smooth Cylinder";
    private const string torus = "Torus";
    private const string meshFileExtension = ".fbx";
    private const string path = "GameObject/3D Object/";


    [MenuItem(path + "Cone")]
    public static void SpawnCone() => SpawnBaseObject(cone).AddMeshCollider().SetConvex();

    [MenuItem(path + "Smooth Cube")]
    public static void SpawnSmoothCube() => SpawnBaseObject(smoothCube).AddColliderAdaptive<BoxCollider>();

    [MenuItem(path + "Smooth Icosphere")]
    public static void SpawnSmoothIcosphere() => SpawnBaseObject(smoothIcosphere).AddMeshCollider().SetConvex();

    [MenuItem(path + "Smooth Cylinder")]
    public static void SpawnSmoothCylinder() => SpawnBaseObject(smoothCylinder).AddMeshCollider().SetConvex();

    [MenuItem(path + "Torus")]
    public static void SpawnTorus() => SpawnBaseObject(torus).AddColliderAdaptive<MeshCollider>();
    
    private static GameObject SpawnBaseObject(string name) => NewGameObject(name).AddMesh(name + meshFileExtension).AddMaterial().SetParent().ApplySettings();
    
}