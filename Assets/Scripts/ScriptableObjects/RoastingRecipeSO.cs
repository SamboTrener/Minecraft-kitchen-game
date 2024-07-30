using UnityEngine;

[CreateAssetMenu]
public class RoastingRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input;
    public KitchenObjectSO Output;
    public float roastingTimerMax;
}
