using System;
using System.Collections.Generic;

namespace Tasks.Application.Generic
{
    public interface IBaseEntityService<Dto, Entity> : IBaseService
    {
        Dto Add(Dto dto);

        bool Delete(Guid id);

        Dto Edit(Dto dto);

        Dto GetById(Guid id);

        Entity MontarEntidade(Dto dto);
    }
}