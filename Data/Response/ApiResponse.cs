using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Response
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }      // Código de estado HTTP (200, 400, etc.)
        public string Message { get; set; } // Mensaje detallado
        public T Data { get; set; }        // Datos de la respuesta (si aplica)

        public ApiResponse(int code, string message, T data = default)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }
}
