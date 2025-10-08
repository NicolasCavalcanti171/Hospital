using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hosp.proj.projetohosp
{
    public struct Paciente
    //(struct)é tipo de dados para agrupar informações de um paciente.

    {
        public string Nome;
        public bool EPreferencial;
    }
    //Define um tipo chamado Paciente.
    //Ele tem dois campos públicos:Nome: guarda o nome do paciente.EPreferencial: indica se o paciente tem prioridade na fila (true ou false).

    class Program
    {
        static Paciente[] fila = new Paciente[15];
        static int numeroDePacientes = 0;
        //fila: cria um array estático com capacidade para até 15 pacientes.
        //numeroDePacientes: controla quantos pacientes estão na fila atualmente.

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
                // remove espaços em branco extras, converte tudo para minúsculas e armazena o resultado na variável opcao.

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
                        //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
                        break;
                }

            } while (opcao != "0");
            //Executa um loop até que o usuário escolha "0".
            //A cada iteração, chama uma função conforme a opção escolhida.
        }

        static void CadastrarPaciente()
        // método para cadastrar um paciente, sem retorno e sem precisar de um objeto da classe.
        {
            Console.Clear();
            //método que apaga todo o texto ou as mensagens exibidas na tela de um console ou terminal. 
            Console.WriteLine("--- Cadastro de Novo Paciente ---");

            if (numeroDePacientes >= 15)
            {
                Console.WriteLine("A fila está cheia! Não é possível cadastrar mais pacientes.");
                Console.ReadKey();
                //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
                return;
            }

            Paciente novoPaciente = new Paciente();

            Console.Write("Digite o nome do paciente: ");
            novoPaciente.Nome = Console.ReadLine().Trim();
            //remove os espaços extras nas bordas e atribui esse valor à propriedade Nome do objeto novoPaciente. 

            Console.Write("O paciente é preferencial? (s/n): ");
            char pref = Console.ReadLine().Trim().ToLower()[0];
            // remove espaços em branco, converte para minúsculas e pega o primeiro caractere, guardando-o na variável pref. 

            novoPaciente.EPreferencial = (pref == 's');
            //Verifica se a fila está cheia; se sim, sai da função.
            //Cria um novo Paciente.
            //Recebe o nome e lê se é preferencial.
            //Armazena essas informações em novoPaciente.

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
            //Se o paciente for preferencial:
            //Encontra o primeiro índice onde há paciente não preferencial (ou o final da fila).
            //Move todos os pacientes após esse ponto para a direita, abrindo espaço.
            //Insere o novo paciente nesse ponto.
            else
            {
                fila[numeroDePacientes] = novoPaciente;
            }
            //Se não for preferencial, adiciona o paciente no final da fila.

            numeroDePacientes++;
            //Atualiza o número de pacientes na fila.
            Console.WriteLine("\nPaciente cadastrado com sucesso!");
            Console.ReadKey();
            //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
        }

        static void ListarPacientes()
        // método para cadastrar um paciente, sem retorno e sem precisar de um objeto da classe.
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
                    // exibe no console, um número sequencial, o nome de um objeto da lista fila e um status.
                }
                //Percorre a fila até o número de pacientes atual.
                //Para cada paciente, define uma string de status conforme preferência.
                //Exibe as informações do paciente.
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
        }

        static void AtenderPaciente()
        // método para cadastrar um paciente, sem retorno e sem precisar de um objeto da classe.
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
                //Se não houver pacientes, sai.
                //Caso contrário, remove o paciente na frente da fila (fila[0]).
                //Move todos os pacientes seguintes para a esquerda (deixa o espaço no final).
                //Decrementa o número de pacientes.

                numeroDePacientes--;
                Console.WriteLine("\nPaciente atendido com sucesso!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
        }

        static void AlterarPaciente()
        // método para cadastrar um paciente, sem retorno e sem precisar de um objeto da classe.
        {
            Console.Clear();
            Console.WriteLine("--- Alterar Dados Cadastrais ---");

            if (numeroDePacientes == 0)
            {
                Console.WriteLine("A fila está vazia. Não há dados para alterar.");
                Console.ReadKey();
                //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
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
                //remove os espaços extras nas bordas e guarda o resultado na variável novoNome.

                Console.Write("O paciente é preferencial? (s/n): ");
                bool novoStatus = Console.ReadLine().Trim().ToLower()[0] == 's';
                // define novoStatus como true se o usuário digitar algo que comece com 's', e false caso contrário.

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
                    //move o ponto de inserção para a frente da fila, passando por todos os pacientes preferenciais. 

                    for (int i = numeroDePacientes; i > pontoDeInsercao; i--)
                    {
                        fila[i] = fila[i - 1];
                    }

                    fila[pontoDeInsercao] = pacienteParaAlterar;
                }
                // move todos os pacientes da fila para abrir espaço para um novo paciente.
                //que é então inserido na posição correta determinada anteriormente.
                else
                {
                    fila[numeroDePacientes] = pacienteParaAlterar;
                }
                //insere um paciente no final da fila, caso não haja uma condição específica que exija uma inserção em outra posição. 

                numeroDePacientes++;
                //Verifica se a fila está vazia; se sim, sai.
                //Lista pacientes para o usuário escolher qual alterar.
                //Recebe o número do paciente a alterar e verifica validade.
                //Remove o paciente da fila (faz shift para esquerda).
                //Atualiza os dados do paciente.
                //Insere novamente o paciente, considerando prioridade.
                //Atualiza o contador.

                Console.WriteLine("\nDados alterados e fila reorganizada com sucesso!");
            }
            else
            {
                Console.WriteLine("\nNúmero inválido!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            //método que pausa a execução do programa e espera que o usuário pressione uma única tecla para continuar.
        }
    }
}
