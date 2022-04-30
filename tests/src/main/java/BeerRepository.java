import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public  class BeerRepository {

    private List<Beer>beers;

    public BeerRepository() {
        this.beers=new ArrayList();
    }

    public Optional<Beer> find(String id) {
        Optional<Beer> entity=null;
        for(Beer beer:beers){
            if(beer.getName().equals(id)){
                entity=Optional.ofNullable(beer);
                return entity;
            }
        }
        return entity;
    }

    public void delete(String id) {
        for (Beer mage:beers){
            if(mage.getName()==id){
                beers.remove(mage);
                return;
            }
        }
        throw new IllegalArgumentException("Nie ma takiego piwa!" );
    }


    public void add(String name,int level) throws IllegalArgumentException {
        for(Beer tmp:beers){
            if(tmp.getName().equals(name)){
                throw new IllegalArgumentException("Juz jest takie piwo!");
            }
        }
        Beer mage=new Beer(name,level);
        beers.add(mage);
    }


}

