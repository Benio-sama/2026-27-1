public class Main {
    public static void main(String[] args) {
        Bank bank = new Bank();

        Bankszamla sz = new Bankszamla("123456789", "Jani", 13);
        Bankszamla sz2 = new Bankszamla("8463785947", "Bela", 67);
        Bankszamla sz3 = new Bankszamla("65836573927", "Sanyi", 69);

        bank.addbankszamla(sz);
        bank.addbankszamla(sz2);
        bank.addbankszamla(sz3);

        System.out.println(sz);
        System.out.println(sz2);
        System.out.println(sz3);

        System.out.println("------------------------------------------------");

        sz.befizetes(13);
        sz.befizetes(-100);
        sz.kivetel(500);
        sz.kivetel(-100);
        sz.kivetel(5);
        sz2.befizetes(1000);

        System.out.println(sz);
        System.out.println(sz2);
        System.out.println(sz3);

        System.out.println("------------------------------------------------");

        System.out.println(bank.leggazdagabb());
        System.out.println(bank.osszesegyenleg());
        System.out.println(bank.keresbankszamla("65836573927"));
    }
}