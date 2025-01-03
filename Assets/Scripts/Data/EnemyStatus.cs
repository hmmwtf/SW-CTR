using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatus", menuName = "EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] private float health = 100f;
    public float Health { get { return health; } }

    [SerializeField] private float attackPower = 10f;
    public float AttackPower { get { return attackPower; } }

    [SerializeField] private float jumpPower = 5f;
    public float JumpPower { get { return jumpPower; } }

    [SerializeField] private float moveSpeed = 2f;
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField] private float defense = 0;
    public float Defense { get { return defense; } }

    [SerializeField] private float attackCoolDown = 1.5f;
    public float AttackCoolDown { get { return attackCoolDown; } }

    [SerializeField] private float detectionRange = 9f;
    public float DetectionRange { get { return detectionRange; } }

    [SerializeField] private float attackRange = 5f;
    public float AttackRange { get { return attackRange; } }

    [SerializeField] private float healingAmount;
    public float HealingAmount { get { return healingAmount; } }

    [SerializeField] private string attackSound = "jump_13";
    public string AttackSound { get { return attackSound; } }

    [SerializeField] private string hitSound = "hit_1";
    public string HitSound { get { return hitSound; } }

    [SerializeField] private string dieSound = "explosion_2";
    public string DieSound { get { return dieSound; } }

    [SerializeField] private float volume = 1f;
    public float Volume { get { return volume; } }

}
