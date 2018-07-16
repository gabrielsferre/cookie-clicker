﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesDinheiro : MonoBehaviour
{

    //Variaveis
    //Quantidade de dinheiro
    public float dinheiro = 0f;

    //Taxa de ganho de dinheiro por segundo
    public float taxaGanho = 1.0F;

    //Taxa de ganho por segundo bonus
    public float taxaGanhoBonus = 0.0F;

    //Incremento inicial do clique do mouse
    public float incrementoClickInicial = 1.0F;

    //Incremento do clique do mouse
    public float incrementoClick = 1.0F;

    //script que controla as construcoes
    private PropriedadesConstrucoes propriedadesConstrucoes;

    //script que controla os upgrades
    private PropriedadesUpgrades propriedadesUpgrades;

    //Testes
    void Start()
    {
        //inicializa propriedadesConstrucoes
        propriedadesConstrucoes = GameObject.FindGameObjectWithTag("propriedadesConstrucoes").GetComponent<PropriedadesConstrucoes>();

        //inicializa propriedadesUpgrades
        propriedadesUpgrades = GameObject.FindGameObjectWithTag("propriedadesUpgrades").GetComponent<PropriedadesUpgrades>();

        //atualiza a taxa de ganho
        calculaTaxaGanho();
    }

    // Update is called once per frame
    void Update()
    {
        atualizaDinheiro();
    }

    //incrementa dinheiro de acordo com a taxa
    private void atualizaDinheiro()
    {

        float taxaTotal = taxaGanho + taxaGanhoBonus;

        dinheiro += taxaTotal * Time.deltaTime;
    }

    //incrementa o valor de 'dinherio' de acordo com 'aumento'
    public void aumentaDinheiro(float aumento)
    {
        dinheiro += aumento;
    }

    //diminui o valor de 'dinherio' de acordo com 'decremento'
    private void diminuiDinheiro(float decremento)
    {

        dinheiro -= decremento;
    }

    //incrementa o valor de 'taxaGanho' de acordo com 'aumento'
    private void aumentaTaxa(float aumento)
    {
        taxaGanho += aumento;
    }

    //diminui o valor de 'taxaGanho' de acordo com 'decremento'
    private void diminuiTaxa(float decremento)
    {
        taxaGanho -= decremento;
    }

    //recalcula o valor da taxa de ganho a partir das construcoes e upgrades
    public void calculaTaxaGanho()
    {
        taxaGanho = 0;

        //contribuicao das construcoes
        for(int i = 0; i < PropriedadesConstrucoes.numeroDeConstrucoes; i++)
        {
            taxaGanho += propriedadesConstrucoes.producaoConstrucoes[i]*propriedadesConstrucoes.quantidadesConstrucoes[i];
        }

        //contribuicao dos upgrades
        for(int i = 0; i < PropriedadesUpgrades.numeroAumentoTaxa; i++)
        {
            //aumenta taxa ganho de uma certa porcentagem caso o jogador possua o update
            taxaGanho *= (propriedadesUpgrades.upgradesAumentoTaxa[i]) ? 1 + propriedadesUpgrades.aumentosTaxa[i] : 1;
        }

        //atualiza o valor do dinheiro ganho no clique
        calculaIncrementoClick();
    }

    //recalcula o valor do dinheiro ganho no clique
    public void calculaIncrementoClick()
    {
        incrementoClick = incrementoClickInicial;

        //contribuicao dos upgrades
        for (int i = 0; i < PropriedadesUpgrades.numeroAumentoClique; i++)
        {
            //aumenta o incremento do clique de uma certa porcentagem da taxa de ganho caso o jogador possua o update
            incrementoClick += (propriedadesUpgrades.upgradesAumentoClique[i]) ? taxaGanho*propriedadesUpgrades.aumentosClique[i] : 0;
        }
    }

    //incrementa dinheiro no valor 'incrementoClick'
    public void aumentaDinheiroNoClick()
    {
        aumentaDinheiro( Mathf.RoundToInt(incrementoClick));
    }

    //realiza a compra de uma construcao ou upgrade
    public void realizaCompra( float preco )
    {
        if( preco <= dinheiro )
        {
            diminuiDinheiro(preco);
        }
    }

    //define bonus para a taxa de ganho de dinheiro por segundo (bonus eh um fator de 'taxaGanho')
    public void adicionaTaxaBonus(float fator)
    {
        //o termo '(fator - 1)' se explica pelo fato de 'taxaGanhoBonus' ser SOMADO a 'taxaGanho' em 'atualizaDinheiro()'
        taxaGanhoBonus = taxaGanho * (fator - 1F);
    }

    //define bonus de dinheiro baseado em 'taxaGanho'
    public void acrescentaBonusDeDinherio(float fator)
    {
        aumentaDinheiro((taxaGanho * fator));
    }

    public void retiraTaxaBonus()
    {
        taxaGanhoBonus = 0;
    }

    //retorna valor da taxa de ganho total (taxaGanho + taxaGanhoBonus)
    public float getTaxaGanhoTotal()
    {
        return taxaGanho + taxaGanhoBonus;
    }

    //define o "sufixo" que sera dado ao numero (milhão, bilhão, trilhão)
    public static string contagemDinheiroTotal(float numero)
    {

        if (numero >= 1000000000000000000)
            return "quintilhões de";

        if (numero >= 1000000000000000)
            return "quadrilhões de";

        if (numero >= 1000000000000)
            return "trilhões de";

        if (numero >= 1000000000)
            return "bilhões de";

        if (numero >= 1000000)
            return "milhões de";

        if (numero >= 1000)
            return "mil";

        return "";
    }
    //sobrecarga para float
    public static string contagem(float numero)
    {

        if (numero >= 1000000000000000000)
            return "quintilhões";

        if (numero >= 1000000000000000)
            return "quadrilhões";

        if (numero >= 1000000000000)
            return "trilhões";

        if (numero >= 1000000000)
            return "bilhões";

        if (numero >= 1000000)
            return "milhões";

        if (numero >= 1000)
            return "mil";

        return "";
    }

    //coloca o numero em um formato igual ao do cookie clicker, contendo no maximo os 6 algarismos mais significativos
    //destes algarismos mais significativos, os 3 menos significativos ficam separados do resto por um ponto se o numero for maior ou igual a mil
    //para numeros menores que mil, exibe tambem o primeiro valor decimal
    public static string abreviaNumero(float numero)
    {
        string stringNumero;

        if (numero < 1000)
        {
            stringNumero = numero.ToString("F2"); //string do numero com duas casas decimais
        }
        else
        {
            stringNumero = numero.ToString("F0"); //string do numero sem casas decimais

            if (stringNumero.Length % 3 == 1)
            {
                stringNumero = stringNumero.Substring(0, 1) + "." + stringNumero.Substring(1, 3);
            }

            else if (stringNumero.Length % 3 == 2)
            {
                stringNumero = stringNumero.Substring(0, 2) + "." + stringNumero.Substring(2, 3);
            }

            else if (stringNumero.Length % 3 == 0)
            {
                stringNumero = stringNumero.Substring(0, 3) + "." + stringNumero.Substring(3, 3);
            }
        }

        return stringNumero;
    }
}