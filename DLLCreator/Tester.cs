using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLLCreator
{
    public static class Tester
    {
        public static object RunMethod(string dll, string methodname) 
        {
            Assembly asm = Assembly.LoadFile(dll);
            Type[] t = asm.GetTypes();
            Type MainClass = t.Where(w => w.Name.ToLower() == "main").FirstOrDefault();
            MethodInfo toInvoke = MainClass.GetMethod(methodname);
            toInvoke.Invoke(null, new object[0]);
            return true;
        }
    }
}
