using CommandPattern;

namespace Hexen.Model.Engine.Commands
{
    public class ChangePlayerCommand : Icommand
    {
        //Reference to the HexenEngine.
        private readonly HexenEngine _engine;

        /// <summary>
        /// Constructor: takes and sets the HexEngin reference.
        /// </summary>
        /// <param name="engine"></param>
        public ChangePlayerCommand(HexenEngine engine)
        {
            _engine = engine;
        }

        /// <summary>
        /// Executes the SwitchPlayer method from the HexEngin.
        /// </summary>
        public void Execute()
        {
            _engine.SwitchPlayer();
        }

        /// <summary>
        /// Executes the SwitchPlayer method from the HexEngin.
        /// </summary>
        public void Undo()
        {
            _engine.SwitchPlayer();
        }
    }
}
