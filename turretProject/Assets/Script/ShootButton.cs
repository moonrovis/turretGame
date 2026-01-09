using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Player player;

    public void OnPointerDown(PointerEventData eventData)
    {
        player = FindAnyObjectByType<Player>();

        if (player != null)
        {
            player.SetMobileShootPressed(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player = FindAnyObjectByType<Player>();

        if (player != null)
        {
            player.SetMobileShootPressed(false);
        }
    }
}
