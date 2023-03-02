using System.Collections.Generic;

using platformer.entities;

namespace platformer
{
    
    class EntityContainer
    {
        World world;
        List<IEntity> entities;
        Queue<IEntity> removalQueue;
        Queue<IEntity> additionQueue;

        //List is for iteration ONLY, do not manipulate the data
        public List<IEntity> Entities => entities;

        public EntityContainer(World world) 
        {
            this.world = world;
            entities = new List<IEntity>();
            removalQueue = new Queue<IEntity>();
            additionQueue = new Queue<IEntity>();
        }

        public void AddEntity(IEntity entity)
        {
            if (entities.Contains(entity) || additionQueue.Contains(entity)) return;

            additionQueue.Enqueue(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            if (!entities.Contains(entity) || removalQueue.Contains(entity)) return;

            removalQueue.Enqueue(entity);
        }

        public void Flush()
        {
            while (additionQueue.Count > 0)
            {
                IEntity e = additionQueue.Dequeue();
                e.World = world;
                entities.Add(e);
            }

            while (removalQueue.Count > 0)
            {
                entities.Remove(removalQueue.Dequeue());
            }
        }
    }
}