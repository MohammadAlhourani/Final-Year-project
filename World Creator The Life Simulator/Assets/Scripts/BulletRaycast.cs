using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

//the attack raycast class
public static class BulletRaycast 
{
    //the enemy shoot
    //shoot a raycast that when is collides with a player
    //it will damage the player
    //ignores enemies
    public static void EnemyShoot(Vector3 t_shootPos, Vector3 t_shootDir)
    {
        int layerMask = ~(LayerMask.GetMask("Enemy"));

        RaycastHit2D raycastHit = Physics2D.Raycast(t_shootPos, t_shootDir, 50, layerMask);

        BulletTracer(t_shootPos, raycastHit.point);

        if(raycastHit.collider != null)
        {
            Debug.Log(raycastHit.collider.gameObject.name);

            raycastHit.collider.gameObject.GetComponent<Human>().Damage(10);
        }
    }

    //the player shoot
    //shoots a raycast that when it collides with an enemy
    //it will damage the enemy
    //ignores the player
    public static void PlayerShoot(Vector3 t_shootPos, Vector3 t_shootDir)
    {
        int layerMask = ~(LayerMask.GetMask("Player"));

        RaycastHit2D raycastHit = Physics2D.Raycast(t_shootPos, t_shootDir, Vector3.Magnitude(t_shootDir), layerMask);

        BulletTracer(t_shootPos, raycastHit.point);

        if (raycastHit.collider != null)
        {
            Debug.Log(raycastHit.collider.gameObject.name);

            if (raycastHit.collider.gameObject.GetComponent<Enemy>() != null)
            {
                raycastHit.collider.gameObject.GetComponent<Enemy>().Damage(10);
            }
            else if (raycastHit.collider.gameObject.GetComponent<FSMEnemy>() != null)
            {
                raycastHit.collider.gameObject.GetComponent<FSMEnemy>().Damage(10);
            }
            else if (raycastHit.collider.gameObject.GetComponent<CNMEnemy>() != null)
            {
                raycastHit.collider.gameObject.GetComponent<CNMEnemy>().Damage(10);
            }
        }
    }

    //creates a mesh to visually represent the raycast
    //then deletes the mesh after a few seconds
    private static void BulletTracer(Vector3 t_startPoint, Vector3 t_endPoint)
    {
        Vector3 direction = (t_endPoint - t_startPoint).normalized;

        Material tracer = (Material)Resources.Load("Textures/BulletTracer");

        float eulerZ =  Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

        float distance = Vector3.Distance(t_startPoint, t_endPoint);

        Vector3 tracerSpawnPos = t_startPoint + direction * 0.5f;

        World_Mesh bulletMesh = World_Mesh.Create(tracerSpawnPos, eulerZ, 0f, distance, tracer, null, 10000);

        float timer = 0.1f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                bulletMesh.DestroySelf();
                return true;
            }
            return false;
        });
    }

}




