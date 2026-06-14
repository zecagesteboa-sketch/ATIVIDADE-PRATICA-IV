using System;

namespace Estacionamento_.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Veiculo VeiculoEstacionamento { get; set; }
        public DateTime HorarioEntrada { get; set; }

       
        public DateTime? HorarioSaida { get; set; }
        public string Vaga { get; set; }
        public string Status { get; set; }

        public void RegistrarSaida()
        {
            HorarioSaida = DateTime.Now;
        }

        public double CalcularTotal()
        {
            
            if (HorarioSaida == null)
                throw new Exception("Veículo ainda não saiu.");

            
            TimeSpan permanencia = HorarioSaida.Value - HorarioEntrada;

            if (permanencia.TotalMinutes <= 15)
                return 0.0;

            int horas = (int)permanencia.TotalHours;
            int minutosRestantes = permanencia.Minutes;

            if (minutosRestantes > 30)
            {
                horas++;
            }

            if (horas == 0 && permanencia.TotalMinutes > 15)
            {
                horas = 1;
            }

            
            return VeiculoEstacionamento.CalcularValorEstacionamento(horas);
        }
    }
}