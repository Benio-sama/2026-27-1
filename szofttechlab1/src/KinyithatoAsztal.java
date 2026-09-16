import java.time.LocalDateTime;

public class KinyithatoAsztal extends Asztal {
    private int teljesHossz;
    private boolean kinyitva = false;

    public KinyithatoAsztal(Gyarak gyar, LocalDateTime ido, int teljesHossz, int suly, int hossz, Anyag anyag) {
        super(gyar, ido, suly, hossz, anyag);
        this.teljesHossz = teljesHossz;
        this.nev = "Hunor";
    }

    public KinyithatoAsztal(Gyarak gyar, LocalDateTime gyartasiIdo) {
//        super(gyar, gyartasiIdo);
        this(gyar, gyartasiIdo, 220, 30, 120, Anyag.FENYO);
    }

    public int getTeljeshossz() {
        return teljesHossz;
    }

    public void kiNyit() {
        kinyitva = true;
    }

    public void beCsuk() {
        kinyitva = false;
    }

    @Override
    public int getHossz() {
        if (kinyitva) {
            return teljesHossz;
        } else {
            return hossz;
        }
    }
}
