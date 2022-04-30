import java.util.Optional;

public class BeerController {
    private BeerRepository repository;

    public BeerController(BeerRepository repository) {
      this.repository=repository;

    }
    public String find(String name) {
        Optional<Beer> mage=  repository.find(name);
        if (mage==null){
            return "not found";
        }
        else{
            return mage.get().toString();
        }

    }
    public String delete(String name) {
        try {
            repository.delete(name);
            return "done";
        }
        catch(IllegalArgumentException e) {
            return "not found";
        }
    }
    public String save(String name, int level) {
        try {
            repository.add(name, level);
            return "done";
        }
        catch (IllegalArgumentException e){
            return "bad request";
        }
    }
}