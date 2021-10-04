using DefaultNamespace;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _impulseForce;
    private bool _ignoreNextCollision; // Если ударяется в 2 объекта за раз, то не будет ошибки
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_ignoreNextCollision) // Если true, то выйди из OnCollisionEnter
            return;

        // Adding resetLevel functionality via Deathpart - initialized when deathBlock is hit
        var deathBlock = other.transform.GetComponent<DeathBlock>();
        if (deathBlock) deathBlock.HitDeathParts();

        AddImpuls(_impulseForce); // Пни мяч с заданной силой

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f); // вызови этот метод через 2 милисекунды после предыдущих дейсвтий
    }

    private void AllowCollision()
    {
        _ignoreNextCollision = false;
    }

    private void AddImpuls(float impulseForce)
    {
        _rb.velocity = Vector3.zero; // обнуляет ускорение
        _rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // импульсом толкает вверх
    }

    public void ResetBall()
    {
        transform.position = _startPos;
    }
}