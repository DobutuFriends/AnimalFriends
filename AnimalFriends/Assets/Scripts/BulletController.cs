using UnityEngine;
using UniRx;
using System;

public class BulletController : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(5.0f))
                  .Subscribe(_ => Destroy(gameObject))
                  .AddTo(this);
    }

    void Update()
    {
        Vector3 vector = new Vector3(speed, 0, 0);

        transform.position += vector;
    }
}
