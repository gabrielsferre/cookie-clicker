﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropriedadesDinheiro : MonoBehaviour
{

    //Variaveis
    //Quantidade de dinheiro
    public ulong dinheiro = 0UL;

    //preço do botão
    public ulong precoBotao = 1UL;

    //incremento na taxa dado pelo botão
    public float incrementoTaxaBotao = 10.0F;

    //Taxa de ganho de dinheiro por segundo
    public float taxaGanho = 1.0F;

    //Taxa de ganho por segundo bonus
    public float taxaGanhoBonus = 0.0F;

    //Incremento no clique do mouse
    public float incrementoClick = 1.0F;

    //Variavel usada na funcao atualizaDinheiro
    private float incremento = 0.0F;

    //Testes
    void Start()
    {
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

        if (incremento < 1)
        {
            incremento += taxaTotal * Time.deltaTime;
        }
        else
        {
            incremento = taxaTotal * Time.deltaTime;
        }

        //soh incrementa 'dinheiro' se 'incremento' for maior que 1 
        dinheiro += (ulong) System.Math.Truncate(incremento);
    }

    //incrementa o valor de 'dinherio' de acordo com 'aumento'
    private void aumentaDinheiro(ulong aumento)
    {
        dinheiro += aumento;
    }

    //diminui o valor de 'dinherio' de acordo com 'decremento'
    private void diminuiDinheiro(ulong decremento)
    {

        dinheiro -= decremento;
    }

    //incrementa o valor de 'taxaGanho' de acordo com 'aumento'
    private void aumentaTaxa(ulong aumento)
    {
        taxaGanho += aumento;
    }

    //diminui o valor de 'taxaGanho' de acordo com 'decremento'
    private void diminuiTaxa(ulong decremento)
    {
        taxaGanho -= decremento;
    }

    //incrementa dinheiro no valor 'incrementoClick'
    public void aumentaDinheiroNoClick()
    {
        aumentaDinheiro( (ulong) Mathf.RoundToInt(incrementoClick));
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
        aumentaDinheiro((ulong)(taxaGanho * fator));
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
    public static string contagem(ulong numero)
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
    //numeros menores que mil nao sao alterados
    public static string abreviaNumero(ulong numero)
    {
        string stringNumero = numero.ToString();

        if (stringNumero.Length >= 4)
        {

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

    //igual a sobrecarga de 'ulong', mas para numeros menores que mil, exibe tambem o primeiro valor decimal
    public static string abreviaNumero(float numero)
    {
        string stringNumero;

        if (numero < 1000)
        {
            stringNumero = numero.ToString("F1");
        }
        else
        {
            stringNumero = numero.ToString("F0");

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