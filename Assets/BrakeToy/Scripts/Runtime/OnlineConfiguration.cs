using UnityEngine;
namespace BrakeToy
{
    [CreateAssetMenu(menuName="Brake Toy/Online configuration")]
    public sealed class OnlineConfiguration:ScriptableObject
    {public string Endpoint="";public bool EligibilityAndPrivacyReviewed;}
}
