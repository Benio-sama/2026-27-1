import java.time.LocalDateTime;

public class Barszek extends Butor {
    private int labakszama = 3;

    public Barszek(Gyarak gyar, LocalDateTime ido, boolean haromlabu) {
        super(gyar, ido);
        nev = "Ivo";
        if (!haromlabu) {
            labakszama = 4;
        }
    }

    public Barszek(Gyarak gyar, LocalDateTime gyartasiIdo) {
//        super(gyar, gyartasiIdo);
        this(gyar, gyartasiIdo, true);
    }

    public int getLabakszama() {
        return labakszama;
    }
}
