using System.Collections;
using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] private int activeTime = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bird"))
        {
            Destroy(collision.gameObject);
            GameController.Instance.Score += 50;
            Destroy(this.gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterTime());
    }

    private IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(this.gameObject);
    }
}
