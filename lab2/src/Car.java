import java.util.ArrayList;
import java.util.List;

public class Car implements IVehicle {
    private List<Part> parts = new ArrayList<Part>();

    @Override
    public double calcSpeed() {
        return 120;
    }
}
