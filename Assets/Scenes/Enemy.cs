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
    private static int enemyCount = 0;

    private void Start()
    {
        if (enemyCount == 0)
        {
            // Gerar inimigos em posições aleatórias no início do jogo
            GenerateEnemies();
        }

        // Definir uma direção de movimento aleatória para cada inimigo
        moveSpeed *= Random.Range(-1, 2);
        // Definir um atraso de disparo aleatório para cada inimigo
        shootDelay = Random.Range(1f, 4f);
        // Iniciar a rotina de disparo
        StartCoroutine(Shoot());

        enemyCount++;
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
        enemyCount--;

        if (enemyCount == 0)
        {
            // Gerar inimigos em posições aleatórias quando todos os inimigos forem destruídos
            GenerateEnemies();
        }
    }

    private void GenerateEnemies()
    {
        // Definir o número de inimigos a serem gerados
        int numberOfEnemies = Random.Range(3, 6);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Gerar posição aleatória para o inimigo
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
            // Criar o inimigo na posição aleatória
            Instantiate(gameObject, randomPosition, Quaternion.identity);
        }
    }
}
