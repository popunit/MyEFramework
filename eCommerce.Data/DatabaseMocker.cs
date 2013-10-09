using AutoMapper;
using eCommerce.Core;
using eCommerce.Data.Domain.Common.Entities;
using eCommerce.Data.Domain.Users.Entities;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Data
{
    internal static class DatabaseMocker
    {
        private readonly static IList<UserRole> UserRoleTable = new List<UserRole> 
        {
            new UserRole{ Actived = true, Id = 1, IsSystemRole = true, RoleName = "GUEST", SystemName="Guest"}
        };

        private readonly static IList<User> UserTable = new List<User>();

        private readonly static IList<GenericCharacteristic> GenericCharacteristicTable = new List<GenericCharacteristic>();

        internal static IList<T> GetTable<T>()
        {
            switch (typeof(T).Name.ToUpperInvariant())
            {
                case "USERROLE":
                    return UserRoleTable as IList<T>;
                case "USER":
                    return UserTable as IList<T>;
                case "GENERICCHARACTERISTIC":
                    return GenericCharacteristicTable as IList<T>;
                default:
                    return null;
            }
        }
    }

    public class FaskeRepository<T> : IRepository<T>
        where T : EntityBase, new()
    {

        public T GetByKeys(params object[] keys)
        {
            // hack here, only support key is Id, but different table should have different keys
            var table = DatabaseMocker.GetTable<T>();
            return (from e in table
                         where e.Id == long.Parse(keys[0].ToString())
                         select e).FirstOrDefault();
        }

        public bool Insert(T entity)
        {
            var table = DatabaseMocker.GetTable<T>();
            if (entity.Id != 0)
            {
                var target = (from e in table
                              where e.Id == entity.Id
                              select e).FirstOrDefault();
                if (null == target)
                {
                    table.Add(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!table.Any()) // empty list
                {
                    entity.Id = 1; // first entity in table
                }
                else
                {
                    entity.Id = table.Last().Id + 1; // [bug] if deleted the last element, the next element added will be the id of the deleted one
                }

                table.Add(entity);
                return true;
            }
        }

        public bool Update(T entity)
        {
            // entity.Id is identity number, it should not be 0;
            if (entity.Id == 0)
                return false;
            var table = DatabaseMocker.GetTable<T>();
            var target = (from e in table
                         where e.Id == entity.Id
                         select e).FirstOrDefault();
            if(null == target)
            {
                return false;
            }
            else
            {
                //target = entity;
                Mapper.Map(entity, target);
                return true;
            }
        }

        public bool Delete(T entity)
        {
            // entity.Id is identity number, it should not be 0;
            if (entity.Id == 0)
                return false;
            var table = DatabaseMocker.GetTable<T>();
            var target = (from e in table
                         where e.Id == entity.Id
                         select e).FirstOrDefault();
            if(null == target)
            {
                return false;
            }
            else
            {
                table.Remove(target);
                return true;
            }
        }

        public IQueryable<T> Table
        {
            get
            {
                return DatabaseMocker.GetTable<T>().AsQueryable<T>();
            }
        }
    }
}
