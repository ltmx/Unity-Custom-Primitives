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


    [MenuItem("GameObject/3D Object/Cone")]
    public static void SpawnCone() => SpawnBaseObject(cone).AddMeshCollider().SetConvex();

    [MenuItem("GameObject/3D Object/Create Smooth Cube")]
    public static void SpawnSmoothCube() => SpawnBaseObject(smoothCube).AddColliderAdaptive<BoxCollider>();

    [MenuItem("GameObject/3D Object/Create Smooth Icosphere")]
    public static void SpawnSmoothIcosphere() => SpawnBaseObject(smoothIcosphere).AddMeshCollider().SetConvex();

    [MenuItem("GameObject/3D Object/Create Smooth Cylinder")]
    public static void SpawnSmoothCylinder() => SpawnBaseObject(smoothCylinder).AddMeshCollider().SetConvex();

    [MenuItem("GameObject/3D Object/Create Torus")]
    public static void SpawnTorus() => SpawnBaseObject(torus).AddColliderAdaptive<MeshCollider>();
    
    private static GameObject SpawnBaseObject(string name) => NewGameObject(name).AddMesh(name).AddMaterial().SetParent().SetAllSettings();
    
}