using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAttackingIndicator : MonoBehaviour
{
    [SerializeField] private static GameObject _objectIndicator;

    public static void toggleIndicator()
    {
        if (_objectIndicator.gameObject.activeSelf)
        {
            _objectIndicator.gameObject.SetActive(false);
        }
        else
        {
            _objectIndicator.gameObject.SetActive(true);
        }
    }
}
