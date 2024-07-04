using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class MachineCafe
{
    public byte PrixCafe { get; } = (byte)EPiece._50Centime;
    public uint NbCafeServi { get; set; }
    public uint ArgentTotal { get; set; }

    public void Inserer(EPiece _piece)
    {
        if ((byte)_piece < PrixCafe)
            return;

        NbCafeServi++;
        ArgentTotal += (byte)_piece;
    }
}
