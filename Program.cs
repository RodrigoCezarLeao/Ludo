using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ludo
{
    public class Ludo
    {
        public static void Main()
        {
            Log log = new Log();
            string logBatch = "";
            
            Helpers.LogText("Bem vindo ao jogo Ludo!", ref logBatch);
            Helpers.LogText("Quantos jogadores (de 2 até 4) ?", ref logBatch);
            int.TryParse(Console.ReadLine(), null, out int qntJogadores);

            if (qntJogadores > 4 || qntJogadores < 2)
                qntJogadores = 2;

            Helpers.LogText($"O jogo terá {qntJogadores} jogadores.", ref logBatch);

            Tabuleiro t = new Tabuleiro(qntJogadores);
            Helpers.Aguarde();

            log.LogFlush(ref logBatch);

            bool continuarJogo = true;            

            while (continuarJogo)
            {
                for (int jogador = 0; jogador < qntJogadores; jogador++)
                {
                    string corJogador = Helpers.seqPeoes[jogador];
                    Console.Clear();                    
                    Helpers.LogText($"Vez do Jogador {corJogador}: \n", ref logBatch);
                    t.ImprimeTabuleiro();
                                        
                    Console.WriteLine("Pressione uma tecla para rolar o dado.");
                    Console.ReadLine();

                    int retornoJogada = fazerJogada(t, jogador, ref logBatch);

                    if (retornoJogada == 0)
                    {                        
                        Helpers.LogText($"O Jogador {corJogador} passou a jogada.\n", ref logBatch);
                        Helpers.Aguarde(3);
                    }
                    else if (retornoJogada == 2)
                    {                        
                        t.ImprimeTabuleiro();
                        Helpers.Aguarde();
                    }
                    else if (retornoJogada == 1 || retornoJogada == 3 || retornoJogada == 4)
                    {
                        Console.WriteLine("Pressione uma tecla para rolar o dado novamente.");
                        Console.ReadLine();

                        int novoValorDado = Helpers.JogarDado();

                        retornoJogada = fazerJogada(t, jogador, ref logBatch, novoValorDado);
                        Helpers.Aguarde(3);

                        if (novoValorDado == 6 || retornoJogada == 3 || retornoJogada == 4)
                        {
                            Helpers.LogText("Pressione uma tecla para rolar o dado pela terceira e última vez.", ref logBatch);
                            Console.ReadLine();

                            retornoJogada = fazerJogada(t, jogador, ref logBatch);
                            Helpers.Aguarde(3);

                            if (retornoJogada == 0)
                            {
                                Helpers.LogText($"O Jogador {corJogador} passou a terceira jogada.\n", ref logBatch);
                                Helpers.Aguarde(3);
                            }
                            else
                            {
                                t.ImprimeTabuleiro();
                                Helpers.Aguarde();
                            }
                        }
                        else
                        {
                            if (retornoJogada == 0)
                            {
                                Helpers.LogText($"O Jogador {corJogador} passou a segunda jogada.\n", ref logBatch);
                                Helpers.Aguarde(3);
                            }
                            else
                            {
                                t.ImprimeTabuleiro();
                                Helpers.Aguarde();
                            }
                        }
                    }

                    if (t.JogadorGanhouOJogo(corJogador))
                    {
                        continuarJogo = false;
                        Helpers.LogText($"\nO Jogador {corJogador} venceu o jogo!!!\n", ref logBatch);
                        t.ImprimeTabuleiro();
                        break;
                    }

                    log.LogFlush(ref logBatch);

                }
            }
            
        }


        // 0 - Passar Jogada
        // 1 - Tirar Peão da Base
        // 2 - Andar normalmente no tabuleiro
        // 3 - Finalizar Peão
        // 4 - Ocorreu Captura
        public static int fazerJogada(Tabuleiro t, int jogador, ref string logBatch, int valorDoDado = -1)
        {   

            int retorno = 0;
            bool ocorreuCaptura = false;

            int valorDado = valorDoDado == -1 ? Helpers.JogarDado() : valorDoDado;            
            string corJogador = Helpers.seqPeoes[jogador];
            
            Helpers.LogText($"O Jogador {corJogador} tirou {valorDado} no dado.", ref logBatch);

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
                    Helpers.LogText($"- Uma possível jogada é retirar o peao {peoes} da base.", ref logBatch);
                }
                else
                {
                    Helpers.LogText($"- Uma possível jogada é retirar os peoes {peoes} da base.", ref logBatch);
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
                    Helpers.LogText($"- Uma possível jogada é andar com o peao {peoes} no tabuleiro.", ref logBatch);
                }
                else
                {
                    Helpers.LogText($"- Uma possível jogada é andar com os peoes {peoes} no tabuleiro.", ref logBatch);
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
                    Helpers.LogText($"- Uma possível jogada é terminar o peao {peoes} no tabuleiro.", ref logBatch);
                }
                else
                {
                    Helpers.LogText($"- Uma possível jogada é terminar os peoes {peoes} no tabuleiro.", ref logBatch);
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

                    peaoSelecionado = (id == null) ? null : peoesDisponiveisParaJogar.FirstOrDefault(x => x.Id == id.ToUpper());
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
                        peaoSelecionado.AndarComPeao(valorDado, t.Peoes, ref ocorreuCaptura);

                        if (ocorreuCaptura)
                            retorno = 4;
                        else
                            retorno = 2;
                    }
                    else if (peaoSelecionado.Status == 3)
                    {
                        peaoSelecionado.AndarComPeao(valorDado, t.Peoes, ref ocorreuCaptura);

                        if (ocorreuCaptura)
                            retorno = 4;
                        else
                            retorno = 3;
                    }
                }
            }
            
            return retorno;
        }
    }
}