using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GMWolf.GML
{
    public static class GML
    {
        [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(GML))]
        static GML()
        {
        }
        
        private delegate void GmlEventPerformAsyncDelegate(int a, int b);
        private delegate int GmlDSMapCreateDelegate(int num);
        private delegate bool GmlDSMapAddDoubleDelegate(int index, string key, double value);
        private delegate bool GmlDSMapAddStringDelegate(int index, string key, string value);
    
        private static GmlEventPerformAsyncDelegate GmlEventPerformAsync;
        private static GmlDSMapCreateDelegate GmlDSMapCreate;
        private static GmlDSMapAddDoubleDelegate GmlDSMapAddDouble;
        private static GmlDSMapAddStringDelegate GmlDSMapAddString;
    
        private const int EVENT_OTHER_SOCIAL = 70;

        [UnmanagedCallersOnly(EntryPoint = "RegisterCallbacks", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe double RegisterCallbacks(char* arg1, char* arg2, char* arg3, char* arg4)
        {
            GmlEventPerformAsync = Marshal.GetDelegateForFunctionPointer<GmlEventPerformAsyncDelegate>(new IntPtr(arg1));
            GmlDSMapCreate = Marshal.GetDelegateForFunctionPointer<GmlDSMapCreateDelegate>(new IntPtr(arg2));
            GmlDSMapAddDouble = Marshal.GetDelegateForFunctionPointer<GmlDSMapAddDoubleDelegate>(new IntPtr(arg3));
            GmlDSMapAddString = Marshal.GetDelegateForFunctionPointer<GmlDSMapAddStringDelegate>(new IntPtr(arg4));
            return 0;
        }
    
    
        [RequiresUnreferencedCode("ToGmlMap")]
        public static void EventPerformAsync(Dictionary<string, dynamic> dictionary)
        {
            GmlEventPerformAsync(dictionary?.ToGmlMap() ?? GmlDSMapCreate(0), EVENT_OTHER_SOCIAL);
        }
    
        [RequiresUnreferencedCode("GmlDSMapAdd")]
        [DynamicDependency("ToGmlMap", typeof(GML))]
        private static int ToGmlMap(this Dictionary<string, dynamic> dictionary)
        {
            int id = GmlDSMapCreate(0);

            foreach (String key in dictionary.Keys)
            {
                GmlDSMapAdd(id, key, dictionary[key]);
            }

            return id;
        }
    
        [DynamicDependency("GmlDSMapAdd", typeof(GML))]
        private static void GmlDSMapAdd(int id, string key, int value)
        {
            GmlDSMapAddDouble(id, key, value);
        }
    
        [DynamicDependency("GmlDSMapAdd", typeof(GML))]
        private static void GmlDSMapAdd(int id, string key, double value)
        {
            GmlDSMapAddDouble(id, key, value);
        }
    
        [DynamicDependency("GmlDSMapAdd", typeof(GML))]
        private static void GmlDSMapAdd(int id, string key, string value)
        {
            GmlDSMapAddString(id, key, value);
        }
    

        [RequiresUnreferencedCode("ToGmlMap")]
        [DynamicDependency("GmlDSMapAdd", typeof(GML))]
        private static void GmlDSMapAdd(int id, string key, Dictionary<string, dynamic> value)
        {
            GmlDSMapAddDouble(id, key, value.ToGmlMap());
        }
    
        [DynamicDependency("ToGmlMap", typeof(GML))]
        private static int ToGmlMap(this Dictionary<string, string> d)
        {
            int id = GmlDSMapCreate(0);
    
            foreach (String key in d.Keys)
            {
                GmlDSMapAddString(id, key, d[key]);
            }
    
            return id;
        }
    
        [RequiresUnreferencedCode("ToGmlMap")]
        [DynamicDependency("ToGmlMap", typeof(GML))]
        private static int ToGmlMap(this Dictionary<String, Dictionary<String, dynamic>> d)
        {
            int id = GmlDSMapCreate(0);

            foreach (String key in d.Keys)
            {
                GmlDSMapAddDouble(id, key, d[key].ToGmlMap());
            }

            return id;
        }

        [RequiresUnreferencedCode("EventPerformAsync")]
        public static object CallScript(double script, params dynamic[] args)
        {
            Dictionary<string, dynamic> map = new Dictionary<string, dynamic>()
             {
                {"type", "script" },
                {"script",  script}
             };
            int n = 0;
            foreach (dynamic arg in args)
            {
                map[n++.ToString()] = arg;
            }

            EventPerformAsync(map);

            return 0;

        }
    }
}
