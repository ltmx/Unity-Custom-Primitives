using UnityEngine;

[CreateAssetMenu(menuName = "Plugins/PrimitivesSettings")]
public class PrimitivesSettings : ScriptableObject
{
    public ColliderType colliderType = ColliderType.Adaptive;
    public Material material;
    public bool AddRigidbody = false;
    public bool UseGravity = true;

    public enum ColliderType
    {
        Adaptive,
        MeshConvex,
        Mesh,
    }
}