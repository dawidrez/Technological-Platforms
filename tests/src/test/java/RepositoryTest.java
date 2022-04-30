import org.junit.Test;

import java.util.Optional;
import static org.assertj.core.api.Assertions.assertThat;
public class RepositoryTest {
    @Test
    public void add_2DifferentNames() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        mRep.add("Zubr2",5);
        //no exception
    }

    @Test(expected=Exception.class)
    public void add_2TheSameNames() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        mRep.add("Zubr",5);
        //exception
    }

    @Test
    public void find_NameInRep() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        Optional<Beer>mage=mRep.find("Zubr");
        String result;
        if (mage==null){
            result="Nie ma takiego piwa";
        }
        else{
           result="Znaleziono";
        }
        assertThat(result).isEqualTo("Znaleziono");
    }


    @Test
    public void find_NameInRepNull() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        Optional<Beer>mage=mRep.find("Zubr2");
        String result;
        if (mage==null){
            result="Nie ma takiego piwa";
        }
        else{
            result="Znaleziono";
        }
        assertThat(result).isEqualTo("Nie ma takiego piwa");
    }

    @Test
    public void delete_BeerFromRepository() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        mRep.delete("Zubr");
       //no exception expected
    }

    @Test(expected=Exception.class)
    public void delete_BeerFromRepository_Exception() {
        BeerRepository mRep = new BeerRepository();
        mRep.add("Zubr",5);
        mRep.delete("Zubr1");
        //exception expected
    }

}


