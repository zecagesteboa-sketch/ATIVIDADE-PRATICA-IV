using System;
using Estacionamento_.Enums; 

namespace Estacionamento_.Models
{
    public class Caminhao : Veiculo
    {
        
        public double TaxaCarga { get; set; } = 20.00;

        
        public Caminhao()
        {
            Tipo = TipoVeiculo.Caminhao; 
        }

       
        public override double CalcularValorEstacionamento(int horasCobradas)
        {
           
            return (horasCobradas * 18.00) + TaxaCarga;
        }
    }
}