using System;
using System.Collections.Generic;

namespace DIO.Bank
{
	class Program
	{
		static List<Conta> listContas = new List<Conta>();  
		static void Main(string[] args)
		{
			
            string opcaoUsuario = ObterOpcaoUsuario();  

			while (opcaoUsuario.ToUpper() != "X")       
            {
                if (!(opcaoUsuario.ToUpper() != "1") || 
                    !(opcaoUsuario.ToUpper() != "2") || 
                    !(opcaoUsuario.ToUpper() != "3") || 
                    !(opcaoUsuario.ToUpper() != "4") || 
                    !(opcaoUsuario.ToUpper() != "5") || 
                    !(opcaoUsuario.ToUpper() != "C"))
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
                            case "C":
                                Console.Clear();
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        
                        }
                    opcaoUsuario = ObterOpcaoUsuario();   // Chamada do método sem pedir nova entrada de dados, senão fica necessário dois <enter> para reconhecer opção digitada                          
                    
                    }else{
                        Console.Write("Selecione uma opção válida do menu:");
                        opcaoUsuario = ObterOpcaoUsuario();	                        
                    }
                        
		    }

        }

        // Forma apresentada para a listagem de contas durante o curso
        /**
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
        */

        
        private static void ListarContas()
        {
            Console.WriteLine("Listar Contas");

            if (listContas.Count == 0)
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            for (int i = 0; i < listContas.Count; i++)  // outro layout que criei para apresentar as contas
            {
                Conta conta = listContas[i];
                Console.Write("#{0} - ", i);
                conta.Consulta();
            }
        }
       

		
        private static void Depositar()
        {
            if (listContas.Count == 0)                              // tratamento de erro: condição incluída para evitar erro no programa
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }
            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser depositado: ");
            double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
        }

		private static void Sacar()
        {
            if (listContas.Count == 0)                              // tratamento de erro: condição incluída para evitar erro no programa
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }

            Console.Write("Digite o número da conta: ");
            int indiceConta = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor a ser sacado: ");
            double valorSaque = double.Parse(Console.ReadLine());
            
            listContas[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            if (listContas.Count == 0)                              // tratamento de erro: condição incluída para evitar erro no programa
            {
                Console.WriteLine("Nenhuma conta cadastrada.");
                return;
            }
            Console.WriteLine("Digite o código de referência da conta de origem: ");
            int indiceContaOrigem = int.Parse(Console.ReadLine());
            //Primeira verificação
            if (indiceContaOrigem > listContas.Count-1)             // tratamento de erro: condição incluída para evitar erro no programa
            {
                Console.WriteLine("Digite uma conta válida entre 0 e {0}.", listContas.Count-1);    //mostra os possíveis códigos de conta 
                Console.WriteLine("Caso queira retornar ao menu principal digite [M].");
                Console.WriteLine("Caso queira tentar transferir novamente digite [T].");
                string menuPrincipal = Console.ReadLine().ToUpper();
                    // trata as opções oferecidas para evitar erro durante a execução e possível entrada errada pelo usuário
                    switch(menuPrincipal)                           
                    {
                    case "M":   
                        Console.WriteLine("Operação cancelada. Retornando ao Menu Principal.");
                        indiceContaOrigem = 0;
                        menuPrincipal = "";
                        return;
                        
                    case "T":
                        Console.WriteLine("Digite uma conta válida entre 0 e {0}.", listContas.Count-1);
                        int novoIndiceContaOrigem = int.Parse(Console.ReadLine());      //a partir daqui começa o aninhamento para validações
                        if (novoIndiceContaOrigem > (listContas.Count-1)){
                            Console.WriteLine("Esta operação foi cancelada devido a nova tentativa inválida.\nRetornando ao menu principal.");
                            return;                                                     //se usuário persite no erro retorna ao menu principal
                        }else{
                            Conta conta = listContas[novoIndiceContaOrigem];    //inicialmente pedia typecast, depois aceitou sem fazer listContas[(int)novoIndiceContaOrigem]
                            Console.Write("#{0} - ", novoIndiceContaOrigem);    //inicialmente pedia typecast, depois aceitou sem fazer (int)novoIndiceContaOrigem
                            conta.Consulta();
                            Console.Write("Confirma conta de origem? [S/N] ");          //apresenta a conta informada para usuário confirmar a operação
                            string confirmaUsuario = Console.ReadLine().ToUpper();
                            
                            switch(confirmaUsuario){
                                case "N":
                                    Console.WriteLine("Operação cancelada. Favor reiniciar a operação.");
                                    return;                                             // caso não confirme retornará ao menu principal

                                case "S":
                                    indiceContaOrigem = novoIndiceContaOrigem;          //incluídas regras de vaidação para evitar seleção da mesma conta e conta inexistente
                                    Console.WriteLine("Digite uma conta válida entre 0 e {0}, e que seja diferente da Conta de Origem {1}.", listContas.Count-1, indiceContaOrigem);
                                    int indiceContaDestino = int.Parse(Console.ReadLine()); 
                                    //conta destino precisa ser diferente da conta de origem e conta de destino precisa estar no intervalo que corresponde aos índices menos 1 porque inicia em zero
                                    if(indiceContaDestino != indiceContaOrigem && indiceContaDestino < listContas.Count-1)
                                    {
                                        Conta contaDestino = listContas[indiceContaDestino]; //inicialmente pedia typecast, depois aceitou sem fazer listContas[(int)indiceContaDestino]
                                        Console.Write("#{0} - ", indiceContaDestino);   //inicialmente pedia typecast, depois aceitou sem fazer (int)indiceContaDestino
                                        contaDestino.Consulta();
                                        Console.Write("Confirma conta de destino? [S/N] ");
                                        string confirmaUsuarioDest = Console.ReadLine().ToUpper();

                                        switch(confirmaUsuarioDest){
                                            case "N":
                                                Console.WriteLine("Operação cancelada. Favor reiniciar a operação.");
                                                return;

                                            case "S":
                                                Console.Write("Digite o valor a ser transferido: ");
                                                double valorTransferencia = double.Parse(Console.ReadLine());

                                                Console.WriteLine("Escolha entre as opções a seguir: ");        //incluídas modalidades de transferência com enumerador
                                                Console.WriteLine("1 - TED: Operação de transferência realizada no mesmo dia.");
                                                Console.WriteLine("2 - DOC: Operação de transferência agendada para o próximo dia útil.");
                                                Console.WriteLine("3 - PIX: Operação de transferência realizada imediatamente.");
                                                int tipoTransferencia =  int.Parse(Console.ReadLine());
                                                //incluído mais um parâmetro para chamar a classe conta informando o TipoTransferencia
                                                listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino], (TipoTransferencia)tipoTransferencia);
                                                
                                                break;
                                        }
                                    } break;
                            }
                        } break;
                        }
                        
                    }else{                                                  //toda esta parte se repete por atender de cara a primeira condição do índice da conta de origem existente            
                        Conta conta = listContas[(int)indiceContaOrigem];
                        Console.Write("#{0} - ", (int)indiceContaOrigem);
                        conta.Consulta();
                        Console.Write("Confirma conta de origem? [S/N] ");
                        string confirmaUsuario = Console.ReadLine().ToUpper();
                                    
                                    switch(confirmaUsuario){
                                        case "N":
                                            Console.WriteLine("Operação cancelada. Favor reiniciar a operação.");
                                            return;

                                        case "S":                                            
                                            Console.WriteLine("Digite uma conta válida entre 0 e {0}, e que seja diferente da Conta de Origem {1}.", listContas.Count-1, indiceContaOrigem);
                                            int indiceContaDestino = int.Parse(Console.ReadLine());
                                            if(indiceContaDestino != indiceContaOrigem && indiceContaDestino < listContas.Count-1)
                                            {
                                                Conta contaDestino = listContas[(int)indiceContaDestino];
                                                Console.Write("#{0} - ", (int)indiceContaDestino);
                                                contaDestino.Consulta();
                                                Console.Write("Confirma conta de destino? [S/N] ");
                                                string confirmaUsuarioDest = Console.ReadLine().ToUpper();

                                                switch(confirmaUsuarioDest){
                                                    case "N":
                                                        Console.WriteLine("Operação cancelada. Favor reiniciar a operação.");
                                                        return;

                                                    case "S":
                                                        Console.Write("Digite o valor a ser transferido: ");
                                                        double valorTransferencia = double.Parse(Console.ReadLine());

                                                        Console.WriteLine("Escolha entre as opções a seguir: ");
                                                        Console.WriteLine("1 - TED: Operação de transferência realizada no mesmo dia.");
                                                        Console.WriteLine("2 - DOC: Operação de transferência agendada para o próximo dia útil.");
                                                        Console.WriteLine("3 - PIX: Operação de transferência realizada imediatamente.");
                                                        int tipoTransferencia =  int.Parse(Console.ReadLine());

                                                        listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino], (TipoTransferencia)tipoTransferencia);
                                                        
                                                        break;
                                                }
                                            }
                                            Console.WriteLine("Entrada de conta inválida. Operação cancelada.");
                                            break;
                                    }
                                
                                
                        
                    }               

        }
        
	    private static void InserirConta()
        {   
            int entradaTipoConta = 0;
            int entradaAgencia = 0;
            int entradaNumConta = listContas.Count + 1;
            double entradaSaldo = 0;
            double entradaCredito = 0;

            Console.WriteLine("Inserir nova conta");
            
            //incluídos testes de verificação para cada etapa da entrada de dados pelo usuário, tratando possíveis erros
            for (int i = 0; i < 3; i++){
                Console.WriteLine("Digite 1 para Conta Física ou 2 para Jurídica: ");
                string entradaTipoUsuario = Console.ReadLine();
                //usei TryParse para validar se string correspondia a número e não permitir entrada inváida de dados
                bool entradaTipoConta1 =  int.TryParse(entradaTipoUsuario, out entradaTipoConta);   
                
                    if (!(entradaTipoConta != 1) || !(entradaTipoConta != 2))
                    {                        
                        i = 3;
                    }else{
                    Console.WriteLine("Esta não é uma opção válida.");
                    int t = i + 1;
                    Console.WriteLine("Você tem mais {0} tentativas.", 3-t);

                        if (t > 2){
                            Console.WriteLine("Operação cancelada. Favor reiniciar.");
                            return;
                        }

                    }

            }

            /**
            for (int i = 0; i > 3; i++){
                
                Console.Write("Digite o número da conta do cliente: ");
                string entradaNumContaUsuario =  (Console.ReadLine());
                bool resultNumConta = int.TryParse(entradaNumContaUsuario, out entradaNumConta);

                if (entradaNumConta < 1000000 && entradaNumConta > 0){
                    i = 3;
                }else{
                    Console.WriteLine("Esta não é uma opção válida. Digite uma conta entre 1 e 999999.");
                    int t = 0;
                    t = t + 1;
                    Console.WriteLine("Você tem mais {0} tentativas.", 3-t);
                        if (t > 2) {
                            Console.WriteLine("Operação cancelada. Favor reiniciar.");
                            return;
                        }
                }
            }
            */
            
            //utilizadas mesmas formas de tratamento para os demais dados, respeitando obviamente, a característica de cada um

            for (int k = 0; k < 3; k++){
                
                Console.WriteLine("Digite o número da agência: ");
                string entradaAgenciaUsuario =  Console.ReadLine();
                bool resultAgencia = int.TryParse(entradaAgenciaUsuario, out entradaAgencia);
                
                    if (entradaAgencia < 1001 && entradaAgencia > 0)
                    {
                        k = 3;
                    }else{
                        Console.Write("Esta não é uma opção válida. Digite um número maior que 0 e menor que 1000.");
                        int t = 0;
                        t = k + 1;
                        Console.WriteLine("Você tem mais {0} tentativas.", 3-t);

                            if (t > 2){
                                Console.WriteLine("Operação cancelada. Favor reiniciar.");
                                return;
                            }                   
                    }
            }

                                  
            Console.Write("Digite o nome do cliente: ");
            string entradaNome = Console.ReadLine();


            for (int i = 0; i < 3; i++){
                
                Console.WriteLine("Digite o saldo inicial: ");
                string entradaSaldoUsuario =  Console.ReadLine();
                if (!(entradaSaldoUsuario != "0")){
                    entradaSaldo = double.Parse(entradaSaldoUsuario);
                    i = 3;
                }else{
                bool resultSaldo = double.TryParse(entradaSaldoUsuario, out entradaSaldo);
                
                    if (entradaSaldo < 10000000000 && entradaSaldo > 0)
                    {
                        i = 3;
                    }else{
                        Console.Write("Esta não é um valor válido.");
                        int t = 0;
                        t = i + 1;
                        Console.WriteLine("Você tem mais {0} tentativas.", 3-t);

                            if (t > 2){
                                Console.WriteLine("Operação cancelada. Favor reiniciar.");
                                return;
                            }                   
                    }
                }
            }

            for (int i = 0; i < 3; i++){
                
                Console.WriteLine("Digite o crédito/limte inicial do cliente: ");
                string entradaCreditoUsuario =  Console.ReadLine();
                if (!(entradaCreditoUsuario != "0")){
                    entradaCredito = double.Parse(entradaCreditoUsuario);
                    i = 3;
                }else{                
                bool resultCredito = double.TryParse(entradaCreditoUsuario, out entradaCredito);
                
                    if (entradaCredito < 100000 && entradaSaldo > 0)
                    {
                        i = 3;
                    }else{
                        Console.Write("Esta não é um valor válido. Maior valor possível para crédito é 100.000.");
                        int t = 0;
                        t = i + 1;
                        Console.WriteLine("Você tem mais {0} tentativas.", 3-t);

                            if (t > 2){
                                Console.WriteLine("Operação cancelada. Favor reiniciar.");
                                return;
                            }                   
                    }
                }
            }
            
            Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
                                        agencia: entradaAgencia,
                                        numConta: entradaNumConta,
                                        saldo: entradaSaldo,
                                        credito: entradaCredito,
                                        nome: entradaNome);

            listContas.Add(novaConta);

        }
        
			
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor!!!");
            Console.WriteLine("Seja bem-vindo(a)!");
            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            
            return opcaoUsuario;
        }
	}
}

