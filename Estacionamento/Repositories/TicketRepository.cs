using System;
using System.Data.SqlClient; 
using Estacionamento_.Data;   
using Estacionamento_.Models; 

namespace Estacionamento_.Repositories
{
    public class TicketRepository
    {
        
        private readonly Conexao _conexao = new Conexao();

        
        public void RegistrarEntrada(Ticket ticket)
        {
           
            using (SqlConnection con = _conexao.ObterConexao())
            {
                
                string query = @"INSERT INTO Tickets (Placa, Modelo, Cor, TipoVeiculo, HorarioEntrada, Vaga, StatusTicket) 
                                 VALUES (@placa, @modelo, @cor, @tipoVeiculo, @horarioEntrada, @vaga, @statusTicket)";

                
                SqlCommand cmd = new SqlCommand(query, con);

                
                cmd.Parameters.AddWithValue("@placa", ticket.VeiculoEstacionamento.Placa);
                cmd.Parameters.AddWithValue("@modelo", ticket.VeiculoEstacionamento.Modelo);
                cmd.Parameters.AddWithValue("@cor", ticket.VeiculoEstacionamento.Cor);
                cmd.Parameters.AddWithValue("@tipoVeiculo", (int)ticket.VeiculoEstacionamento.Tipo); 
                cmd.Parameters.AddWithValue("@horarioEntrada", ticket.HorarioEntrada);
                cmd.Parameters.AddWithValue("@vaga", ticket.Vaga);
                cmd.Parameters.AddWithValue("@statusTicket", ticket.Status);

                
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}