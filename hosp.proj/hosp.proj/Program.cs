using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hosp.proj.projetohosp
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
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1 - Cadastrar novo paciente");
                Console.WriteLine("2 - Listar pacientes na fila");
                Console.WriteLine("3 - Atender próximo paciente");
                Console.WriteLine("4 - Alterar dados de um paciente");
                Console.WriteLine("0 - Sair do programa");
                Console.Write("\nDigite a opção desejada: ");

                opcao = Console.ReadLine().Trim().ToLower();

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
                        Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
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
                Console.ReadKey();
                return;
            }

            Paciente novoPaciente = new Paciente();

            Console.Write("Digite o nome do paciente: ");
            novoPaciente.Nome = Console.ReadLine().Trim();

            Console.Write("O paciente é preferencial? (s/n): ");
            char pref = Console.ReadLine().Trim().ToLower()[0];
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
            Console.ReadKey();
        }

        static void ListarPacientes()
        {
            Console.Clear();
            Console.WriteLine("--- Pacientes na Fila ---");

            if (numeroDePacientes == 0)
            {
                Console.WriteLine("A fila está vazia.");
            }
            else
            {
                for (int i = 0; i < numeroDePacientes; i++)
                {
                    string status = fila[i].EPreferencial ? "[PREFERENCIAL]" : "[COMUM]";
                    Console.WriteLine($"{i + 1}. {fila[i].Nome} - {status}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static void AtenderPaciente()
        {
            Console.Clear();
            Console.WriteLine("--- Atendimento de Paciente ---");

            if (numeroDePacientes == 0)
            {
                Console.WriteLine("Não há pacientes para atender.");
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

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        static void AlterarPaciente()
        {
            Console.Clear();
            Console.WriteLine("--- Alterar Dados Cadastrais ---");

            if (numeroDePacientes == 0)
            {
                Console.WriteLine("A fila está vazia. Não há dados para alterar.");
                Console.ReadKey();
                return;
            }

            ListarPacientes();
            Console.Write("\nDigite o número do paciente que deseja alterar: ");

            if (int.TryParse(Console.ReadLine(), out int numeroEscolhido) &&
                numeroEscolhido > 0 &&
                numeroEscolhido <= numeroDePacientes)
            {
                int indice = numeroEscolhido - 1;
                Paciente pacienteParaAlterar = fila[indice];

                Console.Write($"Digite o novo nome para '{pacienteParaAlterar.Nome}': ");
                string novoNome = Console.ReadLine().Trim();

                Console.Write("O paciente é preferencial? (s/n): ");
                bool novoStatus = Console.ReadLine().Trim().ToLower()[0] == 's';

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
                Console.WriteLine("\nNúmero inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
