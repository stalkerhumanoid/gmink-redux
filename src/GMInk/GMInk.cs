using Ink.Runtime;
using System.Runtime.InteropServices;
using System.Diagnostics.CodeAnalysis;
namespace GMWolf.GMInk
{
    using GML;

    public class GMInk
    {
        private static Story? story;

        static GMInk()
        {
        }

        [UnmanagedCallersOnly(EntryPoint = "Load", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe double Load(char* file)
        {
            string json = File.ReadAllText(Marshal.PtrToStringUTF8((IntPtr)file) ?? "");
            story = new Story(json);

            return 1;
        }


        [UnmanagedCallersOnly(EntryPoint = "CanContinue", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static double CanContinue()
        {
            return (story?.canContinue ?? false) ? 1 : 0 ;
        }


        [UnmanagedCallersOnly(EntryPoint = "Continue", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* Continue()
        {
            var result = story?.Continue() ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }


        [UnmanagedCallersOnly(EntryPoint = "CurrentChoicesCount", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static double CurrentChoicesCount()
        {
            return story?.currentChoices.Count ?? 0;
        }


        [UnmanagedCallersOnly(EntryPoint = "CurrentChoices", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* CurrentChoice(double i)
        {
            var result = story?.currentChoices?[(int)i]?.text ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }


        [UnmanagedCallersOnly(EntryPoint = "ChooseChoiceIndex", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static void ChooseChoiceIndex(double i)
        {
            story?.ChooseChoiceIndex((int)i);
        }


        [UnmanagedCallersOnly(EntryPoint = "SaveState", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* SaveState()
        {
            var result = story?.state.ToJson() ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }


        [UnmanagedCallersOnly(EntryPoint = "LoadState", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe void LoadState(char* json)
        {
            if (story != null)
            {
                var jsonString = Marshal.PtrToStringUTF8((IntPtr)json) ?? "";
                story.state.LoadJson(jsonString);
            }
        }

        [UnmanagedCallersOnly(EntryPoint = "TagCount", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static double TagCount()
        {
            return TagCountInternal();
        }

        public static double TagCountInternal()
        {
            return story?.currentTags?.Count ?? 0;
        }

        [UnmanagedCallersOnly(EntryPoint = "GetTag", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* GetTag(double i)
        {
            string result;
            if ((int)i < TagCountInternal() && i >= 0)
            {
                result = story?.currentTags?[(int)i] ?? "";
            } else
            {
                result = "";
            }
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }

        [UnmanagedCallersOnly(EntryPoint = "TagForContentAtPathCount", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe double TagForContentAtPathCount(char* path)
        {
            var pathString = Marshal.PtrToStringUTF8((IntPtr)path) ?? "";
            return story?.TagsForContentAtPath(pathString)?.Count ?? 0;
        }

        [UnmanagedCallersOnly(EntryPoint = "TagForContentAtPath", CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
        public static unsafe char* TagForContentAtPath(char* path, double i)
        {
            var pathString = Marshal.PtrToStringUTF8((IntPtr)path) ?? "";
            var result = story?.TagsForContentAtPath(pathString)?[(int)i] ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }

        [UnmanagedCallersOnly(EntryPoint = "GlobalTagCount", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static double GlobalTagCount()
        {
            return story?.globalTags?.Count ?? 0;
        }

        [UnmanagedCallersOnly(EntryPoint = "GlobalTag", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* GlobalTag(double i)
        {
            var result = story?.globalTags?[(int)i] ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }

        [UnmanagedCallersOnly(EntryPoint = "ChoosePathString", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe void ChoosePathString(char* path)
        {
            var pathString = Marshal.PtrToStringUTF8((IntPtr)path) ?? "";
            story?.ChoosePathString(pathString);
        }

        [UnmanagedCallersOnly(EntryPoint = "VariableGetReal", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe double VariableGetReal(char* var)
        {
            var varString = Marshal.PtrToStringUTF8((IntPtr)var) ?? "";
            object o = story?.variablesState?[varString] ?? 0;
            return double.Parse(o.ToString() ?? "0");
        }

        [UnmanagedCallersOnly(EntryPoint = "VariableGetString", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe char* VariableGetString(char* var)
        {
            var varString = Marshal.PtrToStringUTF8((IntPtr)var) ?? "";
            var result = (story?.variablesState?[varString] ?? "").ToString() ?? "";
            return (char*)Marshal.StringToHGlobalAnsi(result);
        }

        [UnmanagedCallersOnly(EntryPoint = "VariableSetReal", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe void VariableSetReal(char* var, double value)
        {
            if (story != null)
            {
                var varString = Marshal.PtrToStringUTF8((IntPtr)var) ?? "";
                story.variablesState[varString] = value;
            }
        }

        [UnmanagedCallersOnly(EntryPoint = "VariableSetString", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe void VariableSetString(char* var, char* value)
        {
            if (story != null)
            {
                var varString = Marshal.PtrToStringUTF8((IntPtr)var) ?? "";
                var valueString = Marshal.PtrToStringUTF8((IntPtr)value) ?? "";
                story.variablesState[varString] = valueString;
            }
        }

        [UnmanagedCallersOnly(EntryPoint = "VisitCountAtPathString", CallConvs = new[] {typeof(System.Runtime.CompilerServices.CallConvCdecl)})]
        public static unsafe double VisitCountAtPathString(char* path)
        {
            var pathString = Marshal.PtrToStringUTF8((IntPtr)path) ?? "";
            return story?.state?.VisitCountAtPathString(pathString) ?? 0;
        }

       
       
        public static unsafe void ObserveVariable(char* name, double script)
        { 
            var nameString = Marshal.PtrToStringUTF8((IntPtr)name) ?? "";
            story?.ObserveVariable(nameString, (string varName, object value) =>
            {
                GML.CallScript(script, varName, value);
            });
        }

        public static unsafe void BindExternal(char* name, double script)
        {
            var nameString = Marshal.PtrToStringUTF8((IntPtr)name) ?? "";
            story?.BindExternalFunctionGeneral(nameString, (object[] args) =>
            {
                GML.CallScript(script, args);
                return 0;
            });
        }

    }
}
