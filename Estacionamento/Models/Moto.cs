using System;
using Estacionamento_.Enums; 

namespace Estacionamento_.Models
{
    
    public class Moto : Veiculo
    {
        
        public Moto()
        {
            Tipo = TipoVeiculo.Moto;
        }

       
        public override double CalcularValorEstacionamento(int horasCobradas)
        {
            
            return horasCobradas * 5.00;
        }
    }
}