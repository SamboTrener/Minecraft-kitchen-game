using YG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform iconConteiner;
    [SerializeField] Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        if(YandexGame.EnvironmentData.language == "ru")
        {
            recipeNameText.text = recipeSO.recipeNameRussian;
        }
        else
        {
            recipeNameText.text = recipeSO.recipeName;
        }

        foreach (Transform child in iconConteiner)
        {
            if(child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var kitchenObjectSO in recipeSO.kitchenObjectSOList) 
        {
            var iconTransform = Instantiate(iconTemplate, iconConteiner);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
