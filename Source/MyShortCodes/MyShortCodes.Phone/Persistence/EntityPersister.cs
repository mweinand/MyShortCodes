using System.Collections.Generic;

namespace MyShortCodes.Phone.Persistence
{
    public static class EntityPersister<TEntity>
    {
        public static IList<TEntity> Entities { get; private set; }

        static EntityPersister()
        {
            Entities = new List<TEntity>();
        }
    }
}