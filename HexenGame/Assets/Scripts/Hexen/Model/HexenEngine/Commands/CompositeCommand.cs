using System.Collections.Generic;
using CommandPattern;

namespace Hexen.Model.Engine.Commands
{
    public class CompositeCommand : Icommand
    {
        //List of all commands
        private readonly List<Icommand> _commands;

        /// <summary>
        /// Constructor: takes a list of commands and sets the reference
        /// </summary>
        /// <param name="commands"></param>
        public CompositeCommand(List<Icommand> commands)
        {
            _commands = commands;
        }

        /// <summary>
        /// Executes all commands in the list.
        /// </summary>
        public void Execute()
        {
            foreach (Icommand command in _commands)
            {
                command.Execute();
            }
        }

        /// <summary>
        /// Undo all the commands in the list in reverse order.
        /// </summary>
        public void Undo()
        {
            _commands.Reverse();
            foreach(Icommand command in _commands)
            {
                command.Undo();
            }
        }
    }
}
