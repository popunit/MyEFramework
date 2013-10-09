using AutoMapper;
using eCommerce.Core;
using eCommerce.Core.Common;
using eCommerce.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace eCommerce.Data
{
    public class CommerceDbContext : DbContext, IDatabase
    {
        // TO-DO: should cache in shared
        private static readonly Dictionary<Type, object> Dic = new Dictionary<Type, object>();

        static CommerceDbContext()
        {
            //Database.SetInitializer<CommerceDbContext>(null);

            // TO-DO: Move to function method
            var types = typeof(CommerceDbContext).GetTypesFromCurrentAssembly();
            types.Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                type.IsInheritFrom(typeof(EntityBase)))
                .ForEach(type => Mapper.CreateMap(type, type));

            types.Where(type => !String.IsNullOrEmpty(type.Namespace) &&
                type.IsInheritFrom(typeof(IEmptyEntityMap<>))).
                ForEach(type =>
                {
                    //dynamic instance = Activator.CreateInstance(type);
                    dynamic instance = EmitHelper.FastGetInstance(type)();
                    if (instance.IsValid() == true)
                    {
                        Dic.Add(instance.EntityType, instance.Get());
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
            Type[] types = Assembly.GetAssembly(t).GetTypes();
            var entityTypeConfigurationTypes = new List<Type>();
            types.Where(type => type.IsInheritFrom(typeof(EntityBase))).ForEach(type => 
                entityTypeConfigurationTypes.Add(typeof(EntityTypeConfiguration<>).MakeGenericType(type)));

            entityTypeConfigurationTypes.ForEach(configType => types.Where(type => 
                !String.IsNullOrEmpty(type.Namespace) && 
                type.IsInheritFrom(configType))
                .ForEach(type =>
                {
                    //dynamic instance = Activator.CreateInstance(type);
                    dynamic instance = EmitHelper.FastGetInstance(type)();
                    modelBuilder.Configurations.Add(instance);
                }));
            

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<TEntity> Store<TEntity>() where TEntity : EntityBase, new()
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Execute sql command including stored procedure.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <remarks>[TO-DO] consider simplifying code</remarks>
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

        /// <summary>
        /// Attach entity into context.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : EntityBase, new()
        {
            if (this.GetEntityState(entity) != EntityState.Detached)
                this.Store<TEntity>().Attach(entity);
            return entity;
        }

        public EntityState GetEntityState<TEntity>(TEntity entity) where TEntity : EntityBase, new()
        {
            return this.Entry(entity).State;
        }

        public void SetEntityState<TEntity>(TEntity entity, EntityState state) where TEntity : EntityBase, new()
        {
            this.Entry(entity).State = state;
        }

        public TEntity GetEmptyEntity<TEntity>() where TEntity : EntityBase, new()
        {
            return (TEntity)Dic[typeof(TEntity)];
        }
    }
}
