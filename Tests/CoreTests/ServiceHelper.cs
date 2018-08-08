using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xarial.AppLaunchKit.Attributes;
using Xarial.AppLaunchKit.Services.Attributes;

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

        internal Type MockAppType(string typeName, Version assmVersion, string updatesUrl)
        {
            return MockAppType(typeName, a=>
            {
                return new CustomAttributeBuilder(
                    a.GetConstructor(
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null,
                        new Type[] { typeof(string) }, null),
                    new object[] { updatesUrl });
            }, assmVersion, typeof(UpdatesUrlAttribute));
        }

        internal Type MockAppType(string typeName, Func<Type, CustomAttributeBuilder> attBuilder = null, 
            Version assmVersion = null, params Type[] attTypes)
        {
            if (attBuilder == null)
            {
                attBuilder = new Func<Type, CustomAttributeBuilder>(attType => 
                {
                    return new CustomAttributeBuilder(
                        attType.GetConstructor(
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null,
                        Type.EmptyTypes, null), new object[0]);
                });
            }

            var assmName = new AssemblyName(Guid.NewGuid().ToString());

            if (assmVersion != null)
            {
                assmName.Version = assmVersion;
            }

            var builder =
                AppDomain.CurrentDomain.DefineDynamicAssembly(
                    assmName,
                    AssemblyBuilderAccess.Run);

            var module = builder.DefineDynamicModule(Guid.NewGuid().ToString());

            var typeBuilder = module.DefineType(typeName, TypeAttributes.Public);

            if (attTypes != null)
            {
                foreach (var attType in attTypes)
                {
                    var custAttBuilder = attBuilder.Invoke(attType);
                    typeBuilder.SetCustomAttribute(custAttBuilder);
                }
            }

            var type = typeBuilder.CreateType();

            return type;
        }

        internal string CreateUpgradeServer(byte[] data)
        {
            var filePath = Path.Combine(WorkingDir, Guid.NewGuid() + ".json");
            var testUpgradeUrl = "file:///" + filePath.Replace('\\', '/');
            File.WriteAllBytes(filePath, data);

            return testUpgradeUrl;
        }

        internal static TAtt GetAttribute<TAtt>(Type type)
            where TAtt : Attribute
        {
            TAtt att;
            type.TryGetAttribute<TAtt>(out att);
            return att;
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
