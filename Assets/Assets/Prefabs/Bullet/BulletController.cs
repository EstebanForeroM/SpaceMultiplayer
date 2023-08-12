using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public float destructionTime = 5f;
    [HideInInspector] public float bulletDamage = 10f;

    [Range(0.95f, 1f)][SerializeField] private float desEscaleRatio = 5f;

    private bool startDestruction = false;
    void Start()
    {
        StartCoroutine(Destruction(destructionTime));
    }

    // Update is called once per frame
    void Update()
    {
        if (startDestruction)
        {
            DestructionProcess();
        }
    }

    IEnumerator Destruction(float time)
    {
        yield return new WaitForSeconds(time);
        startDestruction = true;
    }

    private void DestructionProcess() 
    {
        transform.localScale = transform.localScale * desEscaleRatio;
        if (transform.localScale.magnitude < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
