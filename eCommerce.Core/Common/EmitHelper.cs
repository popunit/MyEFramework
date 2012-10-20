using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Common
{
    public static class EmitHelper
    {
        public static Func<object> FastGetInstance(Type type)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, type, new Type[0], typeof(EmitHelper).Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            il.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Ret);
            return (Func<object>)dynamicMethod.CreateDelegate(typeof(Func<object>));
        }
    }
}
