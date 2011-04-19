using System;
using MyShortCodes.Phone.Persistence;

namespace MyShortCodes.Phone.Services
{
    public interface IEntityInsertService
    {
        bool PersistEntity<TEntity>(TEntity entity);
    }

    public class EntityInsertService : IEntityInsertService
    {
        public bool PersistEntity<TEntity>(TEntity entity)
        {
            EntityPersister<TEntity>.Entities.Add(entity);
            return true;
        }
    }
}