using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public AudioClip gameOverSound;
    public Animator enemyAnimator;
    public GameObject gameOverText;

    private int lives = 3;
    private bool isGameOver = false;

    private void Update()
    {
        if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("MenuPrincipal");
            }
            return;
        }

        // Movimentação do jogador usando as setas direcionais
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        transform.Translate(moveSpeed * moveX * Time.deltaTime, moveSpeed * moveY * Time.deltaTime, 0f);

        // Limitar o movimento do jogador dentro da área da tela
        float clampedX = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);
        float clampedY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        // Disparo do jogador usando o clique esquerdo do mouse
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Colisão com inimigo
        if (collision.CompareTag("Enemy"))
        {
            lives--;
            if (lives > 0)
            {
                if (enemyAnimator != null)
                {
                    enemyAnimator.SetTrigger("Explode");
                }
                Destroy(collision.gameObject);
            }
            else
            {
                isGameOver = true;
                StartCoroutine(GameOver());
            }
        }

        // Colisão com tiro do inimigo
        if (collision.CompareTag("EnemyBullet"))
        {
            lives--;
            Destroy(collision.gameObject);

            if (lives <= 0)
            {
                isGameOver = true;
                StartCoroutine(GameOver());
            }
        }
    }

    private void Shoot()
    {
        // Criar um projétil
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        // Tocar o som de disparo
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    private IEnumerator GameOver()
    {
        // Mostrar mensagem de "Game Over"
        gameOverText.SetActive(true);
        // Tocar o som de "Game Over"
        AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
        // Aguardar 3 segundos antes de retornar ao Menu Principal
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MenuPrincipal");
    }
}
