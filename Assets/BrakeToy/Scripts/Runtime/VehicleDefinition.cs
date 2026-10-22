using UnityEngine;
namespace BrakeToy
{
    [CreateAssetMenu(menuName="Brake Toy/Vehicle")]
    public sealed class VehicleDefinition:ScriptableObject
    {public string Title;public int Cost;public GameObject VisualPrefab;public Color TrailColor=Color.cyan;public string Silhouette;}
}
