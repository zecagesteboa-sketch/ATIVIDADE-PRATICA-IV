using System;
using Estacionamento_.Enums; 

namespace Estacionamento_.Models
{
    public abstract class Veiculo
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }

        
        public TipoVeiculo Tipo { get; set; }

        public abstract double CalcularValorEstacionamento(int horasCobradas);
    }
}