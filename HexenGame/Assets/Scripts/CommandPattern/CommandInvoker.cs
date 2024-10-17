using System;
using System.Collections.Generic;

namespace CommandPattern
{
    public class CommandInvoker
    {
        //Stack of all the commands that can be undone
        private Stack<Icommand> _undoStack = new Stack<Icommand>();

        //Stack of all the commands that can be redone
        private Stack<Icommand> _redoStack = new Stack<Icommand>();

        /// <summary>
        /// Execute the command, add the executed command to the undoStack and clear the RedoStack
        /// </summary>
        /// <param name="command"> Comman to be executed</param>
        public void ExecuteCommand(Icommand command)
        {
            command.Execute();
            _undoStack.Push(command);

            _redoStack.Clear();
        }

        /// <summary>
        /// Undo the first command in the undoStack and add that to the RedoStack
        /// </summary>
        public void Undo()
        {
            if (_undoStack.Count == 0) return;

            Icommand command = _undoStack.Pop();
            command.Undo();

            _redoStack.Push(command);
        }

        /// <summary>
        /// Redo the first command in the redoStack and add it back to the UndoStack
        /// </summary>
        public void Redo()
        {
            if (_redoStack.Count == 0) return;

            Icommand command = _redoStack.Pop();
            command.Execute();

            _undoStack.Push(command);
        }
    }
}
