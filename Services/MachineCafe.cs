using Services.BadgeNFCs;
using Services.CarteBleus;
using Services.HardwareNFCs;
using Services.Hardwares;
using System.Numerics;

namespace Services;

public class MachineCafe
{
    public IHardware Hardware { get; private set; }
    public IHardwareCarteBleu? HardwareCarteBleu { get; private set; } = null;
    public IHardwareNfc? HardwareNfc { get; private set; } = null;

    public byte PrixCafe { get; }
    public uint ArgentTotal { get; private set; }

    List<Compte> ListeCompte = [];

    ICarteBleu? carteBleu;
    IBadgeNFC? badgeNFC;

    bool badgeEstPresent = false;
    ushort prelevement = 1_000;

    public MachineCafe()
    {
        PrixCafe = (byte)EPiece._50Centime;

        ListeCompte.Add(new Compte(1, new BadgeNFC(1), 100));
    }

    public MachineCafe AjouterHadwareNfc(IHardwareNfc _hardwareNfc)
    {
        HardwareNfc = _hardwareNfc;
        HardwareNfc.CallbackBadgePresenter = BadgePresenter;
        HardwareNfc.CallbackBadgeRetirer = BadgeRetirer;

        return this;
    }

    public MachineCafe AjouterHadwareCarteBleu(IHardwareCarteBleu _hardwareCarteBleu)
    {
        HardwareCarteBleu = _hardwareCarteBleu;
        HardwareCarteBleu.CallbackEnregistrerCarteBleu = PayerSansContact;

        return this;
    }

    public MachineCafe ModifierHardware(IHardware _hardware)
    {
        Hardware = _hardware;
        Hardware.CallbackInsertionPiece = Inserer;

        return this;
    }

    public MachineCafe Build()
    {
        if(Hardware is null)
            ModifierHardware(new HardwareFaker());

        return this;
    }

    void Inserer(EPiece _piece)
    {
        ArgentTotal += (byte)_piece;

        if (badgeEstPresent && ArgentTotal > 0)
            return;

        if ((byte)ArgentTotal < PrixCafe)
            return;

        Hardware.CoulerCafe();
    }

    void PayerSansContact(ICarteBleu? _carteBleu)
    {
        if (_carteBleu is null)
            return;

        if(badgeEstPresent)
        {
            var compte = ListeCompte
                .Where(x => x.BadgeNFC.Id == badgeNFC?.Id)
                .First();

            ArgentTotal = prelevement;

            return;
        }

        if (ArgentTotal > 0)
            ArgentTotal = 0;

        carteBleu = _carteBleu;

        bool payementValider = carteBleu!.Prelevement((byte)EPiece._50Centime);

        if (!payementValider)
            return;

        ArgentTotal += (byte)EPiece._50Centime;

        _carteBleu.Prelevement((byte)EPiece._50Centime);
        bool cafeTerminer = Hardware.CoulerCafe();

        if (!cafeTerminer)
            _carteBleu.Remboursement((byte)EPiece._50Centime);
    }

    void BadgePresenter(IBadgeNFC _badgeNfc)
    {
        var compte = ListeCompte.Where(x => x.BadgeNFC.Id == _badgeNfc.Id).FirstOrDefault();

        if(compte is null)
        {
            ListeCompte.Add(new Compte(
                Random.Shared.Next(1, 100_000), 
                _badgeNfc, 
                0)
            );
        }

        badgeNFC = _badgeNfc;
        badgeEstPresent = true;
    }

    void BadgeRetirer()
    {
        badgeEstPresent = false;

        var compte = ListeCompte.Where(x => x.BadgeNFC.Id == badgeNFC!.Id).First();

        if (ArgentTotal > 0)
        {
            if (!carteBleu?.Prelevement(prelevement) ?? false)
            {
                ArgentTotal = 0;
                return;
            }

            compte.Argent += ArgentTotal;
            ArgentTotal = 0;
            return;
        }

        if (compte.Argent < PrixCafe)
            return;

        badgeNFC = null;
        Hardware.CoulerCafe();
    }
}
