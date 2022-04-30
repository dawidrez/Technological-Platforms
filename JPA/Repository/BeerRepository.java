package Repository;

import Entities.Brewery;
import Entities.Beer;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import java.util.List;

public class BeerRepository extends Repository<Beer,String> {
    public BeerRepository(EntityManagerFactory emf){super(emf, Beer.class);}

    public List<Beer> BeerCheaperThan(int n) {
        EntityManager em = getEmf().createEntityManager();
        List<Beer> list = em.createQuery("select b from Entities.Beer b where price<" + Integer.toString(n)).getResultList();
        em.close();
        return list;
    }

    public List<Beer> BeerExpensiveBrewery(int n, Brewery brewery) {
        EntityManager em = getEmf().createEntityManager();
        List<Beer> list = em.createQuery("select b from Entities.Beer b where price>" + Integer.toString(n)+" and b.brewery= :brewery")
                .setParameter("brewery",brewery)
                .getResultList();
        em.close();
        return list;
    }

    /*@Override
    public void delete(Mage mage){
        if (mage.getTower()!=null){
            String id=mage.getTower().getName();
            EntityManager em = getEmf().createEntityManager();
            TowerRepository tRep=new TowerRepository(getEmf());
            Tower toChange=tRep.find(id);
            ArrayList<Mage> mages=(ArrayList)toChange.getMages();
            mages.remove(mage);
            toChange.setMages(mages);
            EntityTransaction transaction = em.getTransaction();
            transaction.begin();
            em.merge(toChange);
            transaction.commit();
            em.close();
        }
        super.delete(mage);
    }*/
}
