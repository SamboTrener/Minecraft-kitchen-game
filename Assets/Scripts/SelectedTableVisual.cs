using UnityEngine;

public class SelectedTableVisual : MonoBehaviour
{
    [SerializeField] BaseTable table;
    [SerializeField] GameObject visualGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedTableChanged += Player_OnSelectedTableChanged;
    }

    private void Player_OnSelectedTableChanged(object sender, Player.OnSelectedTableChangedEventArgs e)
    {
        if(e.selectedTable == table)
            visualGameObject.SetActive(true);
        else
            visualGameObject.SetActive(false);
    }
}
