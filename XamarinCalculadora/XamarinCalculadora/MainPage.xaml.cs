using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinCalculadora
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        int estadoAtualCalculadora = 1;
        string operadorMatematico;
        double primeiroNumero, segundoNumero;

        public MainPage()
        {
            InitializeComponent();
            Limpar(new object(), new EventArgs());
        }

        void Limpar(object sender, EventArgs e)
        {
            estadoAtualCalculadora = 1;
            primeiroNumero = 0;
            segundoNumero = 0;
            resultadoText.Text = "0";
        }

        void SelecionarNumero(object sender, EventArgs e) 
        {
            Button btn = (Button)sender;
            string btnNum = btn.Text;

            if(resultadoText.Text == "0" || estadoAtualCalculadora < 0)
            {
                resultadoText.Text = "";

                if (estadoAtualCalculadora < 0)
                    estadoAtualCalculadora *= -1;
            }

            resultadoText.Text += btnNum;
            double numero;

            if(double.TryParse(resultadoText.Text, out numero))
            {
                resultadoText.Text = numero.ToString();

                if(estadoAtualCalculadora == 1)
                {
                    primeiroNumero = numero;
                }
                else
                {
                    segundoNumero = numero;
                    resultadoText.Text = primeiroNumero + " " + operadorMatematico + " " + segundoNumero;
                }
            }
        }

        void SelecionarOperador(object sender, EventArgs e)
        {
            estadoAtualCalculadora = -2;

            Button btn = (Button)sender;
            string btnOperador = btn.Text;
            operadorMatematico = btnOperador;
            resultadoText.Text = primeiroNumero + " " + operadorMatematico;
        }

        void CalcularOperacao(object sender, EventArgs e)
        {
            if(estadoAtualCalculadora == 2)
            {
                double resultado = 0;

                switch (operadorMatematico)
                {
                    case "+":
                        resultado = primeiroNumero + segundoNumero;
                        resultadoText.Text = resultado.ToString();
                        primeiroNumero = resultado;
                        estadoAtualCalculadora = -1;
                        break;

                    case "-":
                        resultado = primeiroNumero - segundoNumero;
                        resultadoText.Text = resultado.ToString();
                        primeiroNumero = resultado;
                        estadoAtualCalculadora = -1;
                        break;

                    case "x":
                        resultado = primeiroNumero * segundoNumero;
                        resultadoText.Text = resultado.ToString();
                        primeiroNumero = resultado;
                        estadoAtualCalculadora = -1;
                        break;

                    case "÷":
                        if (segundoNumero == 0)
                        {
                            DisplayAlert("Atenção", "Não é possível dividir por 0", "OK");
                            estadoAtualCalculadora = 1;
                            primeiroNumero = 0;
                            segundoNumero = 0;
                            resultadoText.Text = "0";

                        }
                        else
                        {
                            resultado = primeiroNumero / segundoNumero;
                            resultadoText.Text = resultado.ToString();
                            primeiroNumero = resultado;
                            estadoAtualCalculadora = -1;
                        }
                        break;
                }
            }
        }
    }
}
