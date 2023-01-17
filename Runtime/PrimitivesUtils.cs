using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PrimitivesUtils
{
    
    private const string primitivesSettings = "Primitives Settings.asset";
    private const string packageDataPath = "Packages/com.ltmx.primitives/Runtime/Objects/";
    
    public static GameObject AddMesh(this GameObject go, string name) => go.AddComponent<MeshFilter>()?.LoadMesh(name).gameObject;

    /// Adds Material From Path
    public static GameObject AddMaterial(this GameObject go, string name)
    {
        //Check if Renderer is Already active
        var g = go.GetComponent<MeshRenderer>(); 
        if (g != null) g.material = Load<Material>(name);
        else go.AddComponent<MeshRenderer>().material = Load<Material>(name);
        return go;
    }
    
    /// Adds Material From Settings Asset
    public static GameObject AddMaterial(this GameObject go)
    {
        //Check if Renderer is Already active
        var g = go.Get<MeshRenderer>();
        if (g != null) g.material = LoadPrimitivesSettings().material;
        else go.AddComponent<MeshRenderer>().material = LoadPrimitivesSettings().material;
        return go;
    }
    
    /// Adds Material From Field
    public static GameObject AddMaterial(this GameObject go, Material material)
    {
        var g = go.GetComponent<MeshRenderer>();
        if (g != null) g.material = material;
        else go.AddComponent<MeshRenderer>().material = material;
        return go;
    }
    
    public static GameObject AddMeshCollider(this GameObject go) => go.Add<MeshCollider>();
    public static GameObject AddBoxCollider(this GameObject go) => go.Add<BoxCollider>();
    public static GameObject AddSphereCollider(this GameObject go) => go.Add<SphereCollider>();
    public static GameObject AddCapsuleCollider(this GameObject go) => go.Add<SphereCollider>();
    


    public static GameObject AddColliderAdaptive<T>(this GameObject go) where T : Component
    {
        return LoadPrimitivesSettings().colliderType switch
        {
            PrimitivesSettings.ColliderType.Adaptive => go.Add<T>(),
            PrimitivesSettings.ColliderType.MeshConvex => go.AddMeshCollider().SetConvex(),
            PrimitivesSettings.ColliderType.Mesh => go.AddMeshCollider(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    
    public static GameObject SetGravityFromSettings(this GameObject go)
    {
        var g = go.Get<Rigidbody>();
        if(g != null) g.useGravity = LoadPrimitivesSettings().UseGravity;
        return go;
    }
    
    
    
    public static GameObject ApplySettings(this GameObject go)
    {
        var s = LoadPrimitivesSettings();
        if (s.material != null) go.AddMaterial(s.material);
        if (s.AddRigidbody) go.AddRigidBody().UseGravity(s.UseGravity);
        return go;
    }
    
    public static T Load<T>(string name) where T : Object => AssetDatabase.LoadAssetAtPath<T>(packageDataPath + name);

    public static PrimitivesSettings LoadPrimitivesSettings() => Load<PrimitivesSettings>(primitivesSettings);

    //Creates A New GameObject
    public static GameObject NewGameObject(string name) => new(name);

    
    public static MeshFilter LoadMesh(this MeshFilter mf, string name)
    {
        mf.mesh = Load<Mesh>(name);
        return mf;
    }
}

public static class PrimitiveCreationExtension
{
    //Sets The gameObject's parent according to the selection
    public static GameObject SetParent(this GameObject go)
    {
        if(Selection.activeTransform != null)
            go.transform.parent = Selection.activeTransform;
        
        return go;
    }
    
    //adds component and returns the GameObject
    public static GameObject Add<T>(this GameObject go) where T : Component
    {
        go.AddComponent<T>();
        return go;
    }
    public static T Get<T>(this GameObject go) where T : Component => go.GetComponent<T>();
    
    public static GameObject UseGravity(this GameObject go, bool state) => go.Get<Rigidbody>().SetGrapity(state).gameObject;
    public static GameObject AddRigidBody(this GameObject go) => go.Add<Rigidbody>();
    public static GameObject SetConvex(this GameObject go) => go.Get<MeshCollider>()?.SetConvex().gameObject;

    public static MeshCollider SetConvex(this MeshCollider m)
    {
        m.convex = true;
        return m;
    }
    public static Rigidbody SetGrapity(this Rigidbody r, bool state)
    {
        r.useGravity = state;
        return r;
    }
    
    
}