using Services;

namespace Tests;

public class MVP1_test
{
    MachineCafe machineCafe = new();

    [Fact]
    public void Servir_un_cafe_test()
    {
        machineCafe.Inserer(EPiece._50Centime);

        Assert.Equal(1, (int)machineCafe.NbCafeServi);
        Assert.Equal(50, (int)machineCafe.ArgentTotal);
    }

    [Fact]
    public void Servir_deux_cafe_test()
    {
        machineCafe.Inserer(EPiece._50Centime);
        machineCafe.Inserer(EPiece._50Centime);

        Assert.Equal(2, (int)machineCafe.NbCafeServi);
        Assert.Equal(100, (int)machineCafe.ArgentTotal);
    }

    [Theory]
    [InlineData(EPiece._1Centime)]
    [InlineData(EPiece._2Centime)]
    [InlineData(EPiece._5Centime)]
    [InlineData(EPiece._10Centime)]
    [InlineData(EPiece._20Centime)]
    public void Pas_assez_argent_servir_cafe_test(EPiece _piece)
    {
        machineCafe.Inserer(_piece);

        Assert.Equal(0, (int)machineCafe.NbCafeServi);
        Assert.Equal(0, (int)machineCafe.ArgentTotal);
    }

    [Theory]
    [InlineData(EPiece._1Euro)]
    [InlineData(EPiece._2Euro)]
    public void Trop_argent_servir_cafe_test(EPiece _piece)
    {
        machineCafe.Inserer(_piece);

        Assert.Equal(1, (int)machineCafe.NbCafeServi);
        Assert.Equal((int)_piece, (int)machineCafe.ArgentTotal);
    }

}