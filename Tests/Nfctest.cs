using Services;
using Services.BadgeNFCs;
using Services.CarteBleus;
using Services.HardwareNFCs;

namespace Tests;
public class Nfctest
{
    MachineCafeBuilder builder = new();

    [Fact]
    public void Cas_nominal_test()
    {
        BadgeNFC nfc = new(1);

        var machineCafe = builder
            .AjouterHadwareNfc(new HardwareNfc())
            .Build();

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.HardwareNfc!.SimulerRetirerBage();

        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Cas_nominal_debit_argent_test()
    {
        BadgeNFC nfc = new(1);

        var machineCafe = builder
            .AjouterHadwareNfc(new HardwareNfc())
            .Build();

        var compte = machineCafe.ListeCompte.First();
        uint argentDebut = compte.Argent;

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.PayerParBadge();
        machineCafe.HardwareNfc!.SimulerRetirerBage();

        Assert.True(argentDebut > compte.Argent);
        Assert.Equal(1, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Cas_nominal_double_cafe_test()
    {
        BadgeNFC nfc = new(1);

        var machineCafe = builder
            .AjouterHadwareNfc(new HardwareNfc())
            .Build();

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.HardwareNfc!.SimulerRetirerBage();

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.HardwareNfc!.SimulerRetirerBage();

        Assert.Equal(2, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]
    public void Recharger_compte_piece_test()
    {
        BadgeNFC nfc = new(1);

        var machineCafe = builder
            .AjouterHadwareNfc(new HardwareNfc())
            .Build();

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.Hardware.SimulerInsertionPiece(EPiece._2Euro);

        machineCafe.HardwareNfc.SimulerRetirerBage();

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabrique);
    }

    [Fact]  
    public void Recharger_compte_carte_bleu_test()
    {
        CarteBleuFaker cb = new("1234", 100);
        BadgeNFC nfc = new(1);

        var machineCafe = builder
            .AjouterHadwareCarteBleu(new HardwareCarteBleuFaker())
            .AjouterHadwareNfc(new HardwareNfc())
            .Build();

        machineCafe.HardwareNfc!.SimulerPresenterBage(nfc);
        machineCafe.HardwareCarteBleu!.SimulerPayementSansContact(cb);

        machineCafe.HardwareNfc.SimulerRetirerBage();

        Assert.Equal(0, (int)machineCafe.Hardware.NbCafeFabrique);
    }
}
