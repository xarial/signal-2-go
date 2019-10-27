using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xarial.Signal2Go.Attributes;
using Xarial.Signal2Go.Services.Attributes;

namespace CoreTests
{
    internal class ServiceHelper : IDisposable
    {
        internal string WorkingDir { get; private set; }

        internal ServiceHelper()
        {
            WorkingDir = Path.Combine(Path.GetTempPath(),
                "Xarial_" + Guid.NewGuid());

            Cleanup();

            Directory.CreateDirectory(WorkingDir);
        }
        
        public void Dispose()
        {
            Cleanup();
        }

        internal Assembly MockAppAssembly(Version assmVersion, params Attribute[] atts)
        {
            var assmName = new AssemblyName(Guid.NewGuid().ToString());

            if (assmVersion != null)
            {
                assmName.Version = assmVersion;
            }

            var builder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                    assmName, AssemblyBuilderAccess.Run);

            if (atts != null)
            {
                foreach (var att in atts)
                {
                    builder.SetCustomAttribute(BuildCustomAttribute(att));
                }
            }

            var module = builder.DefineDynamicModule(Guid.NewGuid().ToString());

            var typeBuilder = module.DefineType(Guid.NewGuid().ToString(), TypeAttributes.Public);

            var type = typeBuilder.CreateType();

            return type.Assembly;
        }

        private CustomAttributeBuilder BuildCustomAttribute(Attribute attribute)
        {
            var type = attribute.GetType();
            ConstructorInfo constructor = null;

            constructor = type.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0);

            var fields = new FieldInfo[0];
            var constrArgs = new object[0];
            var fieldValues = new object[0];

            if (constructor == null)
            {
                constructor = type.GetConstructors().First();

                var paramTypes = constructor.GetParameters().Select(p => p.ParameterType);

                fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                fieldValues = fields.Select(f => f.GetValue(attribute)).ToArray();

                constrArgs = paramTypes.Select(p => Activator.CreateInstance(p)).ToArray();
            }

            return new CustomAttributeBuilder(constructor, constrArgs,
                                             new PropertyInfo[0], new object[0],
                                             fields, fieldValues);
        }

        private void Cleanup()
        {
            if (Directory.Exists(WorkingDir))
            {
                Directory.Delete(WorkingDir, true);
            }
        }
    }
}
