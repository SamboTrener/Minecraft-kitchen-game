using UnityEngine;

public class Villager : MonoBehaviour
{
    public static Villager Instance { get; private set; }

    public bool IsVillagerComming { get; private set; } = true;
    public bool VillagerShouldLeave { get; private set; } = false;

    Vector3 originalPosition;

    void Update()
    {
        if (IsVillagerComming)
            VillagerComming();
        if (VillagerShouldLeave)
            VillagerLeave();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        originalPosition = transform.position;
    }
    void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) => VillagerShouldLeave = true;

    void VillagerComming()
    {
        if (transform.position.x > 8f)
        {
            transform.forward = Vector3.right;
            transform.position += Vector3.left * Time.deltaTime;
        }
        else
        {
            IsVillagerComming = false;
        }
    }

    void VillagerLeave()
    {
        if(transform.position.z >= -8)
        {
            transform.forward = Vector3.forward;
            transform.position += Vector3.back * Time.deltaTime * 3;
        }
        else
        {
            transform.position = originalPosition;
            VillagerShouldLeave = false;
            IsVillagerComming = true;
        }
    }
}
