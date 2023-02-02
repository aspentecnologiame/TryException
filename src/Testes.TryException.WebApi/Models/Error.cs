using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testes.TryException.WebApi.Models
{
    public class Error
    {
        /// <summary>
        /// Código do erro.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Mensagem descritiva do erro.
        /// </summary>
        public string Message { get; set; }
    }
}
