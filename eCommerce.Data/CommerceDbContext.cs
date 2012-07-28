using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Data.Common;

namespace eCommerce.Data
{
    public class CommerceDbContext : DbContext, IDatabase
    {
        // TO-DO: should cache in shared
        private readonly static Dictionary<Type, dynamic> dic = new Dictionary<Type,dynamic>();

        static CommerceDbContext()
        {
            //Database.SetInitializer<CommerceDbContext>(null);

            // TO-DO: Move to function method
            Type t = typeof(CommerceDbContext);
            var types = Assembly.GetAssembly(t).GetTypes();
            types.Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                type.IsInherit(typeof(EntityBase)))
                .ForEach(type =>
                {
                    Mapper.CreateMap(type, type);
                });

            types.Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                type.IsInherit(typeof(IEmptyEntityMap<>))).
                ForEach(type =>
                {
                    //var argTypes = type.BaseType.GetGenericArguments();
                    dynamic instance = Activator.CreateInstance(type);
                    if (instance.IsValid() == true)
                    {
                        dic.Add(instance.EntityType, instance.Get());
                    }
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        /// <remarks>Use connection string as parameter if release, because there are multiple data providers
        /// so that we can dynamically change background database. We only use name of connectionstring in web config
        /// on debug period</remarks>
        public CommerceDbContext(string nameOrConnectionString) : 
            base(nameOrConnectionString)
        { 
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Type t = typeof(CommerceDbContext);
            Assembly.GetAssembly(t)
                .GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace) && 
                    type.IsInherit(typeof(EntityTypeConfiguration<>)))
                .ForEach(type => 
                {
                    dynamic instance = Activator.CreateInstance(type);
                    modelBuilder.Configurations.Add(instance);
                });
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<TEntity> Store<TEntity>() where TEntity : EntityBase, new()
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteCommand<TEntity>(string command, params DbParameter[] parameters) 
            where TEntity : EntityBase, new()
        {
            // flag if there are output parameters in command
            bool hasOutputParameters = false;

            // check all the parameters
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    if (param == null)
                        continue;

                    if (!hasOutputParameters && (param.Direction == ParameterDirection.InputOutput ||
                        param.Direction == ParameterDirection.Output))
                    {
                        hasOutputParameters = true;
                        break;
                    }
                }
            }

            // get tranditional objectcontext
            var context = this.ToObjectContext();
            if (!hasOutputParameters)
            {
                //no output parameters
                var result = this.Database.SqlQuery<TEntity>(command, parameters).ToList();
                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);

                return result;
            }
            else
            {
                var connection = this.Database.Connection;

                //open the connection for use
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                //create a command object
                using (var cmd = connection.CreateCommand())
                {
                    //command to execute
                    cmd.CommandText = command;
                    cmd.CommandType = CommandType.StoredProcedure;

                    // move parameters to command object
                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.Add(p);

                    //database call
                    var reader = cmd.ExecuteReader();
                    //return reader.DataReaderToObjectList<TEntity>();
                    var result = context.Translate<TEntity>(reader).ToList();
                    for (int i = 0; i < result.Count; i++)
                        result[i] = AttachEntityToContext(result[i]);
                    //close up the reader, we're done saving results
                    reader.Close();
                    return result;
                }

            }
        }

        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : EntityBase, new()
        {
            var alreadyAttached = this.Store<TEntity>().Local.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (alreadyAttached == null)
            {
                //attach new entity
                this.Store<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                //entity is already loaded.
                return alreadyAttached;
            }
        }

        public EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : EntityBase, new()
        {
            return this.Entry<TEntity>(entity).State;
        }

        public void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : EntityBase, new()
        {
            this.Entry<TEntity>(entity).State = state;
        }


        public TEntity GetEmptyEntity<TEntity>() where TEntity : EntityBase, new()
        {
            return (TEntity)dic[typeof(TEntity)];
        }
    }
}
