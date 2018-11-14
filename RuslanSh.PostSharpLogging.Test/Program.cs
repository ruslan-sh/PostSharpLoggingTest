using RuslanSh.PostSharpLogging.Test.Domain;
using RuslanSh.PostSharpLogging.Test.Infrastructure.Logging;
using SimpleInjector;

namespace RuslanSh.PostSharpLogging.Test
{
    class Program
    {
        static Container InitializeInjection()
        {
            var result = new Container();
            result.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);
            return result;
        }

        static void Main()
        {
            var container = InitializeInjection();
            LogManager.Initialize(container.GetInstance<ILogger>());
            RunBusinessLogic();
        }

        private static void RunBusinessLogic()
        {
            var iRobot = new Robot();
            var ordinaryHuman = new Human
            {
                Name = "Will Smith",
                Age = 42
            };

            iRobot.SaveHuman(ordinaryHuman);
            iRobot.KillHuman(ordinaryHuman);
        }
    }
}
