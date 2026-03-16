using Microsoft.Maui.Controls.Shapes;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace AppJogoDaVelha
{
    public partial class MainPage : ContentPage
    {
        string[,] matrizJogo = new string[3, 3];
        string vez = "X";

        public MainPage()
        {
            InitializeComponent();
            inicializar_matriz();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button clicado = (Button)sender;

            int coluna = Grid.GetColumn(clicado);
            int linha = Grid.GetRow(clicado) - 1;

            if (matrizJogo[linha, coluna] != "")
            {
                return;
            }

            matrizJogo[linha, coluna] = vez;
            clicado.Text = vez;

            processar_jogo(matrizJogo, vez);

            vez = (vez == "X") ? "O" : "X";
        }

        private void processar_jogo(string[,] matriz, string simbolo)
        {
            if (verificar_vertical(matriz, simbolo) || verificar_horizontal(matriz,simbolo) || verificar_diagonal(matriz, simbolo))
            {
                DisplayAlertAsync("fim de jogo", $"{simbolo} ganhou!!!!!!!!!!!!!!", "Ok");
                limpar_tela();
                return;
            }

            if (velha(matriz))
            {
                DisplayAlertAsync("fim de jogo", "deu velha", "Ok");
                limpar_tela();
                return;
            }
        }

        private void inicializar_matriz()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrizJogo[i, j] = "";
                }
            }
        }

        private bool verificar_diagonal(string[,] matriz, string simbolo)
        {
            if (matriz[0,0] == simbolo && matriz[1,1] == simbolo && matriz[2,2] == simbolo)
            {
                return true;
            }

            if (matriz[0,2] == simbolo && matriz[1,1] == simbolo && matriz[2,0] == simbolo)
            {
                return true;
            }

            return false;
        }

        private bool verificar_horizontal(string[,] matriz, string simbolo)
        {
            for (int linha = 0; linha < 3; linha++)
            {
                if (matriz[linha,0] == simbolo && matriz[linha,1] == simbolo && matriz[linha,2] == simbolo)
                {
                    return true;
                }
            }

            return false;
        }

        private bool verificar_vertical(string[,] matriz, string simbolo)
        {
            for (int coluna = 0; coluna < 3; coluna++)
            {
                if (matriz[0,coluna] == simbolo && matriz[1,coluna] == simbolo && matriz[2,coluna] == simbolo)
                {
                    return true;
                }
            }

            return false;
        }

        private void limpar_tela()
        {
            inicializar_matriz();

            vez = "X";

            foreach (var x in jogo.Children)
            {
                if (x is Button btn)
                {
                    btn.Text = "";
                }
            }
        }

        private bool velha(string[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if(matriz[i, j] == "")
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}
