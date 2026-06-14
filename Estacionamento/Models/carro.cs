using System;
using Estacionamento_.Enums; 

namespace Estacionamento_.Models
{
    
    public class Carro : Veiculo
    {
        
        public Carro()
        {
            Tipo = TipoVeiculo.Carro; 
        }

        
        public override double CalcularValorEstacionamento(int horasCobradas)
        {
            
            return horasCobradas * 10.00;
        }
    }
}