using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleBotaoUpgradeClique : ControleBotaoUpgrade {

	// Use this for initialization
	void Start () {

        //inicializa propriedadesDinheiro
        propriedadesDinheiro = GameObject.FindGameObjectWithTag("propriedadesDinheiro").GetComponent<PropriedadesDinheiro>();

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();

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
        if (propriedadesDinheiro.dinheiro >= propriedadesUpgrades.precoAumentosClique[index])
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
        propriedadesDinheiro.realizaCompra(propriedadesUpgrades.precoAumentosClique[index]);

        //ativa o upgrade
        propriedadesUpgrades.ativaUpgradeClique(index);

        //atualiza o quanto se ganha do clicque
        propriedadesDinheiro.calculaIncrementoClick();
    }

    //atribui a sprite correspondente ao botao
    private void carregaSprite()
    {
        GetComponent<Image>().sprite = propriedadesUpgrades.spritesUpgradesAumentosClique[index];
    }
}
