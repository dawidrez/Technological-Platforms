package Repository;

import Entities.Brewery;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import java.util.List;

public class BreweryRepository extends Repository<Brewery,String> {
    public BreweryRepository(EntityManagerFactory emf){super(emf, Brewery.class);}

        public List<Brewery> BreweryCheaperThan(int n) {
            EntityManager em = getEmf().createEntityManager();
            List<Brewery> list = em.createQuery("select b from Entities.Brewery b where value<" + Integer.toString(n)).getResultList();
            em.close();
            return list;
        }
}
