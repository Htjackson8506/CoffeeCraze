using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition;
    }
}