using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PrimitivesUtils
{
    
    private const string primitivesSettings = "Primitives Settings";
    
    public static GameObject AddMesh(this GameObject go, string name)
    {
        go.AddComponent<MeshFilter>().mesh = Load<Mesh>(name);
        return go;
    }
    
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
        var g = go.GetComponent<MeshRenderer>();
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
    
    public static GameObject SetConvex(this GameObject go)
    {
        var g = go.GetComponent<MeshCollider>();
        if(g != null) g.convex = true;
        return go;
    }

    public static GameObject AddColliderAdaptive<T>(this GameObject go) where T : Component
    {
        switch (LoadPrimitivesSettings().colliderType)
        {
            case PrimitivesSettings.ColliderType.Adaptive:
                return go.Add<T>();
            case PrimitivesSettings.ColliderType.MeshConvex:
                return go.AddMeshCollider().SetConvex();
            case PrimitivesSettings.ColliderType.Mesh:
                return go.AddMeshCollider();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    
    public static GameObject AddRigidBody(this GameObject go)
    {
        return go.Add<Rigidbody>();
    }
    
    public static GameObject UseGravity(this GameObject go, bool state)
    {
        var g = go.GetComponent<Rigidbody>();
        if(g != null) g.useGravity = state;
        return go;
    }
    
    public static GameObject SetGravityFromSettings(this GameObject go)
    {
        var g = go.GetComponent<Rigidbody>();
        if(g != null) g.useGravity = LoadPrimitivesSettings().UseGravity;
        return go;
    }
    
    
    
    public static GameObject SetAllSettings(this GameObject go)
    {
        var s = LoadPrimitivesSettings();
        var material = go.GetComponent<Material>();

        if (s.material != null) go.AddMaterial(s.material);
        if (s.AddRigidbody) go.AddRigidBody().UseGravity(s.UseGravity);


        return go;
    }
    
    
    
    
    // Shortcut For Resources.Load("Primitives" + name)
    public static T Load<T>(string name) where T : Object => Resources.Load<T>("Primitives/" + name);

    public static PrimitivesSettings LoadPrimitivesSettings() => Load<PrimitivesSettings>(primitivesSettings);

    //Creates A New GameObject
    public static GameObject NewGameObject(string name) => new GameObject(name);

    //adds component and returns the GameObject
    public static GameObject Add<T>(this GameObject go) where T : Component
    {
        go.AddComponent<T>();
        return go;
    }
    
    //Sets The gameObject's parent according to the selection
    public static GameObject SetParent(this GameObject go)
    {
        if(Selection.activeTransform != null)
            go.transform.parent = Selection.activeTransform;
        
        return go;
    }
}