import java.time.LocalDateTime;

public class Asztal extends Butor {
    protected int suly;
    protected int hossz;
    protected Anyag anyag;
    protected Szinek szin;
    protected String tipuskod;

    public Asztal(Gyarak gyar, LocalDateTime gyartido, int suly, int hossz, Anyag anyag) {
        super(gyar, gyartido);
        this.suly = suly;
        this.hossz = hossz;
        this.anyag = anyag;
        nev = "Magor";
    }

    public Asztal(Gyarak gyar, LocalDateTime gyartasiIdo) {
//        super(gyar, gyartasiIdo);
//        nev = "Magor";
//        this.anyag = Anyag.FENYO;
//        this.suly = 30;
//        this.hossz = 120;

        this(gyar, gyartasiIdo, 30, 120, Anyag.FENYO);
    }

    public int getSuly() {
        return suly;
    }

    public int getHossz() {
        return hossz;
    }

    public Anyag getAnyag() {
        return anyag;
    }

    public String getNev() {
        return nev;
    }
}
