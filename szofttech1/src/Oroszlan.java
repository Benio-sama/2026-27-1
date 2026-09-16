public class Oroszlan extends Allat implements IHusevo {

    public String szarmazasiHely;
    public static String faj = "Leo";

    public Oroszlan(String nev) {
        super(nev);
    }

    @Override
    public void megnez() {
        System.out.println("en egy oroszlan vagyok");
    }

    @Override
    public int etelAd(int menny) {
        suly += menny/2;
        return suly;
    }

    @Override
    public void vadaszik() {

    }
}
