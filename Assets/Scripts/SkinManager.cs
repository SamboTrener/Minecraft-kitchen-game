using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [SerializeField] SkinnedMeshRenderer mesh;
    [SerializeField] SkinSO defaultSkin;
    [SerializeField] SkinListSO skins;


    private void Awake()
    {
        LoadData();
    }

    void LoadData()
    {
        var savedName = SaveLoadManager.GetCurrentSkinName();
        SkinSO currentSkin = null;
        foreach (var skin in skins.skinSOList)
        {
            if(savedName == skin.Name)
            {
                currentSkin = skin;
                break;
            }
        }
        if (currentSkin == null)
        {
            ChangeSkin(defaultSkin);
        }
        else
        {
            ChangeSkin(currentSkin);
        }
    }

    void ChangeSkin(SkinSO newSkin)
    {
        Debug.Log(newSkin);
        mesh.material = newSkin.Material;
        Player.Instance.SpeedMultiplier = newSkin.SpeedMultiplier;
    }
}
