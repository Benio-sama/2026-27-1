import java.util.ArrayList;
import java.util.List;

public abstract class PublicTransport implements IVehicle {
    private List<Station> stations = new ArrayList<Station>();

    public List<Station> getStations() {
        return new ArrayList<Station>(stations);
    }

    public double calcSpeed() {
        return 0;
    }
}
