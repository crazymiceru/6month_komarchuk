using System;
using UnityEngine;

public class OnGroundView : MonoBehaviour
{
    public bool isOnGround = false;
    private int _countGround = 0;

    public event Action<bool> evtUpdate = delegate { };

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        _countGround++;
        isOnGround = true;
        if (_countGround == 1)
        {
            evtUpdate.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _countGround--;
        if (_countGround == 0)
        {
            isOnGround = false;
            evtUpdate.Invoke(false);
        }
    }

}
