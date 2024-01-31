using AutenticacaoMarcusApi.SharedKernel.Mensageria;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutenticacaoMarcusApi.SharedKernel.Retornos
{
    public class RetornoApi<T>
    {
        public RetornoApi() { }

        public RetornoApi(bool sucesso, T dados, IEnumerable<Mensagem> mensagens)
        {
            Sucesso = sucesso;
            Dados = dados;
            Mensagens = mensagens;
        }

        public RetornoApi(T dados, IEnumerable<Mensagem> mensagens)
        {
            Sucesso = true;
            Dados = dados;
            Mensagens = mensagens;
        }

        public bool Sucesso { get; set; }
        public T Dados { get; set; }
        public IEnumerable<Mensagem> Mensagens { get; set; }
    }
}
