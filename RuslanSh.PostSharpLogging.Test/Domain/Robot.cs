using System;
using RuslanSh.PostSharpLogging.Test.Infrastructure;

namespace RuslanSh.PostSharpLogging.Test.Domain
{
    public class Robot
    {
        [LogMethod]
        public string SaveHuman(Human human)
        {
            return $"Human {human.Name} saved!";
        }

        [LogMethod]
        public string KillHuman(Human human)
        {
            throw new Exception($"I can't kill human {human.Name} because of First Law!");
        }
    }
}
