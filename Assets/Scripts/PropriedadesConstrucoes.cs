using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesConstrucoes : MonoBehaviour {

    //numero de tipos de construcoes existentes no jogo
    private const int numeroDeConstrucoes = 7;

    //razao entre o preco de compra e preco de venda das construcoes
    private const float razaoCompraVenda = 2.3f;

    //aumento do preco da construcao a cada compra
    private const float aumentoPreco = 0.15f;

    //array com precos de compra iniciais das construcoes
    private float[] precosIniciais = { 0f, 0f, 0f, 0f, 0f, 0f, 0f };

    //array com a producao inicial das construcoes
    private float[] producaoInicial = { 0, 0, 0, 0, 0, 0, 0 };

    //array com quantidade que o jogador possui de cada construcao
    public int[] quantidadesConstrucoes = new int[numeroDeConstrucoes];

    //array com preco de compra das construcoes
    public float[] precosCompra = new float[numeroDeConstrucoes];

    //array com preco de venda das construcoes
    public float[] precosVenda = new float[numeroDeConstrucoes];

    //array com a taxa de aumento de dinheiro de cada construcao
    public float[] producaoConstrucoes = new float[numeroDeConstrucoes];

    //array com a producao total ao longo do jogo de cada construcao
    public float[] producaoTotalConstrucoes = new float[numeroDeConstrucoes];

    //define modo: "venda" ou "compra"
    public string modo = "venda";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        //incrementa producao total de cada construcao
        for( int i = 0; i < numeroDeConstrucoes; i++ )
        {
            atualizaDinheiro(i);
        }
	}

    //incrementa dinheiro que a construcao na posicao "index" do array produz
    private void atualizaDinheiro( int index )
    {
        producaoTotalConstrucoes[index] += producaoConstrucoes[index] * Time.deltaTime;
    }

    //calcula preco de compra da construcao na posicao "index" do array
    private float calculaPrecoCompra( int index )
    {
        return precosIniciais[index] * Mathf.Pow(1 + aumentoPreco, quantidadesConstrucoes[index] - 1);
    }

    //calcula preco de venda da construcao
    private float calculaPrecoVenda( int index )
    {
        return precosCompra[index]/razaoCompraVenda;
    }

    //aumenta quantidade de construcoes e atualiza seu preco de venda e compra
    public void compraConstrucao( int index )
    {
        quantidadesConstrucoes[index]++;
        precosCompra[index] = calculaPrecoCompra(index);
        precosVenda[index] = calculaPrecoVenda(index);
    }

    //diminui quantidade de construcoes e atualiza seu preco de venda e compra
    public void vendeConstrucao(int index)
    {
        quantidadesConstrucoes[index]--;
        precosCompra[index] = calculaPrecoCompra(index);
        precosVenda[index] = calculaPrecoVenda(index);
    }
}
