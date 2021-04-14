using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
        static void Main(string[] args)
        {

            bool loginSuceed = fazerLogin();
            if (loginSuceed)
            {
	            string opcaoUsuario = ObterOpcaoUsuario();
				CarregarListContas();
                while (opcaoUsuario.ToUpper() != "X")
                {
                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarContas();
                            break;
                        case "2":
                            InserirConta();
                            break;
                        case "3":
                            Transferir();
                            break;
                        case "4":
                            Sacar();
                            break;
                        case "5":
                            Depositar();
                            break;
                        case "6":
                            MultiplasTransferencias();
                            break;
                        case "C":
                            Console.Clear();
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    opcaoUsuario = ObterOpcaoUsuario();
                }
            }
				Console.WriteLine("Obrigado por utilizar nossos serviços.");
				Console.ReadLine();

        }

        private static void MultiplasTransferencias()
        {
            List<Conta> ContasDestino = new List<Conta>();

            Console.Write("Digite a conta de origem:");
            int ContaOrigemNumber = int.Parse(Console.ReadLine());
            Conta ContaOrigem = listContas[ContaOrigemNumber];
            Console.WriteLine("Digite o valor da transferência:");
            double ValorTranferencia = double.Parse(Console.ReadLine());

            Console.Write("Digite a próxima conta de destino ou X para prosseguir");
            string ProximaContaDestino = Console.ReadLine();

            while (ProximaContaDestino.ToUpper() != "X")
            {
                ContasDestino.Add(listContas[int.Parse(ProximaContaDestino)]);
                Console.Write("Digite a próxima conta de destino ou X para prosseguir: ");
                ProximaContaDestino = Console.ReadLine();
            }

            foreach (Conta c in ContasDestino)
            {
                ContaOrigem.Transferir(valorTransferencia: ValorTranferencia, contaDestino: c);
            }

        }

        private static void Depositar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());

            listContas[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            Console.Write("Digite o número da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
            int indiceContaDestino = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser transferido: ");
            double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
        }

        private static void InserirConta()
        {
            Console.WriteLine("Inserir nova conta");

            Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
            int entradaTipoConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o Nome do Cliente: ");
            string entradaNome = Console.ReadLine();

            Console.Write("Digite o saldo inicial: ");
            double entradaSaldo = double.Parse(Console.ReadLine());

            Console.Write("Digite o crédito: ");
            double entradaCredito = double.Parse(Console.ReadLine());

            Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
                                        saldo: entradaSaldo,
                                        credito: entradaCredito,
                                        nome: entradaNome);

            listContas.Add(novaConta);
        }

        private static void ListarContas()
        {
            Console.WriteLine("Listar contas");

            if (listContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for (int i = 0; i < listContas.Count; i++)
            {
                Conta conta = listContas[i];
                Console.Write("#{0} - ", i);
                Console.WriteLine(conta);
            }
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar contas");
            Console.WriteLine("2- Inserir nova conta");
            Console.WriteLine("3- Transferir");
            Console.WriteLine("4- Sacar");
            Console.WriteLine("5- Depositar");
            Console.WriteLine("6- Para Múltiplas Transferências");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static void CarregarListContas()
        {
            Conta c = new Conta(tipoConta: (TipoConta)1, saldo: 500.00, credito: 100, nome: "Filipe");
            listContas.Add(c);
            c = new Conta(tipoConta: (TipoConta)1, saldo: 1500.00, credito: 500, nome: "Ana");
            listContas.Add(c);
            c = new Conta(tipoConta: (TipoConta)2, saldo: 1500.00, credito: 500, nome: "Henrique");
            listContas.Add(c);


        }

        private static bool fazerLogin()
        {
            string senhaDeAcesso = "12345";
            for (int i = 1; i < 4; i++)
            {
                Console.Write("Digite a senha de Acesso tentativa {0}/3: ", i);
                string senha = Console.ReadLine();
                if (senha == senhaDeAcesso)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
