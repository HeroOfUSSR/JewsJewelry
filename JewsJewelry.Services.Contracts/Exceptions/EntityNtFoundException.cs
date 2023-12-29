using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewsJewelry.Services.Contracts.Exceptions
{
    public class EntityNtFoundException<TEntity> : NtFoundException
    {
        public EntityNtFoundException(Guid id)
            : base($"Сущность {typeof(TEntity)} c id = {id} не найдена.")
        {

        }
    }
}
