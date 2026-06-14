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
                Console.WriteLine(" 2 - Sair do Sistema");
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