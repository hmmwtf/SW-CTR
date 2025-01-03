using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTriangle : EnemyBase
{
    public float attackForce = 7f;

    protected override IEnumerator Attack()
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.AddForce(Vector2.up, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);


        AudioManager.PlaySound(status.AttackSound, status.Volume);
        IsAttacking = true;
        rb.AddForce(dir * attackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        IsAttacking = false;
    }
}
