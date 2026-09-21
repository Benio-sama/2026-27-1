import java.util.ArrayList;
import java.util.List;

public class Bank {
    private List<Bankszamla> bankszamlak = new ArrayList<Bankszamla>();

    public List<Bankszamla> getBankszamla() {
        return bankszamlak;
    }

    public void addbankszamla(Bankszamla bankszamla) {
        bankszamlak.add(bankszamla);
    }

    public Bankszamla keresbankszamla(String szamlaszam) {
        for (Bankszamla bankszamla : bankszamlak) {
            if (szamlaszam.equals(bankszamla.getSzamlaszam())) {
                return bankszamla;
            }
        }
        return null;
    }

    public double osszesegyenleg() {
        double ossz = 0;
        for (Bankszamla bankszamla : bankszamlak) {
            ossz += bankszamla.getEgyenleg();
        }
        return ossz;
    }

    public Bankszamla leggazdagabb() {
        Bankszamla max = bankszamlak.getFirst();
        for (Bankszamla bankszamla : bankszamlak) {
            if (bankszamla.getEgyenleg() > max.getEgyenleg()) {
                max = bankszamla;
            }
        }
        return max;
    }
}
