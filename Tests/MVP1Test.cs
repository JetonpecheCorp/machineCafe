using Services;
using Services.CarteBleus;
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
        machineCafe.Hardware.CallbackAnnuler = machineCafe.Annuler;
        machineCafe.Hardware.CallbackAccepter = machineCafe.Valider;
    }

    [Fact]
    public void Payer_sans_contact_test()
    {
        CarteBleuFaker cb = new("1234-1234-1234-1234", 10);
        machineCafe.SansContact(cb);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabriquer);
    }

    [Fact]
    public void Payer_sans_contact_et_avec_une_piece_test()
    {
        CarteBleuFaker cb = new("1234-1234-1234-1234", 10);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);
        machineCafe.SansContact(cb);

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabriquer);
    }

    [Fact]
    public void Servir_cafe_multiple_piece_test()
    {
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._10Centime);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabriquer);
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
        Assert.Equal((int)_piece, (int)machineCafe.ArgentTotal);
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