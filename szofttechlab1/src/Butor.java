import java.time.LocalDateTime;
import java.util.concurrent.atomic.AtomicInteger;

public abstract class Butor {
    protected String nev;
    protected Gyarak gyar;
    protected LocalDateTime gyartasiIdo;
    private int id;
    private static AtomicInteger sequence = new AtomicInteger();

    public Butor(Gyarak gyar, LocalDateTime gyartasiIdo) {
        this.gyar = gyar;
        this.gyartasiIdo = gyartasiIdo;
        this.id = sequence.incrementAndGet();
    }

    public String getNev() {
        return nev;
    }

    public Gyarak getGyar() {
        return gyar;
    }

    public LocalDateTime getGyartasiIdo() {
        return gyartasiIdo;
    }

    public int getId() {
        return id;
    }
}
