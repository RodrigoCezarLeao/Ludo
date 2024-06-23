using System;
using System.Collections.Generic;
using System.Linq;
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
        // 1 = EM JOGO
        // 2 = CHEGADA
        // 3 = EM JOGO (CASAS ESPECIAIS)
        // 4 = 'PRIMEIRA VOLTA'

        public Peao(string cor, int num) 
        { 
            this.Cor = cor;
            this.Status = 0;
            this.Id = $"P{num}";
        }
        public Peao() { }


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

        public void AndarComPeao(int valorDado)
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
        }
        
        // 0 - Passou a vez
        // 1 - Sair da base
        // 2 - Andar com o peão no tabuleiro
        public int JogarDado()
        {
            // Peão já finalizado
            if (this.Status == 2)
            {
                return 0;
            }

            Random random = new Random();
            int valorDado = random.Next(1,7);

            Console.WriteLine($"Peão {this.Cor} ({this.Id}) tirou {valorDado} no dado.");

            // Saída da base
            if (this.Status == 0 && valorDado == 6)
            {
                this.Posicao = Helpers.PosicaoSequencialParaCoordenada(PosicaoSequencialInicialPorCor(this.Cor));
                this.AndarComPeao(valorDado);                
                this.Status = 4;
                return 1;
            }
            if(this.Status == 0 && valorDado != 6)
            {
                return 0;
            }


            // Andar com o peão no tabuleiro
            this.AndarComPeao(valorDado);

            return 2;
        }
    }
}
