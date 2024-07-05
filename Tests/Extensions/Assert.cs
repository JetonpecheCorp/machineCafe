using Services;

namespace Xunit;

public partial class Assert
{
    public static void XCafeSontServi(MachineCafe _machineCafe, uint _nbCafeServi)
    {
        Equal(_nbCafeServi, _machineCafe.Hardware.NbCafeFabriquer);
    }
}
