using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hosp.proj
{
    
namespace projetóhosp
    {


        public struct Paciente
        {
            public string Nome;
            public bool EPreferencial;
        }

        class Program
        {


            static Paciente[] fila = new Paciente[15];
            static int numeroDePacientes = 0;
            static void Main(string[] args)
            {
                string opcao;

                do
                {
                    Console.Clear();
                    Console.WriteLine("    Sistema de Controle de Fila Hospitalar   ");
                    Console.WriteLine("1 - Cadastrar novo paciente");
                    Console.WriteLine("2 - Listar pacientes na fila");
                    Console.WriteLine("3 - Atender proximo paciente");
                    Console.WriteLine("4 - Alterar dados de um paciente");

                    Console.WriteLine("0 - Sair do programa");
                    Console.Write("Digite a opcao desejada: ");

                    opcao = Console.ReadLine().ToLower();

                    switch (opcao)
                    {
                        case "1":
                            CadastrarPaciente();
                            break;
                        case "2":
                            ListarPacientes();
                            break;
                        case "3":
                            AtenderPaciente();
                            break;
                        case "4":
                            AlterarPaciente();
                            break;
                        case "0":
                            Console.WriteLine("\nSaindo do sistema. Obrigado!");
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida!");

                            break;
                    }

                } while (opcao != "0");
            }

            static void CadastrarPaciente()
            {
                Console.Clear();
                Console.WriteLine("--- Cadastro de Novo Paciente ---");

                if (numeroDePacientes >= 15)
                {
                    Console.WriteLine("A fila está cheia! Não é possível cadastrar mais pacientes.");
                    return;
                }

                Paciente novoPaciente = new Paciente();

                Console.Write("Digite o nome do paciente: ");
                novoPaciente.Nome = Console.ReadLine();

                Console.Write("O paciente é preferencial? (s/n): ");
                char pref = Console.ReadLine().ToLower()[0];
                novoPaciente.EPreferencial = (pref == 's');


                if (novoPaciente.EPreferencial)
                {

                    int pontoDeInsercao = 0;

                    while (pontoDeInsercao < numeroDePacientes && fila[pontoDeInsercao].EPreferencial)
                    {
                        pontoDeInsercao++;
                    }

                    for (int i = numeroDePacientes; i > pontoDeInsercao; i--)
                    {
                        fila[i] = fila[i - 1];
                    }

                    fila[pontoDeInsercao] = novoPaciente;
                }
                else
                {

                    fila[numeroDePacientes] = novoPaciente;
                }

                numeroDePacientes++;
                Console.WriteLine("\nPaciente cadastrado com sucesso!");
            }

            static void ListarPacientes()
            {
                Console.Clear();
                Console.WriteLine("--- Pacientes na Fila ---");

                if (numeroDePacientes == 0)
                {
                    Console.WriteLine("A fila esta vazia.");
                }
                else
                {
                    for (int i = 0; i < numeroDePacientes; i++)
                    {
                        string status = fila[i].EPreferencial ? "[PREFERENCIAL]" : "[COMUM]";
                        Console.WriteLine($"{i + 1}. {fila[i].Nome} - {status}");
                    }
                }
            }

            static void AtenderPaciente()
            {
                Console.Clear();
                Console.WriteLine("--- Atendimento de Paciente ---");

                if (numeroDePacientes == 0)
                {
                    Console.WriteLine("Nao ha pacientes para atender.");
                }
                else
                {

                    Console.WriteLine($"Atendendo paciente: {fila[0].Nome}");

                    for (int i = 0; i < numeroDePacientes - 1; i++)
                    {
                        fila[i] = fila[i + 1];
                    }

                    numeroDePacientes--;

                    Console.WriteLine("\nPaciente atendido com sucesso!");
                }


            }

            static void AlterarPaciente()
            {
                Console.Clear();
                Console.WriteLine("--- Alterar Dados Cadastrais ---");

                if (numeroDePacientes == 0)
                {
                    Console.WriteLine("A fila está vazia. Nao ha dados para alterar.");
                    return;
                }


                ListarPacientes();
                Console.WriteLine("-----------------------------------");
                Console.Write("\nDigite o numero do paciente que deseja alterar: ");

                if (int.TryParse(Console.ReadLine(), out int numeroEscolhido) && numeroEscolhido > 0 && numeroEscolhido <= numeroDePacientes) // aqui eu to falando que se esse numero existir na lista e for um numero ele vai passar pra fase de verificação
                {
                    int indice = numeroEscolhido - 1;


                    Paciente pacienteParaAlterar = fila[indice];


                    Console.Write($"Digite o novo nome para '{pacienteParaAlterar.Nome}': ");
                    string novoNome = Console.ReadLine();
                    Console.Write($"O paciente e preferencial? (s/n): ");
                    bool novoStatus = Console.ReadLine().ToLower()[0] == 's';


                    for (int i = indice; i < numeroDePacientes - 1; i++)
                    {
                        fila[i] = fila[i + 1];
                    }
                    numeroDePacientes--;


                    pacienteParaAlterar.Nome = novoNome;
                    pacienteParaAlterar.EPreferencial = novoStatus;

                    if (pacienteParaAlterar.EPreferencial)
                    {
                        int pontoDeInsercao = 0;
                        while (pontoDeInsercao < numeroDePacientes && fila[pontoDeInsercao].EPreferencial)
                        {
                            pontoDeInsercao++;
                        }

                        for (int i = numeroDePacientes; i > pontoDeInsercao; i--)
                        {
                            fila[i] = fila[i - 1];
                        }

                        fila[pontoDeInsercao] = pacienteParaAlterar;
                    }
                    else
                    {
                        fila[numeroDePacientes] = pacienteParaAlterar;
                    }
                    numeroDePacientes++;

                    Console.WriteLine("\nDados alterados e fila reorganizada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nNumero invalido!");
                }

            }
        }
    }
}
