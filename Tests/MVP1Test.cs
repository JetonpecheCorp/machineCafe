using Services;
using Services.CarteBleus;

namespace Tests;

public class MVP1Test
{
    MachineCafeBuilder builder { get; } = new();

    [Fact]
    public void Payer_sans_contact_test()
    {
        var machineCafe = builder.AjouterHadwareCarteBleu(new HardwareCarteBleuFaker()).Build();

        CarteBleuFaker cb = new(10);
        machineCafe.HardwareCarteBleu!.SimulerPayementSansContact(cb);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Payer_sans_contact_pas_assez_argent_test()
    {
        var machineCafe = builder.AjouterHadwareCarteBleu(new HardwareCarteBleuFaker()).Build();

        CarteBleuFaker cb = new(0);
        machineCafe.HardwareCarteBleu!.SimulerPayementSansContact(cb);

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Payer_sans_contact_et_avec_une_piece_test()
    {
        var machineCafe = builder.AjouterHadwareCarteBleu(new HardwareCarteBleuFaker()).Build();

        CarteBleuFaker cb = new(10);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);

        machineCafe.HardwareCarteBleu!.SimulerPayementSansContact(cb);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Servir_cafe_multiple_piece_test()
    {
        var machineCafe = builder.Build();
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._20Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._10Centime);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Servir_deux_cafe_test()
    {
        var machineCafe = builder.Build();
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._50Centime);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._50Centime);

        Assert.Equal(2, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Theory]
    [InlineData(EPiece._1Centime)]
    [InlineData(EPiece._2Centime)]
    [InlineData(EPiece._5Centime)]
    [InlineData(EPiece._10Centime)]
    [InlineData(EPiece._20Centime)]
    public void Pas_assez_argent_servir_cafe_test(EPiece _piece)
    {
        var machineCafe = builder.Build();
        machineCafe.Hardware.SimulerInsertionPiece(_piece);

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabrique);
        Assert.Equal((int)_piece, (int)machineCafe.ArgentTotal);
    }

    [Theory]
    [InlineData(EPiece._1Euro)]
    [InlineData(EPiece._2Euro)]
    public void Trop_argent_servir_cafe_test(EPiece _piece)
    {
        var machineCafe = builder.Build();
        machineCafe.Hardware.SimulerInsertionPiece(_piece);

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
        Assert.Equal((int)_piece, (int)machineCafe.ArgentTotal);
    }
}