using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBotaoUpgradeConstrucao : ControleBotaoUpgrade {

    //script que controla as construcoes
    private PropriedadesConstrucoes propriedadesConstrucoes;

    //linha e coluna que o upgrade se encontra na matriz
    public int linha;
    public int coluna;

    // Use this for initialization
    void Start () {
        
        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();
        
        //inicializa propriedadesConstrucoes
        propriedadesConstrucoes = GameObject.FindGameObjectWithTag("propriedadesConstrucoes").GetComponent<PropriedadesConstrucoes>();

        //Adiciona a funcao que sera usada no click
        GetComponent<Button>().onClick.AddListener(executaNoClick);

        //Carrega sprite do botao
        carregaSprite();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //metodo chamado quando o botao for clicado
    private void executaNoClick()
    {
        //se o jogador tiver dinheiro o suficiente
        if (propriedadesDinheiro.dinheiro >= propriedadesUpgrades.precoUpgradesConstrucoes[linha,coluna])
        {
            //compra o upgrade
            compraUpgrade();

            //apaga botao
            Destroy(gameObject);
        }
    }

    //compra o upgrade correspondente, pagando seu preco e ativando o upgrade
    //presume que a ordem do array de construcoes eh igual a ordem das linhas na matriz de upgrades de construcoes
    private void compraUpgrade()
    {
        //paga o preco do upgrade
        propriedadesDinheiro.realizaCompra(propriedadesUpgrades.precoUpgradesConstrucoes[linha, coluna]);

        //ativa o upgrade
        propriedadesUpgrades.ativaUpgradeConstrucao(linha, coluna);

        //atualiza a producao das construcoes
        propriedadesConstrucoes.atualizaProducao(linha);
    }

    //atribui a sprite correspondente ao botao
    private void carregaSprite()
    {
        GetComponent<Image>().sprite = propriedadesUpgrades.spritesUpgradesConstrucoes[linha,coluna];
    }
}
