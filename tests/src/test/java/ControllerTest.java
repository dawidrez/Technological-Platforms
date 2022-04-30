import org.junit.Test;

import java.util.Optional;
import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.*;

public class ControllerTest {

    @Test
    public void add_CorrectBeer() {
        BeerRepository mRep = mock(BeerRepository.class);
        doNothing().when(mRep).add("Zubr",5);
        BeerController controller=new BeerController(mRep);

        String result=controller.save("Zubr",5);

        assertThat(result).isEqualTo("done");
    }

    @Test
    public void add_IncorrectBeer() {
        BeerRepository mRep = mock(BeerRepository.class);
        doThrow(IllegalArgumentException.class)
                .when(mRep)
                .add(anyString(),anyInt());
        BeerController controller=new BeerController(mRep);

        String result=controller.save("Zubr",5);

        assertThat(result).isEqualTo("bad request");
    }

    @Test
    public void find_CorrectBeer() {
        Beer mage=new Beer("Zubr",5);
        BeerRepository mRep = mock(BeerRepository.class);
        Optional<Beer> mage2;
        mage2=Optional.ofNullable(mage);
        when(mRep.find("Zubr")).thenReturn(mage2);
        BeerController controller=new BeerController(mRep);

        String result=controller.find("Zubr");

        assertThat(result).isEqualTo(mage.toString());
    }

    @Test
    public void find_IncorrectBeer() {
        BeerRepository mRep = mock(BeerRepository.class);
        when(mRep.find("Zubr")).thenReturn(null);
        BeerController controller=new BeerController(mRep);

        String result=controller.find("Zubr");

        assertThat(result).isEqualTo("not found");
    }


    @Test
    public void delete_CorrectMage() {
        BeerRepository mRep = mock(BeerRepository.class);
        doNothing().when(mRep).delete("Zubr");
        BeerController controller=new BeerController(mRep);

        String result=controller.delete("Zubr");

        assertThat(result).isEqualTo("done");
    }

    @Test
    public void delete_IncorrectMage() {
        BeerRepository mRep = mock(BeerRepository.class);
        doThrow(IllegalArgumentException.class)
                .when(mRep)
                .delete("Zubr");
        BeerController controller=new BeerController(mRep);

        String result=controller.delete("Zubr");

        assertThat(result).isEqualTo("not found");
    }

}