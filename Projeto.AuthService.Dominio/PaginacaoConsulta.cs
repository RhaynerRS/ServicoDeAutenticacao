using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.AuthService.DataTransfer
{
    public class PaginacaoConsulta<T> where T : class
    {
        public int Total { get; set; }
        public List<T> Resultados { get; set; } = new List<T>();
    }
}
