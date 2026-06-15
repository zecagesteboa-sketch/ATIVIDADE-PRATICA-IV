using System;
using System.Data.SqlClient;
using Estacionamento_.Data;
using Estacionamento_.Models;

namespace Estacionamento_.Repositories
{
    public class TicketRepository
    {
        private readonly Conexao _conexao = new Conexao();

        // MÉTODO 1: Registrar Entrada
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

        // MÉTODO 2: Busca o ticket que está "Aberto" pela Placa
        public Ticket BuscarTicketAberto(string placa)
        {
            using (SqlConnection con = _conexao.ObterConexao())
            {
                string query = "SELECT * FROM Tickets WHERE Placa = @placa AND StatusTicket = 'Aberto'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@placa", placa);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Ticket ticket = new Ticket();
                        ticket.HorarioEntrada = Convert.ToDateTime(reader["HorarioEntrada"]);
                        ticket.Vaga = reader["Vaga"].ToString();
                        ticket.Status = reader["StatusTicket"].ToString();

                        int tipoVeiculo = Convert.ToInt32(reader["TipoVeiculo"]);
                        Veiculo veiculo;

                        // Recria o objeto veículo conforme o banco
                        if (tipoVeiculo == 1) veiculo = new Carro();
                        else if (tipoVeiculo == 2) veiculo = new Moto();
                        else veiculo = new Caminhao();

                        veiculo.Placa = reader["Placa"].ToString();
                        veiculo.Modelo = reader["Modelo"].ToString();
                        veiculo.Cor = reader["Cor"].ToString();

                        ticket.VeiculoEstacionamento = veiculo;
                        return ticket;
                    }
                }
            }
            return null; // Retorna nulo se não achar o veículo
        }

        // MÉTODO 3: Atualiza o banco mudando para "Fechado" e gravando a hora de saída
        public void RegistrarSaida(string placa, DateTime horarioSaida)
        {
            using (SqlConnection con = _conexao.ObterConexao())
            {
                string query = "UPDATE Tickets SET HorarioSaida = @horarioSaida, StatusTicket = 'Fechado' WHERE Placa = @placa AND StatusTicket = 'Aberto'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@placa", placa);
                cmd.Parameters.AddWithValue("@horarioSaida", horarioSaida);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}