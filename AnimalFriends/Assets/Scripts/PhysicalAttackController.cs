using UnityEngine;
using UniRx;
using System;

public class PhysicalAttackController : MonoBehaviour
{
    enum Direction { Right = 0, Left = 1, };
    public int attackPower;

    // Use this for initialization
    void Start()
    {
        Observable.Timer(TimeSpan.FromSeconds(0.3f))
                  .Subscribe(_ => Destroy(gameObject))
                  .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDirection(bool isRight)
    {
        Vector2 scale = transform.localScale;
        if (!isRight)
        {
            scale.x = Math.Abs(scale.x) * -1;
        }
        transform.localScale = scale;
    }

    public void SetAttackPower(int power)
    {
        attackPower = power;
    }
}
