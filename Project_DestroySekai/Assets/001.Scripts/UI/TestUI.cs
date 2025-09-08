using UnityEngine;
using UnityEngine.UI;

public class TestUI : MonoBehaviour
{
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()=> { Managers.Scene.LoadScene(Define.Scene.SampleScene); });
    }
}
