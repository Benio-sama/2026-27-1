public abstract class Allat {
    protected int suly;
    public String nev;

    public Allat(String nev) {
        suly = 50;
        this.nev = nev;
    }

    public void megnez() {
        System.out.println("hello, mit nezel?");
    }

    public int etelAd(int menny) {
        suly += menny;
        return suly;
    }
}
