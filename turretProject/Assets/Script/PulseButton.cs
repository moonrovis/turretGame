using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PulseButton : MonoBehaviour
{
    private Button button; // Ссылка на кнопку
    private Vector3 scaleMax = new Vector3(1.1f, 1.1f, 1.1f); // Максимальный масштаб
    private float duration = 0.5f; // Время одного изменения (увеличение/уменьшение)

    private Vector3 scaleDefault;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        scaleDefault = button.transform.localScale;

        // Запускаем циклическую анимацию масштаба
        Pulse();
    }

    private void Pulse()
    {
        button.transform.DOScale(scaleMax, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo); // Бесконечный цикл туда-обратно
    }
}