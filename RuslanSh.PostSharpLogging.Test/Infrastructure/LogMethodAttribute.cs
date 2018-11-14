using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using RuslanSh.PostSharpLogging.Test.Infrastructure.Logging;

namespace RuslanSh.PostSharpLogging.Test.Infrastructure
{
    /// <summary>
    ///   Aspect that, when applied to a method, appends a record to the <see cref="LogManager" /> class whenever this method is
    ///   executed.
    /// </summary>
    [PSerializable]
    [LinesOfCodeAvoided(6)]
    public sealed class LogMethodAttribute : OnMethodBoundaryAspect
    {
        /// <summary>
        ///   Method invoked before the target method is executed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Start ");
            AppendMethodName(args, stringBuilder);
            LogManager.WriteLine(stringBuilder.ToString());
        }


        /// <summary>
        ///   Method invoked after the target method has successfully completed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Done ");
            AppendMethodName(args, stringBuilder);

            if (!args.Method.IsConstructor && ((MethodInfo) args.Method).ReturnType != typeof(void))
            {
                stringBuilder.Append(". Returns:");
                stringBuilder.Append(JsonConvert.SerializeObject(args.ReturnValue));
            }

            LogManager.WriteLine(stringBuilder.ToString());
        }

        /// <summary>
        ///   Method invoked when the target method has failed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("Exception in ");
            AppendMethodName(args, stringBuilder);

            if (!args.Method.IsConstructor && ((MethodInfo) args.Method).ReturnType != typeof(void))
            {
                stringBuilder.AppendLine();
                stringBuilder.Append(JsonConvert.SerializeObject(args.Exception));
            }

            LogManager.WriteLine(stringBuilder.ToString());
        }

        private static void AppendMethodName(MethodExecutionArgs args, StringBuilder stringBuilder)
        {
            var declaringType = args.Method.DeclaringType;
            Formatter.AppendTypeName(stringBuilder, declaringType);
            stringBuilder.Append('.');
            stringBuilder.Append(args.Method.Name);

            if (args.Method.IsGenericMethod)
            {
                var genericArguments = args.Method.GetGenericArguments();
                Formatter.AppendGenericArguments(stringBuilder, genericArguments);
            }

            var arguments = args.Arguments;
            
            Formatter.AppendArguments(stringBuilder, arguments);
        }
    }
}
