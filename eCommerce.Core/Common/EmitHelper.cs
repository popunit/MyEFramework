using System;
using System.Reflection.Emit;

namespace eCommerce.Core.Common
{
    public static class EmitHelper
    {
        public static Func<object> FastGetInstance(Type type)
        {
            if(type.IsNull())
                throw new ArgumentNullException("type");
            var dynamicMethod = new DynamicMethod(string.Empty, type, new Type[0], typeof(EmitHelper).Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Ret);
            return (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
        }
    }
}
