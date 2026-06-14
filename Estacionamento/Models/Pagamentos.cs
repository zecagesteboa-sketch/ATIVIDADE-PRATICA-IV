using System;
using Estacionamento_.Enums; 

namespace Estacionamento_.Models
{
    public class Pagamento
    {
        
        public TipoPagamento Tipo { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public double ValorPago { get; set; }

        
        public void RealizarPagamento(Ticket ticket, double valorInformado, TipoPagamento tipoPagamento)
        {
            
            double valorDevido = ticket.CalcularTotal();

            
            if (valorInformado < valorDevido)
                throw new Exception("Valor insuficiente para o pagamento.");

            ValorPago = valorInformado;
            Tipo = tipoPagamento; 
            DataHoraRegistro = DateTime.Now;

            
            ticket.Status = "Fechado";
        }
    }
}