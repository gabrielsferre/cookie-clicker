using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBotaoUpgradeGoldenCoin : ControleBotaoUpgrade {

    //script que controla a golden coin
    private PropriedadesGoldenCoin propriedadesGoldenCoin;

    // Use this for initialization
    void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();

        //inicializa propriedadesConstrucoes
        propriedadesGoldenCoin = GameObject.FindGameObjectWithTag("propriedadesGoldenCoin").GetComponent<PropriedadesGoldenCoin>();

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
        if (propriedadesDinheiro.dinheiro >= propriedadesUpgrades.precoAumentosFrequenciaGoldenCoin[index])
        {
            //compra o upgrade
            compraUpgrade();

            //apaga botao
            Destroy(gameObject);
        }
    }

    //compra o upgrade correspondente, pagando seu preco e ativando o upgrade
    private void compraUpgrade()
    {
        //paga o preco do upgrade
        propriedadesDinheiro.realizaCompra(propriedadesUpgrades.precoAumentosFrequenciaGoldenCoin[index]);

        //ativa o upgrade
        propriedadesUpgrades.ativaUpgradeFrequenciaGoldenCoin(index);

        //atualiza a probabilidade da golden coin aparecer
        propriedadesGoldenCoin.calculaProbabilidade();
    }

    //atribui a sprite correspondente ao botao
    private void carregaSprite()
    {
        GetComponent<Image>().sprite = propriedadesUpgrades.spritesUpgradeAumentosFrequenciaGoldenCoin[index];
    }
}
