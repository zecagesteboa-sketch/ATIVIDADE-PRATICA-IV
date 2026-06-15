using System;
using Estacionamento_.Models;
using Estacionamento_.Enums;
using Estacionamento_.Repositories;

namespace Estacionamento_
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketRepository ticketRepository = new TicketRepository();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=============================================");
                Console.WriteLine("    SISTEMA DE ESTACIONAMENTO ALFA UNIPAC    ");
                Console.WriteLine("=============================================");
                Console.WriteLine(" 1 - Registrar Entrada de Veículo");
                Console.WriteLine(" 2 - Registrar Saída e Cobrança");
                Console.WriteLine(" 3 - Sair do Sistema");
                Console.WriteLine("=============================================");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("--- REGISTRO DE ENTRADA ---");

                        Console.Write("Digite a Placa do Veículo: ");
                        string placa = Console.ReadLine();

                        Console.Write("Digite o Modelo: ");
                        string modelo = Console.ReadLine();

                        Console.Write("Digite a Cor: ");
                        string cor = Console.ReadLine();

                        Console.WriteLine("\nSelecione o Tipo do Veículo:");
                        Console.WriteLine(" 1 - Carro (R$ 10,00/h)");
                        Console.WriteLine(" 2 - Moto (R$ 5,00/h)");
                        Console.WriteLine(" 3 - Caminhão (R$ 18,00/h + Taxa de Carga)");
                        Console.Write("Opção: ");

                        int tipoEscolhido = int.Parse(Console.ReadLine());
                        Veiculo veiculo;

                        if (tipoEscolhido == 2)
                        {
                            veiculo = new Moto();
                        }
                        else if (tipoEscolhido == 3)
                        {
                            veiculo = new Caminhao();
                        }
                        else if (tipoEscolhido == 1)
                        {
                            veiculo = new Carro();
                        }
                        else
                        {
                            throw new Exception("Opção de tipo de veículo inválida!");
                        }

                        veiculo.Placa = placa;
                        veiculo.Modelo = modelo;
                        veiculo.Cor = cor;

                        Console.Write("Digite a Vaga de Estacionamento (Ex: A1, B3): ");
                        string vaga = Console.ReadLine();

                        Ticket novoTicket = new Ticket
                        {
                            VeiculoEstacionamento = veiculo,
                            HorarioEntrada = DateTime.Now,
                            Vaga = vaga,
                            Status = "Aberto"
                        };

                        ticketRepository.RegistrarEntrada(novoTicket);

                        Console.WriteLine("\n==========================================");
                        Console.WriteLine(" ¡SUCESSO! VEÍCULO REGISTRADO NO BANCO!");
                        Console.WriteLine($" Horário: {novoTicket.HorarioEntrada}");
                        Console.WriteLine("==========================================");
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("\n[ERRO]: No campo Tipo, deve digitar apenas números (1, 2 ou 3).");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[ERRO INESPERADO]: {ex.Message}");
                    }

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                    Console.ReadKey();
                }
                else if (opcao == "2")
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("--- REGISTRO DE SAÍDA E PAGAMENTO ---");
                        Console.Write("Digite a Placa do Veículo que está saindo: ");
                        string placa = Console.ReadLine();

                        Ticket ticketAberto = ticketRepository.BuscarTicketAberto(placa);

                        if (ticketAberto == null)
                        {
                            Console.WriteLine("\n[AVISO]: Nenhum veículo com status 'Aberto' foi encontrado com essa placa.");
                        }
                        else
                        {
                            DateTime horarioSaida = DateTime.Now;
                            TimeSpan tempoPermanencia = horarioSaida - ticketAberto.HorarioEntrada;
                            double totalMinutos = tempoPermanencia.TotalMinutes;
                            double valorTotal = 0;

                            Console.WriteLine($"\nVeículo: {ticketAberto.VeiculoEstacionamento.Modelo} | Placa: {ticketAberto.VeiculoEstacionamento.Placa}");
                            Console.WriteLine($"Entrada: {ticketAberto.HorarioEntrada}");
                            Console.WriteLine($"Saída:   {horarioSaida}");
                            Console.WriteLine($"Tempo de permanência: {(int)tempoPermanencia.TotalHours} horas e {tempoPermanencia.Minutes} minutos");

                            if (totalMinutos <= 15)
                            {
                                Console.WriteLine("\nTempo inferior a 15 minutos. ISENTO de cobrança (Tolerância).");
                            }
                            else
                            {
                                int horasCobradas = (int)tempoPermanencia.TotalHours;
                                int minutosRestantes = tempoPermanencia.Minutes;

                                if (minutosRestantes > 30)
                                {
                                    horasCobradas++;
                                }
                                if (horasCobradas == 0) horasCobradas = 1;

                                double valorHora = 0;
                                double taxaAdicional = 0;

                                if (ticketAberto.VeiculoEstacionamento is Carro) valorHora = 10.0;
                                else if (ticketAberto.VeiculoEstacionamento is Moto) valorHora = 5.0;
                                else if (ticketAberto.VeiculoEstacionamento is Caminhao)
                                {
                                    valorHora = 18.0;
                                    taxaAdicional = 20.0;
                                }

                                valorTotal = (horasCobradas * valorHora) + taxaAdicional;

                                Console.WriteLine($"\nHoras faturadas: {horasCobradas}h");
                                if (taxaAdicional > 0) Console.WriteLine($"Taxa extra de Carga (Caminhão): R$ {taxaAdicional:F2}");
                                Console.WriteLine($"\n>>> VALOR TOTAL A PAGAR: R$ {valorTotal:F2} <<<");

                                Console.WriteLine("\nSelecione a Forma de Pagamento:");
                                Console.WriteLine(" 1 - Dinheiro");
                                Console.WriteLine(" 2 - Cartão");
                                Console.Write("Opção: ");
                                string formaPgto = Console.ReadLine();

                                Console.WriteLine("\nProcessando pagamento...");
                            }

                            ticketRepository.RegistrarSaida(placa, horarioSaida);

                            Console.WriteLine("==========================================");
                            Console.WriteLine(" ¡SUCESSO! SAÍDA E PAGAMENTO CONCLUÍDOS!");
                            Console.WriteLine("==========================================");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n[ERRO NA SAÍDA]: {ex.Message}");
                    }

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                    Console.ReadKey();
                }
                else if (opcao == "3")
                {
                    Console.WriteLine("\nAplicativo encerrado. Até logo!");
                    break;
                }
                else
                {
                    Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                }
            }
        }
    }
}