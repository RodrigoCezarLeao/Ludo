using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ludo
{
    public class Ludo
    {
        public static void Main()
        {
            Console.WriteLine("Bem vindo ao jogo Ludo!");
            Console.WriteLine("Quantos jogadores (de 2 até 4) ?");
            int.TryParse(Console.ReadLine(), null, out int qntJogadores);

            if (qntJogadores > 4 || qntJogadores < 2)
                qntJogadores = 2;

            Console.WriteLine($"O jogo terá {qntJogadores} jogadores.");            
            Tabuleiro t = new Tabuleiro(qntJogadores);
            Helpers.Aguarde();
            

            while (true)
            {
                for (int jogador = 0; jogador < qntJogadores; jogador++)
                {
                    string corJogador = Helpers.seqPeoes[jogador];
                    Console.Clear();
                    Console.WriteLine($"Vez do Jogador {corJogador}: \n");
                    t.ImprimeTabuleiro();

                    Console.WriteLine("Pressione uma tecla para rolar o dado.");
                    Console.ReadLine();

                    int retornoJogada = fazerJogada(t, jogador);

                    if (retornoJogada == 0)
                    {
                        Console.WriteLine($"O Jogador {corJogador} passou a jogada.\n");
                        Helpers.Aguarde(3);
                    }
                    else if (retornoJogada == 2)
                    {                        
                        t.ImprimeTabuleiro();
                        Helpers.Aguarde();
                    }
                    else if (retornoJogada == 1 || retornoJogada == 3)
                    {
                        Console.WriteLine("Pressione uma tecla para rolar o dado novamente.");
                        Console.ReadLine();

                        retornoJogada = fazerJogada(t, jogador);
                        Helpers.Aguarde(3);

                        if (retornoJogada == 0)
                        {
                            Console.WriteLine($"O Jogador {corJogador} passou a segunda jogada.\n");
                            Helpers.Aguarde(3);
                        }
                        else
                        {
                            t.ImprimeTabuleiro();
                            Helpers.Aguarde();
                        }                        
                    }


                }
            }
            
        }


        // 0 - Passar Jogada
        // 1 - Tirar Peão da Base
        // 2 - Andar normalmente no tabuleiro
        // 3 - Finalizar Peão
        public static int fazerJogada(Tabuleiro t, int jogador)
        {
            int retorno = 0;

            int valorDado = Helpers.JogarDado();
            string corJogador = Helpers.seqPeoes[jogador];
            Console.WriteLine($"O Jogador {corJogador} tirou {valorDado} no dado.");


            Peao[] peoesDisponiveisParaJogar = new Peao[4];
            int contPeoesDisponiveisParaJogar = 0;

            bool possivelJogadaTirarDaBase = false;
            bool possivelJogadaAndarNormalmente = false;
            bool possivelJogadaParaTerminar = false;

            if (valorDado == 6)
            {
                if (t.JogadorTemPeaoNaBase(corJogador))
                {
                    possivelJogadaTirarDaBase = true;
                }
            }
            possivelJogadaAndarNormalmente = t.JogadorTemPeaoQuePodemAndarNormalmente(corJogador);
            possivelJogadaParaTerminar = t.JogadorTemPeaoQuePodemTerminar(corJogador, valorDado);


            // Mostrar as opções de jogada para o usuário
            if (possivelJogadaTirarDaBase)
            {
                string peoes = t.ImprimirPeoesDaBase(corJogador);
                if (!peoes.Contains(","))
                {
                    Console.WriteLine($"- Uma possível jogada é retirar o peao {peoes} da base.");
                }
                else
                {
                    Console.WriteLine($"- Uma possível jogada é retirar os peoes {peoes} da base.");
                }

                // Selecionar Peões Jogáveis
                Peao[] peoesNaBase = t.PeoesDoJogadorQueEstaoNaBase(corJogador);
                for(int i = 0; i < peoesNaBase.Length; i++)
                {
                    peoesDisponiveisParaJogar[contPeoesDisponiveisParaJogar] = peoesNaBase[i];
                    contPeoesDisponiveisParaJogar++;
                }
            }
            if (possivelJogadaAndarNormalmente)
            {
                string peoes = t.ImprimirPeoesQuePodemAndarNormalmente(corJogador);
                if (!peoes.Contains(","))
                {
                    Console.WriteLine($"- Uma possível jogada é andar com o peao {peoes} no tabuleiro.");
                }
                else
                {
                    Console.WriteLine($"- Uma possível jogada é andar com os peoes {peoes} no tabuleiro.");
                }

                // Selecionar Peões Jogáveis
                Peao[] peoesParaAndar = t.PeoesDoJogadorQuePodemAndarNormalmente(corJogador);
                for (int i = 0; i < peoesParaAndar.Length; i++)
                {
                    peoesDisponiveisParaJogar[contPeoesDisponiveisParaJogar] = peoesParaAndar[i];
                    contPeoesDisponiveisParaJogar++;
                }
            }
            if (possivelJogadaParaTerminar)
            {
                string peoes = t.ImprimirPeoesQuePodemTerminar(corJogador, valorDado);
                if (!peoes.Contains(","))
                {
                    Console.WriteLine($"- Uma possível jogada é terminar o peao {peoes} no tabuleiro.");
                }
                else
                {
                    Console.WriteLine($"- Uma possível jogada é terminar os peoes {peoes} no tabuleiro.");
                }

                // Selecionar Peões Jogáveis
                Peao[] peoesParaTerminar = t.PeoesDoJogadorQuePodemTerminar(corJogador, valorDado);
                for (int i = 0; i < peoesParaTerminar.Length; i++)
                {
                    peoesDisponiveisParaJogar[contPeoesDisponiveisParaJogar] = peoesParaTerminar[i];
                    contPeoesDisponiveisParaJogar++;
                }
            }

            if (possivelJogadaParaTerminar || possivelJogadaAndarNormalmente || possivelJogadaTirarDaBase)
            {
                Peao? peaoSelecionado = null;

                if (peoesDisponiveisParaJogar.Where(x => x != null).Count() == 1)
                {
                    peaoSelecionado = peoesDisponiveisParaJogar[0];
                }
                else if (peoesDisponiveisParaJogar.Where(x => x != null).Count() > 1)
                {
                    // Escolher a jogada pelo ID do peão
                    Console.WriteLine("\nDigite o ID do peão que você quer jogar: ");
                    string id = Console.ReadLine();

                    peaoSelecionado = peoesDisponiveisParaJogar.FirstOrDefault(x => x.Id == id);
                }

                if (peaoSelecionado != null)
                {
                    if (peaoSelecionado.Status == 0)
                    {
                        peaoSelecionado.TirarPeaoDaBase();
                        retorno = 1;
                    }
                    else if (peaoSelecionado.Status == 1 || peaoSelecionado.Status == 4)
                    {
                        peaoSelecionado.AndarComPeao(valorDado);
                        retorno = 2;
                    }
                    else if (peaoSelecionado.Status == 3)
                    {
                        peaoSelecionado.AndarComPeao(valorDado);
                        retorno = 3;
                    }
                }
            }

            return retorno;
        }
    }
}