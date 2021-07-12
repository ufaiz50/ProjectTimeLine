﻿using Microsoft.EntityFrameworkCore;
using ProjectTimeLine.Context;
using ProjectTimeLine.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTimeLine.Repositories
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Context : MyContext
        where Entity : class
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {
            try
            {
                var find = entities.Find(key);
                entities.Remove(find);
                var delete = myContext.SaveChanges();
                return delete;
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
        }

        public IEnumerable<Entity> Get()
        {
            var find = entities.ToList();
            return find;
        }

        public Entity Get(Key key)
        {
            var find = entities.Find(key);
            return find;
        }

        public int Insert(Entity e)
        {
            entities.Add(e);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Entity e, Key key)
        {
            try
            {
                myContext.Entry(e).State = EntityState.Modified;
                var update = myContext.SaveChanges();
                return update;
            }
            catch (NullReferenceException)
            {
                return 0;
            }
        }
    }
}
