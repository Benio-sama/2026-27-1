public class Bankszamla {
    private String szamlaszam;
    private String tulajdonosneve;
    private double egyenleg;

    public Bankszamla(String szamlaszam, String tulajdonosneve, double egyenleg) {
        this.szamlaszam = szamlaszam;
        this.tulajdonosneve = tulajdonosneve;
        this.egyenleg = egyenleg;
    }

    public String getSzamlaszam() {
        return szamlaszam;
    }

    public void setSzamlaszam(String szamlaszam) {
        this.szamlaszam = szamlaszam;
    }

    public String getTulajdonosneve() {
        return tulajdonosneve;
    }

    public void setTulajdonosneve(String tulajdonosneve) {
        this.tulajdonosneve = tulajdonosneve;
    }

    public double getEgyenleg() {
        return egyenleg;
    }

    public void setEgyenleg(double egyenleg) {
        this.egyenleg = egyenleg;
    }

    public void befizetes(double osszeg) {
        if (osszeg < 0) {
            System.out.println("nem lehet negativ osszeget megadni");
        } else {
            this.egyenleg += osszeg;
        }
    }

    public void kivetel(double osszeg) {
        if (this.egyenleg < osszeg || osszeg < 0) {
            System.out.println("nincs eleg fedezet vagy az osszeg negativ");
        } else {
            this.egyenleg -= osszeg;
        }
    }

    @Override
    public String toString() {
        return this.tulajdonosneve + " " + this.szamlaszam + " " + this.egyenleg;
    }
}
