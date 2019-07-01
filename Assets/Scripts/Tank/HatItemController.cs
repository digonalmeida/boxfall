using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatItemController : MonoBehaviour
{
    private bool _alive;
    private int _lives;
    public void SetItem(ItemConfig item)
    {
        gameObject.SetActive(false);

        if(item != null)
        {
            gameObject.SetActive(true);

            _lives = Mathf.CeilToInt(item.GetCurrentValue());
            _alive = true;
        }
    }

    public bool TakeDamage()
    {
        if(!_alive)
        {
            return false;
        }

        _lives--;

        if (_lives <= 0)
        {
            gameObject.SetActive(false);
            _alive = false;
        }

        return true;
    }
}
