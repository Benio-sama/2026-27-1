import java.util.ArrayList;
import java.util.List;

public class Zoo {
    public static void main(String[] args) {
        Oroszlan a = new Oroszlan("Bela");
        Oroszlan b = new Oroszlan("Szimba");
        a.nev = "asdasdasd";
        List<Allat> allatkert = new ArrayList<Allat>();

        allatkert.add(a);
        allatkert.add(b);

        ((Oroszlan) a).szarmazasiHely = "aa";
    }
}