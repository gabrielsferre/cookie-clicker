using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesConstrucoes : MonoBehaviour {

    //numero de tipos de construcoes existentes no jogo
    public const int numeroDeConstrucoes = 7;

    //razao entre o preco de compra e preco de venda das construcoes
    private const float razaoCompraVenda = 2.3f;

    //aumento do preco da construcao a cada compra
    private const float aumentoPreco = 0.15f;

    //array com precos de compra iniciais das construcoes
    private float[] precosIniciais = { 10f, 100f, 1000f, 10000f, 100000f, 1000000f, 10000000f };

    //array com a producao inicial das construcoes
    private float[] producaoInicial = { 10f, 100f, 1000f, 10000f, 100000f, 1000000f, 10000000f };

    //array com quantidade que o jogador possui de cada construcao
    public int[] quantidadesConstrucoes = new int[numeroDeConstrucoes];

    //array com preco de compra das construcoes
    public float[] precosCompra = new float[numeroDeConstrucoes];

    //array com preco de venda das construcoes
    public float[] precosVenda = new float[numeroDeConstrucoes];

    //array com a taxa de aumento de dinheiro de cada construcao
    public float[] producaoConstrucoes = { 10f, 100f, 1000f, 10000f, 100000f, 1000000f, 10000000f };

    //array com a producao total ao longo do jogo de cada construcao
    public float[] producaoTotalConstrucoes = new float[numeroDeConstrucoes];

    //script que controla os upgrades
    private PropriedadesUpgrades propriedadesUpgrades;

    //define modo: "venda" ou "compra"
    public string modo = "compra";

    void Awake()
    {
        //atualiza preco de compra
        for (int i = 0; i < numeroDeConstrucoes; i++)
        {
            precosCompra[i] = calculaPrecoCompra(i);
        }
    }

    // Use this for initialization
    void Start () {

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();

        //atualiza a producao de todas as construcoes
        for( int i = 0; i < numeroDeConstrucoes; i++)
        {
            atualizaProducao(i);
        }
    }
	
	// Update is called once per frame
	void Update () {

        //incrementa producao total de cada construcao
        for (int i = 0; i < numeroDeConstrucoes; i++)
        {
            atualizaDinheiro(i);
        }
    }

    //incrementa dinheiro que a construcao na posicao "index" do array produziu
    private void atualizaDinheiro( int index )
    {
        producaoTotalConstrucoes[index] += quantidadesConstrucoes[index]*producaoConstrucoes[index] * Time.deltaTime;
    }

    //calcula preco de compra da construcao na posicao "index" do array
    private float calculaPrecoCompra( int index )
    {
        return precosIniciais[index] * Mathf.Pow(1 + aumentoPreco, quantidadesConstrucoes[index]);
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

    //atualiza a producao de uma certa construcao
    public void atualizaProducao(int index)
    {
        //fator que irah multiplicar a producao inicial da construcao
        int fator = 1;

        //percorre os upgrades da construcao
        for (int i = 0; i < PropriedadesUpgrades.numeroDeCoresConstrucoes; i++)
        {
            //multiplica fator por dois se o upgrade estiver ativado
            fator *= (propriedadesUpgrades.upgradesConstrucoes[index, i]) ? 2 : 1;
        }

        producaoConstrucoes[index] = producaoInicial[index] * fator;
    }
}
