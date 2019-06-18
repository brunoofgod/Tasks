using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Application.Generic
{
    public class GridViewDto<T>
    {
        public List<T> Listagem { get; set; }
        public int Paginas { get; set; }

    }
}
