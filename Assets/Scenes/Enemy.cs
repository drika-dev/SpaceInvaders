using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float minX, maxX;
    public float shootDelay = 2f;
    public GameObject bulletPrefab;

    private bool canShoot = true;

    private void Start()
    {
        // Definir uma direção de movimento aleatória para cada inimigo
        moveSpeed *= Random.Range(-1, 2);
        // Definir um atraso de disparo aleatório para cada inimigo
        shootDelay = Random.Range(1f, 4f);
        // Iniciar a rotina de disparo
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        // Movimentação horizontal do inimigo
        transform.Translate(moveSpeed * Time.deltaTime, 0f, 0f);

        // Verificar se o inimigo atingiu as bordas da tela
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {
            // Inverter a direção de movimento
            moveSpeed *= -1;
        }
    }

    IEnumerator Shoot()
    {
        while (canShoot)
        {
            // Criar um projétil
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Definir a velocidade do projétil
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * 5f;
            // Aguardar o próximo disparo
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void OnDisable()
    {
        // Parar a rotina de disparo quando o inimigo for desativado
        StopCoroutine(Shoot());
    }
}
