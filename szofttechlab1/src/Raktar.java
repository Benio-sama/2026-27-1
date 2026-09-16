import java.util.ArrayList;
import java.util.List;

public class Raktar {
    public List<Butor> butorok = new ArrayList<Butor>();

    public Butor add(Butor b) {
        butorok.add(b);
        return b;
    }

    public void remove(Butor b) {
        butorok.remove(b);
    }

    public List<Butor> keresTipusAlapjan(String nev) {
        List<Butor> eredmeny = new ArrayList<>();
        for (Butor b : butorok) {
            if (b.getNev().equals(nev)) {
                eredmeny.add(b);
            }
        }
        return eredmeny;
    }

    public Butor keresIDAlapjan(int id) {
        for (Butor b : butorok) {
            if (b.getId() == id) {
                return b;
            }
        }
        return null;
    }
}
