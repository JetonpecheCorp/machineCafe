using Services;
using Services.Hardwares;

namespace Tests;

public class MVP1Test
{
    readonly MachineCafe machineCafe;

    public MVP1Test()
    {
        machineCafe = new()
        {
            Hardware = new HardwareFaker()
        };

        machineCafe.Hardware.CallbackInsertionPiece = machineCafe.Inserer;
    }

    [Fact]
    public void Servir_deux_cafe_test()
    {
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._50Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._50Centime);

        Assert.Equal(2, (int)machineCafe.Hardware.NbCafeFabriquer);
    }

    [Theory]
    [InlineData(EPiece._1Centime)]
    [InlineData(EPiece._2Centime)]
    [InlineData(EPiece._5Centime)]
    [InlineData(EPiece._10Centime)]
    [InlineData(EPiece._20Centime)]
    public void Pas_assez_argent_servir_cafe_test(EPiece _piece)
    {
        machineCafe.Hardware.SimulerInsertionPiece(_piece);

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabriquer);
        Assert.Equal(0, (int)machineCafe.ArgentTotal);
    }

    [Theory]
    [InlineData(EPiece._1Euro)]
    [InlineData(EPiece._2Euro)]
    public void Trop_argent_servir_cafe_test(EPiece _piece)
    {
        machineCafe.Hardware.SimulerInsertionPiece(_piece);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabriquer);
        Assert.Equal((int)_piece, (int)machineCafe.ArgentTotal);
    }
}