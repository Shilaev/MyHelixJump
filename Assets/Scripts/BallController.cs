using DefaultNamespace;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _impulseForce;
    private bool _ignoreNextCollision; // ���� ��������� � 2 ������� �� ���, �� �� ����� ������
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_ignoreNextCollision) // ���� true, �� ����� �� OnCollisionEnter
            return;

        // Adding resetLevel functionality via Deathpart - initialized when deathBlock is hit
        var deathBlock = other.transform.GetComponent<DeathBlock>();
        if (deathBlock) deathBlock.HitDeathParts();

        AddImpuls(_impulseForce); // ��� ��� � �������� �����

        _ignoreNextCollision = true;
        Invoke("AllowCollision", .2f); // ������ ���� ����� ����� 2 ����������� ����� ���������� ��������
    }

    private void AllowCollision()
    {
        _ignoreNextCollision = false;
    }

    private void AddImpuls(float impulseForce)
    {
        _rb.velocity = Vector3.zero; // �������� ���������
        _rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // ��������� ������� �����
    }

    public void ResetBall()
    {
        transform.position = _startPos;
    }
}