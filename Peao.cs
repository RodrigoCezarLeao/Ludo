using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ludo
{
    public class Peao
    {
        public string Id;
        public Posicao Posicao { get; set; }
        public string Cor { get; set; }

        public int Status { get; set; }
        // 0 = BASE
        // 1 = EM JOGO (APÓS VOLTA DE ÍNDICE)
        // 4 = EM JOGO (ANTES DA VOLTA DE ÍNDICE)
        // 2 = CHEGADA
        // 3 = EM JOGO (CASAS ESPECIAIS)

        public Peao(string cor, int num) 
        { 
            this.Cor = cor;
            this.Status = 0;
            this.Id = num.ToString("X");
        }
        public Peao(string cor, int num, Posicao p)
        {
            this.Cor = cor;
            this.Status = 0;
            this.Id = num.ToString("X");
            this.Posicao = p;
        }
        public Peao() {}


        public int PosicaoSequencialInicialPorCor(string cor)
        {
            if (cor == "G")
            {
                return 1;
            }
            if (cor == "Y")
            {
                return 14;
            }
            if(cor == "B")
            {
                return 27;
            }
            if (cor == "R")
            {
                return 40;
            }

            return -1;
        }

        public void AndarComPeao(int valorDado, Peao[] peoesDoTabuleiro, ref bool ocorreuCaptura)
        {
            int posicaoAtualSequencial = Helpers.CoordenadaParaPosicaoSequencial(this.Posicao);

            if (this.Cor == "B")
            {
                if (this.Status == 4 && posicaoAtualSequencial + valorDado > 51)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 51) - 1;
                    this.Status = 1;
                }
                else if (this.Status == 1 && posicaoAtualSequencial + valorDado > 25)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 25) + (62 - 1);
                    this.Status = 3;
                }
                else if (this.Status == 3 && posicaoAtualSequencial >= 62)
                {
                    if (posicaoAtualSequencial + valorDado == 66 + 1)
                    {
                        posicaoAtualSequencial = 74;
                        this.Status = 2;
                    }
                }
                else if (this.Status == 1 || this.Status == 4)
                {
                    posicaoAtualSequencial += valorDado;
                }
            }
            if (this.Cor == "R")
            {
                if (this.Status == 4 && posicaoAtualSequencial + valorDado > 51)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 51) - 1;
                    this.Status = 1;
                }
                else if (this.Status == 1 && posicaoAtualSequencial + valorDado > 38)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 38) + (67 - 1);
                    this.Status = 3;
                }
                else if (this.Status == 3 && posicaoAtualSequencial >= 67)
                {
                    if (posicaoAtualSequencial + valorDado == 71 + 1)
                    {
                        posicaoAtualSequencial = 75;
                        this.Status = 2;
                    }
                }
                else if (this.Status == 1 || this.Status == 4)
                {
                    posicaoAtualSequencial += valorDado;
                }
            }
            if (this.Cor == "Y")
            {
                if (this.Status == 4 && posicaoAtualSequencial + valorDado > 51)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 51) - 1;
                    this.Status = 1;
                }
                else if (this.Status == 1 && posicaoAtualSequencial + valorDado > 12)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 12) + (57 - 1);
                    this.Status = 3;
                }
                else if (this.Status == 3 && posicaoAtualSequencial >= 57)
                {
                    if (posicaoAtualSequencial + valorDado == 61 + 1)
                    {
                        posicaoAtualSequencial = 73;
                        this.Status = 2;
                    }
                }
                else if (this.Status == 1 || this.Status == 4)
                {
                    posicaoAtualSequencial += valorDado;                    
                }
            }
            if (this.Cor == "G")
            {                
                if (this.Status == 1 && posicaoAtualSequencial + valorDado > 51)
                {
                    posicaoAtualSequencial = ((posicaoAtualSequencial + valorDado) % 51) + (52 - 1);
                    this.Status = 3;
                }
                else if (this.Status == 3 && posicaoAtualSequencial >= 52)
                {
                    if (posicaoAtualSequencial + valorDado == 56 + 1)
                    {
                        posicaoAtualSequencial = 72;
                        this.Status = 2;
                    }
                }
                else if (this.Status == 1 || this.Status == 4)
                {
                    posicaoAtualSequencial += valorDado;
                    this.Status = 1;
                }
            }

            this.Posicao = Helpers.PosicaoSequencialParaCoordenada(posicaoAtualSequencial);

            // Lógica da captura de peão
            Peao[] peoesNaCasaAtual = peoesDoTabuleiro.Where(x => Helpers.CoordenadaParaPosicaoSequencial(x.Posicao) == posicaoAtualSequencial).ToArray();
            if (peoesNaCasaAtual.Length > 1 && !Helpers.casasSegurasGerais.Contains(posicaoAtualSequencial))
            {
                Log log = new Log();
                string logBatch = "";

                ocorreuCaptura = true;

                foreach(Peao p  in peoesNaCasaAtual)
                {
                    if (p.Id != this.Id && p.Cor != this.Cor)
                    {
                        p.Status = 0;
                        Helpers.LogText($"O Peão ({p.Id}) voltou foi capturado!", ref logBatch);
                        int idx = Convert.ToInt32(p.Id, 16);
                        p.Posicao = new Posicao(Helpers.posBasePeoes[idx, 0], Helpers.posBasePeoes[idx, 1]);
                    }
                }

                log.LogFlush(ref logBatch);
            }
            else
            {
                ocorreuCaptura = false;
            }
        }
        
        public void TirarPeaoDaBase()
        {
            this.Posicao = Helpers.PosicaoSequencialParaCoordenada(PosicaoSequencialInicialPorCor(this.Cor));
            this.Status = 4;
        }

        public bool PeaoPodeTerminar(int valorDado)
        {
            if (this.Status == 3)
            {
                int posicaoSequencial = Helpers.CoordenadaParaPosicaoSequencial(this.Posicao);

                if (this.Cor == "G")
                {
                    if (posicaoSequencial + valorDado == 56 + 1)
                        return true;
                    else
                        return false;
                }
                else if (this.Cor == "Y")
                {
                    if (posicaoSequencial + valorDado == 61 + 1)
                        return true;
                    else
                        return false;
                }
                else if (this.Cor == "B")
                {
                    if (posicaoSequencial + valorDado == 66 + 1)
                        return true;
                    else
                        return false;
                }
                else if (this.Cor == "R")
                {
                    if (posicaoSequencial + valorDado == 71 + 1)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

    }
}
