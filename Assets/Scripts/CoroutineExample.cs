using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(PrintOverTime());
    }

    #region First

    IEnumerator PrintOverTime()
    {
        Debug.Log($"Message was printed instantly.");
        yield return new WaitForMouseDown();
        Debug.Log($"Message was printed after right click.");
    }

    #endregion

    #region Second
    IEnumerator MoveUp(float time, Vector3 direction)
    {
        yield return new WaitForSeconds(time);
        transform.position += direction;
    }
    #endregion

    #region Third

    IEnumerator MoveAround()
    {
        transform.position += Vector3.up;
        yield return new WaitForSeconds(1);
        transform.position += Vector3.left;
        yield return new WaitForSeconds(1);
        transform.position += Vector3.down;
        yield return new WaitForSeconds(1);
        transform.position += Vector3.right;
    }

    #endregion

    #region Fourth

        IEnumerator PrintAndDestroy()
        {
            int i = 10;
            while (true)
            {
                Debug.Log($"{i} seconds left.");
                i--;
                if (i == 1) this.enabled = false; // деактивирует скрипт, но всё равно продолжит работать
                if (i == 5) Destroy(this.gameObject); // уничтожит объект и прекратит работу
                yield return new WaitForSeconds(1);
            }
        }

        #endregion
}
