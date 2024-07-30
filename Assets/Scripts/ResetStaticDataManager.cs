using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    private void Awake()
    {
        BaseTable.ResetStaticData();
        CuttingTable.ResetStaticData();
        Trash.ResetStaticData();
        Furnace.ResetStaticData();
        Chest.ResetStaticData();
        Table.ResetStaticData();
        TutorialUI.ResetStaticData();
    }
}
