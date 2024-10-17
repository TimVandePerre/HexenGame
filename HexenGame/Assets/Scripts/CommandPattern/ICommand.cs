using System.Collections;
using System.Collections.Generic;

namespace CommandPattern
{
    public interface Icommand
    {
        void Execute();
        void Undo();
    }
}
