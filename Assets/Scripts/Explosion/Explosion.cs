using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float coolTime;

    private void Start()
    {
        StartCoroutine(Extinction());
    }

    public IEnumerator Extinction()
    {
        yield return new WaitForSeconds(coolTime);
        gameObject.SetActive(false);
    }
}
