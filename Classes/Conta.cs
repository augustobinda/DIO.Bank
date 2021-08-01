using System;

namespace DIO.Bank
{
    public class Conta
    {
        private TipoTransferencia TipoEntrada { get; set; }
        private TipoConta TipoConta { get; set; }
        private double Saldo { get; set; }
        private double Credito { get; set; }
        private string Nome { get; set; }
        private int NumConta { get; set; }
        private int Agencia { get; set; }


        public Conta(TipoConta tipoConta, double saldo, double credito, string nome, int agencia, int numConta)
        {
            this.TipoConta = tipoConta;
            this.Saldo = saldo;
            this.Credito = credito;
            this.Nome = nome;
            this.Agencia = agencia;
            this.NumConta = numConta;
        }

        public void Transferencia(TipoTransferencia tipoTransferencia)
        {
            this.TipoEntrada = tipoTransferencia;
        }

        public bool Sacar(double valorSaque)
        {
            //Validação de saldo suficiente
            if (this.Saldo - valorSaque < (this.Credito *-1)){
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }

            this.Saldo -= valorSaque;
            Console.WriteLine("Saldo atual da conta de {0} é {1:c}", this.Nome, this.Saldo);
            //https://docs.microsoft.com/pt-br/dotnet/standard/base-types/composite-formatting

            return true;
        }

        public void Depositar(double valorDeposito)
        {
            this.Saldo += valorDeposito;
                        
            Console.WriteLine("Saldo atual da conta de {0} é {1:c}", this.Nome, this.Saldo);
        }

        public void Transferir(double valorTransferencia, Conta contaDestino, TipoTransferencia tipoTransferencia)
        {
            this.TipoEntrada = tipoTransferencia;
                        
            if (this.Sacar(valorTransferencia)){
                contaDestino.Depositar(valorTransferencia);
            }
            
            Console.WriteLine(tipoTransferencia);
            
            Console.WriteLine("Sua transferência " + (tipoTransferencia) + " foi realizada com sucesso!");
                                     
                       
        }

        public void Consulta()
        {
            Console.WriteLine("Tipo Conta {0,3} | Agência: {1,3:d5} | Conta: {2,3:d7} |", TipoConta, Agencia, NumConta );
            Console.WriteLine("Nome: {0,3} | Saldo: {1,3:c} | Crédito: {2,3:c} |", Nome, Saldo, Credito );
            Console.WriteLine("-------------------------------------------------------------------");
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "TipoConta " + this.TipoConta + " | ";
            retorno += "Agencia " + this.Agencia + " | ";
            retorno += "Conta " + this.NumConta + " | ";
            retorno += "Nome " + this.Nome + " | ";
            retorno += "Saldo " + this.Saldo + " | ";
            retorno += "Credito " + this.Credito;
            return retorno;
            
        }

        public void Cancelar(double valorDeposito)
        {
            this.Saldo += valorDeposito;
                        
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
        }

        
    }
}