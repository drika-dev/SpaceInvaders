using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gerenciamento : MonoBehaviour
{
    [SerializeField] private string nomeLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelMenuOpcoes;

    public void Jogar(){
        SceneManager.LoadScene(nomeLevelDeJogo);
    }
    public void AbrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelMenuOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        
        painelMenuOpcoes.SetActive(false);
         painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

}
